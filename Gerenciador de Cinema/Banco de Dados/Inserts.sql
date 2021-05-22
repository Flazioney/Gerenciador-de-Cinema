INSERT INTO [dbo].[salas]
           ([Nome]
           ,[qtd_assentos])
     VALUES
           ('Sala A', '50'),
		   ('Sala B', '70'),
		   ('Sala C', '60');

INSERT INTO [dbo].[Login]
           ([nome]
           ,[senha]
           ,[email]
           ,[tipoAcesso])
     VALUES
          ('Administrador', '1','administrador@teste.com', '1')
GO