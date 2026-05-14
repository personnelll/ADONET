SELECT ETU_Id as Id, ETU_NOM as lastName, ETU_MATRICULE as matricule, ETU_PRENOM as firstName from dbo.Etudiant
where ETU_NOM like @lastName