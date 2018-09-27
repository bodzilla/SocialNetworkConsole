USE [SocialNetworkConsole]
GO
/****** Object:  Table [dbo].[Follow]    Script Date: 27/09/2018 05:25:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Follow](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[FollowerUserId] [int] NOT NULL,
	[FollowingUserId] [int] NOT NULL,
 CONSTRAINT [PK_Follow] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 27/09/2018 05:25:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[Text] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 27/09/2018 05:25:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Follow] ON 

INSERT [dbo].[Follow] ([Id], [DateCreated], [FollowerUserId], [FollowingUserId]) VALUES (1, CAST(N'2018-09-27T15:07:21.757' AS DateTime), 1, 3)
SET IDENTITY_INSERT [dbo].[Follow] OFF
SET IDENTITY_INSERT [dbo].[Post] ON 

INSERT [dbo].[Post] ([Id], [DateCreated], [UserId], [Text]) VALUES (1, CAST(N'2018-09-27T13:52:18.947' AS DateTime), 1, N'What a lovely day!')
INSERT [dbo].[Post] ([Id], [DateCreated], [UserId], [Text]) VALUES (2, CAST(N'2018-09-27T13:53:24.023' AS DateTime), 1, N'Lunch is the best meal of the day.')
INSERT [dbo].[Post] ([Id], [DateCreated], [UserId], [Text]) VALUES (3, CAST(N'2018-09-27T13:53:40.377' AS DateTime), 1, N'Working hard, or hardly working?')
INSERT [dbo].[Post] ([Id], [DateCreated], [UserId], [Text]) VALUES (5, CAST(N'2018-09-27T14:46:36.007' AS DateTime), 1, N'Posting this from the Social Network Console!')
INSERT [dbo].[Post] ([Id], [DateCreated], [UserId], [Text]) VALUES (6, CAST(N'2018-09-27T15:16:46.037' AS DateTime), 3, N'Perfect day for a butter beer..')
INSERT [dbo].[Post] ([Id], [DateCreated], [UserId], [Text]) VALUES (7, CAST(N'2018-09-27T15:46:08.643' AS DateTime), 1, N'Wish I could use Entity Framework for this.')
INSERT [dbo].[Post] ([Id], [DateCreated], [UserId], [Text]) VALUES (10, CAST(N'2018-09-27T16:45:12.817' AS DateTime), 3, N'I need a break.')
SET IDENTITY_INSERT [dbo].[Post] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [DateCreated], [Name]) VALUES (1, CAST(N'2018-09-27T13:48:49.897' AS DateTime), N'Bodrul')
INSERT [dbo].[User] ([Id], [DateCreated], [Name]) VALUES (3, CAST(N'2018-09-27T14:58:52.850' AS DateTime), N'Harry')
SET IDENTITY_INSERT [dbo].[User] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User]    Script Date: 27/09/2018 05:25:08 PM ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [IX_User] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Follow]  WITH CHECK ADD  CONSTRAINT [FK_Follow_User] FOREIGN KEY([FollowerUserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Follow] CHECK CONSTRAINT [FK_Follow_User]
GO
ALTER TABLE [dbo].[Follow]  WITH CHECK ADD  CONSTRAINT [FK_Follow_User1] FOREIGN KEY([FollowingUserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Follow] CHECK CONSTRAINT [FK_Follow_User1]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_User]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_User2] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_User2]
GO
