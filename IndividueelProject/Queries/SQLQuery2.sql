/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [factionId]
      ,[race]
      ,[factionName]
      ,[factionLeader]
      ,[factionStronghold]
  FROM [DB_Team10].[dbo].[factions]
  