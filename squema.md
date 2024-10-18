// Cliente Servicio

Table personas {
  id UNIQUEIDENTIFIER [primary key]
  nombres varchar(100)
  identificacion VARCHAR(20)
  direccion VARCHAR(255)
  telefono VARCHAR(20)
  edad TINYINT //UNSIGNED
  genero_id TINYINT
}

Table clientes {
  id UNIQUEIDENTIFIER [primary key]
  password VARCHAR(255)
  persona_id UNIQUEIDENTIFIER
  estado BIT
}

Table generos {
  id TINYINT [primary key]
  descripcion VARCHAR(50)
}

Ref: personas.id < clientes.persona_id
Ref: generos.id < personas.genero_id

// Cuentas Servicio

Table tipo_cuentas {
  id TINYINT [primary key]
  descripcion VARCHAR(50)
}

Table cuentas {
  id UNIQUEIDENTIFIER [primary key]
  numero_cuenta VARCHAR(20)
  saldo_inicial DECIMAL(19,4)
  tipo_cuenta_id TINYINT
  estado BIT
  persona_id UNIQUEIDENTIFIER
}

Table movimientos {
  id UNIQUEIDENTIFIER [primary key]
  valor DECIMAL(19,4)
  saldo DECIMAL(19,4)
  fecha DATETIME 
  cuenta_id UNIQUEIDENTIFIER
}

Ref: cuentas.id < movimientos.cuenta_id
Ref: tipo_cuentas.id < cuentas.tipo_cuenta_id
