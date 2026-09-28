CREATE DATABASE  Restaurante
GO
USE Restaurante
GO

CREATE TABLE [TipoEmpleados] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Nombre] NVARCHAR(100) NOT NULL,
    [SalarioBase] DECIMAL(10,2) NOT NULL
);

CREATE TABLE [Sucursales] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Nombre] NVARCHAR(100) NOT NULL,
    [Direccion] NVARCHAR(200) NULL,
    [Telefono] NVARCHAR(20) NULL,
    [Ciudad] NVARCHAR(100) NULL
);

CREATE TABLE [Empleados] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Nombre] NVARCHAR(100) NOT NULL,
    [Cedula] NVARCHAR(20) NOT NULL,
    [Telefono] NVARCHAR(20) NULL,
    [FechaContratacion] SMALLDATETIME NOT NULL,
    [TipoEmpleado] INT REFERENCES [TipoEmpleados]([Id]) NOT NULL,
    [Sucursal] INT REFERENCES [Sucursales]([Id]) NOT NULL
);

CREATE TABLE [Clientes] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Nombre] NVARCHAR(100) NOT NULL,
    [Cedula] NVARCHAR(20) NULL,
    [Telefono] NVARCHAR(20) NULL,
    [Email] NVARCHAR(100) NULL
);

CREATE TABLE [Categorias] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Nombre] NVARCHAR(100) NOT NULL,
    [Descripcion] NVARCHAR(250) NULL,
    [Disponible] BIT NOT NULL,
    [OrdenMenu] INT NOT NULL
);

CREATE TABLE [Productos] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Nombre] NVARCHAR(100) NOT NULL,
    [Descripcion] NVARCHAR(250) NULL,
    [Precio] DECIMAL(10,2) NOT NULL,
    [Disponible] BIT NOT NULL,
    [Categoria] INT REFERENCES [Categorias]([Id]) NOT NULL
);

CREATE TABLE [Mesas] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Numero] INT NOT NULL,
    [Capacidad] INT NOT NULL,
    [Estado] BIT NOT NULL,
    [Ubicacion] NVARCHAR(100) NULL
);

CREATE TABLE [Pedidos] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Fecha] SMALLDATETIME NOT NULL,
    [Cliente] INT REFERENCES [Clientes]([Id]) NOT NULL,
    [Mesa] INT REFERENCES [Mesas]([Id]) NOT NULL,
    [Empleado] INT REFERENCES [Empleados]([Id]) NOT NULL,
    [Estado] NVARCHAR(50) NOT NULL
);

CREATE TABLE [DetallePedidos] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Pedido] INT REFERENCES [Pedidos]([Id]) NOT NULL,
    [Producto] INT REFERENCES [Productos]([Id]) NOT NULL,
    [Cantidad] INT NOT NULL,
    [PrecioUni] DECIMAL(10,2) NOT NULL
);

CREATE TABLE [MetodoPagos] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Tipo] NVARCHAR(50) NOT NULL,
    [Descripcion] NVARCHAR(150) NULL
);

CREATE TABLE [Facturas] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Pedido] INT REFERENCES [Pedidos]([Id]) NOT NULL,
    [FechaEmi] SMALLDATETIME NOT NULL,
    [Subtotal] DECIMAL(10,2) NOT NULL,
    [Impuesto] DECIMAL(10,2) NOT NULL,
    [Total] DECIMAL(10,2) NOT NULL,
    [MetodoPago] INT REFERENCES [MetodoPagos]([Id]) NOT NULL
);

CREATE TABLE [Reservas] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Cliente] INT REFERENCES [Clientes]([Id]) NOT NULL,
    [Mesa] INT REFERENCES [Mesas]([Id]) NOT NULL,
    [FechaHora] SMALLDATETIME NOT NULL,
    [NPersonas] INT NOT NULL,
    [Estado] NVARCHAR(20) NOT NULL
);

CREATE TABLE [Proveedores] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Nombre] NVARCHAR(100) NOT NULL,
    [Telefono] NVARCHAR(20) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [Direccion] NVARCHAR(150) NOT NULL
);

CREATE TABLE [Ingredientes] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Nombre] NVARCHAR(100) NOT NULL,
    [UniMedida] NVARCHAR(20) NOT NULL,
    [CostoUni] DECIMAL(10,2) NOT NULL,
    [Proveedor] INT REFERENCES [Proveedores]([Id]) NOT NULL
);

CREATE TABLE [RecetaIngredientes] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Producto] INT REFERENCES [Productos]([Id]) NOT NULL,
    [Ingrediente] INT REFERENCES [Ingredientes]([Id]) NOT NULL,
    [CantiReque] DECIMAL(10,2) NOT NULL,
    [Notas] NVARCHAR(250) NULL
);

CREATE TABLE [Inventarios] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Ingrediente] INT REFERENCES [Ingredientes]([Id]) NOT NULL,
    [CantDisponible] DECIMAL(10,2) NOT NULL,
    [CantMinima] DECIMAL(10,2) NOT NULL,
    [UltimaActu] SMALLDATETIME NOT NULL
);

CREATE TABLE [Compras] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Proveedor] INT REFERENCES [Proveedores]([Id]) NOT NULL,
    [Fecha] SMALLDATETIME NOT NULL,
    [Total] DECIMAL(10,2) NOT NULL,
    [Estado] NVARCHAR(50) NOT NULL
);

CREATE TABLE [DetalleCompras] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Ingrediente] INT REFERENCES [Ingredientes]([Id]) NOT NULL,
    [Compra] INT REFERENCES [Compras]([Id]) NOT NULL,
    [Cantidad] DECIMAL(10,2) NOT NULL,
    [PrecioUni] DECIMAL(10,2) NOT NULL
);

CREATE TABLE [Turnos] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Empleado] INT REFERENCES [Empleados]([Id]) NOT NULL,
    [FechaInicio] SMALLDATETIME NOT NULL,
    [FechaFin] SMALLDATETIME NOT NULL,
    [Nombre] NVARCHAR(50) NULL
);

CREATE TABLE [Resenas] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Cliente] INT REFERENCES [Clientes]([Id]) NOT NULL,
    [Producto] INT REFERENCES [Productos]([Id]) NOT NULL,
    [Calificacion] INT NOT NULL,
    [Comentario] NVARCHAR(500) NULL,
    [Fecha] SMALLDATETIME NOT NULL
);



 