using System.Linq;
using DonationApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data.Models;
using Infrastructure.Data.Repositories;

namespace DonationApp.Controllers
{
    public class DonationController : Controller
    {
        private readonly IDonationRepository _donationRepository;

        public DonationController(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        // GET: DonationController
        public IActionResult Index(DonationIndexViewModel donationIndexRequest)
        {
            var donations = _donationRepository
                .GetAll(donationIndexRequest.OrderAscendant, donationIndexRequest.Search)
                .ToList();

            var donationIndexViewModel = new DonationIndexViewModel
            {
                Search = donationIndexRequest.Search,
                Donations = donations,
                OrderAscendant = donationIndexRequest.OrderAscendant
            };

            return View(donationIndexViewModel);
        }

        // GET: DonationController/Details/5
        public IActionResult Details(int id)
        {
            var donation = _donationRepository.GetById(id);

            if (donation != null)
            {
                return View(donation);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: DonationController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DonationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DonationModel donationModel)
        {
            try
            {
                _donationRepository.Create(donationModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DonationController/Edit/5
        public IActionResult Edit(int id)
        {
            var donation = _donationRepository.GetById(id);

            if (donation != null)
            {
                return View(donation);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: DonationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DonationModel donationModel)
        {
            try
            {
                var editDonation = _donationRepository.Edit(donationModel);

                if (editDonation != null)
                {
                    return RedirectToAction(nameof(Details), new { id = editDonation.Id });
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DonationController/Delete/5
        public IActionResult Delete(int id)
        {
            var donation = _donationRepository.GetById(id);

            if (donation != null)
            {
                return View(donation);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: DonationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFromMemory(int id)
        {
            try
            {
                _donationRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
