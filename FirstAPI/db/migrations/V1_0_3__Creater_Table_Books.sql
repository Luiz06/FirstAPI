
CREATE TABLE `firstapi`.`books` (
  `Id` INT(10) NOT NULL,
  `Author` VARCHAR(45) NULL,
  `Title` VARCHAR(45) NULL,
  `Price` DECIMAL(65,2) NULL,
  `LaucherData` DATETIME(6) NULL,
  PRIMARY KEY (`id`)
  )ENGINE=InnoDB;
