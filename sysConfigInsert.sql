USE [ZEKA_IEP]
GO

INSERT INTO [dbo].[SystemConf]
           ([silver_pack]
           ,[gold_pack]
           ,[platinum_pack]
           ,[mrp_group]
           ,[auction_duration]
           ,[currency]
           ,[token_value])
     VALUES
           (100,200,300,20,1000,'USD',3)
GO


