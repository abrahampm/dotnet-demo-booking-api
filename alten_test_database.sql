-- phpMyAdmin SQL Dump
-- version 4.9.5deb2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Feb 07, 2022 at 03:21 PM
-- Server version: 8.0.28-0ubuntu0.20.04.3
-- PHP Version: 7.4.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `alten_test`
--

DELIMITER $$
--
-- Procedures
--
CREATE PROCEDURE `GetAvailableRooms` (IN `startDate` DATE, IN `endDate` DATE)  NO SQL
BEGIN
SELECT * FROM `Rooms` WHERE `Rooms`.`Status` = 1 AND `Rooms`.`Id` NOT IN (
	SELECT `Reservations`.`RoomId` FROM `Reservations` WHERE `Reservations`.`EndDate` >= startDate AND `Reservations`.`StartDate` <=  endDate
);
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `AspNetRoleClaims`
--

CREATE TABLE `AspNetRoleClaims` (
  `Id` int NOT NULL,
  `RoleId` varchar(85) NOT NULL,
  `ClaimType` text,
  `ClaimValue` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `AspNetRoles`
--

CREATE TABLE `AspNetRoles` (
  `Id` varchar(85) NOT NULL,
  `Name` varchar(191) DEFAULT NULL,
  `NormalizedName` varchar(191) DEFAULT NULL,
  `ConcurrencyStamp` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `AspNetRoles`
--

INSERT INTO `AspNetRoles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`) VALUES
('2813590a-5bd4-4e75-92b0-fecaf58b8c8f', 'User', 'USER', 'ff423b81-6ab8-49c9-a211-ea7f538adf9a'),
('7edf16cb-1e5d-4d10-bd0b-651e5676db02', 'Admin', 'ADMIN', '0fe61cfb-e057-45fa-b33a-594b05a26bfc');

-- --------------------------------------------------------

--
-- Table structure for table `AspNetUserClaims`
--

CREATE TABLE `AspNetUserClaims` (
  `Id` int NOT NULL,
  `UserId` varchar(85) NOT NULL,
  `ClaimType` text,
  `ClaimValue` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `AspNetUserLogins`
--

CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(85) NOT NULL,
  `ProviderKey` varchar(85) NOT NULL,
  `ProviderDisplayName` text,
  `UserId` varchar(85) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `AspNetUserRoles`
--

CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(85) NOT NULL,
  `RoleId` varchar(85) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `AspNetUserRoles`
--

INSERT INTO `AspNetUserRoles` (`UserId`, `RoleId`) VALUES
('3ec8145a-c6d8-4fef-9d25-607b9a9b534a', '2813590a-5bd4-4e75-92b0-fecaf58b8c8f'),
('57d59bbc-06fd-4883-9860-142da0099af9', '2813590a-5bd4-4e75-92b0-fecaf58b8c8f'),
('85b4e527-8003-40ff-ada1-513b27ab256c', '2813590a-5bd4-4e75-92b0-fecaf58b8c8f'),
('abc07f43-b27d-416c-b380-bb85371da0ef', '2813590a-5bd4-4e75-92b0-fecaf58b8c8f'),
('cfcc8166-75f0-4220-9b17-284de157aa30', '2813590a-5bd4-4e75-92b0-fecaf58b8c8f'),
('de512330-51b2-4274-8f6f-300e98a09b60', '2813590a-5bd4-4e75-92b0-fecaf58b8c8f'),
('ee815307-63c0-45ec-8a1e-e3a3eb812507', '2813590a-5bd4-4e75-92b0-fecaf58b8c8f'),
('ddca2ada-4347-4c89-9aa7-997fb5b9d7dd', '7edf16cb-1e5d-4d10-bd0b-651e5676db02');

-- --------------------------------------------------------

--
-- Table structure for table `AspNetUsers`
--

CREATE TABLE `AspNetUsers` (
  `Id` varchar(85) NOT NULL,
  `FirstName` text NOT NULL,
  `LastName` text NOT NULL,
  `BirthDate` datetime NOT NULL,
  `UserName` varchar(191) DEFAULT NULL,
  `NormalizedUserName` varchar(191) DEFAULT NULL,
  `Email` varchar(191) DEFAULT NULL,
  `NormalizedEmail` varchar(191) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` text,
  `SecurityStamp` text,
  `ConcurrencyStamp` text,
  `PhoneNumber` text,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` timestamp NULL DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `AspNetUsers`
--

INSERT INTO `AspNetUsers` (`Id`, `FirstName`, `LastName`, `BirthDate`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`) VALUES
('184e0198-c705-4624-a67e-30a6b6012b66', 'user', '1', '2022-02-06 00:20:15', 'user.1', 'USER.1', 'user1@example.com', 'USER1@EXAMPLE.COM', 0, 'AQAAAAEAACcQAAAAEKo6P9r1Nxml3VeewjVJj9AZGACdu0bXR8B3GGiFbte9nyClgzQ7tIahS9b9l7BGhQ==', 'USSCNYMRKMGHKOPYQ2AOXB7ODW4XSCH4', '589f9751-cfe0-411f-8c72-d07b416788f3', NULL, 0, 0, NULL, 1, 0),
('3ec8145a-c6d8-4fef-9d25-607b9a9b534a', 'Juan', 'González2', '1994-09-06 04:00:00', 'Juan.Gonzlez2', 'JUAN.GONZLEZ2', 'juangonzalez2@gmail.com', 'JUANGONZALEZ2@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEBOwaPvkP0lBmGVWpetpwj/iyex68s+X9e1jyK8lYtzhqA67zw/JWKOuU73U4uVJwQ==', '52YCWNIBO74EXE3D25KSKDOLICCWDHQ3', 'c00575dc-5641-424f-bf78-2c3be4190e6b', NULL, 0, 0, NULL, 1, 0),
('57d59bbc-06fd-4883-9860-142da0099af9', 'Freddy', '01', '1987-03-12 05:00:00', 'Freddy.01', 'FREDDY.01', 'freddy01@gmail.com', 'FREDDY01@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEOP2zmEjRcasLht9/BWFITUP5cs9yCwIXrubmEKoXHAT2pTGmSoNhxgE6a6mmOAqnQ==', '5UR2TP3XR7DU4PI5HZI3W4RVEDOK7H7A', '24fc64ea-262c-4881-89a5-eb56bee8d5dd', NULL, 0, 0, NULL, 1, 0),
('85b4e527-8003-40ff-ada1-513b27ab256c', 'user', '2', '2022-02-06 21:02:18', 'user.2', 'USER.2', 'user2@example.com', 'USER2@EXAMPLE.COM', 0, 'AQAAAAEAACcQAAAAEG91YtuSjbCZX4ktYQo+rLe0YKi5njDYESNhyWMDOtyalYHMrA3bTf6DDzknOnxjbA==', 'IS7GB2IAYEXQHXEIPIZV4RTD5IRK7LDD', '52b7da8e-a1f2-437c-8591-3a292632a9fa', NULL, 0, 0, NULL, 1, 0),
('abc07f43-b27d-416c-b380-bb85371da0ef', 'Juan', 'González1', '1986-05-02 04:00:00', 'Juan.Gonzlez1', 'JUAN.GONZLEZ1', 'juangonzalez1@gmail.com', 'JUANGONZALEZ1@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEPV7kb1t5DYO/K6SBHqiAR2i16z5mFF8MylivUaq/xp1YdQRNXf5ogkhbO4ZufgUbg==', 'GXQIIXQSNXWMDL7N27RB2UDUF6OVEOWZ', '3f1d01e1-dd32-473d-a37f-d4b20f51c042', NULL, 0, 0, NULL, 1, 0),
('cfcc8166-75f0-4220-9b17-284de157aa30', 'Juan', 'González3', '1986-02-05 05:00:00', 'Juan.Gonzlez3', 'JUAN.GONZLEZ3', 'juangonzalez3@gmail.com', 'JUANGONZALEZ3@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEIYHQavbOx1dlEV30VEEJSTJ4CRmnD/F53Qb1X3GvcufqDlFZywqYse2IwpM/ZL5Cw==', 'XOGUJULV26A6P2RIVJ36YUMZPKOWQ6JI', '1e9318c4-ca76-482f-a07c-83c3abd09654', NULL, 0, 0, NULL, 1, 0),
('ddca2ada-4347-4c89-9aa7-997fb5b9d7dd', 'Carlos Abraham', 'Pérez Marrero', '2022-02-06 00:21:37', 'CarlosAbraham.PrezMarrero', 'CARLOSABRAHAM.PREZMARRERO', 'abrahampm96@gmail.com', 'ABRAHAMPM96@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEEpzSiQ5qk6M+8hVzzPyr0c9NRA8mUGycxkeJRwWnnVb64UOlhWelJaw3p5GWxediA==', 'L7Q2X3YY5CPS35GXF5MPYVZ4CF3JIYDT', '13365e6e-200d-4774-8da1-f394acc59788', NULL, 0, 0, NULL, 1, 0),
('de512330-51b2-4274-8f6f-300e98a09b60', 'Juan', 'González', '1986-05-02 04:00:00', 'Juan.Gonzlez', 'JUAN.GONZLEZ', 'juangonzalez@gmail.com', 'JUANGONZALEZ@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAENTiGors9C99MxmbRvLA4eJesLQZ+rVtQZgq3ooJtN0IRpw11DIaJpRIQSTp/XFKNQ==', 'XNPAFDOA3XE7ZQEL6ZKQEHFUISDCROOY', '69ee4afa-86c6-4b38-a98d-bdb2189943e8', NULL, 0, 0, NULL, 1, 0),
('ee815307-63c0-45ec-8a1e-e3a3eb812507', 'Juan', 'González5', '1996-02-01 05:00:00', 'Juan.Gonzlez5', 'JUAN.GONZLEZ5', 'juancito@gmail.com', 'JUANCITO@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEKPmhwugnOhNxnNIDRxkwBVKbnMGJM6C+jc9ADl+g3mqdQ11BAWHpR+SyaAIoV+NDw==', 'D5OHQNZFVYG4NNBNHCLTCCFGKQWXM62F', '9808e7b1-425a-48de-820a-c13c5ae98358', NULL, 0, 0, NULL, 1, 0);

-- --------------------------------------------------------

--
-- Table structure for table `AspNetUserTokens`
--

CREATE TABLE `AspNetUserTokens` (
  `UserId` varchar(85) NOT NULL,
  `LoginProvider` varchar(85) NOT NULL,
  `Name` varchar(85) NOT NULL,
  `Value` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Reservations`
--

CREATE TABLE `Reservations` (
  `Id` int NOT NULL,
  `StartDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  `RoomId` int NOT NULL,
  `Description` text,
  `CreatedAt` datetime NOT NULL,
  `UpdatedAt` datetime NOT NULL,
  `ApplicationUserId` varchar(85) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `Reservations`
--

INSERT INTO `Reservations` (`Id`, `StartDate`, `EndDate`, `RoomId`, `Description`, `CreatedAt`, `UpdatedAt`, `ApplicationUserId`) VALUES
(18, '2022-02-08 00:00:00', '2022-02-10 00:00:00', 14, '', '2022-02-07 12:02:10', '2022-02-07 12:07:33', 'ddca2ada-4347-4c89-9aa7-997fb5b9d7dd'),
(19, '2022-02-10 00:00:00', '2022-02-12 00:00:00', 13, '', '2022-02-07 12:02:29', '2022-02-07 12:07:39', 'ddca2ada-4347-4c89-9aa7-997fb5b9d7dd'),
(20, '2022-02-15 00:00:00', '2022-02-18 00:00:00', 13, '', '2022-02-07 12:03:19', '2022-02-07 12:07:46', 'ddca2ada-4347-4c89-9aa7-997fb5b9d7dd'),
(21, '2022-02-08 00:00:00', '2022-02-10 00:00:00', 15, '', '2022-02-07 12:03:32', '2022-02-07 12:07:51', 'ddca2ada-4347-4c89-9aa7-997fb5b9d7dd'),
(23, '2022-02-11 00:00:00', '2022-02-14 00:00:00', 14, '', '2022-02-07 12:34:40', '2022-02-07 12:34:40', 'ddca2ada-4347-4c89-9aa7-997fb5b9d7dd'),
(26, '2022-02-08 00:00:00', '2022-02-11 00:00:00', 12, '', '2022-02-07 13:31:38', '2022-02-07 13:31:38', 'ddca2ada-4347-4c89-9aa7-997fb5b9d7dd'),
(27, '2022-02-08 00:00:00', '2022-02-10 00:00:00', 19, '', '2022-02-07 15:01:31', '2022-02-07 15:01:31', 'ddca2ada-4347-4c89-9aa7-997fb5b9d7dd'),
(29, '2022-02-18 00:00:00', '2022-02-20 00:00:00', 14, '', '2022-02-07 15:11:03', '2022-02-07 15:11:22', 'ddca2ada-4347-4c89-9aa7-997fb5b9d7dd');

-- --------------------------------------------------------

--
-- Table structure for table `Rooms`
--

CREATE TABLE `Rooms` (
  `Id` int NOT NULL,
  `Number` int NOT NULL,
  `Status` int NOT NULL,
  `Type` int NOT NULL,
  `CreatedAt` datetime NOT NULL,
  `UpdatedAt` datetime NOT NULL,
  `Capacity` int NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `Rooms`
--

INSERT INTO `Rooms` (`Id`, `Number`, `Status`, `Type`, `CreatedAt`, `UpdatedAt`, `Capacity`) VALUES
(12, 1212, 1, 1, '0001-01-01 00:00:00', '2022-02-07 13:15:35', 1),
(13, 1213, 1, 2, '2022-02-04 10:50:30', '2022-02-04 10:50:30', 2),
(14, 1214, 1, 2, '2022-02-04 10:51:10', '2022-02-04 10:51:10', 2),
(15, 1215, 1, 2, '2022-02-04 10:51:30', '2022-02-04 10:51:30', 2),
(16, 1216, 0, 1, '0001-01-01 00:00:00', '2022-02-04 11:14:04', 1),
(17, 1217, 1, 1, '0001-01-01 00:00:00', '2022-02-07 15:12:56', 1),
(19, 1215, 1, 1, '2022-02-07 13:12:33', '2022-02-07 13:12:33', 1),
(20, 1234, 0, 2, '2022-02-07 13:13:34', '2022-02-07 13:13:34', 2),
(22, 1237, 1, 2, '2022-02-07 15:13:28', '2022-02-07 15:13:28', 1);

-- --------------------------------------------------------

--
-- Table structure for table `__EFMigrationsHistory`
--

CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `__EFMigrationsHistory`
--

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
('20220203200744_InitialMigration', '5.0.5'),
('20220204144451_UpdateRoomFields', '5.0.5'),
('20220205232649_CreateIdentityModels', '5.0.5'),
('20220207134732_UpdateReservationModel', '5.0.5');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`);

--
-- Indexes for table `AspNetRoles`
--
ALTER TABLE `AspNetRoles`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `RoleNameIndex` (`NormalizedName`);

--
-- Indexes for table `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetUserClaims_UserId` (`UserId`);

--
-- Indexes for table `AspNetUserLogins`
--
ALTER TABLE `AspNetUserLogins`
  ADD PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  ADD KEY `IX_AspNetUserLogins_UserId` (`UserId`);

--
-- Indexes for table `AspNetUserRoles`
--
ALTER TABLE `AspNetUserRoles`
  ADD PRIMARY KEY (`UserId`,`RoleId`),
  ADD KEY `IX_AspNetUserRoles_RoleId` (`RoleId`);

--
-- Indexes for table `AspNetUsers`
--
ALTER TABLE `AspNetUsers`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  ADD KEY `EmailIndex` (`NormalizedEmail`);

--
-- Indexes for table `AspNetUserTokens`
--
ALTER TABLE `AspNetUserTokens`
  ADD PRIMARY KEY (`UserId`,`LoginProvider`,`Name`);

--
-- Indexes for table `Reservations`
--
ALTER TABLE `Reservations`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Reservations_RoomId` (`RoomId`),
  ADD KEY `IX_Reservations_ApplicationUserId` (`ApplicationUserId`);

--
-- Indexes for table `Rooms`
--
ALTER TABLE `Rooms`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `__EFMigrationsHistory`
--
ALTER TABLE `__EFMigrationsHistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `Reservations`
--
ALTER TABLE `Reservations`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT for table `Rooms`
--
ALTER TABLE `Rooms`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  ADD CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  ADD CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `AspNetUserLogins`
--
ALTER TABLE `AspNetUserLogins`
  ADD CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `AspNetUserRoles`
--
ALTER TABLE `AspNetUserRoles`
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `AspNetUserTokens`
--
ALTER TABLE `AspNetUserTokens`
  ADD CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `Reservations`
--
ALTER TABLE `Reservations`
  ADD CONSTRAINT `FK_Reservations_AspNetUsers_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Reservations_Rooms_RoomId` FOREIGN KEY (`RoomId`) REFERENCES `Rooms` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
