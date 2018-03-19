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