DELIMITER //
DROP PROCEDURE IF EXISTS SP_DELETE;
CREATE PROCEDURE SP_DELETE()
BEGIN

drop table PracticeSessionExercise;
drop table PracticeSessionFocusPoint;
drop table Exercise;
drop table PracticeSession;
drop table YearPlanSectionFocusPoint;
drop table YearPlanSection;
drop table TeamMember;
drop table PracticeTeam;
drop table MemberFocusPoint;
drop table FocusPoint;
drop table Position;
drop table TeamMatch;
drop table Feedback;
drop table PlaySession;
drop table RankList;
drop table `Member`;
drop table MemberType;
drop table Token;
drop table `Account`;



END
//
DELIMITER ;