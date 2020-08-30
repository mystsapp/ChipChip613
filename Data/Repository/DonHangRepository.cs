using Data.Dtos;
using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace Data.Repository
{
    public interface IDonHangRepository : IRepository<DonHang>
    {
        IPagedList<DonHangDto> ListDonHang(string searchString, string searchFromDate, string searchToDate, int? page);
    }
    public class DonHangRepository : Repository<DonHang>, IDonHangRepository
    {
        public DonHangRepository(ChiChip613DbContext context) : base(context)
        {
        }

        public IPagedList<DonHangDto> ListDonHang(string searchString, string searchFromDate, string searchToDate, int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand

            var list = new List<DonHangDto>();
            var donHangs = _context.DonHangs;
            var chiTietDonHang = _context.ChiTietDonHangs;
            foreach(var item in donHangs)
            {
                list.Add(new DonHangDto()
                {
                    Id = item.Id,
                    DiaChi = item.DiaChi,
                    DienThoai = item.DienThoai,
                    KhachHang = item.KhachHang,
                    NgayTao = item.NgayTao,
                    NguoiTao = item.NguoiTao,
                    SoLuong = chiTietDonHang.Where(x => x.DonHangId == item.Id).Sum(x => x.SoLuong),
                    SoLuongXX = chiTietDonHang.Where(x => x.DonHangId == item.Id && x.TenSanPham == "Xúc xích").Sum(x => x.SoLuong),
                    ThanhTien = chiTietDonHang.Where(x => x.DonHangId == item.Id).Sum(x => x.ThanhTien)
                });
            }
            list = list.OrderByDescending(x => x.NgayTao).ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.KhachHang.ToLower().Contains(searchString.ToLower()) ||
                                       x.DiaChi.ToLower().Contains(searchString.ToLower())||
                                       x.DienThoai.ToLower().Contains(searchString.ToLower())).ToList();
                
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
