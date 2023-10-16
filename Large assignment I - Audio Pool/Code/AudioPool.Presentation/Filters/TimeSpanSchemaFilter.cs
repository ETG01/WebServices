using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using Microsoft.OpenApi.Any;

public class TimeSpanSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(TimeSpan))
        {
            schema.Type = "string";
            schema.Format = null;
            schema.Example = new OpenApiString("00:00:00");
        }
    }
}