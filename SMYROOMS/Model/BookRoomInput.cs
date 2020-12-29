using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.Model
{
   public  class BookRoomInput
    {
        public int occupancyRefId { get; set; }
        public List<BookPaxInput> paxes { get; set; }
        public BookRoomInput()
        {
            paxes = new List<BookPaxInput>();
        }
    }
}
