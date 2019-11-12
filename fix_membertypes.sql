DELETE FROM `membertype`;
ALTER TABLE `membertype` AUTO_INCREMENT=1;
INSERT INTO `membertype`(`Description`) VALUES ('Player');
INSERT INTO `membertype`(`Description`) VALUES ('Trainer');
SELECT * FROM `membertype`;