﻿using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CityInfo.Configuration.Swagger.Request
{
    public class DefaultRequestHeadersOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Parameters = operation.Parameters ?? new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "CorrelationId",
                Description = "UUID to identify a chain of calls. Autogenerated if not specified. Same CorrelationId is returned in response header.",
                In = "header",
                Required = false,
                Type = "string"
            });

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "RequestId",
                Description = "UUID to identify a single request. Autogenerated if not specified. Same RequestId is returned in response header.",
                In = "header",
                Required = false,
                Type = "string"
            });
        }
    }
}
