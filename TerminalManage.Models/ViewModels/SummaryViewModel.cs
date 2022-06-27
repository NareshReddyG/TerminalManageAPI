using System;

namespace TerminalManage.Models.ViewModels
{
    public class SummaryViewModel
    {
        public int Count { get; set; }
        public string APIId { get; set; }

        public DateTime RequestDateTime { get; set; }
        public DateTime ResponseDateTime { get; set; }

        // Will be in seconds
        public string Duration { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Id { get; set; }
    }
}
