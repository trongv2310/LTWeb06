using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTWeb06.Models
{
    public class ChiTietHoaDon
    {
        public int MaHD { get; set; }    
        public int MaSP { get; set; }   
        public int SoLuong { get; set; }
        public double DonGia { get; set; } 
        public SanPham SanPham { get; set; }
    }
}