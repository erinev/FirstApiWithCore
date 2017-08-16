using CityInfo.Contracts.Errors;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CityInfo.Configuration.Swagger.Responses
{
    /// <summary>
    /// Default response messages which can retruned by all endpoints
    /// </summary>
    public class DefaultResponseOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Responses.Add("500", new Response
            {
                Description = "server_error - The server encountered an unexpected condition that prevented it from fulfilling the request.",
                Schema = context.SchemaRegistry.GetOrRegister(typeof(InternalServerErrorResponse))
            });
        }
    }
}
