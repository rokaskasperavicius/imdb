DROP TABLE IF EXISTS word_indices;
DROP TABLE IF EXISTS ratings;
DROP TABLE IF EXISTS principals;
DROP TABLE IF EXISTS episodes;
DROP TABLE IF EXISTS basics_writers;
DROP TABLE IF EXISTS basics_directors;
DROP TABLE IF EXISTS name_known_for_titles;
DROP TABLE IF EXISTS name_professions;
DROP TABLE IF EXISTS professions;
DROP TABLE IF EXISTS names;
DROP TABLE IF EXISTS akas;
DROP TABLE IF EXISTS basics_genres;
DROP TABLE IF EXISTS genres;
-- This drops the foreign key constraints in user tables
-- C2_build_framework_db.sql should be run afterwards!
DROP TABLE IF EXISTS basics CASCADE;

-- Clean up
CREATE TABLE basics (
  tconst CHARACTER(10),
  titletype VARCHAR(20) NOT NULL,
  primarytitle TEXT NOT NULL,
  originaltitle TEXT NOT NULL,
  isadult BOOLEAN NOT NULL,
  startyear CHARACTER(4),
  endyear CHARACTER(4),
  runtimeminutes INT,
  plot TEXT,
  poster VARCHAR(180),
  PRIMARY KEY (tconst)
);
INSERT INTO basics (SELECT tconst, titletype, primarytitle, originaltitle, isadult, startyear, endyear, runtimeminutes FROM title_basics);

CREATE TABLE genres (
  name VARCHAR(256) PRIMARY KEY
);

-- This basically creates a genres table with all distinct genres found in title_basics
INSERT INTO genres (SELECT DISTINCT unnest(string_to_array(genres, ',')) AS name FROM title_basics);

CREATE TABLE basics_genres (
  tconst CHARACTER(10),
  genre VARCHAR(256),
  PRIMARY KEY (tconst, genre),
  FOREIGN KEY (tconst) REFERENCES basics(tconst) ON DELETE CASCADE,
  FOREIGN KEY (genre) REFERENCES genres(name) ON DELETE CASCADE
);

INSERT INTO basics_genres (SELECT tconst, unnest(string_to_array(genres, ',')) AS genre FROM title_basics);

-- Update from omdb_data
-- Only updates 115495 rows, out of 158974.
-- Running a left join on basics and omdb_data shows that there are 43479 titles in basics that do not have a match in omdb_data
-- That however won't matter as plot and poster will remain NULL for those titles
UPDATE basics b SET plot = od.plot, poster = od.poster FROM omdb_data od WHERE b.tconst = od.tconst;
UPDATE basics SET plot = NULL WHERE plot = '' OR plot = 'N/A';
UPDATE basics SET poster = NULL WHERE poster = 'N/A';
UPDATE basics SET startyear = NULL WHERE startyear = '';
UPDATE basics SET endyear = NULL WHERE endyear = '';

CREATE TABLE akas (
  tconst CHARACTER(10),
  ordering INT,
  title TEXT,
  region VARCHAR(10),
  language VARCHAR(10),
  types VARCHAR(256),
  attributes VARCHAR(256),
  isoriginaltitle BOOLEAN,
  PRIMARY KEY (tconst, ordering),
  FOREIGN KEY (tconst) REFERENCES basics(tconst) ON DELETE CASCADE
);

INSERT INTO akas (SELECT titleid, ordering, title, region, language, types, attributes, isoriginaltitle FROM title_akas);
UPDATE akas SET region = NULL WHERE region = '';
UPDATE akas SET language = NULL WHERE language = '';
UPDATE akas SET types = NULL WHERE types = '';
UPDATE akas SET attributes = NULL WHERE attributes = '';

CREATE TABLE names (
  nconst CHARACTER(10),
  primaryname VARCHAR(256),
  birthyear CHARACTER(4),
  deathyear CHARACTER(4),
  PRIMARY KEY (nconst)
);

INSERT INTO names (SELECT nconst, primaryname, birthyear, deathyear FROM name_basics);
UPDATE names SET birthyear = NULL WHERE birthyear = '';
UPDATE names SET deathyear = NULL WHERE deathyear = '';

CREATE TABLE professions (
  name VARCHAR(256) PRIMARY KEY
);

-- This basically creates a professions table with all distinct professions found in name_basics
INSERT INTO professions (SELECT DISTINCT unnest(string_to_array(primaryprofession, ',')) AS name FROM name_basics);

CREATE TABLE name_professions (
  nconst CHARACTER(10),
  profession VARCHAR(256),
  PRIMARY KEY (nconst, profession),
  FOREIGN KEY (nconst) REFERENCES names(nconst) ON DELETE CASCADE,
  FOREIGN KEY (profession) REFERENCES professions(name) ON DELETE CASCADE
);

INSERT INTO name_professions (SELECT nconst, unnest(string_to_array(primaryprofession, ',')) AS profession FROM name_basics);

CREATE TABLE name_known_for_titles (
  nconst CHARACTER(10),
  tconst CHARACTER(10),
  PRIMARY KEY (nconst, tconst),
  FOREIGN KEY (nconst) REFERENCES names(nconst) ON DELETE CASCADE,
  FOREIGN KEY (tconst) REFERENCES basics(tconst) ON DELETE CASCADE
);

