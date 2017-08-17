using CityInfo.Contracts.Errors;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CityInfo.Configuration.Swagger.Responses.Attributes
{
    public class SwaggerNotFoundResponseAttribute : SwaggerResponseAttribute
    {
        public SwaggerNotFoundResponseAttribute()
            : base(404, typeof(NotFoundResponse), "not_found - The requested resource was not found.")
        {
        }
    }
}
