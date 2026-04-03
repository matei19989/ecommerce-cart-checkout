-- E-Commerce Cart & Checkout Database

CREATE DATABASE ECommerceDb;
GO

USE ECommerceDb;
GO

-- Tables

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(500) NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE()
);

CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000),
    Price DECIMAL(10,2) NOT NULL,
    ImageUrl NVARCHAR(500),
    Category NVARCHAR(100),
    InStock BIT DEFAULT 1
);

CREATE TABLE CartItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    CONSTRAINT FK_CartItems_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_CartItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id),
    CONSTRAINT UQ_CartItems_UserProduct UNIQUE (UserId, ProductId)
);

CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    ShippingAddress NVARCHAR(500) NOT NULL,
    TotalPrice DECIMAL(10,2) NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    CONSTRAINT FK_Orders_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE OrderItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- Seed Products


INSERT INTO Products (Name, Description, Price, ImageUrl, Category) VALUES
('Blue Top', 'A stylish blue cotton top for women.', 500.00, '/images/blue-top.jpg', 'Tops'),
('Men Tshirt', 'Pure cotton men t-shirt in grey color.', 400.00, '/images/men-tshirt.jpg', 'Tshirts'),
('Sleeveless Dress', 'Elegant sleeveless dress for parties.', 1000.00, '/images/sleeveless-dress.jpg', 'Dress'),
('Stylish Dress', 'A beautiful stylish dress for women.', 1500.00, '/images/stylish-dress.jpg', 'Dress'),
('Winter Top', 'Warm winter top with full sleeves.', 600.00, '/images/winter-top.jpg', 'Tops'),
('Summer White Top', 'Light and breezy white top for summer.', 400.00, '/images/summer-white-top.jpg', 'Tops'),
('Fancy Green Top', 'Trendy green top with modern design.', 700.00, '/images/fancy-green-top.jpg', 'Tops'),
('Madame Top', 'Premium quality madame top for women.', 1000.00, '/images/madame-top.jpg', 'Tops'),
('Lace Top', 'Delicate lace top for a sophisticated look.', 1400.00, '/images/lace-top.jpg', 'Tops'),
('Printed Tshirt', 'Colorful printed t-shirt for casual wear.', 350.00, '/images/printed-tshirt.jpg', 'Tshirts'),
('Cotton Jeans', 'Comfortable cotton jeans for everyday use.', 1200.00, '/images/cotton-jeans.jpg', 'Jeans'),
('Denim Jacket', 'Classic denim jacket for all seasons.', 1800.00, '/images/denim-jacket.jpg', 'Jackets'),
('Polo Shirt', 'Smart polo shirt for a casual-formal look.', 800.00, '/images/polo-shirt.jpg', 'Tshirts'),
('Running Shoes', 'Lightweight running shoes for daily workouts.', 2500.00, '/images/running-shoes.jpg', 'Shoes'),
('Leather Belt', 'Genuine leather belt with a classic buckle.', 600.00, '/images/leather-belt.jpg', 'Accessories');