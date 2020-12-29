using Hotels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.DAL
{
  public class AvailabiltyRooms
    {
        public static List<string> getRoomsReference(List<string> Rids,string Hotelcode ,string SessionId,string pid)
        {
            List<string> Rooms = new List<string>();
            SearchDBEntities db = new SearchDBEntities();
            int provider = int.Parse(pid);
            var RoomsDB = db.SearchRoomResults.Where(a => a.HotelCode == Hotelcode && a.ProviderId == provider && a.sID == SessionId).ToList();
            foreach (var item in RoomsDB)
            {
                
                if (Rids.Contains(item.RoomCode))
               {
                    Rooms.Add(item.RoomReference);
               }
            }
            return Rooms;
        }
        public static List<SearchRoomResult> getRoomsRules(  string Hotelcode, string SessionId, string pid)
        {
            List<string> Rooms = new List<string>();
            SearchDBEntities db = new SearchDBEntities();
            int provider = int.Parse(pid);
            var RoomsDB = db.SearchRoomResults.Where(a => a.HotelCode == Hotelcode && a.ProviderId == provider && a.sID == SessionId).ToList();
            
            return RoomsDB;
        }
    }
}
