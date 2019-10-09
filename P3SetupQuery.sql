create table `Member`(
ID int primary key auto_increment,
`Name` varchar(255) not null,
Sex varchar(1) not null
);

create table `Account`
(
MemberID int,
foreign key(MemberID) references `Member`(ID),
Username varchar(32) not null,
PasswordHash varchar(32) not null,
PasswordSalt varchar(128) not null
);

create table Trainer
(
MemberID int primary key,
foreign key(MemberID) references `Member`(ID)
);

create table Player
(
MemberID int primary key,
foreign key(MemberID) references `Member`(ID),
BadmintonPlayerID int 
);

create table Ranklist
(
PlayerMemberID int primary key,
foreign key(PlayerMemberID) references Player(MemberID),
MixPoints int not null,
SinglePoints int not null,
DoublePoints int not null,
OverallPoints int not null
);

create table Team
(
`Name` varchar(64) primary key
);

create table TrainerTeam
(
TrainerMemberID int,
foreign key(TrainerMemberID) references Trainer(MemberID),
TeamName varchar(64),
foreign key(TeamName) references Team(`Name`),
primary key(TrainerMemberID, TeamName)
);

create table PlayerTeam
(
PlayerMemberID int,
foreign key(PlayerMemberID) references Player(MemberID),
TeamName varchar(64),
foreign key(TeamName) references Team(`Name`),
primary key(PlayerMemberID, TeamName)
);

create table PracticeSession
(
ID int primary key auto_increment,
TeamName varchar(64),
foreign key(TeamName) references Team(`Name`),
StartDate datetime not null,
`Location` varchar(255) not null
);

create table `Match`
(
ID int primary key auto_increment,
StartDate datetime not null,
League varchar(32) not null,
OpponentName varchar(64) not null,
`Location` varchar(255) not null
);

create table PlayerMatch
(
MatchID int,
foreign key(MatchID) references `Match`(ID),
PlayerMemberID int,
foreign key(PlayerMemberID) references Player(MemberID),
primary key(MatchID, PlayerMemberID),
Position varchar(16)
);

create table FocusPoint
(
`Name` varchar(64) primary key,
`Description` varchar(1024),
VideoURI varchar(255)
);

create table PlayerFocusPoint
(
PlayerMemberID int,
foreign key(PlayerMemberID) references Player(MemberID),
FocusPointName varchar(64),
foreign key(FocusPointName) references FocusPoint(`Name`),
primary key(PlayerMemberID, FocusPointName)
);

create table PracticeSessionFocusPoint
(
FocusPointName varchar(64),
foreign key(FocusPointName) references FocusPoint(`Name`),
PracticeSessionID int,
foreign key(PracticeSessionID) references PracticeSession(ID),
primary key(FocusPointName, PracticeSessionID)
);