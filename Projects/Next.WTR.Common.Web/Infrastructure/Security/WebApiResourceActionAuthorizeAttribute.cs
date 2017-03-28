namespace Next.WTR.Common.Web.Infrastructure.Security
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Security.Claims;
    using System.Web.Http.Controllers;
    using Thinktecture.IdentityModel;
    using Thinktecture.IdentityModel.WebApi;

    public sealed class WebApiResourceActionAuthorizeAttribute : ResourceActionAuthorizeAttribute
    {
        protected override bool CheckAccess(HttpActionContext actionContext, ClaimsPrincipal principal)
        {
            var actionName = GetActionName(actionContext.ActionDescriptor);
            var actionParameters = GetActionParameters(actionContext.ActionDescriptor);
            var controllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            var action = new Collection<Claim> { new Claim(Common.Infrastructure.Security.ClaimTypes.ActionName, actionName) };
            foreach (var actionParameter in actionParameters)
            {
                action.Add(new Claim(Common.Infrastructure.Security.ClaimTypes.ActionParameter, actionParameter));
            }

            var resource = new Collection<Claim> { new Claim("resource", controllerName) };
            var authorizationContext = new AuthorizationContext(principal, resource, action);
            return ClaimsAuthorization.CheckAccess(authorizationContext);
        }

        private static string GetActionName(HttpActionDescriptor httpActionDescriptor)
        {
            return httpActionDescriptor.ActionName;
        }

        private static IEnumerable<string> GetActionParameters(HttpActionDescriptor httpActionDescriptor)
        {
            return httpActionDescriptor.GetParameters().Select(descriptor => $"{descriptor.ParameterType.FullName} {descriptor.ParameterName}");
        }
    }
}