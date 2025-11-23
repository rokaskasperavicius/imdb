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
    IF COALESCE(TRIM(p_search_query), '') <> '' THEN
        INSERT INTO user_search_history (user_id, search_query)
        VALUES (p_user_id, TRIM(p_search_query))
        ON CONFLICT (user_id, search_query)
        DO UPDATE SET created_at = CURRENT_TIMESTAMP;
    END IF;
END $$;

-- 1-D.2
CREATE OR REPLACE FUNCTION f_string_search(
    p_user_id INT,
    p_search_query TEXT,
    -- Updated to include titletype filter
    p_basic_titletype CHARACTER(20)
)
RETURNS TABLE(tconst CHARACTER(10), primarytitle TEXT)
LANGUAGE plpgsql AS $$
BEGIN
    CALL p_update_user_search_history(p_user_id, p_search_query);

    RETURN QUERY SELECT b.tconst, b.primarytitle FROM (
        SELECT * FROM basics WHERE titletype = p_basic_titletype
        ) b
        WHERE b.primarytitle ILIKE '%' || p_search_query || '%'
        OR (b.plot IS NOT NULL AND b.plot ILIKE '%' || p_search_query || '%')
        LIMIT 10;
END $$;

-- 1-D.3
CREATE OR REPLACE PROCEDURE p_rate(
    p_user_id INT,
    p_basic_tconst CHARACTER(10),
    p_rating INT
) LANGUAGE plpgsql AS $$
DECLARE
    -- Updated to cast to correct numeric type
    vote_count DECIMAL;
BEGIN
    -- Will prevent the user from rating the same title multiple times
    INSERT INTO user_title_ratings (user_id, basic_tconst, rating)
        VALUES (p_user_id, p_basic_tconst, p_rating);

    SELECT numvotes * averagerating INTO vote_count FROM ratings WHERE tconst = p_basic_tconst;

    UPDATE ratings
    SET numvotes = numvotes + 1,
        averagerating = ROUND((vote_count + p_rating) / (numvotes + 1), 1)::NUMERIC(5,1)
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
    CALL p_update_user_search_history(p_user_id, p_search_title);
    CALL p_update_user_search_history(p_user_id, p_search_plot);
    CALL p_update_user_search_history(p_user_id, p_search_character);
    CALL p_update_user_search_history(p_user_id, p_search_actor_name);

    RETURN QUERY SELECT DISTINCT (b.tconst), b.primarytitle FROM basics b
        -- Left joins preserve any titles which don't have any principals or names
        LEFT JOIN principals p ON b.tconst = p.tconst
        LEFT JOIN names n ON p.nconst = n.nconst
        
        WHERE (b.primarytitle IS NOT NULL AND b.primarytitle ILIKE '%' || p_search_title || '%')
        AND (b.plot IS NOT NULL AND b.plot ILIKE '%' || p_search_plot || '%')
        AND (p.characters IS NOT NULL AND p.characters ILIKE '%' || p_search_character || '%')
        AND (n.primaryname IS NOT NULL AND n.primaryname ILIKE '%' || p_search_actor_name || '%')
        LIMIT 10;
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
        WHERE n.primaryname ILIKE '%' || p_search_query || '%'
        LIMIT 10;
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
            ORDER BY frequency DESC, n.primaryname ASC
            LIMIT 10;
END $$;

-- 1-D.7
ALTER TABLE names ADD COLUMN IF NOT EXISTS rating NUMERIC(5,1);
UPDATE names SET rating = q.weighted_average
FROM (
    SELECT p.nconst,
        SUM(r.averagerating * r.numvotes) / SUM(r.numvotes) AS weighted_average
    FROM principals p
    INNER JOIN ratings r ON p.tconst = r.tconst
    GROUP BY p.nconst
) AS q
WHERE names.nconst = q.nconst;

-- 1-D.8
CREATE OR REPLACE FUNCTION f_popular_actors(
    p_user_id INT,
    p_basic_tconst CHARACTER(10)
)
RETURNS TABLE(nconst CHARACTER(10), primaryname VARCHAR(256), rating NUMERIC(5,1))
LANGUAGE plpgsql AS $$
BEGIN
    RETURN QUERY SELECT DISTINCT(n.nconst), n.primaryname, n.rating FROM principals p
        INNER JOIN names n ON p.nconst = n.nconst

        WHERE p.tconst = p_basic_tconst
        AND n.rating IS NOT NULL
        ORDER BY n.rating DESC, n.primaryname ASC
        LIMIT 10;
