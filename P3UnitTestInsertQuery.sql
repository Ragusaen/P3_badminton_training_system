

INSERT INTO `member` (`Name`, `Sex`) VALUES ('John Doe', 0);
INSERT INTO `player` (`MemberID`, `BadmintonPlayerID`) VALUES (LAST_INSERT_ID(), 88170687);

INSERT INTO `member` (`Name`, `Sex`) VALUES ('Jane Doe', 1);
INSERT INTO `player` (`MemberID`, `BadmintonPlayerID`) VALUES (LAST_INSERT_ID(), 89050119);
