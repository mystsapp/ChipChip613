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
    public interface INhapHangRepository : IRepository<NhapHang>
    {
        IPagedList<NhapHangDto> ListNhapHang(string searchString, string searchFromDate, string searchToDate, string trangThai, int? page);
    }
    public class NhapHangRepository : Repository<NhapHang>, INhapHangRepository
    {
        public NhapHangRepository(ChiChip613DbContext context) : base(context)
        {
        }

        public IPagedList<NhapHangDto> ListNhapHang(string searchString, string searchFromDate, string searchToDate, string trangThai, int? page)
        {

            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand

            var nhapHangs = GetAll().AsQueryable();
            List<NhapHangDto> list = new List<NhapHangDto>();
            
            foreach (var item in nhapHangs)
            {
                list.Add(new NhapHangDto()
                {
                    DonGia = item.DonGia,
                    DTNoiNhap = item.DTNoiNhap,
                    DVT = item.DVT,
                    DVT2 = item.DVT2,
                    DVT2Luu = item.DVT2Luu,
                    DVTLuu = item.DVTLuu,
                    GhiChu = item.GhiChu,
                    Id = item.Id,
                    LogFile = item.LogFile,
                    NgayNhap = item.NgayNhap,
                    NgayTao = item.NgayTao,
                    NguoiTao = item.NguoiTao,
                    NoiNhap = item.NoiNhap,
                    SoLuong = item.SoLuong,
                    SoLuong2 = item.SoLuong2,
                    SoLuong2Luu = item.SoLuong2Luu,
                    SoLuongLuu = item.SoLuongLuu,
                    TenHang = item.TenHang,
                    ThanhTien = item.ThanhTien,
                    ThanhTienLuu = item.ThanhTienLuu,
                    TrangThai = item.TrangThai
                });
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.TenHang.ToLower().Contains(searchString.ToLower())).ToList();
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
                    list = list.Where(x => x.NgayNhap >= fromDate &&
                                       x.NgayNhap < toDate.AddDays(1)).ToList();
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
                        list = list.Where(x => x.NgayNhap >= fromDate).ToList();
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
                        list = list.Where(x => x.NgayNhap < toDate.AddDays(1)).ToList();

                    }
                    catch (Exception)
                    {
                        return null;
                    }

                }
            }
            // search trang thai
            if (!string.IsNullOrEmpty(trangThai))
            {
                list = list.Where(x => x.TrangThai == bool.Parse(trangThai)).ToList();
            }
            // search trang thai

            // tinh tong tien

            foreach (var item in list)
            {
                item.TongTien = list.Sum(x => x.ThanhTien);
            }
            
            foreach (var item in list)
            {
                item.TongTienLuu = list.Sum(x => x.ThanhTienLuu);
            }

            // tinh tong tien

            // page the list
            const int pageSize = 10;
            decimal aa = (decimal)list.Count() / (decimal)pageSize;
            var bb = Math.Ceiling(aa);
            if (page > bb)
            {
                page--;
            }
            page = (page == 0) ? 1 : page;
            var listPaged = list.OrderByDescending(x => x.NgayNhap).ToPagedList(page ?? 1, pageSize);
            //if (page > listPaged.PageCount)
            //    page--;
            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;


            return listPaged;
        }
    }
}
