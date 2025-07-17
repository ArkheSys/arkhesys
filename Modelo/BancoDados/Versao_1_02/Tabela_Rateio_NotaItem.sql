USE [Montinorte]
GO

/****** Object:  Table [dbo].[Rateio_NotaItem]    Script Date: 06/08/2010 11:51:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rateio_NotaItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDNotaItem] [int] NULL,
	[IDProjeto] [int] NULL,
	[Valor] [numeric](18, 2) NULL,
	[Sequencia] [int] NULL,
 CONSTRAINT [PK_Rateio_NotaItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Rateio_NotaItem]  WITH CHECK ADD  CONSTRAINT [FK_Rateio_NotaItem_NotaItem] FOREIGN KEY([IDNotaItem])
REFERENCES [dbo].[NotaItem] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Rateio_NotaItem] CHECK CONSTRAINT [FK_Rateio_NotaItem_NotaItem]
GO

ALTER TABLE [dbo].[Rateio_NotaItem]  WITH CHECK ADD  CONSTRAINT [FK_Rateio_NotaItem_Projeto] FOREIGN KEY([IDProjeto])
REFERENCES [dbo].[Projeto] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Rateio_NotaItem] CHECK CONSTRAINT [FK_Rateio_NotaItem_Projeto]
GO

