using ChipChip613.Models;
using Data.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChipChip613.Controllers
{
    public class NhapHangsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public NhapHangViewModel NhapHangVM { get; set; }

        public NhapHangsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            NhapHangVM = new NhapHangViewModel()
            {
                NhapHang = new Data.Models.NhapHang()
            };
        }

        public IActionResult Index(string searchString = null, string searchFromDate = null, string searchToDate = null, string trangThai = null, int page = 1)
        {
            NhapHangVM.StrUrl = UriHelper.GetDisplayUrl(Request);
            ViewBag.searchString = searchString;
            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;
            ViewBag.tinhTrang = trangThai;

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

            NhapHangVM.NhapHangs = _unitOfWork.nhapHangRepository.ListNhapHang(searchString, searchFromDate, searchToDate, trangThai, page);
            if (NhapHangVM.NhapHangs == null)
            {
                NhapHangVM.NhapHangs = _unitOfWork.nhapHangRepository.ListNhapHang("", "", "","", 1);
                SetAlert("Lỗi định dạng ngày tháng.", "error");
            }
            NhapHangVM.TinhTrangs = ListTinhTrang();
            return View(NhapHangVM);
        }

        public IActionResult Create(string strUrl)
        {
            NhapHangVM.StrUrl = strUrl;
            NhapHangVM.NhapHang.DonGia = 0;
            NhapHangVM.NhapHang.SoLuong = 1;
            return View(NhapHangVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(NhapHangVM);
            }

            //NguyenLieuVM.NguyenLieu = new Data.Models.NguyenLieu();
            NhapHangVM.NhapHang.NgayTao = DateTime.Now;
            NhapHangVM.NhapHang.NguoiTao = "Admin";
            NhapHangVM.NhapHang.ThanhTienLuu = NhapHangVM.NhapHang.ThanhTien;
            NhapHangVM.NhapHang.SoLuongLuu = NhapHangVM.NhapHang.SoLuong;
            NhapHangVM.NhapHang.DVTLuu = NhapHangVM.NhapHang.DVT;

            try
            {
                _unitOfWork.nhapHangRepository.Create(NhapHangVM.NhapHang);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(NhapHangVM);
            }
        }

        public IActionResult Edit(long id, string strUrl)
        {
            NhapHangVM.StrUrl = strUrl;
            if (id == 0)
                return NotFound();

            NhapHangVM.NhapHang = _unitOfWork.nhapHangRepository.GetById(id);

            if (NhapHangVM.NhapHang == null)
                return NotFound();

            return View(NhapHangVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(long id, string strUrl)
        {
            if (id != NhapHangVM.NhapHang.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var nhapHang = _unitOfWork.nhapHangRepository.GetSingleNoTracking(x => x.Id == id);
                
                // neu co sua so luong ==> thanhtien thay doi
                if(nhapHang.SoLuong != NhapHangVM.NhapHang.SoLuong)
                {
                    NhapHangVM.NhapHang.SoLuongLuu = NhapHangVM.NhapHang.SoLuong;
                    NhapHangVM.NhapHang.ThanhTienLuu = NhapHangVM.NhapHang.ThanhTien;
                }
                // neu co sua don vi tinh
                if (nhapHang.DVT != NhapHangVM.NhapHang.DVT)
                {
                    NhapHangVM.NhapHang.DVTLuu = NhapHangVM.NhapHang.DVT;
                }

                try
                {
                    _unitOfWork.nhapHangRepository.Update(NhapHangVM.NhapHang);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    ModelState.AddModelError("", ex.Message);
                    return View(NhapHangVM);
                }
            }

            return View(NhapHangVM);
        }

        public IActionResult Details(long id, string strUrl)
        {
            NhapHangVM.StrUrl = strUrl;

            if (id == 0)
                return NotFound();

            var nhapHang = _unitOfWork.nhapHangRepository.GetById(id);
            NhapHangVM.NhapHang = nhapHang;
            if (nhapHang == null)
                return NotFound();

            return View(NhapHangVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id, string strUrl)
        {
            var nhapHang = _unitOfWork.nhapHangRepository.GetById(id);
            var chiPhis = await _unitOfWork.chiPhiRepository.FindAsync(x => x.NhapHangId == id);
            if (nhapHang == null)
                return NotFound();
            try
            {
                _unitOfWork.nhapHangRepository.Delete(nhapHang);
                foreach(var item in chiPhis)
                {
                    _unitOfWork.chiPhiRepository.Delete(item);
                }
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

        public JsonResult GetThanhTien(decimal donGia = 0, decimal soLuong = 1, decimal chiPhiKhac = 0)
        {

            var thanhTien = donGia * (decimal)soLuong + chiPhiKhac;
            return Json(new
            {
                thanhTien = thanhTien
            });
        }

        private List<ListViewModel> ListTinhTrang()
        {
            return new List<ListViewModel>()
            {
                new ListViewModel() { Value = "", Name = "-- select --" },
                new ListViewModel() { Value = "true", Name = "Còn hàng" },
                new ListViewModel() { Value = "false", Name = "Hết hàng" }
            };

        }

        public IActionResult DetailsRedirect(string strUrl)
        {
            return Redirect(strUrl);
        }
    }
}