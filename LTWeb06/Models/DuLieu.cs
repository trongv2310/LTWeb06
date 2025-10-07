using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LTWeb06.Models
{
    public class DuLieu
    {
        static string strcon = "Data Source=DESKTOP-HHFANUM;database=QL_NHASACH;User ID=sa;password=123";
        SqlConnection con = new SqlConnection(strcon);
        public List<Loai> dsLoai = new List<Loai>();
        public List<SanPham> dsSP = new List<SanPham>();
        public DuLieu()
        {
            ThietLap_HienThiLoai();
            ThietLap_HienThiSanPham();
        }
        
        public void ThietLap_HienThiLoai()
        {
            string sql = "select * from tbl_Loai";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow r in dt.Rows)
            {
                Loai loai = new Loai();
                loai.MaLoai = int.Parse(r["MaLoai"].ToString());
                loai.TenLoai = r["TenLoai"].ToString();
                dsLoai.Add(loai);
            }
        }
        public void ThietLap_HienThiSanPham()
        {
            string sql = "select * from tbl_SanPham";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                SanPham sp = new SanPham();
                sp.MaSP = int.Parse(dr["MaSP"].ToString());
                sp.TenSP = dr["TenSP"].ToString();
                sp.MaNSX = dr["MaNSX"].ToString();
                sp.MaLoai = int.Parse(dr["MaLoai"].ToString());
                sp.Gia = double.Parse(dr["Gia"].ToString());
                sp.Hinh = dr["Hinh"].ToString();
                sp.GhiChu = dr["GhiChu"].ToString();
                dsSP.Add(sp);
            }
        }
        public SanPham ThietLap_HienThiCTSanPham(int id)
        {
            SanPham sp = new SanPham();
            string sql = "select * from tbl_SanPham where MaSP="+id;
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow r in dt.Rows)
            {
                sp.MaSP = int.Parse(r["MaSP"].ToString());
                sp.TenSP = r["TenSP"].ToString();
                sp.MaNSX = r["MaNSX"].ToString();
                sp.MaLoai = int.Parse(r["MaLoai"].ToString());
                sp.Gia = double.Parse(r["Gia"].ToString());
                sp.Hinh = r["Hinh"].ToString();
                sp.GhiChu = r["GhiChu"].ToString();
            }
            return sp;
        }
        public Loai ChiTietLoai(int id)
        {
            Loai loai = new Loai();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_Loai where MaLoai=" + id, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow r in dt.Rows)
            {
                loai.MaLoai = int.Parse(r["MaLoai"].ToString());
                loai.TenLoai = r["TenLoai"].ToString();
            }
            return loai;
        }
        public List<SanPham> HienThiSanPhamTheoLoai(int id)
        {
            List<SanPham> ds = new List<SanPham>();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_SanPham where MaLoai=" + id, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach(DataRow r in dt.Rows)
            {
                SanPham sp = new SanPham();
                sp.MaSP = int.Parse(r["MaSP"].ToString());
                sp.TenSP = r["TenSP"].ToString();
                sp.MaNSX = r["MaNSX"].ToString();
                sp.MaLoai = int.Parse(r["MaLoai"].ToString());
                sp.Gia = double.Parse(r["Gia"].ToString());
                sp.Hinh = r["Hinh"].ToString();
                sp.GhiChu = r["GhiChu"].ToString();
                ds.Add(sp);
            }
            return ds;
        }

        public List<SanPham> TimSanPhamTheoLoaiVaGia(int? maLoai, double? giaMin, double? giaMax)
        {
            List<SanPham> kq = new List<SanPham>();
            string sql = "select * from tbl_SanPham where 1=1";

            if (maLoai.HasValue && maLoai.Value > 0)
                sql += " and MaLoai = " + maLoai.Value;

            if (giaMin.HasValue)
                sql += " and Gia >= " + giaMin.Value.ToString().Replace(",", ".");

            if (giaMax.HasValue)
                sql += " and Gia <= " + giaMax.Value.ToString().Replace(",", ".");

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            var dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                SanPham sp = new SanPham();
                sp.MaSP = int.Parse(dr["MaSP"].ToString());
                sp.TenSP = dr["TenSP"].ToString();
                sp.MaNSX = dr["MaNSX"].ToString();
                sp.MaLoai = int.Parse(dr["MaLoai"].ToString());
                sp.Gia = double.Parse(dr["Gia"].ToString());
                sp.Hinh = dr["Hinh"].ToString();
                sp.GhiChu = dr["GhiChu"].ToString();
                kq.Add(sp);
            }

            return kq;
        }

        public List<HoaDon> LayHoaDonTheoMaKH(int maKH)
        {
            List<HoaDon> ds = new List<HoaDon>();
            string sql = "SELECT * FROM tbl_HoaDon WHERE MaKH = @maKH ORDER BY NgayTao DESC";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.SelectCommand.Parameters.AddWithValue("@maKH", maKH);
            var dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow r in dt.Rows)
            {
                HoaDon hd = new HoaDon();
                if (dt.Columns.Contains("MaHoaDon")) hd.MaHoaDon = Convert.ToInt32(r["MaHoaDon"]);
                if (dt.Columns.Contains("NgayTao")) hd.NgayTao = Convert.ToDateTime(r["NgayTao"]);
                if (dt.Columns.Contains("MaKH")) hd.MaKH = Convert.ToInt32(r["MaKH"]);
                if (dt.Columns.Contains("TongTien"))
                    hd.TongTien = Convert.ToDouble(r["TongTien"]);
                ds.Add(hd);
            }

            return ds;
        }

        public List<ChiTietHoaDon> LayChiTietHoaDon(int maHoaDon)
        {
            List<ChiTietHoaDon> ds = new List<ChiTietHoaDon>();

            string sql = @"
        SELECT c.MaHD, c.MaSP, c.SoLuong, ISNULL(s.Gia,0) AS Gia, ISNULL(s.TenSP,'') AS TenSP
        FROM tbl_ChiTiet c
        LEFT JOIN tbl_SanPham s ON c.MaSP = s.MaSP
        WHERE c.MaHD = @maHD";

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.SelectCommand.Parameters.AddWithValue("@maHD", maHoaDon);
            var dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow r in dt.Rows)
            {
                ChiTietHoaDon ct = new ChiTietHoaDon();
                if (dt.Columns.Contains("MaHD")) ct.MaHD = Convert.ToInt32(r["MaHD"]);
                if (dt.Columns.Contains("MaSP")) ct.MaSP = Convert.ToInt32(r["MaSP"]);
                if (dt.Columns.Contains("SoLuong")) ct.SoLuong = Convert.ToInt32(r["SoLuong"]);
                if (dt.Columns.Contains("Gia")) ct.DonGia = Convert.ToDouble(r["Gia"]);
                if (dt.Columns.Contains("TenSP"))
                {
                    ct.SanPham = new SanPham()
                    {
                        MaSP = ct.MaSP,
                        TenSP = r["TenSP"].ToString(),
                        Gia = ct.DonGia
                    };
                }

                ds.Add(ct);
            }

            return ds;
        }



    }
}