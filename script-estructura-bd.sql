use master
go

if DB_ID('OrdenesDotnet') is null
create database OrdenesDotnet
go

use OrdenesDotnet
go

if OBJECT_ID('Categorias') is null
create table Categorias(
	idCategoria int identity(1,1) primary key,
	nombreCategoria nvarchar(150),
	activo bit default(1),
	fechaRegistro datetime,
	fechaActualizacion datetime null
)
go

if OBJECT_ID('Productos') is null
create table Productos(
	idProducto int identity(1,1) primary key,
	nombreProducto nvarchar(150),
	precioProducto decimal(18,2),
	idCategoria int references Categorias(idCategoria),
	stock int,
	activo bit default(1),
	fechaRegistro datetime,
	fechaActualizacion datetime null
)
go

if OBJECT_ID('Clientes') is null
create table Clientes(
	idCliente int identity(1,1) primary key,
	nombres nvarchar(150),
	apellidoPaterno nvarchar(100),
	apellidoMaterno nvarchar(100),
	correoElectronico nvarchar(100),
	telefono nvarchar(100),
	direccion nvarchar(200),
	fechaNacimiento datetime,
	activo bit default(1),
	fechaRegistro datetime,
	fechaActualizacion datetime null
)
go

if OBJECT_ID('Ordenes') is null
create table Ordenes(
	idOrden int identity(1,1) primary key,
	idCliente int references Clientes(idCliente),
	fechaRegistro datetime
)
go

if OBJECT_ID('OrdenDetalle') is null
create table OrdenDetalle(
	idOrdenDetalle int identity(1,1) primary key,
	idOrden int references Ordenes(idOrden),
	idProducto int references Productos(idProducto),
	cantidad int,
	precioUnitario decimal(18,2)
)
go
