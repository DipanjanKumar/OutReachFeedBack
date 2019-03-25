using System.Collections.Generic;

namespace OutReachFeedBack.Models
{
    public class DashboardDTO
    {
        public List<string> LocationList { get; set; }
        public Dictionary<string, string> EventwithID { get; set; }
    }
}