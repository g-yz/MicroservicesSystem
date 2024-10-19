namespace ClientApp.Api.SchemaExamples;

using ClientApp.Services.Contracts;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class ExampleSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(ClientCreateRequest))
        {
            schema.Example = new OpenApiObject
            {
                ["fullname"] = new OpenApiString("Juan Perez"),
                ["address"] = new OpenApiString("123 Calle Sur"),
                ["phone"] = new OpenApiString("999-555-1234"),
                ["email"] = new OpenApiString("myemail@mail.com"),
                ["document"] = new OpenApiString("AB123456"),
                ["years"] = new OpenApiInteger(20),
                ["typeGenderId"] = new OpenApiInteger(1)
            };
        }
        else if (context.Type == typeof(ClientUpdateRequest))
        {
            schema.Example = new OpenApiObject
            {

                ["fullname"] = new OpenApiString("Meria Perez"),
                ["address"] = new OpenApiString("123 Calle Sur"),
                ["phone"] = new OpenApiString("999-555-1234"),
                ["email"] = new OpenApiString("mymail@mail.com"),
                ["status"] = new OpenApiBoolean(true),
                ["document"] = new OpenApiString("CD123456"),
                ["years"] = new OpenApiInteger(25),
                ["typeGenderId"] = new OpenApiInteger(2)
            };
        }
    }
}
