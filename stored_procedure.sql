DELIMITER $$
CREATE PROCEDURE `GetAvailableRooms`(IN `startDate` DATE, IN `endDate` DATE)
    NO SQL
BEGIN
SELECT * FROM `Rooms` WHERE `Rooms`.`Status` = 1 AND `Rooms`.`Id` NOT IN (
    SELECT `Reservations`.`RoomId` FROM `Reservations` WHERE `Reservations`.`EndDate` >= startDate AND `Reservations`.`StartDate` <=  endDate
);
END$$
DELIMITER ;