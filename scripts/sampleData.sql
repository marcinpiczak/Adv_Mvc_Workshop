USE [MyMessagePortalDb]
GO
INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', 0, N'194642b4-72aa-44e0-9cea-d612ce3a0e92', N'tomek@wp.pl', 0, 1, NULL, N'TOMEK@WP.PL', N'TOMEK', N'AQAAAAEAACcQAAAAEKKrkg87YBRANBcCXqyjM9Eh3jrzCVSD9Y0P9UZIUo2eeBXZ6sboqokhGBtfM/nkUQ==', NULL, 0, N'2f2c6e31-9cd5-410e-b3bf-f1eff894300f', 0, N'tomek')
INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'f735f26d-7ebb-48e8-9d7c-24eebca6ad7f', 0, N'f78beab5-cf56-49bd-beb0-ef47b41d14bb', N'marcin@wp.pl', 0, 1, NULL, N'MARCIN@WP.PL', N'MARCIN', N'AQAAAAEAACcQAAAAEMlhSWt+m2cc0FcLfQ8/JkHJqydSU8WMtvhFMQfwEVxI/pVIdqO79vQD6WXG4+6NiQ==', NULL, 0, N'894c8c9f-421f-4dda-8253-15ad4802fa42', 0, N'marcin')

SET IDENTITY_INSERT [dbo].[Channels] ON 
INSERT [dbo].[Channels] ([Id], [ChannelColor], [CreatedById], [DateAdded], [DateModified], [IsDefault], [Name]) VALUES (1, N'73CA84', N'f735f26d-7ebb-48e8-9d7c-24eebca6ad7f', CAST(N'2018-08-11T21:25:11.6478320' AS DateTime2), NULL, 1, N'Default_marcin')
INSERT [dbo].[Channels] ([Id], [ChannelColor], [CreatedById], [DateAdded], [DateModified], [IsDefault], [Name]) VALUES (2, N'9E3C64', N'f735f26d-7ebb-48e8-9d7c-24eebca6ad7f', CAST(N'2018-08-11T22:32:01.7280507' AS DateTime2), NULL, 0, N'Kanał_1')
INSERT [dbo].[Channels] ([Id], [ChannelColor], [CreatedById], [DateAdded], [DateModified], [IsDefault], [Name]) VALUES (3, N'F34F63', N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', CAST(N'2018-08-12T09:12:01.7704255' AS DateTime2), NULL, 1, N'Default_tomek')
INSERT [dbo].[Channels] ([Id], [ChannelColor], [CreatedById], [DateAdded], [DateModified], [IsDefault], [Name]) VALUES (6, N'429859', N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', CAST(N'2018-08-13T22:07:49.5335935' AS DateTime2), NULL, 0, N'Kanał_tomka_1')
INSERT [dbo].[Channels] ([Id], [ChannelColor], [CreatedById], [DateAdded], [DateModified], [IsDefault], [Name]) VALUES (7, N'4C8205', N'f735f26d-7ebb-48e8-9d7c-24eebca6ad7f', CAST(N'2018-08-17T19:45:44.9016531' AS DateTime2), NULL, 0, N'Marcin_nowy_kanał')
SET IDENTITY_INSERT [dbo].[Channels] OFF

SET IDENTITY_INSERT [dbo].[Messages] ON 
INSERT [dbo].[Messages] ([Id], [ChannelId], [CreatedById], [DateAdded], [DateModified], [Text]) VALUES (6, 1, N'f735f26d-7ebb-48e8-9d7c-24eebca6ad7f', CAST(N'2018-08-13T22:07:01.7656276' AS DateTime2), NULL, N'pierwsza wiadomość na domyślnym kanale')
INSERT [dbo].[Messages] ([Id], [ChannelId], [CreatedById], [DateAdded], [DateModified], [Text]) VALUES (7, 6, N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', CAST(N'2018-08-14T20:28:59.5806000' AS DateTime2), NULL, N'pierwsza wiadomść')
INSERT [dbo].[Messages] ([Id], [ChannelId], [CreatedById], [DateAdded], [DateModified], [Text]) VALUES (9, 6, N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', CAST(N'2018-08-15T12:22:14.4904829' AS DateTime2), NULL, N'wiadomość musi mieć 10 znaków')
INSERT [dbo].[Messages] ([Id], [ChannelId], [CreatedById], [DateAdded], [DateModified], [Text]) VALUES (10, 6, N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', CAST(N'2018-08-16T19:59:40.1970281' AS DateTime2), NULL, N'jakaś tam wiadomość oby tylko była dłuższa niż zwykle. Lorem impus i tak dalej...')
INSERT [dbo].[Messages] ([Id], [ChannelId], [CreatedById], [DateAdded], [DateModified], [Text]) VALUES (11, 6, N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', CAST(N'2018-08-16T20:48:10.3993781' AS DateTime2), NULL, N'nowa wiadomość')
INSERT [dbo].[Messages] ([Id], [ChannelId], [CreatedById], [DateAdded], [DateModified], [Text]) VALUES (12, 3, N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', CAST(N'2018-08-17T18:49:41.7680407' AS DateTime2), NULL, N'nowa wiadomość')
INSERT [dbo].[Messages] ([Id], [ChannelId], [CreatedById], [DateAdded], [DateModified], [Text]) VALUES (13, 1, N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', CAST(N'2018-08-17T19:05:48.4497494' AS DateTime2), NULL, N'nowa wiadomość')
INSERT [dbo].[Messages] ([Id], [ChannelId], [CreatedById], [DateAdded], [DateModified], [Text]) VALUES (14, 2, N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', CAST(N'2018-08-17T19:06:07.8704939' AS DateTime2), NULL, N'wiadomość 1')
INSERT [dbo].[Messages] ([Id], [ChannelId], [CreatedById], [DateAdded], [DateModified], [Text]) VALUES (15, 3, N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', CAST(N'2018-08-17T19:21:39.4376037' AS DateTime2), NULL, N'kolejna wiadomość')
INSERT [dbo].[Messages] ([Id], [ChannelId], [CreatedById], [DateAdded], [DateModified], [Text]) VALUES (16, 3, N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', CAST(N'2018-08-17T19:38:58.9985143' AS DateTime2), NULL, N'test dodawania wiadomości bez skryptów walidacyjnych')
INSERT [dbo].[Messages] ([Id], [ChannelId], [CreatedById], [DateAdded], [DateModified], [Text]) VALUES (18, 7, N'f735f26d-7ebb-48e8-9d7c-24eebca6ad7f', CAST(N'2018-08-17T19:46:44.8206719' AS DateTime2), NULL, N'pierwsza wiadomość')
INSERT [dbo].[Messages] ([Id], [ChannelId], [CreatedById], [DateAdded], [DateModified], [Text]) VALUES (19, 7, N'f735f26d-7ebb-48e8-9d7c-24eebca6ad7f', CAST(N'2018-09-07T20:57:36.2586970' AS DateTime2), NULL, N'druga wiadomość')
SET IDENTITY_INSERT [dbo].[Messages] OFF

INSERT [dbo].[ObservedChannels] ([UserId], [ChannelId]) VALUES (N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', 1)
INSERT [dbo].[ObservedChannels] ([UserId], [ChannelId]) VALUES (N'f735f26d-7ebb-48e8-9d7c-24eebca6ad7f', 1)
INSERT [dbo].[ObservedChannels] ([UserId], [ChannelId]) VALUES (N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', 2)
INSERT [dbo].[ObservedChannels] ([UserId], [ChannelId]) VALUES (N'f735f26d-7ebb-48e8-9d7c-24eebca6ad7f', 2)
INSERT [dbo].[ObservedChannels] ([UserId], [ChannelId]) VALUES (N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', 3)
INSERT [dbo].[ObservedChannels] ([UserId], [ChannelId]) VALUES (N'f735f26d-7ebb-48e8-9d7c-24eebca6ad7f', 3)
INSERT [dbo].[ObservedChannels] ([UserId], [ChannelId]) VALUES (N'13c593d7-1d50-4815-a9ac-7ac6c94c3760', 6)
INSERT [dbo].[ObservedChannels] ([UserId], [ChannelId]) VALUES (N'f735f26d-7ebb-48e8-9d7c-24eebca6ad7f', 7)
