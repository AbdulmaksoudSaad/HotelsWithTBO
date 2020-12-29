using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
  public  class ValidationData
    {
        public string optionRefId { get; set; }
        public string status { get; set; }
        public Price price { get; set; }
        public CancelPolicy cancelPolicy { get; set; }
        public ValidationData()
        {
            price = new Price();
            cancelPolicy = new CancelPolicy();
        }

    }
}
