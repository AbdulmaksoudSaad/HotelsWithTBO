using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBO.DAL.Models.Context
{
    public class TBOContext:DbContext
    {
        public TBOContext() : base("TBOHotelsDB")
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<HotelDetail> HotelDetails { get; set; }
        public DbSet<HotelImage> HotelImages { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<Room> Rooms { get; set; }


    }
}
