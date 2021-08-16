using System;

namespace Com.Garment.Shipping.ETL.Service.Models
{
    public class GShippingLocalModel
    {
        public GShippingLocalModel()
        {

        }
        public GShippingLocalModel(int id, string noteNo, DateTimeOffset date, string buyerCode, string buyerName, string transactionTypeCode,string transactionTypeName ,int localSalesNoteId, double quantity, string uomUnit, double price, double amount)
        {
            Id = id;
            NoteNo = noteNo;
            Date = date;
            BuyerCode = buyerCode;
            BuyerName = buyerName;
            TransactionTypeCode = transactionTypeCode;
            TransactionTypeName = transactionTypeName;
            LocalSalesNoteId = localSalesNoteId;
            Quantity = quantity;
            UomUnit = uomUnit;
            Price = price;
            Amount = amount;
        }

        public int Id { get; set; }
        public string NoteNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string BuyerCode { get; set; }
        public string TransactionTypeCode { get; set; }
        public string TransactionTypeName { get; set; }
        public string BuyerName { get; set; }
        public int LocalSalesNoteId { get; set; }
        public double Quantity { get; set; }
        public string UomUnit { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
    }
}