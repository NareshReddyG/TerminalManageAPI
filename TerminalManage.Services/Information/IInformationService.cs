using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TerminalManage.Models.ViewModels;

namespace TerminalManage.Services.Information
{
    public interface IInformationService
    {
        Task<IEnumerable<SummaryViewModel>> GetSummary();
        Task<SummaryViewModel> GetSummaryById(string id);
    }
}
