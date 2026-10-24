
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ToolTrack.Filters;

public class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAuthorize =
            context.MethodInfo.IsDefined(typeof(AuthorizeAttribute), true) ||
            context.MethodInfo.DeclaringType?
                .IsDefined(typeof(AuthorizeAttribute), true) == true;

        if (!hasAuthorize)
            return;

        operation.Security ??= new List<OpenApiSecurityRequirement>();

        operation.Security.Add(new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", null)] =
                new List<string>()
        });
    }
}
