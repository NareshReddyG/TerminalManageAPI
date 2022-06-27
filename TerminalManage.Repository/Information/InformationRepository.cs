using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TerminalManage.Models.ViewModels;

namespace TerminalManage.Repository.Information
{
    public class InformationRepository : IInformationRepository
    {
     
        public async Task<string> GetLogContent()
        {
            var logContent = string.Empty;
            var location = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "DataFiles");

            foreach (string file in Directory.EnumerateFiles(location, "*.log"))
            {
                logContent += await File.ReadAllTextAsync(file);
            }
            return logContent;
        }
    }
}
