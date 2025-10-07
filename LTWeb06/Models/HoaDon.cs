using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTWeb06.Models
{
    public class HoaDon
    {
        public int MaHoaDon { get; set; } 
        public DateTime NgayTao { get; set; }
        public int MaKH { get; set; }
        public double TongTien { get; set; } 
        public List<ChiTietHoaDon> Items { get; set; } = new List<ChiTietHoaDon>();
    }
}