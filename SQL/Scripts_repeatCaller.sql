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


)