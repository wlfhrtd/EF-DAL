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

CREATE TABLE [Artist] (
    [ArtistId] int NOT NULL IDENTITY,
    [OldUserId] uniqueidentifier NULL,
    [UserName] varchar(256) NOT NULL,
    [Country] varchar(50) NULL,
    [Province] varchar(65) NULL,
    [City] varchar(50) NULL,
    [CreateDate] datetime NOT NULL DEFAULT ((getdate())),
    [ModifiedDate] datetime NOT NULL DEFAULT ((getdate())),
    [WebSite] varchar(255) NULL,
    [ProfilePrivacyLevel] tinyint NOT NULL,
    [ContactPrivacyLevel] tinyint NOT NULL,
    [ProfileViews] bigint NOT NULL,
    [ProfileLastViewDate] smalldatetime NULL,
    [Rating] tinyint NULL DEFAULT (((3))),
    [AvatarURL] varchar(255) NULL,
    [EmailAddress] varchar(256) NULL,
    [FileUploadsInBytes] int NOT NULL,
    [FileUploadQuotaInBytes] int NOT NULL,
    [LastActivityDate] datetime NOT NULL DEFAULT ((getdate())),
    [ShowChatStatus] bit NOT NULL DEFAULT (((1))),
    [AllowChatSounds] bit NOT NULL DEFAULT (((1))),
    CONSTRAINT [PK_Artist] PRIMARY KEY ([ArtistId])
);
GO

CREATE TABLE [ArtistSkill] (
    [ArtistTalentID] int NOT NULL IDENTITY,
    [ArtistId] int NOT NULL,
    [TalentName] varchar(50) NOT NULL,
    [SkillLevel] int NOT NULL DEFAULT (((3))),
    [Details] varchar(500) NULL,
    [Styles] varchar(500) NOT NULL,
    CONSTRAINT [PK__tmp_ms_x__A9AD4EAAFEE755FA] PRIMARY KEY ([ArtistTalentID]),
    CONSTRAINT [FK_ArtistSkill_Artist] FOREIGN KEY ([ArtistId]) REFERENCES [Artist] ([ArtistId]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_ArtistSkill_ArtistId] ON [ArtistSkill] ([ArtistId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220723133201_Initial', N'5.0.17');
GO

COMMIT;
GO

