SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Flights](
	[IATAFrom] [varchar](3) NOT NULL,
	[IATATo] [varchar](3) NOT NULL,
 CONSTRAINT [PK_Flights] PRIMARY KEY CLUSTERED 
(
	[IATAFrom] ASC,
	[IATATo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Flights]  WITH CHECK ADD  CONSTRAINT [FK_DirectFlight_Airport_From] FOREIGN KEY([IATAFrom])
REFERENCES [dbo].[Airports] ([IATA])
GO

ALTER TABLE [dbo].[Flights] CHECK CONSTRAINT [FK_DirectFlight_Airport_From]
GO

ALTER TABLE [dbo].[Flights]  WITH CHECK ADD  CONSTRAINT [FK_DirectFlight_Airport_To] FOREIGN KEY([IATATo])
REFERENCES [dbo].[Airports] ([IATA])
GO

ALTER TABLE [dbo].[Flights] CHECK CONSTRAINT [FK_DirectFlight_Airport_To]
GO