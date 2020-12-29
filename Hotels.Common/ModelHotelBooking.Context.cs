﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotels.Common
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HotelBookingDBEntities : DbContext
    {
        public HotelBookingDBEntities()
            : base("name=HotelBookingDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AvailabilityRe> AvailabilityRes { get; set; }
        public virtual DbSet<AvailabilityRoom> AvailabilityRooms { get; set; }
        public virtual DbSet<availabilityRoomRe> availabilityRoomRes { get; set; }
        public virtual DbSet<BookingConfirmationData> BookingConfirmationDatas { get; set; }
        public virtual DbSet<BookingPriceChange> BookingPriceChanges { get; set; }
        public virtual DbSet<CancellationBooking> CancellationBookings { get; set; }
        public virtual DbSet<CancellationRoom> CancellationRooms { get; set; }
        public virtual DbSet<CancelPolicy> CancelPolicies { get; set; }
        public virtual DbSet<CheckAvailabiltyReq> CheckAvailabiltyReqs { get; set; }
        public virtual DbSet<ConfirmationRequest> ConfirmationRequests { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<HotelBookingCustomerCancelation> HotelBookingCustomerCancelations { get; set; }
        public virtual DbSet<HotelBookingDelivery> HotelBookingDeliveries { get; set; }
        public virtual DbSet<HotelBookingNight> HotelBookingNights { get; set; }
        public virtual DbSet<HotelBookingNum> HotelBookingNums { get; set; }
        public virtual DbSet<HotelBookingPax> HotelBookingPaxs { get; set; }
        public virtual DbSet<HotelBookingRoom> HotelBookingRooms { get; set; }
        public virtual DbSet<HotelBookingRoomsStatu> HotelBookingRoomsStatus { get; set; }
        public virtual DbSet<HotelBookingRoomStatusValue> HotelBookingRoomStatusValues { get; set; }
        public virtual DbSet<HotelBookingStatu> HotelBookingStatus { get; set; }
        public virtual DbSet<HotelBookingStatusValue> HotelBookingStatusValues { get; set; }
        public virtual DbSet<HotelsBooking> HotelsBookings { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<PointsOfSale> PointsOfSales { get; set; }
        public virtual DbSet<SourceTraffic> SourceTraffics { get; set; }
    }
}
