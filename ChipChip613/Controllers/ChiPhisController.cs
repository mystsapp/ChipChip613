using ChipChip613.Models;
using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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

            var listNhapHang = new List<NhapHang>();
            foreach (var item in ChiPhiVM.NhapHangs.OrderByDescending(x => x.NgayNhap).Where(x => x.TrangThai))
            {
                listNhapHang.Add(new NhapHang() { Id = item.Id, TenHang = item.TenHang + " - " + item.NgayNhap.ToString("dd/MM/yyyy") });
            }
            ChiPhiVM.NhapHangs = listNhapHang;

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
            string temp = "";

            if (!ModelState.IsValid)
            {
                return View(ChiPhiVM);
            }

            if (ChiPhiVM.ChiPhi.NgayTao == null)
            {
                ChiPhiVM.ChiPhi.NgayTao = DateTime.Now;
            }

            if (string.IsNullOrEmpty(ChiPhiVM.ChiPhi.ChiPhiKhac))
            {
                ChiPhiVM.ChiPhi.ChiPhiKhac = "";
            }

            ChiPhiVM.ChiPhi.NguoiTao = "Admin";

            try
            {
                //  can tru ben hang nhap
                NhapHang nhapHang = new NhapHang();
                NhapHang nhapHangTmp = new NhapHang();
                // neu co chon tu hang nhap
                if (ChiPhiVM.ChiPhi.NhapHangId != 0)
                {
                    nhapHang = _unitOfWork.nhapHangRepository.GetById(ChiPhiVM.ChiPhi.NhapHangId);
                    nhapHangTmp = _unitOfWork.nhapHangRepository.GetById(ChiPhiVM.ChiPhi.NhapHangId);
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
                    // ghi log nhaphang
                    if (nhapHangTmp.SoLuong != nhapHang.SoLuong)
                    {
                        temp += String.Format("- Số lượng thay đổi: {0}->{1}", nhapHangTmp.SoLuong, nhapHang.SoLuong);
                    }
                    if (nhapHangTmp.SoLuong != nhapHang.SoLuong)
                    {
                        temp += String.Format("- Số lượng thay đổi: {0}->{1}", nhapHangTmp.SoLuong, nhapHang.SoLuong);
                    }

                    // kiem tra thay doi
                    if (temp.Length > 0)
                    {

                        string log = System.Environment.NewLine;
                        log += "=============";
                        log += System.Environment.NewLine;
                        log += temp + " -User cập nhật tour: " + "Admin" + " vào lúc: " + System.DateTime.Now.ToString(); // username
                        nhapHangTmp.LogFile = nhapHangTmp.LogFile + log;
                        nhapHang.LogFile = nhapHangTmp.LogFile;
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

            var listNhapHang = new List<ListViewModel>();
            foreach (var item in _unitOfWork.nhapHangRepository.GetAll().OrderByDescending(x => x.NgayNhap))
            {
                string conHang;
                if (item.TrangThai)
                {
                    conHang = "Còn hàng";
                }
                else
                {
                    conHang = "Hết hàng";
                }

                listNhapHang.Add(new ListViewModel() { LongId = item.Id, Name = item.TenHang + " - " + item.NgayNhap.ToString("dd/MM/yyyy") + " - " + conHang });
            }

            listNhapHang.Insert(0, new ListViewModel() { LongId = 0, Name = "-- khác --" });
            ChiPhiVM.ListNhapHang = listNhapHang;

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
            string temp = "", log = "";

            if (id != ChiPhiVM.ChiPhi.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                ChiPhiVM.ChiPhi.NgaySua = DateTime.Now;
                ChiPhiVM.ChiPhi.NguoiSua = "Admin";

                // kiem tra thay doi : trong getbyid() va ngoai view
                #region log file
                //var t = _unitOfWork.tourRepository.GetById(id);
                var t = _unitOfWork.chiPhiRepository.GetSingleNoTracking(x => x.Id == id);
                if (t.NhapHangId != ChiPhiVM.ChiPhi.NhapHangId)
                {
                    temp += String.Format("- Tên nhập hàng thay đổi: {0}->{1}",
                        (t.NhapHangId == 0) ? "0" : _unitOfWork.nhapHangRepository.GetById(t.NhapHangId).TenHang,
                        (ChiPhiVM.ChiPhi.NhapHangId == 0) ? "0" : _unitOfWork.nhapHangRepository.GetById(ChiPhiVM.ChiPhi.NhapHangId).TenHang);
                }
                if (t.ChiPhiKhac != ChiPhiVM.ChiPhi.ChiPhiKhac)
                {
                    temp += String.Format("- Chi phí khác thay đổi: {0}->{1}", t.ChiPhiKhac, ChiPhiVM.ChiPhi.ChiPhiKhac);
                }
                if (t.DVT != ChiPhiVM.ChiPhi.DVT)
                {
                    temp += String.Format("- DVT thay đổi: {0}->{1}", t.DVT, ChiPhiVM.ChiPhi.DVT);
                }
                if (t.SoLuong != ChiPhiVM.ChiPhi.SoLuong)
                {
                    temp += String.Format("- Số lượng thay đổi: {0}->{1}", t.SoLuong, ChiPhiVM.ChiPhi.SoLuong);
                }
                if (t.DVT2 != ChiPhiVM.ChiPhi.DVT2)
                {
                    temp += String.Format("- DVT2 thay đổi: {0}->{1}", t.DVT2, ChiPhiVM.ChiPhi.DVT2);
                }
                if (t.SoLuong2 != ChiPhiVM.ChiPhi.SoLuong2)
                {
                    temp += String.Format("- Số lượng 2 thay đổi: {0}->{1}", t.SoLuong2, ChiPhiVM.ChiPhi.SoLuong2);
                }
                if (t.DonGia != ChiPhiVM.ChiPhi.DonGia)
                {
                    temp += String.Format("- Đơn giá thay đổi: {0:N0}->{1:N0}", t.DonGia, ChiPhiVM.ChiPhi.DonGia);
                }
                if (t.ThanhTien != ChiPhiVM.ChiPhi.ThanhTien)
                {
                    temp += String.Format("- Thành tiền thay đổi: {0:N0}->{1:N0}", t.ThanhTien, ChiPhiVM.ChiPhi.ThanhTien);
                }
                if (t.NgayTao != ChiPhiVM.ChiPhi.NgayTao)
                {
                    temp += String.Format("- Ngày xuất thay đổi: {0:dd/MM/yyyy}->{1:dd/MM/yyyy}", t.NgayTao, ChiPhiVM.ChiPhi.NgayTao);
                }
                if (t.GhiChu != ChiPhiVM.ChiPhi.GhiChu)
                {
                    temp += String.Format("- Ghi chú thay đổi: {0}->{1}", t.GhiChu, ChiPhiVM.ChiPhi.GhiChu);
                }

                #endregion
                // kiem tra thay doi
                if (temp.Length > 0)
                {

                    log = System.Environment.NewLine;
                    log += "=============";
                    log += System.Environment.NewLine;
                    log += temp + " -User cập nhật tour: " + "Admin" + " vào lúc: " + System.DateTime.Now.ToString(); // username
                    t.LogFile = t.LogFile + log;
                    ChiPhiVM.ChiPhi.LogFile = t.LogFile;
                }

                if (string.IsNullOrEmpty(ChiPhiVM.ChiPhi.ChiPhiKhac))
                {
                    ChiPhiVM.ChiPhi.ChiPhiKhac = "";
                }

                try
                {
                    var chiPhi = _unitOfWork.chiPhiRepository.GetSingleNoTracking(x => x.Id == id);
                    //  can tru ben hang nhap //////////////////////////////////////////////////////////////
                    NhapHang nhapHang = new NhapHang();
                    if (ChiPhiVM.ChiPhi.NhapHangId == chiPhi.NhapHangId) // 
                    {
                        nhapHang = _unitOfWork.nhapHangRepository.GetById(ChiPhiVM.ChiPhi.NhapHangId);
                        if (nhapHang != null) // null khi dang sua chiphikhac
                        {
                            // neu thay doi soluong
                            if (ChiPhiVM.SoLuongCu != ChiPhiVM.ChiPhi.SoLuong)
                            {
                                // so luong ban dau truoc khi thay doi
                                var slBanDau = nhapHang.SoLuong + ChiPhiVM.SoLuongCu;
                                // can tru lai tu dau - sl con lai
                                nhapHang.SoLuong = slBanDau - ChiPhiVM.ChiPhi.SoLuong;
                                nhapHang.ThanhTien = nhapHang.SoLuong * nhapHang.DonGia;
                            }
                            // neu thay doi soluong2
                            if (ChiPhiVM.SoLuong2Cu != ChiPhiVM.ChiPhi.SoLuong2)
                            {
                                // so luong 2 ban dau
                                var slBanDau2 = nhapHang.SoLuong2 + ChiPhiVM.SoLuong2Cu;
                                // can tru lai tu dau - sl con lai
                                nhapHang.SoLuong2 = slBanDau2 - ChiPhiVM.ChiPhi.SoLuong2;

                            }

                            if (nhapHang.SoLuong == 0 /*&& nhapHang.SoLuong2 == 0*/)
                            {
                                nhapHang.TrangThai = false;
                                // thanh tien con lai
                                nhapHang.ThanhTien = 0;
                            }
                            //else
                            //{
                            //    // thanh tien con lai
                            //    nhapHang.ThanhTien = nhapHang.ThanhTien - (ChiPhiVM.ChiPhi.SoLuong * ChiPhiVM.ChiPhi.DonGia);
                            //}

                            _unitOfWork.nhapHangRepository.Update(nhapHang);
                        }

                        
                    }
                    else
                    {
                        return NotFound();
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
                //  can tru lai hang` nhap
                if (chiPhi.NhapHangId != 0) // ko phai chiphikhac -> hang` nhap
                {
                    var nhapHang = _unitOfWork.nhapHangRepository.GetById(chiPhi.NhapHangId); // chac chan dung' voi hang nhap vi dung Id
                    nhapHang.SoLuong = nhapHang.SoLuong + chiPhi.SoLuong;
                    nhapHang.SoLuong2 = nhapHang.SoLuong2 + chiPhi.SoLuong2;
                    nhapHang.ThanhTien = nhapHang.ThanhTien + chiPhi.ThanhTien;
                    nhapHang.LogFile += "=========== " + "Xóa chi phí ngày: " +
                                        DateTime.Now +
                                        " với SL1 = " + chiPhi.SoLuong +
                                        " SL2 = " + chiPhi.SoLuong2 +
                                        " thành tiền = " + chiPhi.ThanhTien +
                                        " ============";
                    nhapHang.TrangThai = true;

                    _unitOfWork.nhapHangRepository.Update(nhapHang);

                }
                //  can tru lai hang` nhap
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

        public JsonResult GetThanhTien(decimal donGia = 0, decimal soLuong = 0, decimal chiPhiKhac = 0)
        {
            var thanhTien = donGia * soLuong + chiPhiKhac;
            return Json(new
            {
                thanhTien = thanhTien
            });
        }
        public JsonResult KiemTraSL(decimal soLuong = 1, long hangNhapId = 0)
        {
            var nhapHang = _unitOfWork.nhapHangRepository.GetById(hangNhapId);
            bool status = false;
            decimal sLuong2 = 0;
            decimal thanhTien = 0;

            if (soLuong <= nhapHang.SoLuong) // con hang
            {
                status = true;
                if (nhapHang.SoLuong2 != 0) // co dien sl 2
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
        public JsonResult KiemTraSLEdit(decimal soLuong = 1, long hangNhapId = 0, long chiPhiId = 0)
        {
            var nhapHang = _unitOfWork.nhapHangRepository.GetById(hangNhapId);
            var chiPhi = _unitOfWork.chiPhiRepository.GetById(chiPhiId);
            bool status = false;
            decimal sLuong2 = 0;
            decimal thanhTien = 0;

            var slBanDau = nhapHang.SoLuong + chiPhi.SoLuong;

            if (soLuong <= slBanDau) // con hang
            {
                status = true;
                if (nhapHang.SoLuong2 != 0) // co dien sl 2
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

            if (soLuong2 <= nhapHang.SoLuong2) // con hang
            {
                status = true;
                if (nhapHang.SoLuong != 0) // co dien sl 2
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

        public JsonResult KiemTraSL2Edit(decimal soLuong2 = 1, long hangNhapId = 0, long chiPhiId = 0)
        {
            var nhapHang = _unitOfWork.nhapHangRepository.GetById(hangNhapId);
            var chiPhi = _unitOfWork.chiPhiRepository.GetById(chiPhiId);
            bool status = false;
            decimal sLuong = 0;
            decimal thanhTien = 0;

            if (nhapHang.SoLuong2 > 0)
            {
                var slBanDau = nhapHang.SoLuong2 + chiPhi.SoLuong2;
                if (soLuong2 <= slBanDau) // con hang
                {
                    status = true;
                    if (nhapHang.SoLuong != 0) // co dien sl 2
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
            else
            {
                return Json(new
                {
                    status = true,
                    sLuong = sLuong,
                    thanhTien = thanhTien
                });
            }


        }
    }
}