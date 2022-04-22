using System.Text;

namespace WebApiUsingBasicAuthMiddleware.Middleware
{
    public class BasicAuthenticationMiddleware
    {
        const string USER_NAME = "admin";

        const string PASSWORD = "admin";

        readonly RequestDelegate _next;

        public BasicAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string header = context.Request.Headers["Authorization"];

            if (header != null && header.StartsWith("Basic"))
            {
                var encodedCredentials = header.Substring("Basic".Length).Trim();
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                var separatorIndex = credentials.IndexOf(':');
                var userName = credentials.Substring(0, separatorIndex);
                var password = credentials.Substring(separatorIndex + 1);

                if (userName == USER_NAME && password == PASSWORD)
                {
                    await _next(context);
                    return;
                }
            }

            // ブラウザの認証ダイアログを出すには、レスポンスヘッダーに
            // WWW-Authenticate: Baic が必要
            context.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Response.StatusCode = 401;
        }
    }
}
