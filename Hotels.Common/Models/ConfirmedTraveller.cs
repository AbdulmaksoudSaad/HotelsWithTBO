using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
   public class ConfirmedTraveller
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> ChildAge { get; set; }
        public ConfirmedTraveller()
        {
            ChildAge = new List<string>(); 
        }

    }
}