END $$;

-- 1-D.9
CREATE OR REPLACE FUNCTION f_similar_movies(
    p_basic_tconst CHARACTER(10),
    -- Updated to include titletype filter
    p_basic_titletype CHARACTER(20)
)
RETURNS TABLE(tconst CHARACTER(10), genres_count BIGINT)
LANGUAGE plpgsql AS $$
BEGIN
    RETURN QUERY SELECT bg2.tconst, COUNT(*) as genres_count
        FROM basics_genres bg1
        INNER JOIN basics_genres bg2
        ON bg1.genre = bg2.genre AND bg1.tconst <> bg2.tconst
        INNER JOIN basics b ON bg2.tconst = b.tconst
        WHERE bg1.tconst = p_basic_tconst AND b.titletype = p_basic_titletype
        GROUP BY bg2.tconst
        ORDER BY genres_count DESC
        LIMIT 10;
END $$;

-- 1-D.10
CREATE OR REPLACE FUNCTION f_person_words(
    p_nconst CHARACTER(10)
)
RETURNS TABLE(word TEXT, frequency BIGINT)
LANGUAGE plpgsql AS $$
BEGIN
    RETURN QUERY SELECT wi.word, COUNT(*) AS frequency
        FROM principals p
        INNER JOIN word_indices wi ON p.tconst = wi.tconst
        WHERE p.nconst = p_nconst
        GROUP BY wi.word
        ORDER BY frequency DESC, wi.word ASC
        LIMIT 10;
END $$;

-- 1-D.11
CREATE OR REPLACE FUNCTION f_exact_match_query(
    p_user_id INT,
    p_search_query_1 TEXT,
    p_search_query_2 TEXT,
    p_search_query_3 TEXT
)
RETURNS TABLE(tconst CHARACTER(10), primarytitle TEXT)
LANGUAGE plpgsql AS $$
BEGIN
    RETURN QUERY SELECT b.tconst, b.primarytitle FROM basics b,
        (
            SELECT wi.tconst from word_indices wi where word = p_search_query_1
                INTERSECT
            SELECT wi.tconst from word_indices wi where word = p_search_query_2
                INTERSECT
            SELECT wi.tconst from word_indices wi where word = p_search_query_3
        ) w WHERE b.tconst = w.tconst
        LIMIT 10;
END $$;

-- 1-D.12
CREATE OR REPLACE FUNCTION f_best_match_query(
    p_user_id INT,
    p_search_query_1 TEXT,
    p_search_query_2 TEXT,
    p_search_query_3 TEXT
)
RETURNS TABLE(tconst CHARACTER(10), primarytitle TEXT, rank BIGINT)
LANGUAGE plpgsql AS $$
BEGIN
    RETURN QUERY SELECT b.tconst, b.primarytitle, SUM(w.relevance) AS rank FROM basics b,
        (
            SELECT DISTINCT(wi.tconst), 1 relevance from word_indices wi where word = p_search_query_1
                UNION ALL
            SELECT DISTINCT(wi.tconst), 1 relevance from word_indices wi where word = p_search_query_2
                UNION ALL
            SELECT DISTINCT(wi.tconst), 1 relevance from word_indices wi where word = p_search_query_3
        ) w WHERE b.tconst = w.tconst
        GROUP BY b.tconst, b.primarytitle
        ORDER BY rank DESC
        LIMIT 10;
END $$;

-- 1-D.13
CREATE OR REPLACE FUNCTION f_word_to_word_query(
    p_user_id INT,
    p_search_query_1 TEXT,
    p_search_query_2 TEXT,
    p_search_query_3 TEXT
)
RETURNS TABLE(word TEXT, rank BIGINT)
LANGUAGE plpgsql AS $$
BEGIN
    RETURN QUERY SELECT w1.word, COUNT(w1.tconst) AS rank
        FROM word_indices AS w1
        INNER JOIN (
            SELECT DISTINCT wi.tconst
            FROM word_indices wi
            WHERE wi.word IN (p_search_query_1, p_search_query_2, p_search_query_3)
        ) AS w2
        ON w1.tconst = w2.tconst
        -- Exclude the query words themselves from the results
        WHERE w1.word NOT IN (p_search_query_1, p_search_query_2, p_search_query_3)
        GROUP BY w1.word
        ORDER BY rank DESC
        LIMIT 10;
END $$;
