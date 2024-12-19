CREATE DATABASE mind_company;
GO

USE mind_company;
GO

-- Departments table
CREATE TABLE departments (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(50) NOT NULL UNIQUE
);
GO

-- Employees table
CREATE TABLE employees (
    id INT IDENTITY(1,1) PRIMARY KEY,
    first_name NVARCHAR(50) NOT NULL,
    last_name NVARCHAR(50) NOT NULL,
    hire_date DATE NOT NULL,
    department_id INT NOT NULL,
    phone NVARCHAR(15),
    address NVARCHAR(200),
    CONSTRAINT FK_employees_departments 
        FOREIGN KEY (department_id) 
        REFERENCES departments(id)
);
GO 

INSERT INTO departments (name) VALUES 
    ('IT'),
    ('HR'),
    ('Finance'),
    ('Marketing'),
    ('Talent Acquisition');