
use [aldakkanehCore];

	CREATE TABLE UserRoles (
UserID INT,
Role NVARCHAR(50),
CONSTRAINT PK_UserRoles PRIMARY KEY (UserID, Role),
CONSTRAINT FK_UserRole_User FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
use [aldakkanehCore];
INSERT INTO UserRoles (UserID, Role)
VALUES
(1, 'Admin'),
(2, 'Client');



