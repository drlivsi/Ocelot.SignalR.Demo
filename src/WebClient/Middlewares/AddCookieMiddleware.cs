namespace WebClient.Middlewares
{
    public class AddCookieMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!context.Request.Cookies.ContainsKey("LbCookie"))
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1)
                };

                context.Response.Cookies.Append("LbCookie", Guid.NewGuid().ToString(), cookieOptions);
            }

            await next.Invoke(context);
        }
    }
}
