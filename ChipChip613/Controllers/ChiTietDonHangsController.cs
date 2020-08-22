﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChipChip613.Models;
using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ChipChip613.Controllers
{
    public class ChiTietDonHangsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ChiTietDonHangViewModel ChiTietDonHangVM { get; set; }

        public ChiTietDonHangsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            ChiTietDonHangVM = new ChiTietDonHangViewModel()
            {
                ChiTietDonHang = new Data.Models.ChiTietDonHang(),
                DonHang = new Data.Models.DonHang(),
                SPDaChon = new Data.Models.SPDaChon(),
                SanPhams = SanPhams().OrderByDescending(x => x.Name)
            };
        }
        public IActionResult Create(long donHangId, string strUrl)
        {
            ChiTietDonHangVM.StrUrl = strUrl;
            ChiTietDonHangVM.DonHang = _unitOfWork.donHangRepository.GetById(donHangId);

            // sp truyen thong
            ChiTietDonHangVM.SPDaChon.SoLuong = 1;
            //ChiTietDonHangVM.SPDaChon.DonGia = 10000;
            ChiTietDonHangVM.SPDaChon.ThanhTien = (decimal)ChiTietDonHangVM.SPDaChon.SoLuong * ChiTietDonHangVM.SPDaChon.DonGia;
            // get list sp da chon
            ChiTietDonHangVM.SPDaChons = _unitOfWork.sPDaChonRepository.Find(x => x.DonHangId == donHangId);
            // get list sp da chon

            return View(ChiTietDonHangVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSPDaChon(long donHangId, string strUrl)
        {
            ChiTietDonHangVM.StrUrl = strUrl;
            // them moi sp da chon
            if (!ModelState.IsValid)
            {
                return View(ChiTietDonHangVM);
            }

            //NguyenLieuVM.NguyenLieu = new Data.Models.NguyenLieu();
            ChiTietDonHangVM.SPDaChon.DonHangId = donHangId;
            try
            {
                _unitOfWork.sPDaChonRepository.Create(ChiTietDonHangVM.SPDaChon);
                await _unitOfWork.Complete();
                SetAlert("Thêm mới thành công.", "success");
                return RedirectToAction(nameof(Create), new { donHangId = donHangId, strUrl = strUrl });
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return View(ChiTietDonHangVM);
            }
            // them moi sp da chon

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, long donHangId, string strUrl)
        {
            var sPDaChon = _unitOfWork.sPDaChonRepository.GetById(id);
            if (sPDaChon == null)
                return NotFound();
            try
            {
                _unitOfWork.sPDaChonRepository.Delete(sPDaChon);
                await _unitOfWork.Complete();
                SetAlert("Xóa thành công.", "success");
                return RedirectToAction(nameof(Create), new { donHangId = donHangId, strUrl = strUrl });
            }
            catch (Exception ex)
            {
                SetAlert(ex.Message, "error");
                return Redirect(strUrl);
            }
        }


        private List<SanPhamViewModel> SanPhams()
        {
            return new List<SanPhamViewModel>()
            {
                new SanPhamViewModel()
                {
                    Id = 1,
                    Name = "Truyền thống"
                },
                new SanPhamViewModel()
                {
                    Id = 1,
                    Name = "Xúc xích"
                },
            };
        }

    }
}