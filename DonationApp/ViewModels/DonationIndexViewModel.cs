using System.Collections.Generic;
using Infrastructure.Data.Models;

namespace DonationApp.ViewModels
{
    public class DonationIndexViewModel
    {
        public string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<DonationModel> Donations { get; set; }
    }
}
