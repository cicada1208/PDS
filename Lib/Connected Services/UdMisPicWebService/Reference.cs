﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.42000
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lib.UdMisPicWebService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UdMisPicWebService.WebUdMisPicSoap")]
    public interface WebUdMisPicSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebUdMisPic", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string WebUdMisPic(string searchword);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebUdMisPic", ReplyAction="*")]
        System.Threading.Tasks.Task<string> WebUdMisPicAsync(string searchword);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WebUdMisPicSoapChannel : Lib.UdMisPicWebService.WebUdMisPicSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebUdMisPicSoapClient : System.ServiceModel.ClientBase<Lib.UdMisPicWebService.WebUdMisPicSoap>, Lib.UdMisPicWebService.WebUdMisPicSoap {
        
        public WebUdMisPicSoapClient() {
        }
        
        public WebUdMisPicSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebUdMisPicSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebUdMisPicSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebUdMisPicSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string WebUdMisPic(string searchword) {
            return base.Channel.WebUdMisPic(searchword);
        }
        
        public System.Threading.Tasks.Task<string> WebUdMisPicAsync(string searchword) {
            return base.Channel.WebUdMisPicAsync(searchword);
        }
    }
}
