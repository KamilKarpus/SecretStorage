using System;

namespace SS.Collections.Application.ReadModels
{
    public class LogsView
    {
        public Guid LogId { get; set; }
        public string AccesorName { get; set; }
        public int StatusId { get;set; }
        public string Status { get; set; }
        public DateTime Time { get; set; }
    }
}
