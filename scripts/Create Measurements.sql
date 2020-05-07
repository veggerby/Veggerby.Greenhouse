IF OBJECT_ID('dbo.Measurements', 'U') IS NOT NULL 
  DROP TABLE dbo.Measurements; 

CREATE TABLE [dbo].[Measurements]
(
    [MeasurementId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [DeviceId] NVARC
    [TimeUtc] DATETIME NOT NULL,
    [Temperature] FLOAT NOT NULL,
    [Pressure] FLOAT NOT NULL,
    [Humidity] FLOAT NOT NULL
)

IF OBJECT_ID('dbo.Device', 'U') IS NOT NULL 
  DROP TABLE dbo.Device; 

CREATE TABLE [dbo].[Decive]
(
    [DeviceId] NVARCHAR(10) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(MAX) NOT NULL
)
