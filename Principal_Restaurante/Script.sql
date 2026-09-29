CREATE DATABASE  Restaurante3
GO
USE Restaurante3
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

-- 1. Tablas independientes (sin claves foráneas)
INSERT INTO [TipoEmpleados] ([Nombre], [SalarioBase]) VALUES
('Administrador', 2500000.00),
('Cajero', 1300000.00),
('Mesero', 1300000.00);

INSERT INTO [Sucursales] ([Nombre], [Direccion], [Telefono], [Ciudad]) VALUES
('Sucursal Central', 'Calle 50 # 45-10', '6044445566', 'Medellín'),
('Sucursal Poblado', 'Carrera 43A # 10-20', '6043332211', 'Medellín');

INSERT INTO [Clientes] ([Nombre], [Cedula], [Telefono], [Email]) VALUES
('Carlos Mendoza', '1017123456', '3001234567', 'carlos.mendoza@email.com'),
('Ana María Gómez', '1020987654', '3109876543', 'ana.gomez@email.com');

INSERT INTO [Categorias] ([Nombre], [Descripcion], [Disponible], [OrdenMenu]) VALUES
('Bebidas', 'Refrescos, jugos y bebidas calientes', 1, 1),
('Platos Fuertes', 'Especialidades de la casa y carnes', 1, 2);

INSERT INTO [Mesas] ([Numero], [Capacidad], [Estado], [Ubicacion]) VALUES
(1, 4, 1, 'Terraza'),
(2, 2, 1, 'Salón Principal');

INSERT INTO [MetodoPagos] ([Tipo], [Descripcion]) VALUES
('Efectivo', 'Pago en billetes/monedas'),
('Tarjeta', 'Tarjeta de crédito o débito');

INSERT INTO [Proveedores] ([Nombre], [Telefono], [Email], [Direccion]) VALUES
('Distribuidora Avícola S.A.', '3120001122', 'ventas@avicola.com', 'Carrera 65 # 30-12'),
('Lácteos del Campo', '3159998877', 'contacto@lacteos.com', 'Calle 10 # 20-30');

-- 2. Tablas dependientes de nivel 1
INSERT INTO [Empleados] ([Nombre], [Cedula], [Telefono], [FechaContratacion], [TipoEmpleado], [Sucursal]) VALUES
('Santiago Serna', '35456', '232131', '2026-01-15 08:00', 1, 1),
('Laura Restrepo', '1035444333', '3015554433', '2026-02-01 08:00', 3, 1);

INSERT INTO [Productos] ([Nombre], [Descripcion], [Precio], [Disponible], [Categoria]) VALUES
('Hamburguesa Artesanal', 'Carne 200g, queso cheddar y tocineta', 28000.00, 1, 2),
('Jugo Natural de Lulo', 'En agua o leche 500ml', 8000.00, 1, 1);

INSERT INTO [Ingredientes] ([Nombre], [UniMedida], [CostoUni], [Proveedor]) VALUES
('Carne Molida Premium', 'Kg', 18000.00, 1),
('Queso Cheddar', 'Kg', 22000.00, 2);

INSERT INTO [Compras] ([Proveedor], [Fecha], [Total], [Estado]) VALUES
(1, '2026-03-01 09:00', 180000.00, 'Completado');

-- 3. Tablas dependientes de nivel 2
INSERT INTO [Pedidos] ([Fecha], [Cliente], [Mesa], [Empleado], [Estado]) VALUES
('2026-03-29 12:30', 1, 1, 2, 'Atendido');

INSERT INTO [Reservas] ([Cliente], [Mesa], [FechaHora], [NPersonas], [Estado]) VALUES
(2, 2, '2026-03-30 19:00', 2, 'Confirmada');

INSERT INTO [RecetaIngredientes] ([Producto], [Ingrediente], [CantiReque], [Notas]) VALUES
(1, 1, 0.20, 'Porción de carne por hamburguesa'),
(1, 2, 0.05, 'Una tajada de queso cheddar');

INSERT INTO [Inventarios] ([Ingrediente], [CantDisponible], [CantMinima], [UltimaActu]) VALUES
(1, 15.50, 5.00, '2026-03-29 08:00'),
(2, 8.00, 2.00, '2026-03-29 08:00');

INSERT INTO [DetalleCompras] ([Ingrediente], [Compra], [Cantidad], [PrecioUni]) VALUES
(1, 1, 10.00, 18000.00);

INSERT INTO [Turnos] ([Empleado], [FechaInicio], [FechaFin], [Nombre]) VALUES
(1, '2026-03-29 08:00', '2026-03-29 16:00', 'Turno Mañana'),
(2, '2026-03-29 12:00', '2026-03-29 20:00', 'Turno Tarde');

-- 4. Tablas dependientes de nivel 3
INSERT INTO [DetallePedidos] ([Pedido], [Producto], [Cantidad], [PrecioUni]) VALUES
(1, 1, 2, 28000.00),
(1, 2, 2, 8000.00);

INSERT INTO [Facturas] ([Pedido], [FechaEmi], [Subtotal], [Impuesto], [Total], [MetodoPago]) VALUES
(1, '2026-03-29 13:15', 62000.00, 11160.00, 73160.00, 2);

INSERT INTO [Resenas] ([Cliente], [Producto], [Calificacion], [Comentario], [Fecha]) VALUES
(1, 1, 5, 'Excelente sabor y cocción de la carne.', '2026-03-29 14:00');

 