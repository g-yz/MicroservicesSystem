# Proyecto de Microservicios

## Requisitos

- .NET SDK
- Docker
- Docker Compose

## Instrucciones

### Ejecución del projecto

1. Clonar el repositorio:
2. Abrir el projecto en Visual Studio
3. Ejecutar el projecto usando docker compose
4. Verificar que los servicios esten activos y se haya ejecutado la carga inicial de datos sobre la base de datos
5. Abrir los servicios en el navegador
    * Cliente API: https://localhost:6011/swagger/index.html
    * Cuenta API: https://localhost:6010/swagger/index.html
6. Abrir las collections de la ruta /Collections en Postman

### Ejecución del los test de integración

1. Ejecutar el projecto usando docker compose
2. Ejecutar los test de integracion

### Modificar la base de datos

1. Abrir el esquema correspondiente
    * \ClienteAPI\DB_scripts\BaseDatos
    * \ClienteAPI\DB_scripts\BaseDatos.Seeding
    * \CuentaAPI\DB_scripts\BaseDatos
    * \CuentaAPI\DB_scripts\BaseDatos.Seeding
2. Modificar y guardar
3. Actualizar los modelos usando UpdateModels.sh
4. Ejecutar el projecto usando docker compose
5. Abrir los servicios en el navegador
    * Cliente API: https://localhost:6011/swagger/index.html
    * Cuenta API: https://localhost:6010/swagger/index.html
