-- Create the database
CREATE DATABASE aldakkanehCore;

-- Use the database
USE aldakkanehCore;

-- Create the Categories table
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100),
    CategoryImage NVARCHAR(MAX)
);
-- Create the Products table
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100),
    Description NVARCHAR(MAX), -- Use NVARCHAR(MAX) for larger text data
    Price DECIMAL(10, 2),
    CategoryID INT,
    ProductImage NVARCHAR(MAX),
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

-- Create the Users table
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);

-- Create the Orders table
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    OrderDate DATETIME NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Create the Carts table
CREATE TABLE Carts (
    CartID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Create the CartItems table
CREATE TABLE CartItems (
    CartItemID INT PRIMARY KEY IDENTITY(1,1),
    CartID INT,
    ProductID INT,
    Quantity INT,
    FOREIGN KEY (CartID) REFERENCES Carts(CartID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);


-- إدخال الفئات في جدول Categories
use aldakkanehCore;
-- إدخال فئة شيبس
INSERT INTO Categories (CategoryName, CategoryImage)
VALUES (N'شيبس', N'img/شيبس.jpg');  

-- إدخال فئة حبوب وبقوليات
INSERT INTO Categories (CategoryName, CategoryImage)
VALUES (N'حبوب وبقوليات', N'img/حبوب وبقوليات.jpg|');  

-- إدخال فئة شوكلاتة
INSERT INTO Categories (CategoryName, CategoryImage)
VALUES (N'شوكلاتة', N'img/شوكلاتة.png');  

-- إدخال فئة مواد تنظيف
INSERT INTO Categories (CategoryName, CategoryImage)
VALUES (N'مواد تنظيف', N'img/مواد تنظيف.jpg');
 
