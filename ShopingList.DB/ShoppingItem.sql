CREATE TABLE [dbo].[ShopingList]
(
	[ShoppingItemId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserID] UNIQUEIDENTIFIER NOT NULL, 
    [ProductID] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [FK_ShoppingItem_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product]([ProductID]) ,
	CONSTRAINT [FK_ShoppingItem_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User]([UserID]) 
)
