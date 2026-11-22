
CREATE DATABASE  Restaurante

GO
USE Restaurante
GO
CREATE TABLE [Auditorias] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Usuario] NVARCHAR(100) NOT NULL,
    [Accion] NVARCHAR(100) NOT NULL,
    [Fecha] SMALLDATETIME NOT NULL

);
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
CREATE TABLE [Roles] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Nombre] NVARCHAR(50) NOT NULL UNIQUE,
    [Descripcion] NVARCHAR(150) NULL
);

CREATE TABLE [Permisos] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Codigo] NVARCHAR(100) NOT NULL UNIQUE,   -- ej: 'Pedidos.Ver', 'Pedidos.Editar'
    [Descripcion] NVARCHAR(150) NULL
);

CREATE TABLE [RolPermisos] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Rol] INT REFERENCES [Roles]([Id]) NOT NULL,
    [Permiso] INT REFERENCES [Permisos]([Id]) NOT NULL,
    UNIQUE ([Rol], [Permiso])
);

CREATE TABLE [Usuarios] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [NombreUsuario] NVARCHAR(50) NOT NULL UNIQUE,
    [Clave] NVARCHAR(200) NOT NULL,
    [Activo] BIT NOT NULL,
    [UltimoAcceso] SMALLDATETIME NULL,
    [Rol] INT REFERENCES [Roles]([Id]) NOT NULL,
    [Empleado] INT NULL REFERENCES [Empleados]([Id]),
    [Cliente] INT NULL REFERENCES [Clientes]([Id]),
    CHECK (([Empleado] IS NULL AND [Cliente] IS NOT NULL)
        OR ([Empleado] IS NOT NULL AND [Cliente] IS NULL))
);

   USE Restaurante;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    -------------------------------------------------------
    -- 1. CREAR LOS ROLES
    -------------------------------------------------------

    INSERT INTO Roles (Nombre, Descripcion)
    SELECT v.Nombre, v.Descripcion
    FROM (VALUES
        ('Administrador', 'Acceso completo al sistema'),
        ('Empleado', 'Gestion de las operaciones del restaurante'),
        ('Cliente', 'Acceso a pedidos, reservas y resenas propias')
    ) v(Nombre, Descripcion)
    WHERE NOT EXISTS (
        SELECT 1
        FROM Roles r
        WHERE r.Nombre = v.Nombre
    );


    -------------------------------------------------------
    -- 2. CREAR LOS PERMISOS
    -------------------------------------------------------

    CREATE TABLE #Modulos (
        Nombre NVARCHAR(50)
    );

    INSERT INTO #Modulos (Nombre) VALUES
    ('Clientes'),
    ('Empleados'),
    ('Categorias'),
    ('Productos'),
    ('Mesas'),
    ('Pedidos'),
    ('DetallePedidos'),
    ('MetodoPagos'),
    ('Facturas'),
    ('Reservas'),
    ('Proveedores'),
    ('Ingredientes'),
    ('RecetaIngredientes'),
    ('Inventarios'),
    ('Compras'),
    ('DetalleCompras'),
    ('Turnos'),
    ('Resenas'),
    ('Usuarios'),
    ('Roles'),
    ('Permisos'),
    ('Auditorias');

    CREATE TABLE #Acciones (
        Nombre NVARCHAR(30),
        Descripcion NVARCHAR(50)
    );

    INSERT INTO #Acciones (Nombre, Descripcion) VALUES
    ('Ver', 'Consultar'),
    ('Crear', 'Crear'),
    ('Actualizar', 'Actualizar'),
    ('Eliminar', 'Eliminar');

    INSERT INTO Permisos (Codigo, Descripcion)
    SELECT
        CONCAT(m.Nombre, '.', a.Nombre),
        CONCAT(a.Descripcion, ' ', m.Nombre)
    FROM #Modulos m
    CROSS JOIN #Acciones a
    WHERE NOT EXISTS (
        SELECT 1
        FROM Permisos p
        WHERE p.Codigo = CONCAT(m.Nombre, '.', a.Nombre)
    );

    -- Permisos especiales para clientes

    INSERT INTO Permisos (Codigo, Descripcion)
    SELECT v.Codigo, v.Descripcion
    FROM (VALUES
        ('Pedidos.VerPropios', 'Consultar pedidos propios'),
        ('Reservas.VerPropias', 'Consultar reservas propias'),
        ('Resenas.VerPropias', 'Consultar resenas propias'),
        ('Perfil.VerPropio', 'Consultar perfil propio'),
        ('Perfil.ActualizarPropio', 'Actualizar perfil propio')
    ) v(Codigo, Descripcion)
    WHERE NOT EXISTS (
        SELECT 1
        FROM Permisos p
        WHERE p.Codigo = v.Codigo
    );


    -------------------------------------------------------
    -- 3. ASIGNAR TODOS LOS PERMISOS AL ADMINISTRADOR
    -------------------------------------------------------

    INSERT INTO RolPermisos (Rol, Permiso)
    SELECT r.Id, p.Id
    FROM Roles r
    CROSS JOIN Permisos p
    WHERE r.Nombre = 'Administrador'
      AND NOT EXISTS (
          SELECT 1
          FROM RolPermisos rp
          WHERE rp.Rol = r.Id
            AND rp.Permiso = p.Id
      );


    -------------------------------------------------------
    -- 4. PERMISOS DEL EMPLEADO
    -------------------------------------------------------

    CREATE TABLE #PermisosEmpleado (
        Codigo NVARCHAR(100)
    );

    INSERT INTO #PermisosEmpleado (Codigo) VALUES
    ('Clientes.Ver'),
    ('Empleados.Ver'),
    ('Categorias.Ver'),
    ('Productos.Ver'),
    ('Mesas.Ver'),

    ('Pedidos.Ver'),
    ('Pedidos.Crear'),
    ('Pedidos.Actualizar'),

    ('DetallePedidos.Ver'),
    ('DetallePedidos.Crear'),
    ('DetallePedidos.Actualizar'),

    ('Reservas.Ver'),
    ('Reservas.Crear'),
    ('Reservas.Actualizar'),

    ('Facturas.Ver'),
    ('Facturas.Crear'),
    ('MetodoPagos.Ver'),

    ('Proveedores.Ver'),
    ('Ingredientes.Ver'),
    ('Inventarios.Ver'),
    ('Compras.Ver'),
    ('DetalleCompras.Ver'),
    ('Turnos.Ver'),
    ('Resenas.Ver');


    INSERT INTO RolPermisos (Rol, Permiso)
    SELECT r.Id, p.Id
    FROM Roles r
    INNER JOIN #PermisosEmpleado pe ON 1 = 1
    INNER JOIN Permisos p ON p.Codigo = pe.Codigo
    WHERE r.Nombre = 'Empleado'
      AND NOT EXISTS (
          SELECT 1
          FROM RolPermisos rp
          WHERE rp.Rol = r.Id
            AND rp.Permiso = p.Id
      );


    -------------------------------------------------------
    -- 5. PERMISOS DEL CLIENTE
    -------------------------------------------------------

    CREATE TABLE #PermisosCliente (
        Codigo NVARCHAR(100)
    );

    INSERT INTO #PermisosCliente (Codigo) VALUES
    ('Productos.Ver'),
    ('Categorias.Ver'),

    ('Pedidos.Crear'),
    ('DetallePedidos.Crear'),
    ('Pedidos.VerPropios'),

    ('Reservas.Crear'),
    ('Reservas.VerPropias'),

    ('Resenas.Crear'),
    ('Resenas.VerPropias'),

    ('Perfil.VerPropio'),
    ('Perfil.ActualizarPropio');


    INSERT INTO RolPermisos (Rol, Permiso)
    SELECT r.Id, p.Id
    FROM Roles r
    INNER JOIN #PermisosCliente pc ON 1 = 1
    INNER JOIN Permisos p ON p.Codigo = pc.Codigo
    WHERE r.Nombre = 'Cliente'
      AND NOT EXISTS (
          SELECT 1
          FROM RolPermisos rp
          WHERE rp.Rol = r.Id
            AND rp.Permiso = p.Id
      );


    -------------------------------------------------------
    -- 6. CREAR USUARIOS DE EMPLEADOS
    -------------------------------------------------------

    -- Administrador: Santiago Serna
    INSERT INTO Usuarios
        (NombreUsuario, Clave, Activo, Rol, Empleado, Cliente)
    SELECT
        'admin',
        'Admin123!',
        1,
        r.Id,
        e.Id,
        NULL
    FROM Empleados e
    INNER JOIN Roles r ON r.Nombre = 'Administrador'
    WHERE e.Cedula = '1001001001'
      AND NOT EXISTS (
          SELECT 1 FROM Usuarios u
          WHERE u.NombreUsuario = 'admin'
      );

    -- Los demás empleados
    INSERT INTO Usuarios
        (NombreUsuario, Clave, Activo, Rol, Empleado, Cliente)
    SELECT
        v.Usuario,
        'Empleado123!',
        1,
        r.Id,
        e.Id,
        NULL
    FROM (VALUES
        ('laura.restrepo', '1001001002'),
        ('miguel.torres', '1001001003'),
        ('daniela.rios', '1001001004'),
        ('felipe.castro', '1001001005')
    ) v(Usuario, Cedula)
    INNER JOIN Empleados e ON e.Cedula = v.Cedula
    INNER JOIN Roles r ON r.Nombre = 'Empleado'
    WHERE NOT EXISTS (
        SELECT 1
        FROM Usuarios u
        WHERE u.NombreUsuario = v.Usuario
    );


    -------------------------------------------------------
    -- 7. CREAR USUARIOS DE CLIENTES
    -------------------------------------------------------

    INSERT INTO Usuarios
        (NombreUsuario, Clave, Activo, Rol, Empleado, Cliente)
    SELECT
        v.Usuario,
        'Cliente123!',
        1,
        r.Id,
        NULL,
        c.Id
    FROM (VALUES
        ('carlos.mendoza', '1017123456'),
        ('ana.gomez', '1020987654'),
        ('juan.perez', '1034567890'),
        ('laura.rodriguez', '1045678901'),
        ('andres.martinez', '1056789012')
    ) v(Usuario, Cedula)
    INNER JOIN Clientes c ON c.Cedula = v.Cedula
    INNER JOIN Roles r ON r.Nombre = 'Cliente'
    WHERE NOT EXISTS (
        SELECT 1
        FROM Usuarios u
        WHERE u.NombreUsuario = v.Usuario
    );


    -------------------------------------------------------
    -- 8. FINALIZAR
    -------------------------------------------------------

    DROP TABLE #Modulos;
    DROP TABLE #Acciones;
    DROP TABLE #PermisosEmpleado;
    DROP TABLE #PermisosCliente;

    COMMIT TRANSACTION;

    PRINT 'Roles, permisos, relaciones y usuarios creados correctamente.';

