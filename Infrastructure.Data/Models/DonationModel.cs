using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Models
{
    public class DonationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Courier { get; set; }
        public int Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfRegister { get; set; }
        public bool Status { get; set; }

    }
}
