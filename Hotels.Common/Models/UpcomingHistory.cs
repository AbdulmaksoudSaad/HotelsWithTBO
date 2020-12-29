using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    public class UpcomingHistory
    {
        public List<ConfirmationModel> Upcoming { get; set; }
        public List<ConfirmationModel> Histories { get; set; }
        public UpcomingHistory()
            {
            Upcoming = new List<ConfirmationModel>();
            Histories = new List<ConfirmationModel>();
            }

    }
}
