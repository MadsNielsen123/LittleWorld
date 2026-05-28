CREATE TABLE IF NOT EXISTS AppMessages (
    Id INT NOT NULL AUTO_INCREMENT,
    Value VARCHAR(255) NOT NULL,
    PRIMARY KEY (Id)
);

INSERT INTO AppMessages (Value)
SELECT 'Hello from LittleWorld database!'
WHERE NOT EXISTS (SELECT 1 FROM AppMessages);
