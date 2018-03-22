use REPEAT_CALLER;

create table Usuarios(
	UserId int primary key identity(1, 1),  
    UserName varchar(50),  
	[Password] varchar(50),
    Roles varchar(11),
    NombreCompleto varchar(100),
    idSede int,  
    IsActive bit, -- 1:active,0:notactive
    firstTime bit
)

create table Sede(
	sedeId int primary key identity(1, 1),
	nombreSede varchar(30)
)
insert into Sede(nombreSede) values ('COLONIAL')



insert into Usuarios(UserName,[Password],Roles,NombreCompleto,idSede,IsActive,firstTime)
values ('admin','123456','Ejecutivo','admin admin, admin',1,1,0)

select * from Usuarios
select * from Sede

alter procedure USP_CHECK_LOGIN
(
	@userName varchar(50),
	@password varchar(50)
)AS
BEGIN
	select top 1 usu.UserId,usu.UserName,usu.Roles,s.sedeId from Usuarios usu,Sede s
	where usu.idSede=s.sedeId and UserName=@userName and [Password]=@password and usu.IsActive=1
END

exec USP_CHECK_LOGIN 'admin','123456'

alter procedure USP_OBTENER_USUARIO
(
@iduser int
)
as
begin
	select top 1 usu.NombreCompleto,s.nombreSede,usu.firstTime from Usuarios usu,Sede s
	where usu.idSede=s.sedeId and usu.UserId=@iduser	
end

CREATE PROCEDURE USP_CAMBIAR_CONTRASENIA
(
@iduser int,
@password varchar(50)
)
as
begin
	update Usuarios set [Password]=@password,firstTime=0 where UserId=@iduser
end	

alter PROCEDURE USP_LISTAR_USUARIOS
(
@sedeId int
)
as
begin
	select UserId,UserName,Roles,NombreCompleto,IsActive,firstTime from Usuarios where idSede=@sedeId;
end

exec USP_LISTAR_USUARIOS 1

alter procedure USP_CREAR_USUARIO
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

create procedure USP_ACTUALIZAR_USUARIO
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

create procedure USP_ACTUALIZAR_ESTADO_USUARIO
(
@iduser int,
@estado bit
)
as
begin
update Usuarios set IsActive=@estado where UserId=@iduser
end

create procedure USP_RESETEAR_CONTRASENIA(
	@iduser int
)
AS
BEGIN
	update Usuarios set firstTime=1 ,[Password]=UserName where UserId=@iduser
END

create table CAMPANIA
(
	idCampania int primary key identity(1, 1),
	nombreCampania varchar(100),
	idSede int
)

