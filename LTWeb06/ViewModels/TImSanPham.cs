using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTWeb06.ViewModels
{
    public class TImSanPham
    {
        public string TuKhoa { get; set; }
        public List<Models.SanPham> DSSP { get; set; }
    }
}