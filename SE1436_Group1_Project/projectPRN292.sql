CREATE DATABASE [ProjectPRN292]
GO
USE [ProjectPRN292]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 7/24/2021 4:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[accountID] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[accountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 7/24/2021 4:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[className] [nvarchar](50) NOT NULL,
	[accountID] [int] NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[className] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExamCode]    Script Date: 7/24/2021 4:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExamCode](
	[examCode] [nvarchar](50) NOT NULL,
	[examName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[examCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScoreStudent]    Script Date: 7/24/2021 4:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScoreStudent](
	[StudentID] [nvarchar](50) NOT NULL,
	[examCode] [nvarchar](50) NOT NULL,
	[className] [nvarchar](50) NOT NULL,
	[totalScore] [float] NOT NULL,
	[scoreDetail] [ntext] NULL,
	[studentName] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([accountID], [username], [password]) VALUES (1, N'congbv', N'123')
INSERT [dbo].[Account] ([accountID], [username], [password]) VALUES (2, N'manhnv', N'123')
INSERT [dbo].[Account] ([accountID], [username], [password]) VALUES (3, N'trangnb', N'123')
INSERT [dbo].[Account] ([accountID], [username], [password]) VALUES (4, N'huongnt7', N'123')
INSERT [dbo].[Account] ([accountID], [username], [password]) VALUES (30, N'1212', N'1212')
INSERT [dbo].[Account] ([accountID], [username], [password]) VALUES (31, N'congbv1', N'123')
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
INSERT [dbo].[ExamCode] ([examCode], [examName]) VALUES (N'PRF192', N'C')
INSERT [dbo].[ExamCode] ([examCode], [examName]) VALUES (N'PRJ301', N'Java Web')
INSERT [dbo].[ExamCode] ([examCode], [examName]) VALUES (N'PRN292', N'C#')
INSERT [dbo].[ExamCode] ([examCode], [examName]) VALUES (N'PRO192', N'Java')
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Account] FOREIGN KEY([accountID])
REFERENCES [dbo].[Account] ([accountID])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Account]
GO
ALTER TABLE [dbo].[ScoreStudent]  WITH CHECK ADD  CONSTRAINT [FK_ScoreStudent_Class] FOREIGN KEY([className])
REFERENCES [dbo].[Class] ([className])
GO
ALTER TABLE [dbo].[ScoreStudent] CHECK CONSTRAINT [FK_ScoreStudent_Class]
GO
ALTER TABLE [dbo].[ScoreStudent]  WITH CHECK ADD  CONSTRAINT [FK_ScoreStudent_ExamCode] FOREIGN KEY([examCode])
REFERENCES [dbo].[ExamCode] ([examCode])
GO
ALTER TABLE [dbo].[ScoreStudent] CHECK CONSTRAINT [FK_ScoreStudent_ExamCode]
GO
