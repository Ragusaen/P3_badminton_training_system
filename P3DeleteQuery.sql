DELIMITER //
DROP PROCEDURE IF EXISTS SP_DELETE;
CREATE PROCEDURE SP_DELETE()
BEGIN

drop table if exists PracticeSessionExercise;
drop table if exists PracticeSessionFocusPoint;
drop table if exists Exercise;
drop table if exists PracticeSession;
drop table if exists YearPlanSectionFocusPoint;
drop table if exists YearPlanSection;
drop table if exists TeamMember;
drop table if exists PracticeTeam;
drop table if exists MemberFocusPoint;
drop table if exists FocusPoint;
drop table if exists Position;
drop table if exists TeamMatch;
drop table if exists Feedback;
drop table if exists PlaySession;
drop table if exists RankList;
drop table if exists `Member`;
drop table if exists MemberType;
drop table if exists Token;
drop table if exists `Account`;



END
//
DELIMITER ;