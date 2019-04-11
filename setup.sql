USE canadianball;

-- CREATE TABLE teams (
--     name VARCHAR(200) NOT NULL,
--     mascot VARCHAR(200) NOT NULL,
--     id INT AUTO_INCREMENT,
--     PRIMARY KEY (id)
-- );

-- CREATE TABLE players (
--     name VARCHAR(200) NOT NULL,
--     teamId INT NOT NULL,
--     id INT AUTO_INCREMENT,

--     PRIMARY KEY (id),
--     FOREIGN KEY (teamId)
--         REFERENCES teams(id)
--         ON DELETE CASCADE
-- );

-- CREATE TABLE games (
--     id INT AUTO_INCREMENT,
--     team1Id INT NOT NULL,
--     team2Id INT NOT NULL,
--     PRIMARY KEY (id),

--     FOREIGN KEY (team1Id)
--         REFERENCES teams(id),

--     FOREIGN KEY (team2Id)
--         REFERENCES teams(id)
    
-- );