END TRY
BEGIN CATCH

    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    THROW;

END CATCH;
GO


-------------------------------------------------------
-- 9. COMPROBAR LOS RESULTADOS
-------------------------------------------------------

SELECT * FROM Roles ORDER BY Id;
GO

SELECT * FROM Permisos ORDER BY Codigo;
GO

SELECT
    r.Nombre AS Rol,
    p.Codigo AS Permiso
FROM RolPermisos rp
INNER JOIN Roles r ON r.Id = rp.Rol
INNER JOIN Permisos p ON p.Id = rp.Permiso
ORDER BY r.Nombre, p.Codigo;
GO

SELECT
    u.Id,
    u.NombreUsuario,
    u.Activo,
    r.Nombre AS Rol,
    e.Nombre AS Empleado,
    c.Nombre AS Cliente
FROM Usuarios u
INNER JOIN Roles r ON r.Id = u.Rol
LEFT JOIN Empleados e ON e.Id = u.Empleado
LEFT JOIN Clientes c ON c.Id = u.Cliente
ORDER BY u.Id;
GO
       

       
        
use Restaurante;
GO
-- ============================================================
-- 1. TABLAS INDEPENDIENTES
-- ============================================================

INSERT INTO [TipoEmpleados] ([Nombre], [SalarioBase]) VALUES
('Administrador', 2500000.00),
('Cajero', 1300000.00),
('Mesero', 1300000.00),
('Chef', 2200000.00),
('Auxiliar de Cocina', 1500000.00);


