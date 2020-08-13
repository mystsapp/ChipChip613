using ChipChip613.Models;
using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChipChip613.Controllers
{
    public class ChiPhisController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public ChiPhiViewModel ChiPhiVM { get; set; }

        public ChiPhisController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            ChiPhiVM = new ChiPhiViewModel()
            {
                ChiPhi = new Data.Models.ChiPhi(),
                NhapHangs = _unitOfWork.nhapHangRepository.GetAll().OrderBy(x => x.Id)
            };
        }


        public IActionResult Index(string searchString = null, string searchFromDate = null, string searchToDate = null, int page = 1)
        {
            ChiPhiVM.StrUrl = UriHelper.GetDisplayUrl(Request);
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

            var nhapHangs = _unitOfWork.nhapHangRepository.GetAll();
            ChiPhiVM.ChiPhiDtos = _unitOfWork.chiPhiRepository.ListChiPhi(nhapHangs, searchString, searchFromDate, searchToDate, page);
            if (ChiPhiVM.ChiPhiDtos == null)
            {
                ChiPhiVM.ChiPhiDtos = _unitOfWork.chiPhiRepository.ListChiPhi(nhapHangs, "", "", "", 1);
                SetAlert("Lỗi định dạng ngày tháng.", "error");
            }
            return View(ChiPhiVM);
        }

        public IActionResult Create(long ddlHangNhapId, string strUrl)
        {
            ChiPhiVM.StrUrl = strUrl;

            // frm DdlHangNhap change
            if (ddlHangNhapId != 0)
            {
                NhapHang nhapHang = _unitOfWork.nhapHangRepository.GetById(ddlHangNhapId);
                ChiPhiVM.ChiPhi.NhapHangId = ddlHangNhapId;
                ChiPhiVM.ChiPhi.DonGia = nhapHang.DonGia;
                ChiPhiVM.ChiPhi.DVT = nhapHang.DVT;
                ChiPhiVM.ChiPhi.DVT2 = nhapHang.DVT2;
                ChiPhiVM.ChiPhi.SoLuong2 = nhapHang.SoLuong2;
                ChiPhiVM.ChiPhi.SoLuong = nhapHang.SoLuong;
                ChiPhiVM.ChiPhi.ThanhTien = nhapHang.DonGia * (decimal)ChiPhiVM.ChiPhi.SoLuong;

            }
            // frm DdlHangNhap change
          
          
            return View(ChiPhiVM);
        }


        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(string strUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(ChiPhiVM);
            }

            ChiPhiVM.ChiPhi.NgayTao = DateTime.Now;
            ChiPhiVM.ChiPhi.NguoiTao = "Admin";
            try
            {
                //  can tru ben hang nhap
                NhapHang nhapHang = new NhapHang();
                // neu co chon tu hang nhap
                if (ChiPhiVM.ChiPhi.NhapHangId != 0)
                {
                    nhapHang = _unitOfWork.nhapHangRepository.GetById(ChiPhiVM.ChiPhi.NhapHangId);
                    nhapHang.SoLuong = nhapHang.SoLuong - ChiPhiVM.ChiPhi.SoLuong;
                    if (nhapHang.SoLuong == 0)
                    {
                        nhapHang.TrangThai = false;
                        nhapHang.ThanhTien = 0;
                    }
                    else
                    {
                        nhapHang.ThanhTien = nhapHang.ThanhTien - (ChiPhiVM.ChiPhi.SoLuong * ChiPhiVM.ChiPhi.DonGia);
                    }
                    
                    if (ChiPhiVM.ChiPhi.SoLuong2 != 0 && nhapHang.SoLuong2 != 0)
                    {
                        nhapHang.SoLuong2 = nhapHang.SoLuong2 - ChiPhiVM.ChiPhi.SoLuong2;
                    }
                    _unitOfWork.nhapHangRepository.Update(nhapHang);
                }
                //  can tru ben hang nhap
                _unitOfWork.chiPhiRepository.Create(ChiPhiVM.ChiPhi);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return Redirect(strUrl);
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(ChiPhiVM);
            }
        }

        public IActionResult Edit(long id, string strUrl)
        {
            ChiPhiVM.StrUrl = strUrl;
            if (id == 0)
                return NotFound();

            ChiPhiVM.ChiPhi = _unitOfWork.chiPhiRepository.GetById(id);
            ChiPhiVM.SoLuongCu = ChiPhiVM.ChiPhi.SoLuong; // dung de can tru trong post
            ChiPhiVM.SoLuong2Cu = ChiPhiVM.ChiPhi.SoLuong2; // dung de can tru trong post

            if (ChiPhiVM.ChiPhi == null)
                return NotFound();

            return View(ChiPhiVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(long id, string strUrl)
        {
            if (id != ChiPhiVM.ChiPhi.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    //  can tru ben hang nhap //////////////////////////////////////////////////////////////
                    NhapHang nhapHang = new NhapHang();
                    if (ChiPhiVM.ChiPhi.NhapHangId != 0) // khi co chon hang nhap
                    {
                        nhapHang = _unitOfWork.nhapHangRepository.GetById(ChiPhiVM.ChiPhi.NhapHangId);

                        // neu thay doi soluong
                        if (ChiPhiVM.SoLuongCu != ChiPhiVM.ChiPhi.SoLuong)
                        {
                            // so luong ban dau truoc khi thay doi
                            var slBanDau = nhapHang.SoLuong + ChiPhiVM.SoLuongCu;
                            // can tru lai tu dau - sl con lai
                            nhapHang.SoLuong = slBanDau - ChiPhiVM.ChiPhi.SoLuong;

                        }
                        // neu thay doi soluong2
                        if (ChiPhiVM.SoLuong2Cu != ChiPhiVM.ChiPhi.SoLuong)
                        {
                            // so luong 2 ban dau
                            var slBanDau2 = nhapHang.SoLuong2 + ChiPhiVM.SoLuong2Cu;
                            // can tru lai tu dau - sl con lai
                            nhapHang.SoLuong2 = slBanDau2 - ChiPhiVM.ChiPhi.SoLuong2;

                        }

                        if (nhapHang.SoLuong == 0 || nhapHang.SoLuong2 == 0)
                        {
                            nhapHang.TrangThai = false;
                            // thanh tien con lai
                            nhapHang.ThanhTien = 0;
                        }
                        else
                        {
                            // thanh tien con lai
                            nhapHang.ThanhTien = nhapHang.ThanhTien - (ChiPhiVM.ChiPhi.SoLuong * ChiPhiVM.ChiPhi.DonGia);
                        }

                        _unitOfWork.nhapHangRepository.Update(nhapHang);
                    }
                    //  can tru ben hang nhap ////////////////////////////////////////////////////////////////////////

                    _unitOfWork.chiPhiRepository.Update(ChiPhiVM.ChiPhi);
                    await _unitOfWork.Complete();
                    SetAlert("Cập nhật thành công", "success");
                    return Redirect(strUrl);
                }
                catch (Exception ex)
                {
                    SetAlert(ex.Message, "error");
                    return View(ChiPhiVM);
                }
            }

            return View(ChiPhiVM);
        }

        public IActionResult Details(long id, string strUrl)
        {
            ChiPhiVM.StrUrl = strUrl;

            if (id == 0)
                return NotFound();

            var chiPhi = _unitOfWork.chiPhiRepository.GetById(id);

            if (chiPhi == null)
                return NotFound();

            ChiPhiVM.ChiPhi = chiPhi;

            return View(ChiPhiVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id, string strUrl)
        {
            var chiPhi = _unitOfWork.chiPhiRepository.GetById(id);
            if (chiPhi == null)
                return NotFound();
            try
            {
                _unitOfWork.chiPhiRepository.Delete(chiPhi);
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

        public JsonResult GetThanhTien(decimal donGia = 0, int soLuong = 1, decimal chiPhiKhac = 0)
        {
            var thanhTien = donGia * (decimal)soLuong + chiPhiKhac;
            return Json(new
            {
                thanhTien = thanhTien
            });
        }
        public JsonResult KiemTraSL(int soLuong = 1, long hangNhapId = 0)
        {
            var nhapHang = _unitOfWork.nhapHangRepository.GetById(hangNhapId);
            bool status = false;
            decimal sLuong2 = 0;
            decimal thanhTien = 0;

            if(soLuong <= nhapHang.SoLuong) // con hang
            {
                status = true;
                if(nhapHang.SoLuong2 != 0) // co dien sl 2
                {
                    sLuong2 = (soLuong * nhapHang.SoLuong2) / nhapHang.SoLuong;
                }
                thanhTien = nhapHang.DonGia * soLuong;
            }

            return Json(new
            {
                status = status,
                sLuong2 = sLuong2,
                thanhTien = thanhTien
            });
        }
        
        public JsonResult KiemTraSL2(decimal soLuong2 = 1, long hangNhapId = 0)
        {
            var nhapHang = _unitOfWork.nhapHangRepository.GetById(hangNhapId);
            bool status = false;
            decimal sLuong = 0;
            decimal thanhTien = 0;

            if(soLuong2 <= nhapHang.SoLuong2) // con hang
            {
                status = true;
                if(nhapHang.SoLuong != 0) // co dien sl 2
                {
                    sLuong = (soLuong2 * nhapHang.SoLuong) / nhapHang.SoLuong2;
                }
                thanhTien = nhapHang.DonGia * sLuong;
            }

            return Json(new
            {
                status = status,
                sLuong = sLuong,
                thanhTien = thanhTien
            });
        }
    }
}