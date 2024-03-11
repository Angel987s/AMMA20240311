CREATE DATABASE AMMA20240311DB;
GO





USE AMMA20240311DB;
GO

select*from DetalleEmpleado
CREATE TABLE Empleado(
    ID INT PRIMARY KEY identity,
    Nombre VARCHAR(100),
    Apellido VARCHAR(100),
    Email VARCHAR(100),
    FechaContratacion DATE,
    Salario DECIMAL(10, 2)
);
GO


CREATE TABLE DetalleEmpleado(
    ID INT PRIMARY KEY identity,
    EmpleadoID INT,
    Nombre VARCHAR(100),
    Apellido VARCHAR(100),
    Telefono VARCHAR(20),
    Email VARCHAR(100),
    FOREIGN KEY (EmpleadoID) REFERENCES Empleado(ID) ON DELETE CASCADE
);
GO