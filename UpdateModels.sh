Scaffold-DbContext "Name=DatabaseConnection" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context ClienteDbContext -Project ClienteAPI
Scaffold-DbContext "Name=DatabaseConnection" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context CuentaDbContext -Project CuentaAPI
