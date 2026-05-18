USE [CoursSGBD]

delete from [dbo].[Kot]
delete from [dbo].[Etudiant]

INSERT INTO [dbo].[Etudiant] ([ETU_NOM],[ETU_PRENOM],[ETU_MATRICULE])
VALUES ('Smith','John','HE12345'),
	   ('Doe','Jane','PS54321'),
	   ('Brown','Charlie','PS67890')