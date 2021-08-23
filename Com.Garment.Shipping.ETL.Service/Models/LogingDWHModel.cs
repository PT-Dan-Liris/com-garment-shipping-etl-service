using System;
using Com.Garment.Shipping.ETL.Service.DBAdapters;

namespace Com.Garment.Shipping.ETL.Service.Models
{
    public class LogingDWHModel
    {
        public LogingDWHModel(int id, DateTime updatedAt, string updatedBy, bool status)
        {
            this.Id = id;
            this.UpdatedAt = updatedAt;
            this.UpdatedBy = updatedBy;
            this.Status = status;

        }
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool Status { get; set; }
    }
}