CREATE TABLE vaults(
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(255),
  description VARCHAR(255),
  isPrivate TINYINT,
  creatorId VARCHAR (255),
  PRIMARY KEY (id),
  FOREIGN KEY (creatorId)
  REFERENCES profiles(id)
  ON DELETE CASCADE
)


CREATE TABLE keeps(
  id INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(255),
  description VARCHAR(255),
  img VARCHAR(255),
  views INT NOT NULL,
  shares INT NOT NULL,
  keeps INT NOT NULL,
  creatorId VARCHAR (255),
  PRIMARY KEY (id),
  FOREIGN KEY (creatorId)
  REFERENCES profiles(id)
  ON DELETE CASCADE
)

CREATE TABLE vaultkeeps(
  id INT NOT NULL AUTO_INCREMENT,
  creatorId VARCHAR(255),
  vaultId INT NOT NULL,
  keepId INT NOT NULL,
  PRIMARY KEY (id),

  FOREIGN KEY (vaultId)
  REFERENCES vaults (id)
  ON DELETE CASCADE,

  FOREIGN KEY (keepId)
  REFERENCES keeps (id)
  ON DELETE CASCADE,

  FOREIGN KEY (creatorId)
  REFERENCES profiles(id)
  ON DELETE CASCADE
)

CREATE TABLE profiles(
id VARCHAR (255),
name VARCHAR (255),
email VARCHAR (255),
picture VARCHAR (255),
PRIMARY KEY (id)
)