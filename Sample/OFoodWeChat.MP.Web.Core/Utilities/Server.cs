using Microsoft.AspNetCore.Http;

namespace OFoodWeChat.MP.Web.Core.Utilities
{
    public static class Server
    {
        public static HttpContext HttpContext
        {
            get
            {
                HttpContext context = new DefaultHttpContext();
                return context;
            }
        }
    }
}
