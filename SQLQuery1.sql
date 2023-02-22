USE [CIIC.TongXun]
GO

SELECT A.[Id]
      ,[ArticleTitle]
      ,[ArticleContent]
      ,[IsPublic]
      ,[CreateTime]
      ,[CreateBy]
      ,[IsDelete]
      ,[ArticlePropertyId]
      ,[CategoryId]
	  ,B.JournalId
	  ,C.JournalName --ÆÚ¿¯Ãû
  FROM [Article] A LEFT JOIN dbo.JournalArticleRelation B ON B.Id = A.Id
  LEFT JOIN ArticleJournal C ON	B.JournalId=C.JournalId
GO


