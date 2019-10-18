DELIMITER //
DROP PROCEDURE IF EXISTS SP_UT_CREATE_PLAYER;
CREATE PROCEDURE SP_UT_CREATE_PLAYER(
	IN playerName VARCHAR(255),
    IN sex int,
    IN bpID int
)
BEGIN
INSERT INTO `member` (`Name`, `Sex`) VALUES (playerName, sex);
INSERT INTO `player` (`MemberID`, `BadmintonPlayerID`) VALUES (LAST_INSERT_ID(), bpID);
END
//
DELIMITER ;


DELIMITER //
DROP PROCEDURE IF EXISTS SP_UNIT_TEST_READY;
CREATE PROCEDURE SP_UNIT_TEST_READY()
BEGIN

CALL SP_DELETE;
CALL SP_SETUP;

# Create players and their corresponding members
CALL SP_UT_CREATE_PLAYER('John Doe', 0, 88190246);
CALL SP_UT_CREATE_PLAYER('Jane Doe', 1, 92020288);
CALL SP_UT_CREATE_PLAYER('Johnathan Doemouth', 0, 15083094);
CALL SP_UT_CREATE_PLAYER('Peter S. Parker', 0, 89010165);

# Create accounts
INSERT INTO `account` (`MemberID`, `Username`, `PasswordHash`, `PasswordSalt`) VALUES (1, 'johninator', CAST(0xB52C497703A85E8E34450AEB2336029B3A828526C6BFF26B58401BD131B72F43 as binary(32)), CAST(0x42 as binary(128)));


END
//
DELIMITER ;