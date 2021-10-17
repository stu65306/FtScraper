# FtScraper by STU65306

This project demonstrates how to capture data from an API and store it in a database. This has been done as part of the advanced programming portion of my degree. 

To setup this project it is necessary to setup a database and create two tables with the following code. 
Creating the Account table:

```
CREATE TABLE [dbo].[Account] (
    [Id]              INT         IDENTITY (1, 1) NOT NULL,
    [Name]            NCHAR (100) NOT NULL,
    [BrcgsId]         NCHAR (10)  NULL,
    [BrcgsGrade]      NCHAR (10)  NULL,
    [BrcgsStandard]   NCHAR (50)  NULL,
    [BrcgsCategories] NCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
```
Creating the Contact table:
```
CREATE TABLE [dbo].[Contact] (
    [Id]          INT        IDENTITY (1, 1) NOT NULL,
    [PhoneNumber] NCHAR (15) NULL,
    [FaxNumber]   NCHAR (15) NULL,
    [Email]       NCHAR (50) NULL,
    [FirstName]   NCHAR (30) NULL,
    [LastName]    NCHAR (20) NULL,
    [ContactType] NCHAR (20) NULL,
    [AccountId]   INT        NOT NULL,
    CONSTRAINT [FkAccount] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Account] ([Id])
);
```

To configure the application open the `Program.cs` file and modigy the connection string on line 15. 
By default it will assume a local database called FtScraper will be available. 

For the unit tests do the same for `DatabaseTests.cs` line 11. 

It's always recommended to run the unit tests first, as the program will not check the database is accessible before it starts. 
