DROP TABLE IF EXISTS user_search_history;
DROP TABLE IF EXISTS user_title_bookmarks;
DROP TABLE IF EXISTS user_title_ratings;
DROP TABLE IF EXISTS users;

CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL
);

CREATE TABLE user_title_bookmarks (
    user_id INT,
    basic_tconst CHARACTER(10) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (user_id, basic_tconst),
    FOREIGN KEY (basic_tconst) REFERENCES basics(tconst) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);

CREATE TABLE user_title_ratings (
    user_id INT,
    basic_tconst CHARACTER(10) NOT NULL,
    rating INT CHECK (rating >= 1 AND rating <= 10),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (user_id, basic_tconst),
    FOREIGN KEY (basic_tconst) REFERENCES basics(tconst) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);

CREATE TABLE user_search_history (
    id SERIAL PRIMARY KEY,
    user_id INT,
    search_query TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);

-- 1-D.1
CREATE OR REPLACE PROCEDURE p_create_user(
    p_name VARCHAR,
    p_email VARCHAR,
    p_password_hash VARCHAR
) LANGUAGE plpgsql AS $$
BEGIN
    INSERT INTO users (name, email, password_hash)
    VALUES (p_name, p_email, p_password_hash);
END $$;

CREATE OR REPLACE PROCEDURE p_bookmark_title(
    p_user_id INT,
    p_basic_tconst CHARACTER(10)
) LANGUAGE plpgsql AS $$
BEGIN
    INSERT INTO user_title_bookmarks (user_id, basic_tconst)
    VALUES (p_user_id, p_basic_tconst);
END $$;

CREATE OR REPLACE PROCEDURE p_remove_bookmark_title(
    p_user_id INT,
    p_basic_tconst CHARACTER(10)
) LANGUAGE plpgsql AS $$
BEGIN
    DELETE FROM user_title_bookmarks
    WHERE user_id = p_user_id AND basic_tconst = p_basic_tconst;
END $$;

CREATE OR REPLACE PROCEDURE p_update_user_search_history(
    p_user_id INT,
    p_search_query TEXT
) LANGUAGE plpgsql AS $$
BEGIN
    INSERT INTO user_search_history (user_id, search_query)
    VALUES (p_user_id, p_search_query);
END $$;

-- 1-D.2
CREATE OR REPLACE FUNCTION f_string_search(
    p_user_id INT,
    p_search_query TEXT
)
RETURNS TABLE(tconst CHARACTER(10), primarytitle TEXT)
LANGUAGE plpgsql AS $$
BEGIN
    CALL p_update_user_search_history(p_user_id, p_search_query);

    RETURN QUERY SELECT b.tconst, b.primarytitle FROM basics b
        WHERE b.primarytitle ILIKE '%' || p_search_query || '%'
        OR (b.plot IS NOT NULL AND b.plot ILIKE '%' || p_search_query || '%');
END $$;

-- 1-D.3
CREATE OR REPLACE PROCEDURE p_rate(
    p_user_id INT,
    p_basic_tconst CHARACTER(10),
    p_rating INT
) LANGUAGE plpgsql AS $$
DECLARE
    vote_count NUMERIC(5, 1);
BEGIN
    -- Will prevent the user from rating the same title multiple times
    INSERT INTO user_title_ratings (user_id, basic_tconst, rating)
        VALUES (p_user_id, p_basic_tconst, p_rating);

    SELECT numvotes * averagerating INTO vote_count FROM ratings WHERE tconst = p_basic_tconst;

    UPDATE ratings
    SET numvotes = numvotes + 1,
        averagerating = (vote_count + p_rating) / (numvotes + 1)
    WHERE tconst = p_basic_tconst;
END $$;

