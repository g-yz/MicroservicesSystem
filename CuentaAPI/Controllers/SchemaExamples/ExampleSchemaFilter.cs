using CuentaAPI.Contracts;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CuentaAPI.Controllers.SchemaExamples;

public class ExampleSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(CuentaCreateRequest))
        {
            schema.Example = new OpenApiObject
            {
                ["numeroCuenta"] = new OpenApiString("1234567890"),
                ["saldoInicial"] = new OpenApiDouble(10000.00),
                ["tipoCuentaId"] = new OpenApiInteger(1),
                ["clienteId"] = new OpenApiString(Guid.NewGuid().ToString())
            };
        }
        else if (context.Type == typeof(CuentaUpdateRequest))
        {
            schema.Example = new OpenApiObject
            {
                ["numeroCuenta"] = new OpenApiString("1234567890"),
                ["saldoInicial"] = new OpenApiDouble(20000.00),
                ["tipoCuentaId"] = new OpenApiInteger(1),
                ["estado"] = new OpenApiBoolean(true),
                ["clienteId"] = new OpenApiString(Guid.NewGuid().ToString())
            };
        }
        else if (context.Type == typeof(MovimientoAddRequest))
        {
            schema.Example = new OpenApiObject
            {
                ["valor"] = new OpenApiDouble(150.75),
                ["cuentaId"] = new OpenApiString(Guid.NewGuid().ToString())
            };
        }
    }
}
