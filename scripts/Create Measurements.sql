IF OBJECT_ID('dbo.Measurements', 'U') IS NOT NULL 
  DROP TABLE dbo.Measurements; 

CREATE TABLE [dbo].[Measurements]
(
    [MeasurementId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [TimeUtc] DATETIME NOT NULL,
    [Temperature] FLOAT NOT NULL,
    [Pressure] FLOAT NOT NULL,
    [Humidity] FLOAT NOT NULL
)
