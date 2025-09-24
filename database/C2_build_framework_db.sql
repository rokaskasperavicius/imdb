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
    akas_id CHARACTER(10) NOT NULL,
    akas_ordering INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (user_id, akas_id, akas_ordering),
    FOREIGN KEY (akas_id, akas_ordering) REFERENCES akas(tconst, ordering) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);

CREATE TABLE user_title_ratings (
    user_id INT,
    akas_id CHARACTER(10) NOT NULL,
    akas_ordering INT NOT NULL,
    rating INT CHECK (rating >= 1 AND rating <= 10),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (user_id, akas_id, akas_ordering),
    FOREIGN KEY (akas_id, akas_ordering) REFERENCES akas(tconst, ordering) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);

CREATE TABLE user_search_history (
    id SERIAL PRIMARY KEY,
    user_id INT,
    search_query TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);

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
    p_akas_id CHARACTER(10),
    p_akas_ordering INT
) LANGUAGE plpgsql AS $$
BEGIN
    INSERT INTO user_title_bookmarks (user_id, akas_id, akas_ordering)
    VALUES (p_user_id, p_akas_id, p_akas_ordering);
END $$;

CREATE OR REPLACE PROCEDURE p_remove_bookmark_title(
    p_user_id INT,
    p_akas_id CHARACTER(10),
    p_akas_ordering INT
) LANGUAGE plpgsql AS $$
BEGIN
    DELETE FROM user_title_bookmarks
    WHERE user_id = p_user_id AND akas_id = p_akas_id AND akas_ordering = p_akas_ordering;
END $$;

CREATE OR REPLACE PROCEDURE p_update_user_search_history(
    p_user_id INT,
    p_search_query TEXT
) LANGUAGE plpgsql AS $$
BEGIN
    INSERT INTO user_search_history (user_id, search_query)
    VALUES (p_user_id, p_search_query);
END $$;

CREATE OR REPLACE FUNCTION p_string_search(
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

CREATE OR REPLACE PROCEDURE p_rate(
    p_user_id INT,
    p_akas_id CHARACTER(10),
    p_akas_ordering INT,
    p_rating INT
) LANGUAGE plpgsql AS $$
DECLARE
    vote_count NUMERIC(5, 1);
BEGIN
    -- Will prevent the user from rating the same title multiple times
    INSERT INTO user_title_ratings (user_id, akas_id, akas_ordering, rating)
        VALUES (p_user_id, p_akas_id, p_akas_ordering, p_rating);

    SELECT numvotes * averagerating INTO vote_count FROM ratings WHERE tconst = p_akas_id;

    UPDATE ratings
    SET numvotes = numvotes + 1,
        averagerating = (vote_count + p_rating) / (numvotes + 1)
    WHERE tconst = p_akas_id;
END $$;

CALL p_create_user('Test Test', 'test@gmail.com', '$2b$12$Lr2MhaZtkgxAvsl2.SoHOe3rcy12I7VlRrjwZomvjaQMCJ1r5Y8TC');
CALL p_bookmark_title(1, 'tt14609642', 1);
CALL p_remove_bookmark_title(1, 'tt14609642', 1);
SELECT * FROM p_string_search(1, 'Josh Groban');

-- Will be 9.6 for 5 votes
SELECT * FROM ratings WHERE tconst = 'tt0318848';
CALL p_rate(1, 'tt0318848', 1, 1);
-- Will be 8.2 for 6 votes
SELECT * FROM ratings WHERE tconst = 'tt0318848';