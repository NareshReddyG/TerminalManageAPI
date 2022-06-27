using System;
using System.Collections.Generic;

namespace TerminalManage.Models.ViewModels
{
    public class ApiConnectionInformation
    {
        public string ApiUrl { get; set; }
        public object BillingUrl { get; set; }
        public string ApiUserName { get; set; }
        public string SoapApiUrl { get; set; }
        public string SoapApiUserName { get; set; }
    }

    public class InfoDictionary
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public int Type { get; set; }
        public bool IsGlobal { get; set; }
    }

    public class RawRequest
    {
        public string ApiId { get; set; }
        public ApiConnectionInformation ApiConnectionInformation { get; set; }
        public List<InfoDictionary> InfoDictionary { get; set; }
        public int ApiType { get; set; }
    }

    public class InformationRequestViewModel
    {
        public string INFORMATION_TYPE { get; set; }
        public string ID { get; set; }
        public string Duration { get; set; }
        public object UserId { get; set; }
        public int ApiType { get; set; }
        public string Host { get; set; }
        public object RawRequest { get; set; }
        public object Response { get; set; }
        //public DateTime? RequestDateTime { get; set; }
    }



}
