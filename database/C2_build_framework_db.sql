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
    user_id INT NOT NULL,
    search_query TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);
