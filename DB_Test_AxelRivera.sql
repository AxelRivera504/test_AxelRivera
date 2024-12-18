--Crearcion BD
CREATE DATABASE test_AxelRivera

GO

USE test_AxelRivera

GO

--Tablas
CREATE TABLE Clientes (
    cliente_id	INT IDENTITY(1,1),
    nombre		VARCHAR(255) NOT NULL,
    direccion	VARCHAR(255),
    telefono	VARCHAR(20),
    correo		VARCHAR(100)

	CONSTRAINT PK_Clientes_cliente_id PRIMARY KEY(cliente_id)
);

GO

CREATE TABLE Productos (
    producto_id			INT IDENTITY(1,1),
    nombre_producto		VARCHAR(255) NOT NULL,
    descripcion			VARCHAR(500),
    precio				DECIMAL(10, 2) NOT NULL,
    cantidad_disponible INT NOT NULL,
	descontinuado       BIT
	CONSTRAINT PK_Productos_producto_id PRIMARY KEY(producto_id)
);

GO

CREATE TABLE Facturas (
    factura_id		INT IDENTITY(1,1),
    cliente_id		INT,
    fecha			DATE NOT NULL,
    total			DECIMAL(10, 2) NOT NULL,

	CONSTRAINT PK_Facturas_factura_id			PRIMARY KEY(factura_id),
    CONSTRAINT FK_Facturas_Clientes_cliente_id  FOREIGN KEY (cliente_id) REFERENCES Clientes(cliente_id)
);

GO

CREATE TABLE DetallesFactura (
    detalle_id		INT IDENTITY(1,1),
    factura_id		INT NOT NULL,
    producto_id		INT NOT NULL,
    cantidad		INT NOT NULL,
    precio_unitario DECIMAL(10, 2) NOT NULL,

	CONSTRAINT PK_DetallesFactura_detalle_id			PRIMARY KEY(detalle_id),
    CONSTRAINT FK_DetallesFactura_Facturas_factura_id	FOREIGN KEY (factura_id) REFERENCES Facturas(factura_id),
    CONSTRAINT FK_DetallesFactura_Productos_producto_id	FOREIGN KEY (producto_id) REFERENCES Productos(producto_id)
);

GO

CREATE TABLE MovimientosInventario (
    movimiento_id	INT IDENTITY(1,1),
    producto_id		INT NOT NULL,
    tipo_movimiento VARCHAR(1) NOT NULL,
    cantidad		INT NOT NULL,
    fecha			DATE NOT NULL,

	CONSTRAINT PK_MovimientosInventario_movimiento_id			PRIMARY KEY (movimiento_id),
    CONSTRAINT FK_MovimientosInventario_Productos_producto_id	FOREIGN KEY (producto_id) REFERENCES Productos(producto_id),
	CONSTRAINT CK_MovimientosInventario_tipo_movimiento			CHECK(tipo_movimiento IN ('S','E')) --S para salida y E para entrada de productos esto en caso de compra y la salida en caso de ventas
);

GO

--Vista
CREATE OR ALTER VIEW Vw_ReporteDetalladoVentas AS
SELECT 
	clie.cliente_id AS codigoCliente,
    clie.nombre AS nombreCliente,
    ISNULL(clie.direccion, 'N/A') AS direccionCliente,
    ISNULL(clie.telefono, 'N/A') AS telefonoCliente,
    fact.factura_id AS idFactura,
    fact.fecha AS fechaFactura,
	prod.producto_id as productoId,
    prod.nombre_producto AS nombreProducto,
    detF.cantidad AS cantidadVendida,
    detF.precio_unitario AS precioUnitario,
    (detF.cantidad * detF.precio_unitario) AS totalProducto
FROM 
    Facturas fact
INNER JOIN 
    DetallesFactura detF ON fact.factura_id = detF.factura_id
INNER JOIN  
    Clientes clie ON fact.cliente_id = clie.cliente_id
INNER JOIN  
    Productos prod ON detF.producto_id = prod.producto_id;

--Inserts

INSERT INTO Clientes (nombre, direccion, telefono, correo)
VALUES 
('Juan Perez', 'Calle Falsa 123', '123456789', 'juan.perez@example.com'),
('Maria Lopez', 'Avenida Siempre Viva 742', '987654321', 'maria.lopez@example.com'),
('Pedro Gomez', 'Calle Sol 456', '456123789', 'pedro.gomez@example.com');

INSERT INTO Productos (nombre_producto, descripcion, precio, cantidad_disponible)
VALUES 
('Laptop HP', 'Laptop de 15 pulgadas con 8GB RAM y 256GB SSD', 800.00, 10,0),
('Mouse Logitech', 'Mouse inalámbrico ergonómico', 25.00, 50,0),
('Teclado Mecánico', 'Teclado mecánico RGB para gaming', 60.00, 30,0);


INSERT INTO Facturas (cliente_id, fecha, total)
VALUES 
(1, '2024-06-10', 885.00),
(2, '2024-06-11', 110.00);


INSERT INTO DetallesFactura (factura_id, producto_id, cantidad, precio_unitario)
VALUES 
(1, 1, 1, 800.00),  
(1, 2, 2, 25.00),   
(2, 3, 1, 60.00),   
(2, 2, 2, 25.00);   


-- Movimientos de entrada (E) para los productos
INSERT INTO MovimientosInventario (producto_id, tipo_movimiento, cantidad, fecha)
VALUES 
(1, 'E', 10, '2024-06-01'),  
(2, 'E', 50, '2024-06-01'),  
(3, 'E', 30, '2024-06-01');  

-- Movimientos de salida (S) debido a ventas
INSERT INTO MovimientosInventario (producto_id, tipo_movimiento, cantidad, fecha)
VALUES 
(1, 'S', 1, '2024-06-10'), 
(2, 'S', 2, '2024-06-10'), 
(3, 'S', 1, '2024-06-11'), 
(2, 'S', 2, '2024-06-11'); 