INSERT INTO [Sucursales] ([Nombre], [Direccion], [Telefono], [Ciudad]) VALUES
('Sucursal Central', 'Calle 50 # 45-10', '6044445566', 'Medellín'),
('Sucursal Poblado', 'Carrera 43A # 10-20', '6043332211', 'Medellín'),
('Sucursal Laureles', 'Carrera 70 # 45-30', '6045556677', 'Medellín'),
('Sucursal Envigado', 'Carrera 27 # 35-15', '6044447788', 'Envigado'),
('Sucursal Bello', 'Calle 30 # 50-25', '6043338899', 'Bello');


INSERT INTO [Clientes] ([Nombre], [Cedula], [Telefono], [Email]) VALUES
('Carlos Mendoza', '1017123456', '3001234567', 'carlos.mendoza@email.com'),
('Ana María Gómez', '1020987654', '3109876543', 'ana.gomez@email.com'),
('Juan Pérez', '1034567890', '3204567890', 'juan.perez@email.com'),
('Laura Rodríguez', '1045678901', '3156789012', 'laura.rodriguez@email.com'),
('Andrés Martínez', '1056789012', '3007890123', 'andres.martinez@email.com');


INSERT INTO [Categorias] ([Nombre], [Descripcion], [Disponible], [OrdenMenu]) VALUES
('Bebidas', 'Refrescos, jugos y bebidas calientes', 1, 1),
('Platos Fuertes', 'Especialidades de la casa y carnes', 1, 2),
('Entradas', 'Entradas y acompañamientos', 1, 3),
('Postres', 'Postres y dulces de la casa', 1, 4),
('Ensaladas', 'Ensaladas frescas y saludables', 1, 5);


