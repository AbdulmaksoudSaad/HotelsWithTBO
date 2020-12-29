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
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SearchDBEntities : DbContext
    {
        public SearchDBEntities()
            : base("name=SearchDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CheckAvailability> CheckAvailabilities { get; set; }
        public virtual DbSet<HotelPackage> HotelPackages { get; set; }
        public virtual DbSet<SearchCriteria> SearchCriterias { get; set; }
        public virtual DbSet<SearchHotelResult> SearchHotelResults { get; set; }
        public virtual DbSet<SearchRoomData> SearchRoomDatas { get; set; }
        public virtual DbSet<SearchRoomResult> SearchRoomResults { get; set; }
        public virtual DbSet<SearchSession> SearchSessions { get; set; }
        public virtual DbSet<Supplement> Supplements { get; set; }
        public virtual DbSet<ProviderSession> ProviderSessions { get; set; }
    
        public virtual int SaveRoomResult()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SaveRoomResult");
        }
    
        public virtual int SaveSearchResult()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SaveSearchResult");
        }
    
        public virtual int SaveSearchResultForChannel()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SaveSearchResultForChannel");
        }
    }
}
