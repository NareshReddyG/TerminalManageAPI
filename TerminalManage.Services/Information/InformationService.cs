using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TerminalManage.Models.ViewModels;
using TerminalManage.Repository.Information;

namespace TerminalManage.Services.Information
{
    public class InformationService : IInformationService
    {
        private readonly IInformationRepository _informationRepository;
        public InformationService(IInformationRepository informationRepository)
        {
            _informationRepository = informationRepository;
        }
        public async Task<IEnumerable<SummaryViewModel>> GetSummary()
        {
            var logs = await GetAllLog();
            // Get INFORMATION_TYPE":"REQUEST" as we have required data in Request item
            var requestLogs = logs.Where(x => x.Contains(@"REQUEST")).Select(x => JsonConvert.DeserializeObject<InformationRequestViewModel>(x.Replace("", "").Split(new char[] { '|' }).FirstOrDefault(x => IsJson(x) == true)));
           
            return requestLogs.Where(x => IsJson(x.RawRequest?.ToString()))
                .Select(x => JsonConvert.DeserializeObject<RawRequest>(x.RawRequest?.ToString()))
                .GroupBy(x => x.ApiId)
                .Select(x => new SummaryViewModel
                {
                    APIId = x.Key,
                    Count = x.Count()
                });

        }

        private async Task<string[]> GetAllLog()
        {
            var content = await _informationRepository.GetLogContent();
            //As log file entires are by newLine, split by NewLine 
            var contents = content.Split(new char[] { '' }, StringSplitOptions.RemoveEmptyEntries);
            return contents;
        }

        //Nice to have: Create a new library named TerminalManage.Utility and move there. 
        private bool IsJson(string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }
        public bool IsValidXml(string xml)
        {
            try
            {
                XDocument.Parse(xml);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<SummaryViewModel> GetSummaryById(string id)
        {
            var summary = new SummaryViewModel();
            var allLogs = await GetAllLog();

            // Get INFORMATION_TYPE":"REQUEST" as we have required data in Request item
            var logs = allLogs.Select(x=> x.Replace("", "")).Where(x=>x.Contains(id));
            
            // Request Information
            var requestLog = logs.Where(x => x.Contains("REQUEST")).Select(x=>x.Split(new char[] { '|' })).FirstOrDefault();
            var requestLogItem = JsonConvert.DeserializeObject <InformationRequestViewModel>(requestLog.FirstOrDefault(x => IsJson(x) == true || IsValidXml(x) == true));
           
            summary.RequestDateTime = Convert.ToDateTime(requestLog.FirstOrDefault(x => IsJson(x) == false && IsValidXml(x) == false));
            summary.Request = requestLogItem.RawRequest?.ToString();
                
            //Response Information
            var responseLog = logs.Where(x => x.Contains("RESPONSE")).Select(s => s.Split(new char[] { '|' })).FirstOrDefault();
            var responseLogItem = JsonConvert.DeserializeObject<InformationRequestViewModel>(responseLog.FirstOrDefault(x => IsJson(x) == true || IsValidXml(x) == true));

            summary.ResponseDateTime = Convert.ToDateTime(responseLog.FirstOrDefault(x => IsJson(x) == false && IsValidXml(x) == false));
            summary.Response = responseLogItem.Response?.ToString();
            summary.Duration = responseLogItem.Duration;

          
            return summary;

        }


    }
}
