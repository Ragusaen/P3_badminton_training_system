DELIMITER //
DROP PROCEDURE IF EXISTS SP_DELETE;
CREATE PROCEDURE SP_DELETE()
BEGIN

drop table PracticeSessionFocusPoint;
drop table PlayerFocusPoint;
drop table PlayerMatch;
drop table `Match`;
drop table FocusPoint;
drop table PracticeSession;
drop table PlayerTeam;
drop table TrainerTeam;
drop table Team;
drop table RankList;
drop table `Account`;
drop table Player;
drop table Trainer;
drop table `Member`;

END
//
DELIMITER ;