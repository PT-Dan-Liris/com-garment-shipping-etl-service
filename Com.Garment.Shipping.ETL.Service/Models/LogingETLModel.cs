using System;

namespace Com.Garment.Shipping.ETL.Service.Models
{
    public class LogingETLModel
    {
        public LogingETLModel(int id, string dataArea, DateTime updatedAt, string updatedBy, bool status)
        {
            this.Id = id;
            this.DataArea = dataArea;
            this.UpdatedAt = updatedAt;
            this.UpdatedBy = updatedBy;
            this.Status = status;

        }
        public int Id { get; set; }
        public string DataArea { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool Status { get; set; }
    }
}