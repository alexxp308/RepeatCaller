USE [REPEAT_CALLER]
GO
/****** Object:  UserDefinedTableType [dbo].[TIPIType]    Script Date: 03/26/2018 15:55:30 ******/
CREATE TYPE [dbo].[TIPIType] AS TABLE(
	[BaseId] [int] NULL,
	[campaniaId] [int] NULL,
	[fechaBase] [varchar](10) NULL,
	[FECHA_CREACION] [varchar](50) NULL,
	[FECHA_DE_CREACION] [varchar](50) NULL,
	[TIPO_LOGIN] [varchar](10) NULL,
	[LOGIN_AGENTE] [varchar](50) NULL,
	[NOMBRE DE ASESOR] [varchar](200) NULL,
	[ID_INTERACCION] [varchar](50) NULL,
	[TITULO_INTERACCION] [varchar](200) NULL,
	[TIPO_INTERACCION] [varchar](100) NULL,
	[CLASE_INTERACCION] [varchar](200) NULL,
	[SUBCLASE_INTERACCION] [varchar](100) NULL,
	[NOMBRE_CONTACO] [varchar](100) NULL,
	[APELLIDO_CONTACTO] [varchar](100) NULL,
	[TELEFONO] [varchar](50) NULL,
	[CICLO_FACTURACION] [varchar](50) NULL,
	[MODALIDAD] [varchar](100) NULL,
	[SERVICIO_AFECTADO] [varchar](100) NULL,
	[INCONVENIENTE] [varchar](200) NULL
)
GO
/****** Object:  Table [dbo].[Sede]    Script Date: 03/26/2018 15:55:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sede](
	[sedeId] [int] IDENTITY(1,1) NOT NULL,
	[nombreSede] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[sedeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Reporte_TIPI]    Script Date: 03/26/2018 15:55:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Reporte_TIPI](
	[BaseId] [int] NULL,
	[campaniaId] [int] NULL,
	[fechaBase] [varchar](10) NULL,
	[FECHA_CREACION] [varchar](50) NULL,
	[FECHA_DE_CREACION] [varchar](50) NULL,
	[TIPO_LOGIN] [varchar](10) NULL,
	[LOGIN_AGENTE] [varchar](50) NULL,
	[NOMBRE DE ASESOR] [varchar](200) NULL,
	[ID_INTERACCION] [varchar](50) NULL,
	[TITULO_INTERACCION] [varchar](200) NULL,
	[TIPO_INTERACCION] [varchar](100) NULL,
	[CLASE_INTERACCION] [varchar](200) NULL,
	[SUBCLASE_INTERACCION] [varchar](100) NULL,
	[NOMBRE_CONTACO] [varchar](100) NULL,
	[APELLIDO_CONTACTO] [varchar](100) NULL,
	[TELEFONO] [varchar](50) NULL,
	[CICLO_FACTURACION] [varchar](50) NULL,
	[MODALIDAD] [varchar](100) NULL,
	[SERVICIO_AFECTADO] [varchar](100) NULL,
	[INCONVENIENTE] [varchar](200) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Reporte_IVR]    Script Date: 03/26/2018 15:55:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Reporte_IVR](
	[BaseId] [int] NULL,
	[campaniaId] [int] NULL,
	[fechaBase] [varchar](10) NULL,
	[Time Segment] [varchar](50) NULL,
	[USER] [varchar](100) NULL,
	[NOMBRE DEL ASESOR] [varchar](100) NULL,
	[ANY] [varchar](50) NULL,
	[RESPUESTA PREGUNTA 1] [varchar](4) NULL,
	[RESPUESTA PREGUNTA 2] [varchar](4) NULL,
	[TIEMPO MEDIO DE RESPUESTA PREGUNTA 1] [varchar](8) NULL,
	[TIEMPO MEDIO DE RESPUESTA PREGUNTA 2] [varchar](8) NULL,
	[TIEMPO TOTAL DE PERMANENCIA EN EL IVR] [varchar](8) NULL,
	[CORTE DE LLAMADA (ASESOR, IVR O CLIENTE)] [varchar](50) NULL,
	[CANAL] [varchar](10) NULL,
	[BPO (PROVEEDOR)] [varchar](50) NULL,
	[CAMPAÑA] [varchar](100) NULL,
	[SKILL] [varchar](200) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Reporte_CDR]    Script Date: 03/26/2018 15:55:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Reporte_CDR](
	[BaseId] [int] NULL,
	[campaniaId] [int] NULL,
	[fechaBase] [varchar](10) NULL,
	[Campaña] [varchar](100) NULL,
	[BPO] [varchar](50) NULL,
	[Canal] [varchar](50) NULL,
	[MSISDN] [varchar](50) NULL,
	[Fecha] [varchar](50) NULL,
	[Hora] [varchar](50) NULL,
	[Duracion] [varchar](50) NULL,
	[Codigo de skill] [varchar](10) NULL,
	[Nombre de skill] [varchar](100) NULL,
	[Usuario/Agente] [varchar](50) NULL,
	[ID de agente] [varchar](50) NULL,
	[Nombre de agente] [varchar](100) NULL,
	[Cortada por] [varchar](50) NULL,
	[Estado] [varchar](50) NULL,
	[Codigo Transferencia] [varchar](100) NULL,
	[Descripción Transferencia] [varchar](100) NULL,
	[Tipo Transferencia] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Perfil]    Script Date: 03/26/2018 15:55:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Perfil](
	[PerfilId] [int] IDENTITY(1,1) NOT NULL,
	[nombrePerfil] [varchar](11) NULL,
PRIMARY KEY CLUSTERED 
(
	[PerfilId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedTableType [dbo].[IVRType]    Script Date: 03/26/2018 15:55:30 ******/
