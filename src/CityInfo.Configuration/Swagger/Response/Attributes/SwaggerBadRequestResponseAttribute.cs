using CityInfo.Contracts.Errors;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CityInfo.Configuration.Swagger.Response.Attributes
{
    public class SwaggerBadRequestResponseAttribute : SwaggerResponseAttribute
    {
        public SwaggerBadRequestResponseAttribute()
            : base(400, typeof(BadRequestResponse), "bad_request - The request is not well-formed. For example, it might be missing a parameter or using the same parameter twice.")
        {
        }
    }
}
