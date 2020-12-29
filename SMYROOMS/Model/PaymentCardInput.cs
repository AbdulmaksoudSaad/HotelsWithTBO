using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
    public class PaymentCardInput
    {
        public string cardType { get; set; }
        public string number { get; set; }
        public HolderInput holder { get; set; }
        public ExpireDateInput expire { get; set; }
        public string CVC { get; set; }

        public PaymentCardInput()
        {
            holder = new HolderInput();
            expire = new ExpireDateInput();
        }
    }
}
