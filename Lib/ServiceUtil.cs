using System;
using System.ServiceModel;
using System.Web.Services.Description;

namespace Lib
{
    public class ServiceUtil
    {
        private static ADWebService.AdUserIdentifySoapClient _ADProxy;
        /// <summary>
        /// AD WebService
        /// </summary>
        public static ADWebService.AdUserIdentifySoapClient ADProxy
        {
            get
            {
                if (_ADProxy == null ||
                    _ADProxy.State == CommunicationState.Closed ||
                    _ADProxy.State == CommunicationState.Faulted)
                {
                    _ADProxy?.Abort();
                    String baseAddress = "http://cychwcf1/WebTeam/UserIdentify/Authenticate.asmx";
                    EndpointAddress endpoint = new EndpointAddress(baseAddress);
                    BasicHttpBinding binding = new BasicHttpBinding();
                    //binding.SendTimeout = new TimeSpan(0,1,0);
                    //binding.OpenTimeout = new TimeSpan(0, 1, 0);
                    //binding.MaxReceivedMessageSize = 65536;
                    _ADProxy = new ADWebService.AdUserIdentifySoapClient(binding, endpoint);
                }
                return _ADProxy;
            }
        }

        private static UdMisPicWebService.WebUdMisPicSoapClient _UdMisPicProxy;
        /// <summary>
        /// UdPic WebService
        /// </summary>
        public static UdMisPicWebService.WebUdMisPicSoapClient UdMisPicProxy
        {
            get
            {
                if (_UdMisPicProxy == null ||
                    _UdMisPicProxy.State == CommunicationState.Closed ||
                    _UdMisPicProxy.State == CommunicationState.Faulted)
                {
                    _UdMisPicProxy?.Abort();
                    String baseAddress = "http://www.cych.org.tw/pharm/webudmispic.asmx";
                    EndpointAddress endpoint = new EndpointAddress(baseAddress);
                    BasicHttpBinding binding = new BasicHttpBinding();
                    binding.SendTimeout = new TimeSpan(0, 0, 2); 
                    _UdMisPicProxy = new UdMisPicWebService.WebUdMisPicSoapClient(binding, endpoint);
                }
                return _UdMisPicProxy;
            }
        }

        //private static ARWcfService.ARServiceClient _ARProxy;
        ///// <summary>
        ///// NIS ARWcfService
        ///// </summary>
        //public static ARWcfService.ARServiceClient ARProxy
        //{
        //    get
        //    {
        //        if (_ARProxy == null ||
        //            _ARProxy.State == CommunicationState.Closed ||
        //            _ARProxy.State == CommunicationState.Faulted)
        //        {
        //            _ARProxy?.Abort();
        //            String baseAddress = "http://H3680/NisWebServiceSetup_test/ARService.svc";
        //            Uri uri = new Uri(baseAddress);
        //            EndpointIdentity identity = new DnsEndpointIdentity("localhost");
        //            EndpointAddress endpoint = new EndpointAddress(uri, identity);
        //            WSHttpBinding binding = new WSHttpBinding();
        //            binding.Security = new WSHttpSecurity();
        //            binding.Security.Mode = SecurityMode.None;
        //            _ARProxy = new ARWcfService.ARServiceClient(binding, endpoint);
        //        }
        //        return _ARProxy;
        //    }
        //}

    }
}
