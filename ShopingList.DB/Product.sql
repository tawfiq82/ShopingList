CREATE TABLE [dbo].[Product]
(
	[ProductID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [ImgUrl] NVARCHAR(50) NULL, 
    [CategoryID] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [FK_Product_Category] FOREIGN KEY (CategoryID) REFERENCES [dbo].[Category]([CategoryID]) 
)
