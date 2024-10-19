using AccountApp.Services.Contracts;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AccountApp.Api.SchemaExamples;

public class ExampleSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(AccountCreateRequest))
        {
            schema.Example = new OpenApiObject
            {
                ["accountNumber"] = new OpenApiString("1234567890"),
                ["openingBalance"] = new OpenApiDouble(10000.00),
                ["typeAccountId"] = new OpenApiInteger(1),
                ["clientId"] = new OpenApiString(Guid.NewGuid().ToString())
            };
        }
        else if (context.Type == typeof(AccountUpdateRequest))
        {
            schema.Example = new OpenApiObject
            {
                ["accountNumber"] = new OpenApiString("1234567890"),
                ["openingBalance"] = new OpenApiDouble(20000.00),
                ["typeAccountId"] = new OpenApiInteger(1),
                ["status"] = new OpenApiBoolean(true),
                ["clientId"] = new OpenApiString(Guid.NewGuid().ToString())
            };
        }
        else if (context.Type == typeof(MovementAddRequest))
        {
            schema.Example = new OpenApiObject
            {
                ["value"] = new OpenApiDouble(150.75),
                ["accountId"] = new OpenApiString(Guid.NewGuid().ToString())
            };
        }
    }
}
