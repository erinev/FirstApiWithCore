﻿using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CityInfo.Configuration.Swagger.Response
{
    public class DefaultResponseHeadersOpearationFilter : IOperationFilter
    {
        private readonly string _correlationIdHeaderName = "CorrelationId";
        private readonly Header _correlationIdHeaderDescription = new Header
        {
            Type = "string",
            Description = "UUID to identify a chain of calls."
        };

        private readonly string _requestIdHeaderName = "RequestId";
        private readonly Header _requestIdHeaderDescription = new Header
        {
            Type = "string",
            Description = "UUID to identify single request."
        };

        public void Apply(Operation operation, OperationFilterContext context)
        {
            foreach (KeyValuePair<string, Swashbuckle.AspNetCore.Swagger.Response> response in operation.Responses)
            {
                if (response.Value.Headers?.Count > 0)
                {
                    if (response.Value.Headers != null && !response.Value.Headers.ContainsKey(_correlationIdHeaderName))
                    {
                        response.Value.Headers.Add(_correlationIdHeaderName, _correlationIdHeaderDescription);
                    }

                    if (response.Value.Headers != null && !response.Value.Headers.ContainsKey(_requestIdHeaderName))
                    {
                        response.Value.Headers.Add(_requestIdHeaderName, _requestIdHeaderDescription);
                    }
                }
                else
                {
                    response.Value.Headers = new Dictionary<string, Header>
                    {
                        {_correlationIdHeaderName, _correlationIdHeaderDescription },
                        {_requestIdHeaderName, _requestIdHeaderDescription }
                    };
                }
            }
        }
    }
}
