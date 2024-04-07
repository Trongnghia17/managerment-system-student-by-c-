CREATE DATABASE studentdb;
CREATE TABLE `student` (
  `StdId` int(5) NOT NULL,
  `StdFirstName` varchar(15) NOT NULL,
  `StdLastName` varchar(15) NOT NULL,
  `Birthdate` date NOT NULL,
  `Gender` varchar(10) NOT NULL,
  `Phone` varchar(15) NOT NULL,
  `Address` text NOT NULL,
  `Photo` longblob NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `course` (
  `CourseId` int(10) NOT NULL,
  `CourseName` varchar(50) NOT NULL,
  `CourseHour` int(5) NOT NULL,
  `Description` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `score` (
  `StudentId` int(5) NOT NULL,
  `CourseName` varchar(50) NOT NULL,
  `Score` double NOT NULL,
  `Description` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


CREATE TABLE `user` (
  `userid` int(5) NOT NULL,
  `username` varchar(20) NOT NULL,
  `password` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `pointtraining` (
  `StudentId` int(5) NOT NULL,
  `PointTraining` double NOT NULL,
  `Description` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

ALTER TABLE `pointtraining`
  ADD PRIMARY KEY (`StudentId`);
ALTER TABLE `pointtraining`
  MODIFY `StudentId` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
COMMIT;

INSERT INTO `user` (`userid`, `username`, `password`) VALUES
(0, 'Admin', '123456');

ALTER TABLE `course`
  ADD PRIMARY KEY (`CourseId`);

ALTER TABLE `student`
  ADD PRIMARY KEY (`StdId`);

ALTER TABLE `user`
  ADD PRIMARY KEY (`userid`);


ALTER TABLE `course`
  MODIFY `CourseId` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

ALTER TABLE `student`
  MODIFY `StdId` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
COMMIT;


