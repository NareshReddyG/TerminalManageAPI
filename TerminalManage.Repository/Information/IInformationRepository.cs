using System.Threading.Tasks;

namespace TerminalManage.Repository.Information
{
    public interface IInformationRepository
    {
        Task<string> GetLogContent();
    }
}
