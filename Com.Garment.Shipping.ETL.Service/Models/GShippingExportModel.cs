using System;

namespace Com.Garment.Shipping.ETL.Service.Models
{
    public class GShippingExportModel
    {
        public GShippingExportModel()
        {

        }
        public GShippingExportModel(int idPackingLists, string invoiceNo, DateTimeOffset truckingDate, string buyerAgentCode, string buyerAgentName, string destination, string sectionCode, int packingListId, int idShippingInvoice, int garmentShippingInvoiceId, string buyerBrandName, string comodityCode, string comodityName, string unitCode, double quantity, string uomUnit, double cMTPrice, double amount)
        {
            IdPackingLists = idPackingLists;
            InvoiceNo = invoiceNo;
            TruckingDate = truckingDate;
            BuyerAgentCode = buyerAgentCode;
            BuyerAgentName = buyerAgentName;
            Destination = destination;
            SectionCode = sectionCode;
            PackingListId = packingListId;
            IdShippingInvoice = idShippingInvoice;
            GarmentShippingInvoiceId = garmentShippingInvoiceId;
            BuyerBrandName = buyerBrandName;
            ComodityCode = comodityCode;
            ComodityName = comodityName;
            UnitCode = unitCode;
            Quantity = quantity;
            UomUnit = uomUnit;
            CMTPrice = cMTPrice;
            Amount = amount;
        }

        public int IdPackingLists { get; set; }
        public string InvoiceNo { get; set; }
        public DateTimeOffset TruckingDate { get; set; }
        public string BuyerAgentCode { get; set; }
        public string BuyerAgentName { get; set; }
        public string Destination { get; set; }
        public string SectionCode { get; set; }
        public int PackingListId { get; set; }
        public int IdShippingInvoice { get; set; }
        public int GarmentShippingInvoiceId { get; set; }
        public string BuyerBrandName { get; set; }
        public string ComodityCode { get; set; }
        public string ComodityName { get; set; }
        public string UnitCode { get; set; }
        public double Quantity { get; set; }
        public string UomUnit { get; set; }
        public double CMTPrice { get; set; }
        public double Amount { get; set; }
    }
}