USE [master]
GO
/****** Object:  Database [GC_BD]    Script Date: 22/05/2021 16:37:59 ******/
CREATE DATABASE [GC_BD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GC_BD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SRV_BD\MSSQL\DATA\GC_BD.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GC_BD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SRV_BD\MSSQL\DATA\GC_BD_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [GC_BD] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GC_BD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GC_BD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GC_BD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GC_BD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GC_BD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GC_BD] SET ARITHABORT OFF 
GO
ALTER DATABASE [GC_BD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GC_BD] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [GC_BD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GC_BD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GC_BD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GC_BD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GC_BD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GC_BD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GC_BD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GC_BD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GC_BD] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GC_BD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GC_BD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GC_BD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GC_BD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GC_BD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GC_BD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GC_BD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GC_BD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GC_BD] SET  MULTI_USER 
GO
ALTER DATABASE [GC_BD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GC_BD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GC_BD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GC_BD] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [GC_BD]
GO
/****** Object:  StoredProcedure [dbo].[buscaFilmes]    Script Date: 22/05/2021 16:37:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[buscaFilmes] as select * from sessao order by data_exb desc;
GO
/****** Object:  StoredProcedure [dbo].[buscaLogin]    Script Date: 22/05/2021 16:37:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[buscaLogin] @usuario varchar(100), @senha varchar(100) AS 
BEGIN 
	DECLARE @autenticacao VARCHAR(10)='Falhou' 

	IF NOT EXISTS(Select 1 as total from LOGIN where email = @usuario and senha = @senha)
	BEGIN 
		SET @autenticacao='Sucesso'
	END
	select @autenticacao AS EstaAutenticado;
END
GO
/****** Object:  StoredProcedure [dbo].[delpcdSe]    Script Date: 22/05/2021 16:37:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[delpcdSe] @idses int
AS 
BEGIN 
	DECLARE @deletar VARCHAR(100)='Não pode ser excluido menor que a data' 
	insert into logs (erroslogs) values (@idses)
	delete from sessao where id_sessao in (select id_sessao from sessao where id_sessao = @idses AND data_exb >= DATEADD(DAY, 10, SYSDATETIME()))
	BEGIN 
		SET @deletar='Sucesso'
		
		insert into logs (erroslogs, datatime) values ('Exclusão de sessão id_sessao: '+ STR(@idses) + ' ' + @deletar, CURRENT_TIMESTAMP)
	END
	select @deletar AS EstaDeletado;
	insert into logs (erroslogs, datatime) values ('Exclusão de sessão id_sessao: '+ STR(@idses) + ' ' + @deletar, CURRENT_TIMESTAMP)
END


GO
/****** Object:  StoredProcedure [dbo].[pcdfilmes]    Script Date: 22/05/2021 16:37:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure[dbo].[pcdfilmes] @titulo varchar(20), @descricao varchar(50), @duracao time as 

INSERT INTO [dbo].[tbl_filmes]
           ([Titulo]
           ,[Descricao]
           ,[duracao])
     VALUES
           (@titulo,
           @descricao,
           @duracao)





GO
/****** Object:  StoredProcedure [dbo].[pcdSessao]    Script Date: 22/05/2021 16:37:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pcdSessao] @dataexb date, @hrini time, @valoring decimal(15, 2), @idfilme int, @idsala int AS 
BEGIN 
	DECLARE @autenticacao VARCHAR(10)='Falhou'
INSERT INTO [dbo].[sessao]
           ([data_exb]
           ,[hr_ini]
           ,[hr_fim]
           ,[valor_ing]
           ,[id_filme]
           ,[id_sala])
     VALUES
           (@dataexb, 
            @hrini,
			(select CAST(DATEADD(ss, SUM(DATEDIFF(ms, b.duracao,  @hrini)), '00:00:00.000') as time) from sessao a join filmes b on b.id_filme = a.id_filme where a.id_filme = @idfilme), 
           @valoring
           ,@idfilme
           ,@idsala)
	BEGIN 
		SET @autenticacao='Sucesso'
	END
	select @autenticacao AS EstaAutenticado;
END







GO
/****** Object:  StoredProcedure [dbo].[pcdUpdtFilme]    Script Date: 22/05/2021 16:37:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pcdUpdtFilme] @id int, @Titulo varchar(50), @Descricao varchar(100), @duracao time(7), @dados image, @ContentType varchar(max)
as
UPDATE [dbo].[filmes]
   SET [Titulo] = @Titulo
      ,[Descricao] = @Descricao
      ,[duracao] = @duracao
      ,[dados] = @dados
      ,[ContentType] = @ContentType
 WHERE id_filme = @id

GO
/****** Object:  StoredProcedure [dbo].[selectpcdsessao]    Script Date: 22/05/2021 16:37:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[selectpcdsessao] as select
b.Titulo,
b.Descricao,
c.Nome as Sala,
b.duracao,
a.valor_ing as Preco,
a.data_exb as Data,
a.hr_ini as Inicio,
a.hr_fim as Hr_Fim,
c.qtd_assentos as Quantidade_Assentos
from tbl_sessao a inner join  tbl_salas c
on a.id_sala = c.id_sala
inner join tbl_filmes b 
on a.id_filme = b.id_filme;



GO
/****** Object:  Table [dbo].[filmes]    Script Date: 22/05/2021 16:37:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[filmes](
	[id_filme] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](50) NULL,
	[Descricao] [varchar](100) NULL,
	[duracao] [time](7) NULL,
	[dados] [image] NULL,
	[ContentType] [varchar](max) NULL,
	[Extension] [nvarchar](4) NULL,
	[Length] [int] NULL,
 CONSTRAINT [PK_tbl_filmes] PRIMARY KEY CLUSTERED 
(
	[id_filme] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Login]    Script Date: 22/05/2021 16:37:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Login](
	[id_login] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[senha] [varchar](20) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[tipoAcesso] [int] NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[id_login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[logs]    Script Date: 22/05/2021 16:37:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[logs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[erroslogs] [varchar](max) NULL,
	[datatime] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[salas]    Script Date: 22/05/2021 16:37:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[salas](
	[id_sala] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](20) NOT NULL,
	[qtd_assentos] [int] NOT NULL,
 CONSTRAINT [PK_tbl_salas] PRIMARY KEY CLUSTERED 
(
	[id_sala] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sessao]    Script Date: 22/05/2021 16:37:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sessao](
	[id_sessao] [int] IDENTITY(1,1) NOT NULL,
	[data_exb] [date] NOT NULL,
	[hr_ini] [time](7) NOT NULL,
	[hr_fim] [time](7) NOT NULL,
	[valor_ing] [decimal](15, 2) NOT NULL,
	[id_filme] [int] NOT NULL,
	[id_sala] [int] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[sessao]  WITH NOCHECK ADD  CONSTRAINT [FK_tbl_sessao_tbl_filmes] FOREIGN KEY([id_filme])
REFERENCES [dbo].[filmes] ([id_filme])
ON UPDATE CASCADE
ON DELETE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[sessao] NOCHECK CONSTRAINT [FK_tbl_sessao_tbl_filmes]
GO
ALTER TABLE [dbo].[sessao]  WITH NOCHECK ADD  CONSTRAINT [FK_tbl_sessao_tbl_salas] FOREIGN KEY([id_sala])
REFERENCES [dbo].[salas] ([id_sala])
ON UPDATE CASCADE
ON DELETE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[sessao] NOCHECK CONSTRAINT [FK_tbl_sessao_tbl_salas]
GO
USE [master]
GO
ALTER DATABASE [GC_BD] SET  READ_WRITE 
GO