-- Some titles in knownfortitles do not exist in basics, so we need to filter them out
-- Out of 1,540,864 rows only 432,906 use valid tconsts
INSERT INTO name_known_for_titles (nconst, tconst)
  SELECT nb.nconst, t.tconst
    FROM name_basics nb, unnest(string_to_array(nb.knownfortitles, ',')) AS t(tconst)
    INNER JOIN basics b ON b.tconst = t.tconst;

CREATE TABLE basics_directors (
  tconst CHARACTER(10),
  nconst CHARACTER(10),
  PRIMARY KEY (tconst, nconst),
  FOREIGN KEY (nconst) REFERENCES names(nconst) ON DELETE CASCADE,
  FOREIGN KEY (tconst) REFERENCES basics(tconst) ON DELETE CASCADE
);

-- Some people in directors do not exist in names, so we need to filter them out
-- Out of 175,569 rows only 173,266 use valid nconsts
INSERT INTO basics_directors (tconst, nconst)
  SELECT tc.tconst, t.nconst
    FROM title_crew tc, unnest(string_to_array(tc.directors, ',')) AS t(nconst)
    INNER JOIN names n ON n.nconst = t.nconst;

CREATE TABLE basics_writers (
  tconst CHARACTER(10),
  nconst CHARACTER(10),
  PRIMARY KEY (tconst, nconst),
  FOREIGN KEY (nconst) REFERENCES names(nconst) ON DELETE CASCADE,
  FOREIGN KEY (tconst) REFERENCES basics(tconst) ON DELETE CASCADE
);

-- Some people in writers do not exist in names, so we need to filter them out
-- Out of 239,511 rows only 224,666 use valid nconsts
INSERT INTO basics_writers (tconst, nconst)
  SELECT tc.tconst, t.nconst
    FROM title_crew tc, unnest(string_to_array(tc.writers, ',')) AS t(nconst)
    INNER JOIN names n ON n.nconst = t.nconst;

CREATE TABLE episodes (
  tconst CHARACTER(10),
  parenttconst CHARACTER(10),
  seasonnumber INT,
  episodenumber INT,
  PRIMARY KEY (tconst),
  FOREIGN KEY (tconst) REFERENCES basics(tconst) ON DELETE CASCADE,
  FOREIGN KEY (parenttconst) REFERENCES basics(tconst) ON DELETE CASCADE
);

INSERT INTO episodes (SELECT * FROM title_episode);

CREATE TABLE principals (
  tconst CHARACTER(10),
  ordering INT,
  nconst CHARACTER(10) NOT NULL,
  category VARCHAR(50) NOT NULL,
  job TEXT,
  characters TEXT,
  PRIMARY KEY (tconst, ordering),
  FOREIGN KEY (tconst) REFERENCES basics(tconst) ON DELETE CASCADE,
  FOREIGN KEY (nconst) REFERENCES names(nconst) ON DELETE CASCADE
);

-- Some people in title_principals do not exist in names, so we need to filter them out
INSERT INTO principals (tconst, ordering, nconst, category, job, characters)
  SELECT tp.tconst, tp.ordering, tp.nconst, tp.category, tp.job, tp.characters
    FROM title_principals tp
    INNER JOIN name_basics nb ON nb.nconst = tp.nconst;

UPDATE principals SET job = NULL WHERE job = '';
-- Remove surrounding [' and ']
UPDATE principals SET
	characters = regexp_replace(characters, '^\[''|''\]$', '', 'g');
-- Set rest of empty strings to NULL
UPDATE principals SET characters = NULL WHERE characters = '';

CREATE TABLE ratings (
  tconst CHARACTER(10),
  averagerating NUMERIC(5,1) NOT NULL,
  numvotes INT NOT NULL,
  PRIMARY KEY (tconst),
  FOREIGN KEY (tconst) REFERENCES basics(tconst) ON DELETE CASCADE
);

INSERT INTO ratings (SELECT * FROM title_ratings);

CREATE TABLE word_indices (
  tconst CHARACTER(10),
  word TEXT,
  field CHARACTER(1),
  lexeme TEXT,
  PRIMARY KEY (tconst, word, field),
  FOREIGN KEY (tconst) REFERENCES basics(tconst) ON DELETE CASCADE
);

INSERT INTO word_indices (SELECT * FROM wi);

-- Indices
CREATE INDEX IF NOT EXISTS idx_basics_primarytitle ON basics(primarytitle);
CREATE INDEX IF NOT EXISTS idx_names_primaryname ON names(primaryname);
CREATE INDEX IF NOT EXISTS idx_principals_nconst ON principals(nconst);
CREATE INDEX IF NOT EXISTS idx_principals_characters ON principals(characters);
CREATE INDEX IF NOT EXISTS idx_word_indices_word ON word_indices(word);

-- Clean up old tables
DROP TABLE IF EXISTS title_akas;
DROP TABLE IF EXISTS title_basics;
DROP TABLE IF EXISTS title_crew;
DROP TABLE IF EXISTS title_episode;
DROP TABLE IF EXISTS title_principals;
DROP TABLE IF EXISTS title_ratings;
DROP TABLE IF EXISTS name_basics;
DROP TABLE IF EXISTS omdb_data;
DROP TABLE IF EXISTS wi;