INSERT INTO [Mesas] ([Numero], [Capacidad], [Estado], [Ubicacion]) VALUES
(1, 4, 1, 'Terraza'),
(2, 2, 1, 'Salón Principal'),
(3, 6, 1, 'Salón Principal'),
(4, 4, 1, 'Segundo Piso'),
(5, 8, 1, 'Terraza');


INSERT INTO [MetodoPagos] ([Tipo], [Descripcion]) VALUES
('Efectivo', 'Pago en billetes y monedas'),
('Tarjeta', 'Tarjeta de crédito o débito'),
('Transferencia', 'Transferencia bancaria'),
('Nequi', 'Pago mediante Nequi'),
('Daviplata', 'Pago mediante Daviplata');


INSERT INTO [Proveedores] ([Nombre], [Telefono], [Email], [Direccion]) VALUES
('Distribuidora Avícola S.A.', '3120001122', 'ventas@avicola.com', 'Carrera 65 # 30-12'),
('Lácteos del Campo', '3159998877', 'contacto@lacteos.com', 'Calle 10 # 20-30'),
('Frutas Medellín', '3104567891', 'ventas@frutasmed.com', 'Carrera 52 # 40-18'),
('Carnes Premium', '3007894561', 'contacto@carnespremium.com', 'Calle 33 # 60-25'),
('Distribuciones El Sabor', '3201234567', 'ventas@elsabor.com', 'Carrera 80 # 25-10');


-- ============================================================
-- 2. TABLAS DEPENDIENTES DE NIVEL 1
-- ============================================================

