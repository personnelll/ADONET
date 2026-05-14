SELECT [ETU_id],[ETU_NOM],[ETU_PRENOM],[ETU_MATRICULE]
  FROM [CoursSGBD].[dbo].[Etudiant]
  where [ETU_NOM] like @ETU_NOM