CREATE TYPE [dbo].[IVRType] AS TABLE(
	[BaseId] [int] NULL,
	[campaniaId] [int] NULL,
	[fechaBase] [varchar](10) NULL,
	[Time Segment] [varchar](50) NULL,
	[USER] [varchar](100) NULL,
	[NOMBRE DEL ASESOR] [varchar](100) NULL,
	[ANY] [varchar](50) NULL,
	[RESPUESTA PREGUNTA 1] [varchar](4) NULL,
	[RESPUESTA PREGUNTA 2] [varchar](4) NULL,
	[TIEMPO MEDIO DE RESPUESTA PREGUNTA 1] [varchar](8) NULL,
	[TIEMPO MEDIO DE RESPUESTA PREGUNTA 2] [varchar](8) NULL,
	[TIEMPO TOTAL DE PERMANENCIA EN EL IVR] [varchar](8) NULL,
	[CORTE DE LLAMADA (ASESOR, IVR O CLIENTE)] [varchar](50) NULL,
	[CANAL] [varchar](10) NULL,
	[BPO (PROVEEDOR)] [varchar](50) NULL,
	[CAMPAÑA] [varchar](100) NULL,
	[SKILL] [varchar](200) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[CDRType]    Script Date: 03/26/2018 15:55:30 ******/
CREATE TYPE [dbo].[CDRType] AS TABLE(
	[BaseId] [int] NULL,
	[campaniaId] [int] NULL,
	[fechaBase] [varchar](10) NULL,
	[Campaña] [varchar](100) NULL,
	[BPO] [varchar](50) NULL,
	[Canal] [varchar](50) NULL,
	[MSISDN] [varchar](50) NULL,
	[Fecha] [varchar](50) NULL,
	[Hora] [varchar](50) NULL,
	[Duracion] [varchar](50) NULL,
	[Codigo de skill] [varchar](10) NULL,
	[Nombre de skill] [varchar](100) NULL,
	[Usuario/Agente] [varchar](50) NULL,
	[ID de agente] [varchar](50) NULL,
	[Nombre de agente] [varchar](100) NULL,
	[Cortada por] [varchar](50) NULL,
	[Estado] [varchar](50) NULL,
	[Codigo Transferencia] [varchar](100) NULL,
	[Descripción Transferencia] [varchar](100) NULL,
	[Tipo Transferencia] [varchar](50) NULL
)
GO
/****** Object:  Table [dbo].[CAMPANIA]    Script Date: 03/26/2018 15:55:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CAMPANIA](
	[idCampania] [int] IDENTITY(1,1) NOT NULL,
	[nombreCampania] [varchar](100) NULL,
	[idSede] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idCampania] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BASE]    Script Date: 03/26/2018 15:55:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BASE](
	[baseId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[campaniaId] [int] NULL,
	[fechaBase] [varchar](10) NULL,
	[fhCreacion] [varchar](19) NULL,
	[nombreArchivo] [varchar](200) NULL,
	[tipo] [varchar](4) NULL,
	[isActive] [bit] NULL,
	[isCompatible] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[baseId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 03/26/2018 15:55:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Roles] [varchar](11) NULL,
	[NombreCompleto] [varchar](100) NULL,
	[idSede] [int] NULL,
	[IsActive] [bit] NULL,
	[firstTime] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[USP_RESETEAR_CONTRASENIA]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[USP_RESETEAR_CONTRASENIA](
	@iduser int
)
AS
BEGIN
	update Usuarios set firstTime=1 ,[Password]=UserName where UserId=@iduser
END
GO
/****** Object:  StoredProcedure [dbo].[USP_REPORTE_CRUCE_DATOS]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[USP_REPORTE_CRUCE_DATOS](
	@campaniaId int,
	@fechaBase varchar(10)
)
AS
BEGIN
	declare @fechaFinal varchar(10) = convert(varchar(10),dateadd(DAY,-1,convert(date,@fechaBase)),120)
	declare @fechaInicial varchar(10) = convert(varchar(10),dateadd(DAY,-15,convert(date,@fechaFinal)),120)
	
	-- DETALLE ------------------------------------------------------------------------------------------------
	SELECT c.[Usuario/Agente],c.Fecha,c.MSISDN,t.TITULO_INTERACCION FROM 
	Reporte_CDR c,Reporte_TIPI t
	where c.Fecha = t.FECHA_DE_CREACION and c.[Usuario/Agente] = t.LOGIN_AGENTE and ('51'+c.MSISDN) = t.TELEFONO
	and c.campaniaId=@campaniaId and c.campaniaId = t.campaniaId and c.fechaBase = t.fechaBase and c.fechaBase between @fechaInicial AND @fechaFinal
	
	-- TOTALES NUMERO ------------------------------------------------------------------------------------------------
	select c.Fecha,c.MSISDN,COUNT(*) TOTAL from 
	Reporte_CDR c,Reporte_TIPI t
	where c.Fecha = t.FECHA_DE_CREACION and c.[Usuario/Agente] = t.LOGIN_AGENTE and ('51'+c.MSISDN) = t.TELEFONO
	and c.campaniaId=@campaniaId and c.campaniaId = t.campaniaId and c.fechaBase = t.fechaBase and c.fechaBase between @fechaInicial AND @fechaFinal
	group by c.Fecha,c.MSISDN
	
	-- TITULO INTERACCION ---------------------------------------------------------------------------------------------
	select t.TITULO_INTERACCION,c.Fecha,c.MSISDN,COUNT(*) TOTAL from 
	Reporte_CDR c,Reporte_TIPI t
	where c.Fecha = t.FECHA_DE_CREACION and c.[Usuario/Agente] = t.LOGIN_AGENTE and ('51'+c.MSISDN) = t.TELEFONO
	and c.campaniaId=@campaniaId and c.campaniaId = t.campaniaId and c.fechaBase = t.fechaBase and c.fechaBase between @fechaInicial AND @fechaFinal
	group by c.Fecha,c.MSISDN,t.TITULO_INTERACCION
	
	-- TOTALES AGENTE --------------------------------------------------------------------------------------------------
	select c.[Usuario/Agente],c.Fecha,c.MSISDN,COUNT(*) TOTAL 
	from Reporte_CDR c,Reporte_TIPI t
	where c.Fecha = t.FECHA_DE_CREACION and c.[Usuario/Agente] = t.LOGIN_AGENTE and ('51'+c.MSISDN) = t.TELEFONO
	and c.campaniaId=@campaniaId and c.campaniaId = t.campaniaId and c.fechaBase = t.fechaBase and c.fechaBase between @fechaInicial AND @fechaFinal
	group by c.Fecha,c.MSISDN,c.[Usuario/Agente]
END
GO
/****** Object:  StoredProcedure [dbo].[USP_REGRESAR_BASE_ANTERIOR]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[USP_REGRESAR_BASE_ANTERIOR](
@campaniaId int,
@fechaBase VARCHAR(10),
@tipo varchar(4),
@baseId INT
)
AS
BEGIN
	update BASE set isCompatible = 0 where baseId = @baseId;
	declare @result varchar(10);
	declare @exist int = (select COUNT(*) from BASE where isActive=1 and campaniaId=@campaniaId and fechaBase=@fechaBase and tipo=@tipo and isCompatible=1);
	if(@exist>0)
	begin
		set @result = 'A'
	end
	else
	begin
		set @result = 'B'
	end
	select @result
END
GO
/****** Object:  StoredProcedure [dbo].[USP_OBTENER_USUARIO]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[USP_OBTENER_USUARIO]
(
@iduser int
)
as
begin
	select top 1 usu.NombreCompleto,s.nombreSede,usu.firstTime from Usuarios usu,Sede s
	where usu.idSede=s.sedeId and usu.UserId=@iduser	
end
GO
/****** Object:  StoredProcedure [dbo].[USP_OBTENER_STATUS]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[USP_OBTENER_STATUS](
 @campaniaId int,
 @fechaBase varchar(10)
)
as
begin
	select fhCreacion,nombreArchivo,tipo from BASE where campaniaId=@campaniaId and fechaBase = @fechaBase and isActive=1
end
GO
/****** Object:  StoredProcedure [dbo].[USP_OBTENER_BASES]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_OBTENER_BASES]
(
	@campaniaId INT,
	@tipo varchar(4),
	@fechaBase VARCHAR(10)
)
AS
BEGIN
	IF(@campaniaId = 0)
	begin
		if(@tipo = '0')
		begin
			select b.baseId,u.NombreCompleto,c.nombreCampania,b.fechaBase,b.fhCreacion,
			b.nombreArchivo,b.tipo,b.isActive,b.isCompatible from 
			BASE b,Usuarios u,CAMPANIA c WHERE fechaBase = @fechaBase and u.UserId = b.userId and b.campaniaId = c.idCampania order by isActive desc,fhCreacion desc
		end
		else
		begin
			select b.baseId,u.NombreCompleto,c.nombreCampania,b.fechaBase,b.fhCreacion,
			b.nombreArchivo,b.tipo,b.isActive,b.isCompatible 
			from BASE b,Usuarios u,CAMPANIA c WHERE tipo = @tipo and fechaBase = @fechaBase and u.UserId = b.userId and b.campaniaId = c.idCampania order by isActive desc,fhCreacion desc
		end
	end
	else
	begin
		if(@tipo = '0')
		begin
			select b.baseId,u.NombreCompleto,c.nombreCampania,b.fechaBase,b.fhCreacion,
			b.nombreArchivo,b.tipo,b.isActive,b.isCompatible  
			from BASE b,Usuarios u,CAMPANIA c  WHERE campaniaId=@campaniaId and fechaBase = @fechaBase and u.UserId = b.userId and b.campaniaId = c.idCampania order by isActive desc,fhCreacion desc
		end
		else
		begin
			select b.baseId,u.NombreCompleto,c.nombreCampania,b.fechaBase,b.fhCreacion,
			b.nombreArchivo,b.tipo,b.isActive,b.isCompatible 
			from BASE b,Usuarios u,CAMPANIA c  WHERE campaniaId=@campaniaId and tipo = @tipo and fechaBase = @fechaBase and u.UserId = b.userId and b.campaniaId = c.idCampania order by isActive desc,fhCreacion desc
		end
	end
END
GO
/****** Object:  StoredProcedure [dbo].[USP_LISTAR_USUARIOS]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_LISTAR_USUARIOS]
(
@sedeId int
)
as
begin
	select UserId,UserName,Roles,NombreCompleto,IsActive,firstTime from Usuarios where idSede=@sedeId;
end
GO
/****** Object:  StoredProcedure [dbo].[USP_LISTAR_CAMPANIAS]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[USP_LISTAR_CAMPANIAS]
(
	@idSede int
)
AS
BEGIN
	SELECT * FROM CAMPANIA WHERE idSede = @idSede;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GUARDAR_BASE]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[USP_GUARDAR_BASE]
(
 @userId int,
 @archivo varchar(200),
 @tipo varchar(4),
 @campaniaId int,
 @fechaBase varchar(10)
)
as
BEGIN
	insert into BASE(userId,campaniaId,fechaBase,fhCreacion,nombreArchivo,tipo,isActive,isCompatible) values
	(@userId,@campaniaId,@fechaBase,CONVERT(varchar(19),GETDATE(),120),@archivo,@tipo,0,1)
	declare @id int= @@IDENTITY;
	select @id
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_BASE_CARGADA]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[USP_GET_BASE_CARGADA](
@campaniaId int,
@tipo varchar(4),
@fechaBase varchar(10)
)
AS
BEGIN
	select fhCreacion from BASE WHERE campaniaId=@campaniaId and tipo=@tipo and fechaBase=@fechaBase and isActive=1;
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CREAR_USUARIO]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[USP_CREAR_USUARIO]
(
@username varchar(50),
@roles varchar(11),
@nombreCompleto varchar(100)
)
as
begin
	insert into Usuarios(UserName,[Password],Roles,NombreCompleto,idSede,IsActive,firstTime) values
	(@username,@username,@roles,@nombreCompleto,1,1,1);
	declare @idUser int = @@IDENTITY;
	select UserId,UserName,Roles,NombreCompleto,IsActive,firstTime from Usuarios where UserId=@idUser
end
GO
/****** Object:  StoredProcedure [dbo].[USP_CREAR_CAMPANIA]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[USP_CREAR_CAMPANIA](
	@nombreCampania varchar(100),
	@idSede int
)
as
BEGIN
	DECLARE @CONT INT = (SELECT COUNT(*) FROM CAMPANIA WHERE nombreCampania=@nombreCampania and idSede=@idSede)
	if(@CONT=0)
	begin
			INSERT INTO CAMPANIA (nombreCampania,idSede) VALUES (@nombreCampania,@idSede)
			declare @idCampania int= @@IDENTITY;
			SELECT * FROM CAMPANIA WHERE idCampania = @idCampania
	end
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CHECK_LOGIN]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[USP_CHECK_LOGIN]
(
	@userName varchar(50),
	@password varchar(50)
)AS
BEGIN
	select top 1 usu.UserId,usu.UserName,usu.Roles,s.sedeId from Usuarios usu,Sede s
	where usu.idSede=s.sedeId and UserName=@userName and [Password]=@password and usu.IsActive=1
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CARGAR_TABLA_TIPI]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[USP_CARGAR_TABLA_TIPI]
(
	@tabla TIPIType READONLY,
	@campaniaId int,
	@fechaBase VARCHAR(10),
	@baseId int
)
as
begin
	delete t from REPORTE_TIPI t inner join BASE b on b.isActive=1 and b.tipo='TIPI' and b.campaniaId = @campaniaId  and b.fechaBase = @fechaBase and t.BaseId = b.baseId
	update BASE set isActive=0 where isActive=1 and tipo='TIPI' and campaniaId=@campaniaId and fechaBase=@fechaBase
	update BASE set isActive=1 where baseId = @baseId;
	insert into Reporte_TIPI select * from @tabla
	select COUNT(*) from Reporte_TIPI where baseId = @baseId;
end
GO
/****** Object:  StoredProcedure [dbo].[USP_CARGAR_TABLA_IVR]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[USP_CARGAR_TABLA_IVR]
(
	@tabla IVRType READONLY,
	@campaniaId int,
	@fechaBase VARCHAR(10),
	@baseId int
)
as
begin
	delete t from REPORTE_IVR t inner join BASE b on b.isActive=1 and b.tipo='IVR' and b.campaniaId = @campaniaId  and b.fechaBase = @fechaBase and t.BaseId = b.baseId
	update BASE set isActive=0 where isActive=1 and tipo='IVR' and campaniaId=@campaniaId and fechaBase=@fechaBase
	update BASE set isActive=1 where baseId = @baseId;
	insert into Reporte_IVR select * from @tabla
	select COUNT(*) from REPORTE_IVR where baseId = @baseId;
end
GO
/****** Object:  StoredProcedure [dbo].[USP_CARGAR_TABLA_CDR]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[USP_CARGAR_TABLA_CDR]
(
	@tabla CDRType READONLY,
	@campaniaId int,
	@fechaBase VARCHAR(10),
	@baseId int
)
as
begin
	delete t from REPORTE_CDR t inner join BASE b on b.isActive=1 and b.tipo='CDR' and b.campaniaId = @campaniaId  and b.fechaBase = @fechaBase and t.BaseId = b.baseId
	update BASE set isActive=0 where isActive=1 and tipo='CDR' and campaniaId=@campaniaId and fechaBase=@fechaBase
	update BASE set isActive=1 where baseId = @baseId;
	insert into Reporte_CDR select * from @tabla
	select COUNT(*) from REPORTE_CDR where baseId = @baseId;
end
GO
/****** Object:  StoredProcedure [dbo].[USP_CAMBIAR_CONTRASENIA]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CAMBIAR_CONTRASENIA]
(
@iduser int,
@password varchar(50)
)
as
begin
	update Usuarios set [Password]=@password,firstTime=0 where UserId=@iduser
end
GO
/****** Object:  StoredProcedure [dbo].[USP_ACTUALIZAR_USUARIO]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[USP_ACTUALIZAR_USUARIO]
(
@userId int,
@username varchar(50),
@roles varchar(11),
@nombreCompleto varchar(100)
)
as
begin
	update Usuarios set UserName=@username, Roles=@roles, NombreCompleto=@nombreCompleto
	where UserId=@userId
	
	select UserId,UserName,Roles,NombreCompleto,IsActive,firstTime from Usuarios where UserId=@userId
end
GO
/****** Object:  StoredProcedure [dbo].[USP_ACTUALIZAR_ESTADO_USUARIO]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[USP_ACTUALIZAR_ESTADO_USUARIO]
(
@iduser int,
@estado bit
)
as
begin
update Usuarios set IsActive=@estado where UserId=@iduser
end
GO
/****** Object:  StoredProcedure [dbo].[USP_ACTUALIZAR_CAMPANIA]    Script Date: 03/26/2018 15:55:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[USP_ACTUALIZAR_CAMPANIA](
	@nombreCampania varchar(100),
	@idSede int,
	@idCampania INT
)
as
begin
	DECLARE @CONT INT = (SELECT COUNT(*) FROM CAMPANIA WHERE nombreCampania=@nombreCampania and idSede=@idSede)
	if(@CONT=0)
	begin
		update CAMPANIA SET nombreCampania=@nombreCampania where idCampania = @idCampania
		SELECT * FROM CAMPANIA WHERE idCampania = @idCampania
	end
end
GO
