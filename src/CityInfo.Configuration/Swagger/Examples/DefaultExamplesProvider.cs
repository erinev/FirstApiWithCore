﻿using System;
using System.Collections.Generic;
using System.Net;
using CityInfo.Contracts.Errors;

namespace CityInfo.Configuration.Swagger.Examples
{
    internal static class DefaultExamplesProvider
    {
        private static BadRequestResponse BadRequestResponse => new BadRequestResponse(HttpStatusCode.BadRequest.ToString(), "The request is invalid.", new Dictionary<string, string>());
        private static UnauthorizedResponse UnauthorizedResponse => new UnauthorizedResponse(HttpStatusCode.Unauthorized.ToString(), "Authorization has been denied for this request.");
        private static ForbiddenResponse ForbiddenResponse => new ForbiddenResponse(HttpStatusCode.Forbidden.ToString(), "No permissions to access resource.", new Dictionary<string, string>());
        private static NotFoundResponse NotFoundResponse => new NotFoundResponse(HttpStatusCode.NotFound.ToString(), "Resource not found.", new Dictionary<string, string>());
        private static readonly InternalServerErrorResponse InternalServerErrorResponse = new InternalServerErrorResponse(HttpStatusCode.InternalServerError.ToString(), "Something unexpected happened. Try again later.");

        private static readonly IDictionary<Type, object> ExampleByType = new Dictionary<Type, object>
        {
            { typeof(BadRequestResponse), BadRequestResponse },
            { typeof(UnauthorizedResponse), UnauthorizedResponse },
            { typeof(ForbiddenResponse), ForbiddenResponse },
            { typeof(NotFoundResponse), NotFoundResponse },
            { typeof(InternalServerErrorResponse), InternalServerErrorResponse }
        };

        public static object Get(Type key)
        {
            object example;
            return ExampleByType.TryGetValue(key, out example) ? example : null;
        }
    }
}