IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [dbo].[Customers] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [FullName] AS [LastName] + ', ' + [FirstName],
    [TimeStamp] rowversion NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [dbo].[Makes] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [TimeStamp] rowversion NULL,
    CONSTRAINT [PK_Makes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [dbo].[CreditRisks] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [FullName] AS [LastName] + ', ' + [FirstName],
    [CustomerId] int NOT NULL,
    [TimeStamp] rowversion NULL,
    CONSTRAINT [PK_CreditRisks] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CreditRisks_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[Inventory] (
    [Id] int NOT NULL IDENTITY,
    [IsDrivable] bit NOT NULL DEFAULT CAST(1 AS bit),
    [MakeId] int NOT NULL,
    [Color] nvarchar(50) NOT NULL,
    [PetName] nvarchar(50) NOT NULL,
    [TimeStamp] rowversion NULL,
    CONSTRAINT [PK_Inventory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Make_Inventory] FOREIGN KEY ([MakeId]) REFERENCES [dbo].[Makes] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [dbo].[Orders] (
    [Id] int NOT NULL IDENTITY,
    [CustomerId] int NOT NULL,
    [CarId] int NOT NULL,
    [TimeStamp] rowversion NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Orders_Inventory] FOREIGN KEY ([CarId]) REFERENCES [dbo].[Inventory] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_CreditRisks_CustomerId] ON [dbo].[CreditRisks] ([CustomerId]);
GO

CREATE INDEX [IX_Inventory_MakeId] ON [dbo].[Inventory] ([MakeId]);
GO

CREATE INDEX [IX_Orders_CarId] ON [dbo].[Orders] ([CarId]);
GO

CREATE UNIQUE INDEX [IX_Orders_CustomerId_CarId] ON [dbo].[Orders] ([CustomerId], [CarId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220723122747_Initial', N'5.0.17');
GO

COMMIT;
GO

