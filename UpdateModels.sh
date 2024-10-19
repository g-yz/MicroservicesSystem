Scaffold-DbContext "Name=DatabaseConnection" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context AccountDbContext -Project AccountApp.Data
Scaffold-DbContext "Name=DatabaseConnection" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context ClientDbContext -Project ClientApp.Data
