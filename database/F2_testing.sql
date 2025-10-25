CALL p_create_user('Test Test', 'test@gmail.com', '$2b$12$Lr2MhaZtkgxAvsl2.SoHOe3rcy12I7VlRrjwZomvjaQMCJ1r5Y8TC');
CALL p_bookmark_title(1, 'tt14609642');
CALL p_remove_bookmark_title(1, 'tt14609642');
SELECT * FROM f_string_search(1, 'Josh Groban', 'movie');

-- Will be 9.6 for 5 votes
SELECT * FROM ratings WHERE tconst = 'tt0318848';
CALL p_rate(1, 'tt0318848', 1);
-- Will be 8.2 for 6 votes
SELECT * FROM ratings WHERE tconst = 'tt0318848';

SELECT * FROM f_structured_string_search(1, 'news', 'john krasinski hosts', 'self', 'emily');
SELECT * FROM f_string_search_names(1, 'pedro');
SELECT * FROM f_frequent_co_players('nm0050959');
SELECT * FROM f_popular_actors(1, 'tt0052520');
SELECT * FROM f_similar_movies('tt0052520', 'movie');
SELECT * FROM f_person_words('nm0050959');
SELECT * FROM f_exact_match_query(1, 'apple', 'mads', 'mikkelsen');
SELECT * FROM f_best_match_query(1, 'apple', 'mads', 'mikkelsen');
SELECT * FROM f_word_to_word_query(1, 'apple', 'mads', 'mikkelsen');