INSERT INTO [Empleados]
([Nombre], [Cedula], [Telefono], [FechaContratacion], [TipoEmpleado], [Sucursal])
VALUES
('Santiago Serna', '1001001001', '3011111111', '2026-01-15 08:00', 1, 1),
('Laura Restrepo', '1001001002', '3012222222', '2026-02-01 08:00', 3, 2),
('Miguel Torres', '1001001003', '3013333333', '2026-02-15 08:00', 2, 3),
('Daniela Ríos', '1001001004', '3014444444', '2026-03-01 08:00', 4, 4),
('Felipe Castro', '1001001005', '3015555555', '2026-03-10 08:00', 5, 5);


INSERT INTO [Productos]
([Nombre], [Descripcion], [Precio], [Disponible], [Categoria])
VALUES
('Hamburguesa Artesanal', 'Carne 200g, queso cheddar y tocineta', 28000.00, 1, 2),
('Jugo Natural de Lulo', 'Jugo natural de lulo en agua o leche', 8000.00, 1, 1),
('Papas con Queso', 'Papas fritas con queso cheddar', 12000.00, 1, 3),
('Cheesecake de Fresa', 'Cheesecake con salsa de fresa', 14000.00, 1, 4),
('Ensalada César', 'Lechuga, pollo, queso parmesano y aderezo', 18000.00, 1, 5);


INSERT INTO [Ingredientes]
([Nombre], [UniMedida], [CostoUni], [Proveedor])
VALUES
('Carne Molida Premium', 'Kg', 18000.00, 4),
('Queso Cheddar', 'Kg', 22000.00, 2),
('Lulo', 'Kg', 7500.00, 3),
('Papa', 'Kg', 4000.00, 5),
('Lechuga', 'Kg', 5000.00, 3);


INSERT INTO [Compras]
([Proveedor], [Fecha], [Total], [Estado])
VALUES
(1, '2026-03-01 09:00', 180000.00, 'Completado'),
(2, '2026-03-05 10:00', 220000.00, 'Completado'),
(3, '2026-03-10 08:30', 150000.00, 'Completado'),
(4, '2026-03-15 11:00', 320000.00, 'Completado'),
(5, '2026-03-20 09:30', 95000.00, 'Pendiente');


-- ============================================================
-- 3. TABLAS DEPENDIENTES DE NIVEL 2
-- ============================================================

INSERT INTO [Pedidos]
([Fecha], [Cliente], [Mesa], [Empleado], [Estado])
VALUES
('2026-03-29 12:30', 1, 2, 2, 'Atendido'),
('2026-03-29 13:15', 2, 1, 2, 'Atendido'),
('2026-03-29 14:00', 3, 3, 3, 'Atendido'),
('2026-03-29 19:30', 4, 4, 3, 'Pendiente'),
('2026-03-30 20:00', 5, 5, 1, 'Atendido');


INSERT INTO [Reservas]
([Cliente], [Mesa], [FechaHora], [NPersonas], [Estado])
VALUES
(1, 2, '2026-03-30 19:00', 2, 'Confirmada'),
(2, 3, '2026-03-30 20:00', 4, 'Confirmada'),
(3, 5, '2026-03-31 18:30', 6, 'Confirmada'),
(4, 1, '2026-04-01 19:30', 3, 'Pendiente'),
(5, 4, '2026-04-02 20:00', 4, 'Confirmada');


INSERT INTO [RecetaIngredientes]
([Producto], [Ingrediente], [CantiReque], [Notas])
VALUES
(1, 1, 0.20, 'Porción de carne por hamburguesa'),
(1, 2, 0.05, 'Queso cheddar para la hamburguesa'),
(2, 3, 0.15, 'Lulo para un jugo de 500 ml'),
(3, 4, 0.25, 'Papa para una porción'),
(5, 5, 0.10, 'Lechuga para una ensalada');