-- 1-D.4
CREATE OR REPLACE FUNCTION f_structured_string_search(
    p_user_id INT,
    p_search_title TEXT,
    p_search_plot TEXT,
    p_search_character TEXT,
    p_search_actor_name VARCHAR(256)
)
RETURNS TABLE(tconst CHARACTER(10), primarytitle TEXT)
LANGUAGE plpgsql AS $$
BEGIN
    IF COALESCE(TRIM(p_search_title), '') <> '' THEN
        CALL p_update_user_search_history(p_user_id, TRIM(p_search_title));
    END IF;

    IF COALESCE(TRIM(p_search_plot), '') <> '' THEN
        CALL p_update_user_search_history(p_user_id, TRIM(p_search_plot));
    END IF;

    IF COALESCE(TRIM(p_search_character), '') <> '' THEN
        CALL p_update_user_search_history(p_user_id, TRIM(p_search_character));
    END IF;

    IF COALESCE(TRIM(p_search_actor_name), '') <> '' THEN
        CALL p_update_user_search_history(p_user_id, TRIM(p_search_actor_name));
    END IF;

    RETURN QUERY SELECT DISTINCT (b.tconst), b.primarytitle FROM basics b
        -- Left joins preserve any titles which don't have any principals or names
        LEFT JOIN principals p ON b.tconst = p.tconst
        LEFT JOIN names n ON p.nconst = n.nconst
        
        WHERE (b.primarytitle IS NOT NULL AND b.primarytitle ILIKE '%' || p_search_title || '%')
        AND (b.plot IS NOT NULL AND b.plot ILIKE '%' || p_search_plot || '%')
        AND (p.characters IS NOT NULL AND p.characters ILIKE '%' || p_search_character || '%')
        AND (n.primaryname IS NOT NULL AND n.primaryname ILIKE '%' || p_search_actor_name || '%');
END $$;

-- 1-D.5
CREATE OR REPLACE FUNCTION f_string_search_names(
    p_user_id INT,
    p_search_query TEXT
)
RETURNS TABLE(nconst CHARACTER(10), primaryname VARCHAR(256))
LANGUAGE plpgsql AS $$
BEGIN
    CALL p_update_user_search_history(p_user_id, p_search_query);

    RETURN QUERY SELECT n.nconst, n.primaryname FROM names n
        WHERE n.primaryname ILIKE '%' || p_search_query || '%';
END $$;

-- 1-D.6
-- I consider this as a function with no search query
-- I expect this to be combined with f_string_search_names in the API
CREATE OR REPLACE FUNCTION f_frequent_co_players(
    p_nconst CHARACTER(10)
)
RETURNS TABLE(nconst CHARACTER(10), primaryname VARCHAR(256), frequency BIGINT)
LANGUAGE plpgsql AS $$
BEGIN
    RETURN QUERY
        SELECT n.nconst, n.primaryname, COUNT(*) AS frequency
            FROM principals p
            -- For each principal, find other principals in the same titles
            INNER JOIN principals p2 ON p.tconst = p2.tconst AND p.nconst <> p2.nconst
            -- Get each co-player's name
            INNER JOIN names n ON p2.nconst = n.nconst
            -- Only consider the original principal
            WHERE p.nconst = p_nconst
            GROUP BY n.nconst, n.primaryname
            ORDER BY frequency DESC, n.primaryname ASC;
END $$;

CALL p_create_user('Test Test', 'test@gmail.com', '$2b$12$Lr2MhaZtkgxAvsl2.SoHOe3rcy12I7VlRrjwZomvjaQMCJ1r5Y8TC');
CALL p_bookmark_title(1, 'tt14609642');
CALL p_remove_bookmark_title(1, 'tt14609642');
SELECT * FROM f_string_search(1, 'Josh Groban');

-- Will be 9.6 for 5 votes
SELECT * FROM ratings WHERE tconst = 'tt0318848';
CALL p_rate(1, 'tt0318848', 1);
-- Will be 8.2 for 6 votes
SELECT * FROM ratings WHERE tconst = 'tt0318848';

SELECT * FROM f_structured_string_search(1, 'news', 'john krasinski hosts', 'self', 'emily');
SELECT * FROM f_string_search_names(1, 'pedro');
SELECT * FROM f_frequent_co_players('nm0050959');

-- 1-D.7
-- Name rating: Derive a rating of names (just actors or all names, as you prefer)
-- based on ratings of the titles they are related to. Modify the database to store
-- also these name ratings. Make sure to give higher influence to titles with more
-- votes in the calculation. You can do this by calculating a weighted average of
-- the averagerating for the titles, where the numvotes is used as weight.
ALTER TABLE names ADD COLUMN IF NOT EXISTS rating NUMERIC(5,1);
UPDATE names SET rating = subquery.weighted_average
FROM (
    SELECT p.nconst,
        SUM(r.averagerating * r.numvotes) / NULLIF(SUM(r.numvotes), 0) AS weighted_average
    FROM principals p
    INNER JOIN ratings r ON p.tconst = r.tconst
    GROUP BY p.nconst
) AS subquery
WHERE names.nconst = subquery.nconst;