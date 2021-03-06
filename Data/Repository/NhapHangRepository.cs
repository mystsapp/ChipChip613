﻿using Data.Interfaces;
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
        IPagedList<NhapHang> ListNhapHang(string searchString, string searchFromDate, string searchToDate, int? page);
    }
    public class NhapHangRepository : Repository<NhapHang>, INhapHangRepository
    {
        public NhapHangRepository(ChiChip613DbContext context) : base(context)
        {
        }

        public IPagedList<NhapHang> ListNhapHang(string searchString, string searchFromDate, string searchToDate, int? page)
        {
            
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand

            var list = GetAll().AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.TenHang.ToLower().Contains(searchString.ToLower()));
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
                                       x.NgayNhap < toDate.AddDays(1));
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
                        list = list.Where(x => x.NgayNhap >= fromDate);
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
                        list = list.Where(x => x.NgayNhap < toDate.AddDays(1));

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