create procedure USP_CREAR_CAMPANIA(
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

create procedure USP_LISTAR_CAMPANIAS
(
	@idSede int
)
AS
BEGIN
	SELECT * FROM CAMPANIA WHERE idSede = @idSede;
END

create procedure USP_ACTUALIZAR_CAMPANIA(
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

select * from CAMPANIA

create table Reporte_CDR(
	BaseId INT,
	Campaña varchar(100),
	BPO varchar(50),
	Canal varchar(50),
	MSISDN varchar(50),
	Fecha varchar(50),
	Hora varchar(50),
	Duracion varchar(50),
	[Codigo de skill] varchar(10),
	[Nombre de skill] varchar(100),
	[Usuario/Agente] varchar(50),
	[ID de agente] varchar(50),
	[Nombre de agente] varchar(100),
	[Cortada por] varchar(50),
	Estado varchar(50),
	[Codigo Transferencia] varchar(100),
	[Descripción Transferencia] varchar(100),
	[Tipo Transferencia] varchar(50)
)

create table BASE(
baseId int primary key identity(1, 1),
userId int,
campaniaId int,
fechaBase varchar(10),
fhCreacion varchar(19),
nombreArchivo varchar(200),
tipo varchar(4),
isActive bit,
isCompatible bit
)

alter procedure USP_GUARDAR_BASE
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
exec USP_GUARDAR_BASE 1,'EJEM','CDR',1,'23232'
select * from base

create procedure USP_GET_BASE_CARGADA(
@campaniaId int,
@tipo varchar(4),
@fechaBase varchar(10)
)
AS
BEGIN
	select fhCreacion from BASE WHERE campaniaId=@campaniaId and tipo=@tipo and fechaBase=@fechaBase and isActive=1;
END

CREATE TYPE CDRType AS TABLE 
(
	BaseId INT,
    Campaña varchar(100),
	BPO varchar(50),
	Canal varchar(50),
	MSISDN varchar(50),
	Fecha varchar(50),
	Hora varchar(50),
	Duracion varchar(50),
	[Codigo de skill] varchar(10),
	[Nombre de skill] varchar(100),
	[Usuario/Agente] varchar(50),
	[ID de agente] varchar(50),
	[Nombre de agente] varchar(100),
	[Cortada por] varchar(50),
	Estado varchar(50),
	[Codigo Transferencia] varchar(100),
	[Descripción Transferencia] varchar(100),
	[Tipo Transferencia] varchar(50)
)

alter procedure USP_CARGAR_TABLA_CDR
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
select *from base
select * from REPORTE_TIPI

create table Reporte_IVR(
	BaseId INT,
 [Time Segment] VARCHAR(50),
 [USER] varchar(100),
 [NOMBRE DEL ASESOR] varchar(100),
[ANY] varchar(50),
[RESPUESTA PREGUNTA 1] varchar(4),
[RESPUESTA PREGUNTA 2] varchar(4),
[TIEMPO MEDIO DE RESPUESTA PREGUNTA 1] varchar(8),
[TIEMPO MEDIO DE RESPUESTA PREGUNTA 2] varchar(8),
[TIEMPO TOTAL DE PERMANENCIA EN EL IVR] varchar(8),
[CORTE DE LLAMADA (ASESOR, IVR O CLIENTE)] varchar(50),
[CANAL] varchar(10),
[BPO (PROVEEDOR)] varchar(50),
[CAMPAÑA] varchar(100),
[SKILL] varchar(200)
)
drop TYPE IVRType
CREATE TYPE IVRType AS TABLE 
(
BaseId INT,
[Time Segment] VARCHAR(50),
 [USER] varchar(100),
 [NOMBRE DEL ASESOR] varchar(100),
[ANY] varchar(50),
[RESPUESTA PREGUNTA 1] varchar(4),
[RESPUESTA PREGUNTA 2] varchar(4),
[TIEMPO MEDIO DE RESPUESTA PREGUNTA 1] varchar(8),
[TIEMPO MEDIO DE RESPUESTA PREGUNTA 2] varchar(8),
[TIEMPO TOTAL DE PERMANENCIA EN EL IVR] varchar(8),
[CORTE DE LLAMADA (ASESOR, IVR O CLIENTE)] varchar(50),
[CANAL] varchar(10),
[BPO (PROVEEDOR)] varchar(50),
[CAMPAÑA] varchar(100),
[SKILL] varchar(200)
)

alter procedure USP_CARGAR_TABLA_IVR
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

CREATE TABLE Reporte_TIPI(
BaseId INT,
	FECHA_CREACION varchar(50),
	FECHA_DE_CREACION varchar(50),
	TIPO_LOGIN varchar(10),
	LOGIN_AGENTE varchar(50),
	[NOMBRE DE ASESOR] varchar(200),
	ID_INTERACCION varchar(50),
	TITULO_INTERACCION varchar(200),
	TIPO_INTERACCION varchar(100),
	CLASE_INTERACCION varchar(200),
	SUBCLASE_INTERACCION varchar(100),
	NOMBRE_CONTACO varchar(100),
	APELLIDO_CONTACTO varchar(100),
	TELEFONO varchar(50),
	CICLO_FACTURACION varchar(50),
	MODALIDAD varchar(100),
	SERVICIO_AFECTADO varchar(100),
	INCONVENIENTE varchar(200)
)

CREATE TYPE TIPIType AS TABLE 
(
BaseId INT,
FECHA_CREACION varchar(50),
	FECHA_DE_CREACION varchar(50),
	TIPO_LOGIN varchar(10),
	LOGIN_AGENTE varchar(50),
	[NOMBRE DE ASESOR] varchar(200),
	ID_INTERACCION varchar(50),
	TITULO_INTERACCION varchar(200),
	TIPO_INTERACCION varchar(100),
	CLASE_INTERACCION varchar(200),
	SUBCLASE_INTERACCION varchar(100),
	NOMBRE_CONTACO varchar(100),
	APELLIDO_CONTACTO varchar(100),
	TELEFONO varchar(50),
	CICLO_FACTURACION varchar(50),
	MODALIDAD varchar(100),
	SERVICIO_AFECTADO varchar(100),
	INCONVENIENTE varchar(200)
)

ALTER procedure USP_CARGAR_TABLA_TIPI
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

select * from Reporte_CDR
SELECT * FROM BASE where fechaBase = '2018-03-21'
select baseId from BASE b where (select b.campaniaId from BASE b ,@tabla t where b.baseId = t.BaseId)
select baseId from BASE b where b.campaniaId = (select bb.campaniaId from @tabla t,BASE bb where bb.baseId = t.baseId)

alter procedure USP_REGRESAR_BASE_ANTERIOR(
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

SELECT * FROM BASE

alter PROCEDURE USP_OBTENER_BASES
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

exec USP_OBTENER_BASES 0,'0','2018-03-21'
select * from Reporte_CDR
select * from Reporte_TIPI
SELECT c.[Usuario/Agente],c.Fecha,c.MSISDN,t.TITULO_INTERACCION FROM Reporte_CDR c,Reporte_TIPI t where 
c.Fecha = t.FECHA_DE_CREACION and c.[Usuario/Agente] = t.LOGIN_AGENTE and ('51'+c.MSISDN) = t.TELEFONO

