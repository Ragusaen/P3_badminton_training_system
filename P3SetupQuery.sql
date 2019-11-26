DROP SCHEMA IF EXISTS `p3_db`;
CREATE SCHEMA `p3_db`;
USE `p3_db`;

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
MemberType int not null,
Username varchar(32),
foreign key(Username) references `Account`(Username),
`Name` varchar(256) not null,
OnRankList bit not null,
Sex int not null,
BadmintonPlayerID int,
`Comment` varchar(512)
);

create table PracticeTeam(
ID int primary key auto_increment,
`Name` varchar(64) not null,
TrainerID int,
foreign key(TrainerID) references `Member`(ID)
);

create table TeamMember(
TeamID int,
foreign key(TeamID) references PracticeTeam(ID),
MemberID int,
foreign key(MemberID) references `Member`(ID),
primary key(TeamID, MemberID)
);

create table FocusPoint(
ID int primary key auto_increment,
`Name` varchar(64) not null,
IsPrivate bit not null,
`Description` varchar(1024),
VideoURL varchar(256)
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
TrainerID int not null,
foreign key(TrainerID) references `Member`(ID),
MainFocusPointID int not null,
foreign key (MainFocusPointID) references focuspoint(ID),
TeamID int not null,
foreign key (TeamId) references practiceteam(ID)
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
ExerciseIndex int not null,
Minutes int not null
);

create table RankList(
MemberID int primary key,
foreign key(MemberID) references `Member`(ID),
MixPoints int not null,
SinglesPoints int not null,
DoublesPoints int not null,
LevelPoints int not null,
`Level` int not null,
`AgeGroup` int not null
);

create table TeamMatch(
PlaySessionID int primary key,
foreign key(PlaySessionID) references PlaySession(ID),
CaptainID int,
foreign key(CaptainID) references `Member`(ID),
OpponentName varchar(64) not null,
League int not null,
LeagueRound int not null,
TeamIndex int not null,
Season int not null
);

create table `Position`(
MemberID int,
foreign key(MemberID) references `Member`(ID),
TeamMatchPlaySessionID int,
foreign key(TeamMatchPlaySessionID) references TeamMatch(PlaySessionID),
`Type` int not null,
`Order` int not null,
`IsExtra` bit not null,
primary key(MemberID, TeamMatchPlaySessionID, `Type`, `Order`)
);

create table Feedback(
MemberID int,
foreign key(MemberID) references `Member`(ID),
PlaySessionID int,
foreign key(PlaySessionID) references PlaySession(ID),
primary key(MemberID, PlaySessionID),
Ready int not null,
Effort int not null,
Challenge int not null,
Absorb int not null,
Good varchar(1024),
Bad varchar(1024),
FocusPoint varchar(1024),
`Day` varchar(1024)
);	