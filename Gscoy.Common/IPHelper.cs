
namespace Gscoy.Common
{
    public class IPHelper
    {
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns>IP</returns>
        public static string GetClientIP()
        {
            string[] ipAddressArray = GetAllClientIP();
            string ipAddress = string.Empty;
            foreach (string str in ipAddressArray)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(str, @"(([01]?[\d]{1,2})|(2[0-4][\d])|(25[0-5]))(\.(([01]?[\d]{1,2})|(2[0-4][\d])|(25[0-5]))){3}"))
                {
                    ipAddress = str;
                    break;
                }
            }

            return ipAddress;
        }

        /// <summary>
        /// 获取客户端的端口
        /// </summary>
        /// <returns></returns>
        public static string GetClientPort()
        {
            System.Web.HttpContext httpContext = System.Web.HttpContext.Current;
            string port = httpContext.Request.ServerVariables["REMOTE_PORT"];
            return port ?? string.Empty;
        }

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns>IP</returns>
        public static string[] GetAllClientIP()
        {
            System.Web.HttpContext httpContext = System.Web.HttpContext.Current;
            string ipAddress = string.Empty;
            if (httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
            {
                ipAddress = httpContext.Request.ServerVariables["Remote_Addr"];
            }
            else
            {
                ipAddress = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            return ipAddress.Split(',');
        }
    }
}
