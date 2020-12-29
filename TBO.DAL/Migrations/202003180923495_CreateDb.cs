namespace TBO.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityCode = c.String(),
                        CityName = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                        CountryCode = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        FacilityId = c.Int(nullable: false, identity: true),
                        FacilityVal = c.String(),
                        HotelCode = c.String(),
                        HotelDetailId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FacilityId)
                .ForeignKey("dbo.HotelDetails", t => t.HotelDetailId, cascadeDelete: true)
                .Index(t => t.HotelDetailId);
            
            CreateTable(
                "dbo.HotelDetails",
                c => new
                    {
                        HotelDetailId = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        HotelLocation = c.String(),
                        CountryName = c.String(),
                        CountryCode = c.String(),
                        Description = c.String(),
                        FaxNumber = c.String(),
                        Map = c.String(),
                        PhoneNumber = c.String(),
                        PinCode = c.String(),
                        HotelWebsiteUrl = c.String(),
                        TripAdvisorRating = c.String(),
                        TripAdvisorReviewURL = c.String(),
                        CityName = c.String(),
                        HotelCode = c.String(),
                        HotelName = c.String(),
                        HotelRating = c.String(),
                        Attraction = c.String(),
                        CityCode = c.String(),
                    })
                .PrimaryKey(t => t.HotelDetailId);
            
            CreateTable(
                "dbo.HotelImages",
                c => new
                    {
                        HotelImageId = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                        HotelCode = c.String(),
                        HotelDetailId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HotelImageId)
                .ForeignKey("dbo.HotelDetails", t => t.HotelDetailId, cascadeDelete: true)
                .Index(t => t.HotelDetailId);
            
            CreateTable(
                "dbo.RoomImages",
                c => new
                    {
                        RoomImageId = c.Int(nullable: false, identity: true),
                        RoomName = c.String(),
                        RoomTypeCode = c.String(),
                        URL = c.String(),
                        HotelCode = c.String(),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomImageId)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        RoomName = c.String(),
                        RoomTypeCode = c.String(),
                        HotelCode = c.String(),
                    })
                .PrimaryKey(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomImages", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.HotelImages", "HotelDetailId", "dbo.HotelDetails");
            DropForeignKey("dbo.Facilities", "HotelDetailId", "dbo.HotelDetails");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropIndex("dbo.RoomImages", new[] { "RoomId" });
            DropIndex("dbo.HotelImages", new[] { "HotelDetailId" });
            DropIndex("dbo.Facilities", new[] { "HotelDetailId" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropTable("dbo.Rooms");
            DropTable("dbo.RoomImages");
            DropTable("dbo.HotelImages");
            DropTable("dbo.HotelDetails");
            DropTable("dbo.Facilities");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
