USE [Montinorte]
GO

/****** Object:  Table [dbo].[Rateio_Mov]    Script Date: 06/08/2010 11:50:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rateio_Mov](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDMovimento] [int] NULL,
	[IDProjeto] [int] NULL,
	[Valor] [numeric](18, 2) NULL,
	[Sequencia] [int] NULL,
 CONSTRAINT [PK_RateioMov] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Rateio_Mov]  WITH CHECK ADD  CONSTRAINT [FK_Rateio_Mov_Movimento] FOREIGN KEY([IDMovimento])
REFERENCES [dbo].[Movimento] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Rateio_Mov] CHECK CONSTRAINT [FK_Rateio_Mov_Movimento]
GO

ALTER TABLE [dbo].[Rateio_Mov]  WITH CHECK ADD  CONSTRAINT [FK_Rateio_Mov_Projeto] FOREIGN KEY([IDProjeto])
REFERENCES [dbo].[Projeto] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Rateio_Mov] CHECK CONSTRAINT [FK_Rateio_Mov_Projeto]
GO

