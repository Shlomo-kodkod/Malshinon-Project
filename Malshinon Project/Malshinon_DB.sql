CREATE DATABASE IF NOT EXISTS Malshinon;	
USE Malshinon;
DROP TABLE IF EXISTS IntelReports;
DROP TABLE IF EXISTS People;


CREATE TABLE People(
    id INT AUTO_INCREMENT,
    first_name VARCHAR(250) NOT NULL UNIQUE,
    last_name VARCHAR(250) NOT NULL,
    secret_code VARCHAR(250) UNIQUE,
    type ENUM('reporter', 'target', 'both', 'potential_agent') NOT NULL,
    num_reports INT DEFAULT 0,
    num_mentions INT DEFAULT 0,
    PRIMARY KEY (id)
    );
    
CREATE TABLE IntelReports(
    id INT AUTO_INCREMENT,
    reporter_id INT NOT NULL,
    target_id INT NOT NULL,
    text TEXT(500) NOT NULL,
    timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY(id),
    FOREIGN KEY (reporter_id) REFERENCES People (id),
    FOREIGN KEY (target_id) REFERENCES People (id)
    );

    

    


    