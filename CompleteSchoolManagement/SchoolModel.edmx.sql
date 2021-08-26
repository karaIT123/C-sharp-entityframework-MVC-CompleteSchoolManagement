
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/22/2021 06:58:35
-- Generated from EDMX file: C:\Users\15147\source\repos\CompleteSchoolManagement\CompleteSchoolManagement\SchoolModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [C:\USERS\15147\SOURCE\REPOS\COMPLETESCHOOLMANAGEMENT\COMPLETESCHOOLMANAGEMENT\APP_DATA\SCHOOLDB.MDF];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CoursesMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MaterialSet] DROP CONSTRAINT [FK_CoursesMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentCourses_Student]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudentCourses] DROP CONSTRAINT [FK_StudentCourses_Student];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentCourses_Courses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudentCourses] DROP CONSTRAINT [FK_StudentCourses_Courses];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[StudentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudentSet];
GO
IF OBJECT_ID(N'[dbo].[CoursesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CoursesSet];
GO
IF OBJECT_ID(N'[dbo].[MaterialSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MaterialSet];
GO
IF OBJECT_ID(N'[dbo].[TeacherSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TeacherSet];
GO
IF OBJECT_ID(N'[dbo].[StudentCourses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudentCourses];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'StudentSet'
CREATE TABLE [dbo].[StudentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Grade] nvarchar(max)  NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [isFirstLogin] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CoursesSet'
CREATE TABLE [dbo].[CoursesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [StudentId] int  NOT NULL
);
GO

-- Creating table 'MaterialSet'
CREATE TABLE [dbo].[MaterialSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Note] nvarchar(max)  NOT NULL,
    [Movie] nvarchar(max)  NOT NULL,
    [Laboratory] nvarchar(max)  NOT NULL,
    [Exercise] nvarchar(max)  NOT NULL,
    [Solution] nvarchar(max)  NOT NULL,
    [CoursesId] int  NOT NULL
);
GO

-- Creating table 'TeacherSet'
CREATE TABLE [dbo].[TeacherSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'StudentCourses'
CREATE TABLE [dbo].[StudentCourses] (
    [Student_Id] int  NOT NULL,
    [Courses_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'StudentSet'
ALTER TABLE [dbo].[StudentSet]
ADD CONSTRAINT [PK_StudentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CoursesSet'
ALTER TABLE [dbo].[CoursesSet]
ADD CONSTRAINT [PK_CoursesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MaterialSet'
ALTER TABLE [dbo].[MaterialSet]
ADD CONSTRAINT [PK_MaterialSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TeacherSet'
ALTER TABLE [dbo].[TeacherSet]
ADD CONSTRAINT [PK_TeacherSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Student_Id], [Courses_Id] in table 'StudentCourses'
ALTER TABLE [dbo].[StudentCourses]
ADD CONSTRAINT [PK_StudentCourses]
    PRIMARY KEY CLUSTERED ([Student_Id], [Courses_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CoursesId] in table 'MaterialSet'
ALTER TABLE [dbo].[MaterialSet]
ADD CONSTRAINT [FK_CoursesMaterial]
    FOREIGN KEY ([CoursesId])
    REFERENCES [dbo].[CoursesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CoursesMaterial'
CREATE INDEX [IX_FK_CoursesMaterial]
ON [dbo].[MaterialSet]
    ([CoursesId]);
GO

-- Creating foreign key on [Student_Id] in table 'StudentCourses'
ALTER TABLE [dbo].[StudentCourses]
ADD CONSTRAINT [FK_StudentCourses_Student]
    FOREIGN KEY ([Student_Id])
    REFERENCES [dbo].[StudentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Courses_Id] in table 'StudentCourses'
ALTER TABLE [dbo].[StudentCourses]
ADD CONSTRAINT [FK_StudentCourses_Courses]
    FOREIGN KEY ([Courses_Id])
    REFERENCES [dbo].[CoursesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentCourses_Courses'
CREATE INDEX [IX_FK_StudentCourses_Courses]
ON [dbo].[StudentCourses]
    ([Courses_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------