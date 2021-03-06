﻿using Data.Dtos;
using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Data.Repository
{
    public interface IChiPhiRepository : IRepository<ChiPhi>
    {
        public IPagedList<ChiPhiDto> ListChiPhi(IEnumerable<NhapHang> nhapHangs, string searchString, string searchFromDate, string searchToDate, int? page);
    }
    public class ChiPhiRepository : Repository<ChiPhi>, IChiPhiRepository
    {
        public ChiPhiRepository(ChiChip613DbContext context) : base(context)
        {
        }

        public IPagedList<ChiPhiDto> ListChiPhi(IEnumerable<NhapHang> nhapHangs, string searchString, string searchFromDate, string searchToDate, int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand

            //var list = GetAll().AsQueryable();
            var list = new List<ChiPhiDto>();
            foreach(var item in _context.ChiPhis)
            {
                list.Add(new ChiPhiDto()
                {
                    Id = item.Id,
                    ChiPhiKhac = item.ChiPhiKhac,
                    DonGia = item.DonGia,
                    DVT = item.DVT,
                    DVT2 = item.DVT2,
                    GhiChu = item.GhiChu,
                    NgayTao = item.NgayTao,
                    NguoiTao = item.NguoiTao,
                    SoLuong = item.SoLuong,
                    SoLuong2 = item.SoLuong2,
                    TenHangNhap = nhapHangs.Where(x => x.Id == item.NhapHangId).FirstOrDefault().TenHang,
                    ThanhTien = item.ThanhTien
                });
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.TenHangNhap.ToLower().Contains(searchString.ToLower()) || 
                                       x.ChiPhiKhac.ToLower().Contains(searchString.ToLower()) || 
                                       x.DonGia.ToString().ToLower().Contains(searchString.ToLower())|| 
                                       x.SoLuong.ToString().ToLower().Contains(searchString.ToLower())|| 
                                       x.ThanhTien.ToString().ToLower().Contains(searchString.ToLower())).ToList();
            }

            var count = list.Count();
            DateTime fromDate, toDate;
            if (!string.IsNullOrEmpty(searchFromDate) && !string.IsNullOrEmpty(searchToDate))
            {

                try
                {
                    fromDate = DateTime.Parse(searchFromDate);
                    toDate = DateTime.Parse(searchToDate);

                    if (fromDate > toDate)
                    {
                        return null;
                    }
                    list = list.Where(x => x.NgayTao >= fromDate &&
                                       x.NgayTao < toDate.AddDays(1)).ToList();
                }
                catch (Exception)
                {

                    return null;
                }


                //list.Where(x => x.NgayTao >= fromDate && x.NgayTao < (toDate.AddDays(1))/*.ToPagedList(page, pageSize)*/;



            }
            else
            {
                if (!string.IsNullOrEmpty(searchFromDate))
                {
                    try
                    {
                        fromDate = DateTime.Parse(searchFromDate);
                        list = list.Where(x => x.NgayTao >= fromDate).ToList();
                    }
                    catch (Exception)
                    {
                        return null;
                    }

                }
                if (!string.IsNullOrEmpty(searchToDate))
                {
                    try
                    {
                        toDate = DateTime.Parse(searchToDate);
                        list = list.Where(x => x.NgayTao < toDate.AddDays(1)).ToList();

                    }
                    catch (Exception)
                    {
                        return null;
                    }

                }
            }
            // page the list
            const int pageSize = 15;
            decimal aa = (decimal)list.Count() / (decimal)pageSize;
            var bb = Math.Ceiling(aa);
            if (page > bb)
            {
                page--;
            }
            page = (page == 0) ? 1 : page;
            var listPaged = list.ToPagedList(page ?? 1, pageSize);
            //if (page > listPaged.PageCount)
            //    page--;
            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;


            return listPaged;

        }
    }
}
