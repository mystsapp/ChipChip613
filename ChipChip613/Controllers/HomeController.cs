using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChipChip613.Models;
using Data.Repository;

namespace ChipChip613.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ChiPhiViewModel ChiPhiVM { get; set; }

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            ChiPhiVM = new ChiPhiViewModel
            {
                ChiPhi = new Data.Models.ChiPhi(),
                NhapHangs = _unitOfWork.nhapHangRepository.GetAll()
            };
        }

        public IActionResult Index(long ddlHangNhapId = 0)
        {
            if(ddlHangNhapId != 0)
            {
                ChiPhiVM.ChiPhi.NhapHangId = ddlHangNhapId;

            }
            return View(ChiPhiVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
