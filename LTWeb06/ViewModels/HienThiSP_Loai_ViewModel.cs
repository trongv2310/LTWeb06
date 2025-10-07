using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LTWeb06.Models;

namespace LTWeb06.ViewModels
{
    public class HienThiSP_Loai_ViewModel
    {
        public Loai Loai { get; set; }
        public List<SanPham> DSSP { get; set; }
    }
}