INSERT INTO [Inventarios]
([Ingrediente], [CantDisponible], [CantMinima], [UltimaActu])
VALUES
(1, 15.50, 5.00, '2026-03-29 08:00'),
(2, 8.00, 2.00, '2026-03-29 08:00'),
(3, 20.00, 5.00, '2026-03-29 08:00'),
(4, 30.00, 10.00, '2026-03-29 08:00'),
(5, 12.00, 4.00, '2026-03-29 08:00');


INSERT INTO [DetalleCompras]
([Ingrediente], [Compra], [Cantidad], [PrecioUni])
VALUES
(1, 1, 10.00, 18000.00),
(2, 2, 10.00, 22000.00),
(3, 3, 20.00, 7500.00),
(4, 4, 80.00, 4000.00),
(5, 5, 19.00, 5000.00);


INSERT INTO [Turnos]
([Empleado], [FechaInicio], [FechaFin], [Nombre])
VALUES
(1, '2026-03-29 08:00', '2026-03-29 16:00', 'Turno Mañana'),
(2, '2026-03-29 08:00', '2026-03-29 16:00', 'Turno Mañana'),
(3, '2026-03-29 12:00', '2026-03-29 20:00', 'Turno Tarde'),
(4, '2026-03-29 14:00', '2026-03-29 22:00', 'Turno Tarde'),
(5, '2026-03-30 08:00', '2026-03-30 16:00', 'Turno Mañana');


-- ============================================================
-- 4. TABLAS DEPENDIENTES DE NIVEL 3
-- ============================================================

INSERT INTO [DetallePedidos]
([Pedido], [Producto], [Cantidad], [PrecioUni])
VALUES
(1, 1, 2, 28000.00),
(1, 2, 2, 8000.00),
(2, 3, 1, 12000.00),
(3, 4, 2, 14000.00),
(4, 5, 1, 18000.00);


INSERT INTO [Facturas]
([Pedido], [FechaEmi], [Subtotal], [Impuesto], [Total], [MetodoPago])
VALUES
(1, '2026-03-29 13:15', 72000.00, 12960.00, 84960.00, 2),
(2, '2026-03-29 14:00', 12000.00, 2160.00, 14160.00, 1),
(3, '2026-03-29 15:00', 28000.00, 5040.00, 33040.00, 3),
(4, '2026-03-29 20:30', 18000.00, 3240.00, 21240.00, 4),
(5, '2026-03-30 21:00', 28000.00, 5040.00, 33040.00, 5);


INSERT INTO [Resenas]
([Cliente], [Producto], [Calificacion], [Comentario], [Fecha])
VALUES
(1, 1, 5, 'Excelente sabor y cocción de la carne.', '2026-03-29 14:00'),
(2, 3, 4, 'Las papas estaban muy buenas.', '2026-03-29 15:00'),
(3, 4, 5, 'El cheesecake estuvo delicioso.', '2026-03-29 16:00'),
(4, 5, 4, 'La ensalada estaba fresca.', '2026-03-29 21:00'),
(5, 2, 5, 'El jugo de lulo estaba excelente.', '2026-03-30 21:30');



SELECT * FROM TipoEmpleados;
GO

SELECT * FROM Sucursales;
GO

SELECT * FROM Empleados;
GO

SELECT * FROM Clientes;
GO

SELECT * FROM Categorias;
GO

SELECT * FROM Productos;
GO

SELECT * FROM Mesas;
GO

SELECT * FROM Pedidos;
GO

SELECT * FROM DetallePedidos;
GO

SELECT * FROM MetodoPagos;
GO

SELECT * FROM Facturas;
GO

SELECT * FROM Reservas;
GO

SELECT * FROM Proveedores;
GO

SELECT * FROM Ingredientes;
GO

SELECT * FROM RecetaIngredientes;
GO

SELECT * FROM Inventarios;
GO

SELECT * FROM Compras;
GO

SELECT * FROM DetalleCompras;
GO

SELECT * FROM Turnos;
GO

SELECT * FROM Resenas;
GO