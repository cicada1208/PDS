using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class HostUtil
    {
        /// <summary>
        /// 取得 HostName
        /// </summary>
        public static string GetHostName() =>
            Dns.GetHostName();

        /// <summary>
        /// 取得內網 IP Address
        /// </summary>
        public static string GetHostAddress()
        {
            string result = string.Empty;
            var ipAddress = Dns.GetHostAddresses(GetHostName());
            foreach( IPAddress ipa in ipAddress)
            {
                if (ipa.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    result = ipa.ToString();
            }
            return result;
        }

        /// <summary>
        /// 取得內網 IP Address 與 HostName
        /// </summary>
        public static string GetHostNameAndAddress() =>
            $"{GetHostAddress()}({GetHostName()})";

    }
}
