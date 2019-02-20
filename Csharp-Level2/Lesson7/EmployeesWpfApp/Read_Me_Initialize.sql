--Перед запуском приложения необходимо:
--1. Создать  базу данных MS SQL LocalDB с именем "my_employees_db".

--2. Создать в базе таблицы Departments и Employees:
CREATE TABLE [dbo].[Departments] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR (50)    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC));
CREATE TABLE [dbo].[Employees] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [FirstName]     NVARCHAR (50)    NOT NULL,
    [LastName]      NVARCHAR (50)    NOT NULL,
    [Department_Id] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employees_Department] FOREIGN KEY ([Department_Id]) REFERENCES [dbo].[Departments] ([Id]));

--3. Заполнить таблицы начальными данными:
INSERT INTO [dbo].[Departments] ([Id], [Name]) VALUES (N'73f35b2e-8d0d-448f-b111-10bedd4a9e0c', N'Департамент развития')
INSERT INTO [dbo].[Departments] ([Id], [Name]) VALUES (N'12862fc4-0df9-4155-aa5c-186ad0df5cc9', N'Управление и финансы')
INSERT INTO [dbo].[Employees] ([Id], [FirstName], [LastName], [Department_Id]) VALUES (N'd11135f3-7504-4cb1-8883-1f44273bc0ba', N'Иван', N'Петров', N'12862fc4-0df9-4155-aa5c-186ad0df5cc9')
INSERT INTO [dbo].[Employees] ([Id], [FirstName], [LastName], [Department_Id]) VALUES (N'278836fe-80e8-4c4a-b27a-36d9783384bc', N'Светлана', N'Сенчукова', N'12862fc4-0df9-4155-aa5c-186ad0df5cc9')
INSERT INTO [dbo].[Employees] ([Id], [FirstName], [LastName], [Department_Id]) VALUES (N'7ad557d8-554d-486f-9c90-6fb127292904', N'Сергей', N'Филипчук', N'73f35b2e-8d0d-448f-b111-10bedd4a9e0c')
INSERT INTO [dbo].[Employees] ([Id], [FirstName], [LastName], [Department_Id]) VALUES (N'd35b6ba1-4f8f-40ef-88c4-a53d2bd6e00f', N'Дмитрий', N'Захаров', N'73f35b2e-8d0d-448f-b111-10bedd4a9e0c')

