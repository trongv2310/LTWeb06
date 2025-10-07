using LTWeb06.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;

namespace LTWeb06.Controllers
{
    public class DangNhapController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(DangNhap dn)
        {

            if (dn == null || string.IsNullOrWhiteSpace(dn.SDT) || string.IsNullOrWhiteSpace(dn.MatKhau))
            {
                ViewBag.ThongBao = "Vui lòng nhập SĐT và mật khẩu.";
                return View();
            }
            string connStr = ConfigurationManager.ConnectionStrings[""].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string sql = @"
                    SELECT MaKH, TenKH, SDT
                    FROM tbl_KhachHang
                    WHERE SDT = @sdt AND MatKhau = @mk
                ";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@sdt", dn.SDT.Trim());
                    cmd.Parameters.AddWithValue("@mk", dn.MatKhau.Trim());

                    con.Open();
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            int maKH = Convert.ToInt32(r["MaKhachHang"]);
                            string sdtDb = r["SoDienThoai"].ToString();

                            Session["MaKH"] = maKH;
                            Session["SDT"] = sdtDb;

                            return RedirectToAction("HienThiSanPham", "Home");
                        }
                        else
                        {
                            ViewBag.ThongBao = "Sai SĐT hoặc Mật Khẩu";
                            return View();
                        }
                    }
                }
            }
        }
    }
}
