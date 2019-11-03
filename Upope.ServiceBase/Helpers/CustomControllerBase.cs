using Microsoft.AspNetCore.Mvc;
using Upope.ServiceBase.Models.ApiModels;

namespace Upope.ServiceBase.Helpers
{
    public class CustomControllerBase: ControllerBase
    {
        public override OkObjectResult Ok(object value)
        {
            var status = new OkResponse(new Status(200, "Ok"), value);
            return base.Ok(status);
        }

        public override BadRequestObjectResult BadRequest(object error)
        {
            var response = MapToResponse(error, 400);

            return base.BadRequest(new BadRequestResponse(response.Status, response.Response));
        }

        public override UnauthorizedObjectResult Unauthorized(object error)
        {
            var response = MapToResponse(error, 401);

            return base.Unauthorized(new UnAuthorizedResponse(response.Status, response.Response));
        }

        private static HttpResponse MapToResponse(object value, int code)
        {
            var objectType = value.GetType();
            var objectProperties = objectType.GetProperties();

            var message = string.Empty;

            foreach (var property in objectProperties)
            {
                if (property.Name == "code" || property.Name == "Code")
                {
                    var codeValue = property.GetValue(value).ToString();
                    int.TryParse(codeValue, out code);
                }

                if (property.Name == "message" || property.Name == "Message")
                {
                    value = property.GetValue(value).ToString();
                }

                if (property.Name == "value" || property.Name == "Value")
                {
                    message = property.GetValue(value).ToString();
                }
            }

            var status = new Status(code, message);

            return new HttpResponse() { Status = status, Response = value };
        }
    }
}
