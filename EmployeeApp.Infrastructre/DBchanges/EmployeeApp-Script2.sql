
CREATE DATABASE [EmployeeApp]
 
USE [EmployeeApp]
GO
/****** Object:  Table [dbo].[DepartmentMaster]    Script Date: 16-03-2024 19:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepartmentMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [varchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedAt] [datetime] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeMaster]    Script Date: 16-03-2024 19:10:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentID] [int] NULL,
	[Name] [varchar](50) NULL,
	[DOB] [date] NULL,
	[Gender] [varchar](1) NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedAt] [datetime] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DepartmentMaster] ON 

INSERT [dbo].[DepartmentMaster] ([ID], [DepartmentName], [CreatedAt], [CreatedBy], [UpdatedBy], [UpdatedAt], [IsActive]) VALUES (1, N'Development', CAST(N'2024-03-16T01:00:27.030' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[DepartmentMaster] ([ID], [DepartmentName], [CreatedAt], [CreatedBy], [UpdatedBy], [UpdatedAt], [IsActive]) VALUES (2, N'Sales', CAST(N'2024-03-16T01:00:27.030' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[DepartmentMaster] ([ID], [DepartmentName], [CreatedAt], [CreatedBy], [UpdatedBy], [UpdatedAt], [IsActive]) VALUES (3, N'Account', CAST(N'2024-03-16T01:00:27.030' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[DepartmentMaster] ([ID], [DepartmentName], [CreatedAt], [CreatedBy], [UpdatedBy], [UpdatedAt], [IsActive]) VALUES (4, N'HR', CAST(N'2024-03-16T01:00:27.030' AS DateTime), NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[DepartmentMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeMaster] ON 

INSERT [dbo].[EmployeeMaster] ([ID], [DepartmentID], [Name], [DOB], [Gender], [CreatedAt], [CreatedBy], [UpdatedBy], [UpdatedAt], [IsActive]) VALUES (7, 0, N'5656', CAST(N'2024-03-16' AS Date), N'M', CAST(N'2024-03-16T13:36:52.690' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[EmployeeMaster] ([ID], [DepartmentID], [Name], [DOB], [Gender], [CreatedAt], [CreatedBy], [UpdatedBy], [UpdatedAt], [IsActive]) VALUES (9, 3, N'Johnaaa', CAST(N'2023-03-01' AS Date), N'M', CAST(N'2024-03-16T13:56:45.650' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[EmployeeMaster] ([ID], [DepartmentID], [Name], [DOB], [Gender], [CreatedAt], [CreatedBy], [UpdatedBy], [UpdatedAt], [IsActive]) VALUES (11, 3, N'Rana', CAST(N'2024-03-01' AS Date), N'M', CAST(N'2024-03-16T14:00:21.993' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[EmployeeMaster] ([ID], [DepartmentID], [Name], [DOB], [Gender], [CreatedAt], [CreatedBy], [UpdatedBy], [UpdatedAt], [IsActive]) VALUES (13, 0, N'Test 3', CAST(N'2024-03-16' AS Date), N'F', CAST(N'2024-03-16T14:51:16.227' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[EmployeeMaster] ([ID], [DepartmentID], [Name], [DOB], [Gender], [CreatedAt], [CreatedBy], [UpdatedBy], [UpdatedAt], [IsActive]) VALUES (14, 0, N'', CAST(N'2024-03-16' AS Date), N'M', CAST(N'2024-03-16T17:52:54.467' AS DateTime), NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[EmployeeMaster] OFF
GO
ALTER TABLE [dbo].[DepartmentMaster] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[DepartmentMaster] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[EmployeeMaster] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[EmployeeMaster] ADD  DEFAULT ((1)) FOR [IsActive]
GO
USE [master]
GO
ALTER DATABASE [EmployeeApp] SET  READ_WRITE 
GO
