DELIMITER //
DROP PROCEDURE IF EXISTS SP_SETUP;
CREATE PROCEDURE SP_SETUP()
BEGIN

create table `Account`(
Username varchar(32) primary key,
PasswordHash binary(32) not null,
PasswordSalt binary(128) not null
);

create table Token(
AccessToken binary(64) primary key,
AccountUsername varchar(32) not null,
foreign key(AccountUsername) references `Account`(Username)
);

create table `Member`(
ID int primary key auto_increment,
MemberTypeID int not null,
Username varchar(32),
foreign key(Username) references `Account`(Username),
`Name` varchar(256) not null,
`Status` bit not null,
Sex int not null,
BadmintonPlayerID int
);

create table PracticeTeam(
ID int primary key auto_increment,
`Name` varchar(64) not null
);

create table TeamMember(
TeamID int,
foreign key(TeamID) references PracticeTeam(ID),
MemberID int,
foreign key(MemberID) references `Member`(ID),
primary key(TeamID, MemberID)
);


create table YearPlanSection(
ID int primary key auto_increment,
TeamID int not null,
foreign key(TeamID) references PracticeTeam(ID),
StartDate datetime not null,
EndDate datetime not null
);


create table FocusPoint(
ID int primary key auto_increment,
`Name` varchar(64) not null,
IsPrivate bit not null,
`Description` varchar(1024),
VideoURL varchar(256)
);

create table YearPlanSectionFocusPoint(
YearPlanSectionID int,
foreign key(YearPlanSectionID) references YearPlanSection(ID),
FocusPointID int,
foreign key(FocusPointID) references FocusPoint(ID),
primary key(YearPlanSectionID, FocusPointID)
);

create table MemberFocusPoint(
MemberID int,
foreign key(MemberID) references `Member`(ID),
FocusPointID int,
foreign key(FocusPointID) references FocusPoint(ID),
primary key(MemberID, FocusPointID)
);

create table PlaySession(
ID int primary key auto_increment,
`Type` int not null,
Location varchar(256) not null,
StartDate datetime not null,
EndDate datetime not null
);

create table PracticeSession(
PlaySessionID int primary key,
foreign key(PlaySessionID) references PlaySession(ID),
YearPlanSectionID int not null,
foreign key(YearPlanSectionID) references YearPlanSection(ID),
TrainerID int not null,
foreign key(TrainerID) references `Member`(ID)
);

create table PracticeSessionFocusPoint(
FocusPointID int,
foreign key(FocusPointID) references FocusPoint(ID),
PracticeSessionPlaySessionID int,
foreign key(PracticeSessionPlaySessionID) references PracticeSession(PlaySessionID),
primary key(FocusPointID, PracticeSessionPlaySessionID)
);

create table Exercise(
ID int primary key auto_increment,
`Name` varchar(64),
`Description` varchar(256)
);

create table PracticeSessionExercise(
ExerciseID int,
foreign key(ExerciseID) references Exercise(ID),
PracticeSessionPlaySessionID int,
foreign key(PracticeSessionPlaySessionID) references PracticeSession(PlaySessionID),
primary key(ExerciseID, PracticeSessionPlaySessionID),
ExerciseIndex int not null
);

create table RankList(
MemberID int primary key,
MixPoints int not null,
SinglesPoints int not null,
DoublesPoints int not null,
LevelPoints int not null,
`Level` varchar(16)
);

create table TeamMatch(
PlaySessionID int primary key,
foreign key(PlaySessionID) references PlaySession(ID),
CaptainID int not null,
foreign key(CaptainID) references `Member`(ID),
OpponentName varchar(64) not null,
League varchar(32) not null,
LeagueRound int not null,
Season int not null
);

create table `Position`(
MemberID int,
foreign key(MemberID) references `Member`(ID),
TeamMatchPlaySessionID int,
foreign key(TeamMatchPlaySessionID) references TeamMatch(PlaySessionID),
Position varchar(32),
primary key(MemberID, TeamMatchPlaySessionID, Position)
);

create table Feedback(
MemberID int,
foreign key(MemberID) references `Member`(ID),
PlaySessionID int,
foreign key(PlaySessionID) references PlaySession(ID),
primary key(MemberID, PlaySessionID),
Ready int,
Effort int,
Challenge int,
Absorb int,
Good varchar(1024),
Bad varchar(1024),
FocusPoint varchar(1024),
`Day` varchar(1024)
);

END
//
DELIMITER ;