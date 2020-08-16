using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChipChip613.Models;
using Data.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ChipChip613.Controllers
{
    public class DonHangsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public DonHangViewModel DonHangVM { get; set; }

        public DonHangsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            DonHangVM = new DonHangViewModel()
            {
                DonHang = new Data.Models.DonHang(),
                ChiTietDonHang = new Data.Models.ChiTietDonHang()
            };
        }

        public IActionResult Index(string searchString = null, string searchFromDate = null, string searchToDate = null, int page = 1)
        {
            DonHangVM.StrUrl = UriHelper.GetDisplayUrl(Request);
            ViewBag.searchString = searchString;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;

            // for delete
            //if (id != 0)
            //{
            //    var nganhNghe = _unitOfWork.dMNganhNgheRepository.GetById(id);
            //    if (nganhNghe == null)
            //    {
            //        var lastId = _unitOfWork.dMNganhNgheRepository
            //                                  .GetAll().OrderByDescending(x => x.Id)
            //                                  .FirstOrDefault().Id;
            //        id = lastId;
            //    }
            //}

            DonHangVM.DonHangDtos = _unitOfWork.donHangRepository.ListDonHang(searchString, searchFromDate, searchToDate, page);
            return View(DonHangVM);
        }

        public IActionResult Create(string strUrl)
        {
            DonHangVM.StrUrl = strUrl;
            DonHangVM.DonHang.KhachHang = "Khách lẽ";

            DonHangVM.ChiTietDonHang.SoLuong = 1;
            return View(DonHangVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(DonHangVM);
            }

            //NganhNgheVM.DMNganhNghe = new Data.Models_IB.DMNganhNghe();
            DonHangVM.DonHang.NgayTao = DateTime.Now;
            DonHangVM.DonHang.NguoiTao = "Admin";
            try
            {
                _unitOfWork.donHangRepository.Create(DonHangVM.DonHang);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(DonHangVM);
            }

        }

        public async Task<IActionResult> Edit(long id, string strUrl)
        {
            DonHangVM.StrUrl = strUrl;
            if (id == 0)
                return NotFound();

            DonHangVM.DonHang = await _unitOfWork.donHangRepository.GetByIdAsync(id);

            if (DonHangVM.DonHang == null)
                return NotFound();

            return View(DonHangVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(long id, string strUrl)
        {
            if (id != DonHangVM.DonHang.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                //DonHangVM.DonHang.NgaySua = DateTime.Now;
                //DonHangVM.DonHang.NguoiSua = "Admin";
                try
                {

                    _unitOfWork.donHangRepository.Update(DonHangVM.DonHang);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(DonHangVM);
                }
            }

            return View(DonHangVM);
        }

        public async Task<IActionResult> Details(long id, string strUrl)
        {
            DonHangVM.StrUrl = strUrl;

            if (id == 0)
                return NotFound();

            var donHang = await _unitOfWork.donHangRepository.GetByIdAsync(id);
            DonHangVM.DonHang = donHang;
            if (donHang == null)
                return NotFound();

            return View(DonHangVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id, string strUrl)
        {
            var donHang = await _unitOfWork.donHangRepository.GetByIdAsync(id);
            if (donHang == null)
                return NotFound();
            try
            {
                _unitOfWork.donHangRepository.Delete(donHang);
                await _unitOfWork.Complete();
                SetAlert("Xóa thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return Redirect(strUrl);
            }
        }

        //public JsonResult IsStringNameAvailable(string TenCreate)
        //{
        //    var boolName = _unitOfWork.dMNganhNgheRepository.Find(x => x.TenNganhNghe.Trim().ToLower() == TenCreate.Trim().ToLower()).FirstOrDefault();
        //    if (boolName == null)
        //    {
        //        return Json(true);
        //    }
        //    else
        //    {
        //        return Json(false);
        //    }
        //}

    }
}