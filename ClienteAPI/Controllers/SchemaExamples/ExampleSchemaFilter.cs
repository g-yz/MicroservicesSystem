namespace ClienteAPI.Controllers.SchemaExamples;

using ClienteAPI.Contracts;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class ExampleSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(ClienteCreateRequest))
        {
            schema.Example = new OpenApiObject
            {
                ["nombres"] = new OpenApiString("Juan Perez"),
                ["direccion"] = new OpenApiString("123 Calle Sur"),
                ["telefono"] = new OpenApiString("999-555-1234"),
                ["password"] = new OpenApiString("MiPassword"),
                ["identificacion"] = new OpenApiString("AB123456"),
                ["edad"] = new OpenApiInteger(20),
                ["tipoGeneroId"] = new OpenApiInteger(1)
            };
        }
        else if (context.Type == typeof(ClienteUpdateRequest))
        {
            schema.Example = new OpenApiObject
            {

                ["nombres"] = new OpenApiString("Meria Perez"),
                ["direccion"] = new OpenApiString("123 Calle Sur"),
                ["telefono"] = new OpenApiString("999-555-1234"),
                ["password"] = new OpenApiString("MiPasswordSeguro"),
                ["estado"] = new OpenApiBoolean(true),
                ["identificacion"] = new OpenApiString("CD123456"),
                ["edad"] = new OpenApiInteger(25),
                ["tipoGeneroId"] = new OpenApiInteger(2)
            };
        }
    }
}
