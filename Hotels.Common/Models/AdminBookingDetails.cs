using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Common.Models
{
    
    public class CBNumberData
    {
        public int PointOfSale { get; set; }
        public List<PointsOfSale> PointsOfSaleList { get; set; }
        public int Language { get; set; }
        public List<Language> LanguagesList { get; set; }
        public int? Source { get; set; }
        public List<SourceTraffic> SourceList { get; set; }
        public int Supplier { get; set; }
        public List<object> SuppliersList { get; set; }
        public int Serial { get; set; }
        public CBNumberData()
        {
            PointsOfSaleList = new List<PointsOfSale>();
            LanguagesList = new List<Language>();
            SourceList = new List<SourceTraffic>();
        }
    }

    public class BookingStatusList
    {
        public int ID { get; set; }
        public string Booking_No { get; set; }
        public string Booking_Status { get; set; }
        public DateTime Status_Time { get; set; }
        public string Agent_Id { get; set; }
        public string AgentName { get; set; }
        public string Comment { get; set; }
        public string Remark { get; set; }
    }

    public class CustomerCancellation
    {
        public int ID { get; set; }
        public string Booking_No { get; set; }
        public int Room_No { get; set; }
        public DateTime Date_From { get; set; }
        public object Date_To { get; set; }
        public object Amend_Restriction { get; set; }
        public object Cancel_Restriction { get; set; }
        public double Cancel_Amount { get; set; }
        public double Customer_Cancel_Amount { get; set; }
        public double No_Show_Amount { get; set; }
        public string Rule_Text { get; set; }
        public int Markup_Id { get; set; }
        public double Markup_Amount { get; set; }
    }

    public class SupplierCancellation
    {
        public int ID { get; set; }
        public string Booking_No { get; set; }
        public int Room_No { get; set; }
        public DateTime Date_From { get; set; }
        public object Date_To { get; set; }
        public string Amend_Restriction { get; set; }
        public string Cancel_Restriction { get; set; }
        public double Cancel_Amount { get; set; }
        public double Customer_Cancel_Amount { get; set; }
        public double No_Show_Amount { get; set; }
    }

    public class BookingPassenger
    {
        public string bookingNo { get; set; }
        public int roomNo { get; set; }
        public string paxType { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string salutation { get; set; }
        public string nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DateOfBirth_string { get; set; }
        public int PassengerId { get; set; }
        public string phone { get; set; }
        public string phoneCode { get; set; }
    }

    public class BookingNight
    {
        public string bookingNo { get; set; }
        public DateTime nightDate { get; set; }
        public double rate { get; set; }
        public string currency { get; set; }
        public int roomNo { get; set; }
        public double exchangeRate { get; set; }
        public double rateInDinar { get; set; }
    }

    public class RoomStatusList
    {
        public int ID { get; set; }
        public string Status { get; set; }
    }

    public class BookingRoom
    {
        public string bookingNo { get; set; }
        public int roomNo { get; set; }
        public string roomCategory { get; set; }
        public string roomType { get; set; }
        public string meal { get; set; }
        public string specialReq { get; set; }
        public int paxQty { get; set; }
        public int AdultsNo { get; set; }
        public int ChildrenNo { get; set; }
        public string ChildrenAges { get; set; }
        public string Cancellations { get; set; }
        public List<CustomerCancellation> CustomerCancellations { get; set; }
        public List<SupplierCancellation> SupplierCancellations { get; set; }
        public List<BookingPassenger> bookingPassengers { get; set; }
        public List<BookingNight> bookingNights { get; set; }
        public double TotalCostPerRoom { get; set; }
        public int RoomStatusID { get; set; }
        public string RoomStatus { get; set; }
        public string Remark { get; set; }
        public List<RoomStatusList> RoomStatusList { get; set; }
        public BookingRoom()
        {
            CustomerCancellations = new List<CustomerCancellation>();
            SupplierCancellations = new List<SupplierCancellation>();
            bookingPassengers = new List<BookingPassenger>();
            bookingNights = new List<BookingNight>();
            RoomStatusList = new List<RoomStatusList>();
        }
    }

    public class CityTaxModel
    {
        public int ID { get; set; }
        public string CityID { get; set; }
        public string Type { get; set; }
        public double Value { get; set; }
        public string Currency { get; set; }
    }

    public class CityTourismTaxModel
    {
        public int ID { get; set; }
        public string CityID { get; set; }
        public string Type { get; set; }
        public double Value { get; set; }
        public string Currency { get; set; }
        public int StarRating { get; set; }
    }

    public class BookingDelivery
    {
        public int Id { get; set; }
        public string Booking_No { get; set; }
        public string DeliveryId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneCode { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime BookingTime { get; set; }
        public string BookingStatus { get; set; }
        public string DeliveryDate { get; set; }
        public string DeliveryStatus { get; set; }
        public string DeliveryCost { get; set; }
    }

    public class AdminBookingDetails
    {
        public string bookingNo { get; set; }
        public CBNumberData CBNumberData { get; set; }
        public string hotel { get; set; }
        public string hotelName { get; set; }
        public List<object> HotelsList { get; set; }
        public string city { get; set; }
        public string cityName { get; set; }
        public List<object> CitiesList { get; set; }
        public object country { get; set; }
        public string countryName { get; set; }
        public List<object> CountriesList { get; set; }
        public DateTime checkIN { get; set; }
        public DateTime checkOut { get; set; }
        public int roomsQty { get; set; }
        public int paxQty { get; set; }
        public string hotelConfirmationNo { get; set; }
        public DateTime bookingTime { get; set; }
        public List<object> bookingStatus { get; set; }
        public string FormOfPayment { get; set; }
        public List<object> FormOfPaymentList { get; set; }
        public string customerID { get; set; }
        public string customerPhone { get; set; }
        public string customerFirstName { get; set; }
        public string customerLastName { get; set; }
        public string customerEmail { get; set; }
        public string customerAddress { get; set; }
        public DateTime customerDoB { get; set; }
        public object customerPhoneCode { get; set; }
        public string paxName { get; set; }
        public string provider { get; set; }
        public double sellPrice { get; set; }
        public List<object> SellCurrencyList { get; set; }
        public string sellCurrency { get; set; }
        public double sellCurrencyExchRate { get; set; }
        public double localSellPrice { get; set; }
        public double costAmount { get; set; }
        public string costCurrency { get; set; }
        public double costCurrencyExchRate { get; set; }
        public double localCostAmount { get; set; }
        public string lastBookingStatus { get; set; }
        public string TheBookingStatus { get; set; }
        public List<BookingStatusList> BookingStatusList { get; set; }
        public string bookingConf { get; set; }
        public string hotelConf { get; set; }
        public string salesChannel { get; set; }
        public double ForeignAmount { get; set; }
        public double TotalCostDinars { get; set; }
        public object PaymnetCharge { get; set; }
        public List<BookingRoom> bookingRooms { get; set; }
        public double SupplierCost { get; set; }
        public CityTaxModel CityTax { get; set; }
        public CityTourismTaxModel CityTourismTax { get; set; }
        public string bookingPhoneCode { get; set; }
        public bool DelivaryRequested { get; set; }
        public BookingDelivery bookingDelivery { get; set; }
        public AdminBookingDetails()
        {
            bookingDelivery = new BookingDelivery();
            CityTourismTax = new CityTourismTaxModel();
            CityTax = new CityTaxModel();
            bookingRooms = new List<BookingRoom>();
            BookingStatusList = new List<BookingStatusList>();
            CBNumberData = new CBNumberData();
        }
    }
}
