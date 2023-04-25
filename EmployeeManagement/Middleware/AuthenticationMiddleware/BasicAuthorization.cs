using System.Text;

namespace EmployeeManagement.Middleware.AuthenticationMiddleware
{
    public class BasicAuthorization
    {
        private readonly RequestDelegate next;
        private readonly string relm;
        public BasicAuthorization(RequestDelegate Next,string Relm)
        {
            next = Next;
            relm = Relm;
        }
        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            var header = context.Request.Headers["Authorization"].ToString();
            var EncodeCredentials = header.Substring(6);
            var credentials=Encoding.UTF8.GetString(Convert.FromBase64String(EncodeCredentials));
            string[] Useridpassword=credentials.Split(':');
            var userid = Useridpassword[0];
            var password = Useridpassword[1];
            if(userid !="admin" || password != "admin")
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            await next(context);
        }
    }
}
