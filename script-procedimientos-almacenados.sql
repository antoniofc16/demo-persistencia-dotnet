use OrdenesDotnet
go

create or alter proc SP_ConsultarCategorias(
	@activo bit = 1	
)
as
begin
	select
	idCategoria,
	nombreCategoria,
	activo
	from Categorias
	where activo = @activo
end
go

create or alter proc SP_RegistrarCategoria(
	@nombreCategoria nvarchar(150)
)
as
begin
	set nocount on

	insert into Categorias (nombreCategoria, fechaRegistro) values (@nombreCategoria, GETUTCDATE())

	select CAST(SCOPE_IDENTITY() as int)
end
go

create or alter proc SP_ActualizarCategoria(
	@idCategoria int,
	@nombreCategoria nvarchar(150),
	@activo bit
)
as
begin
	set nocount on

	update Categorias 
	set nombreCategoria = @nombreCategoria,
		activo = @activo,
		fechaActualizacion = GETUTCDATE()
	where idCategoria = @idCategoria

	select @idCategoria
end
go

create or alter proc SP_EliminarCategoria(
	@idCategoria int
)
as
begin
	set nocount on

	if exists (
        select 1 from Productos where idCategoria = @idCategoria
    )
        throw 50001, 'La categoria tiene productos asociados.', 1;

	delete from Categorias 
	where idCategoria = @idCategoria

	select 1
end
go

create or alter proc SP_ConsultarProductos(
	@activo bit = 1
)
as
begin
	select
	idProducto,
	nombreProducto,
	precioProducto,
	stock,
	idCategoria,
	activo
	from Productos
	where activo = @activo
end
go

create or alter proc SP_RegistrarProducto(
	@nombreProducto nvarchar(150),
	@precioProducto decimal(18,2),
	@stock int,
	@idCategoria int
)
as
begin
	set nocount on

	insert into Productos (nombreProducto, precioProducto, stock, idCategoria, fechaRegistro) values (@nombreProducto, @precioProducto, @stock, @idCategoria, GETUTCDATE())
	
	select CAST(SCOPE_IDENTITY() as int)
end
go

create or alter proc SP_ActualizarProducto(
	@idProducto int,
	@nombreProducto nvarchar(150),
	@precioProducto decimal(18,2),
	@stock int,
	@idCategoria int,
	@activo bit
)
as
begin
	set nocount on

	update Productos 
	set	nombreProducto = @nombreProducto,
		precioProducto = @precioProducto,
		stock = @stock,
		idCategoria = @idCategoria,
		activo = @activo,
		fechaActualizacion = GETUTCDATE()
	where idProducto = @idProducto
	
	select @idProducto
end
go

create or alter proc SP_EliminarProducto(
	@idProducto int
)
as
begin
	set nocount on

	if exists (
        select 1 from OrdenDetalle where idProducto = @idProducto
    )
        throw 50001, 'El producto tiene ordenes asociadas.', 1;

	delete from Productos 
	where idProducto = @idProducto

	select 1
end
go

create or alter proc SP_ConsultaClientes(
	@activo bit = 1
)
as
begin
	select
	idCliente,
	nombres,
	apellidoPaterno,
	apellidoMaterno,
	correoElectronico,
	telefono,
	direccion,
	fechaNacimiento,
	activo
	from Clientes
	where activo = @activo
end
go


create or alter proc SP_RegistraCliente(
	@nombres nvarchar(150),
	@apellidoPaterno nvarchar(100),
	@apellidoMaterno nvarchar(100),
	@correoElectronico nvarchar(100),
	@telefono nvarchar(100),
	@direccion nvarchar(200),
	@fechaNacimiento datetime
)
as
begin
	set nocount on

	insert into Clientes (nombres, apellidoPaterno, apellidoMaterno, correoElectronico, telefono, direccion, fechaNacimiento, fechaRegistro)
	values (@nombres, @apellidoPaterno, @apellidoMaterno, @correoElectronico, @telefono, @direccion, @fechaNacimiento, GETUTCDATE())
	
	select CAST(SCOPE_IDENTITY() as int)
end
go

create or alter proc SP_ActualizaCliente(
	@idCliente int,
	@nombres nvarchar(150),
	@apellidoPaterno nvarchar(100),
	@apellidoMaterno nvarchar(100),
	@correoElectronico nvarchar(100),
	@telefono nvarchar(100),
	@direccion nvarchar(200),
	@fechaNacimiento datetime,
	@activo bit
)
as
begin
	set nocount on

	update Clientes 
	set nombres = @nombres, 
		apellidoPaterno = @apellidoPaterno, 
		apellidoMaterno = @apellidoMaterno, 
		correoElectronico = @correoElectronico, 
		telefono = @telefono, 
		direccion = @direccion, 
		fechaNacimiento = @fechaNacimiento, 
		fechaActualizacion = GETUTCDATE(),
		activo = @activo
	where idCliente = @idCliente
	
	select @idCliente
end
go

create or alter proc SP_EliminarCliente(
	@idCliente int
)
as
begin
	set nocount on

	if exists (
        select 1 from Ordenes where idCliente = @idCliente
    )
        throw 50001, 'El cliente tiene ordenes registradas.', 1;

	delete from Clientes
	where idCliente = @idCliente

	select 1
end
go

create or alter proc SP_RegistrarOrden(
	@idCliente int
)
as
begin
	set nocount on

	insert into Ordenes (idCliente,fechaRegistro)
	values (@idCliente, GETUTCDATE())
	
	select CAST(SCOPE_IDENTITY() as int)
end
go

create or alter proc SP_RegistrarOrdenDetalle(
	@idOrden int,
	@idProducto int,
	@precioUnitario decimal(18,2),
	@cantidad int
)
as
begin
	set nocount on


	if not exists (
		select 1 from OrdenDetalle where idOrden =  @idOrden and idProducto = @idProducto
	)
	begin
		insert into OrdenDetalle (idOrden,idProducto,cantidad,precioUnitario)
		values (@idOrden,@idProducto,@cantidad,@precioUnitario)
	end
	else
	begin
		update OrdenDetalle
		set cantidad = @cantidad
		where idOrden = @idOrden and idProducto = @idProducto
	end
	
	select @idOrden
end
go

create or alter proc SP_ObtenerOrden(
	@idOrden int
)
as
begin
	select
	idOrden,
	o.idCliente,
	o.fechaRegistro,
	nombres,
	apellidoPaterno,
	apellidoMaterno
	from Ordenes o
	join Clientes c on o.idCliente = c.idCliente
	where idOrden = @idOrden
end
go

create or alter proc SP_ObtenerOrdenDetalle(
	@idOrden int
)
as
begin
	select
	idOrdenDetalle,
	od.idProducto,
	cantidad,
	precioUnitario,
	nombreProducto,
	nombreCategoria
	from OrdenDetalle od
	join Productos p on od.idProducto = p.idProducto
	join Categorias c on c.idCategoria = p.idCategoria
	where idOrden = @idOrden
end
go