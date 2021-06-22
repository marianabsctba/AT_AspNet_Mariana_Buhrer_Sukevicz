using System;
using System.Collections.Generic;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Repositories
{
    public interface IDonationRepository
    {
        IEnumerable<DonationModel> GetAll(
            bool orderAscendant,
            string search = null);
        DonationModel GetById(int id);
        DonationModel Create(DonationModel donationModel);
        DonationModel Edit(DonationModel donationModel);
        void Delete(int id);
    }
}