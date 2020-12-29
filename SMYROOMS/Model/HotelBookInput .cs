using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
  public  class HotelBookInput
    {
        public string optionRefId { get; set; }
        public string language { get; set; }
        public string clientReference { get; set; }
        public HolderInput holder { get; set; }
        public DeltaPriceInput deltaPrice { get; set; }
        public PaymentCardInput paymentCard { get; set; }
        public List<BookRoomInput> rooms { get; set; }
        public HotelBookInput()
        {
            holder = new HolderInput();
            deltaPrice = new DeltaPriceInput();
            paymentCard = new PaymentCardInput();
            rooms = new List<BookRoomInput>();

        }
    }
}
