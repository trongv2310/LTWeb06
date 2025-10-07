using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTWeb06.Models;
using LTWeb06.ViewModels;

namespace LTWeb06.Controllers
{
    public class HomeController : Controller
    {
        public DuLieu dl = new DuLieu();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult HienThiLoai()
        {
            List<Loai> dsLoai = dl.dsLoai;
            return View(dsLoai);
        }
        public ActionResult HienThiSanPham()
        {
            List<SanPham> dssp = dl.dsSP;
            return View(dssp);
        }
        [HttpGet]
        public ActionResult HienThiCTSanPham(int id)
        {
            SanPham sp = new SanPham();
            sp = dl.ThietLap_HienThiCTSanPham(id);
            return View(sp);
        }
        [HttpGet]
        public ActionResult HienThiSPTheoLoai(int id)
        {
            HienThiSP_Loai_ViewModel vm= new HienThiSP_Loai_ViewModel();
            vm.Loai = dl.ChiTietLoai(id);
            List<SanPham> dssp = dl.HienThiSanPhamTheoLoai(id);
            vm.DSSP = dssp;
            return View(vm);
        }

        public ActionResult TimKiem(int? maLoai, double? giaMin, double? giaMax)
        {
            var dsLoai = dl.dsLoai;
            var ketqua = dl.TimSanPhamTheoLoaiVaGia(maLoai, giaMin, giaMax);

            ViewBag.DSLoai = dsLoai;
            ViewBag.MaLoai = maLoai;
            ViewBag.GiaMin = giaMin;
            ViewBag.GiaMax = giaMax;

            return View(ketqua); 
        }

        public ActionResult LichSu()
        {
            // Kiểm tra session MaKH (ưu tiên)
            var maKHobj = Session["MaKH"];
            if (maKHobj == null)
            {
                ViewBag.ThongBao = "Bạn chưa đăng nhập.";
                return View(new List<HoaDon>());
            }

            int maKH;
            if (!int.TryParse(maKHobj.ToString(), out maKH))
            {
                ViewBag.ThongBao = "Lỗi thông tin phiên đăng nhập.";
                return View(new List<HoaDon>());
            }

            var dsHD = dl.LayHoaDonTheoMaKH(maKH);

            // Nếu bảng tbl_HoaDon không lưu TongTien, ta có thể tính tổng cho từng hóa đơn:
            foreach (var hd in dsHD)
            {
                // nếu TongTien đã được gán từ DB (cột TongTien) thì không tính lại
                if (hd.TongTien == 0)
                {
                    var items = dl.LayChiTietHoaDon(hd.MaHoaDon);
                    hd.Items = items;
                    hd.TongTien = items.Sum(it => it.DonGia * it.SoLuong);
                }
            }

            return View(dsHD);
        }

        public ActionResult ChiTietHD(int id)
        {
            var maKHobj = Session["MaKH"];
            if (maKHobj == null)
            {
                return RedirectToAction("Index", "DangNhap");
            }
            int maKH;
            if (!int.TryParse(maKHobj.ToString(), out maKH))
            {
                return RedirectToAction("Index", "DangNhap");
            }

            // Lấy danh sách hóa đơn của MaKH để kiểm tra quyền truy cập
            var ds = dl.LayHoaDonTheoMaKH(maKH);
            var hd = ds.FirstOrDefault(h => h.MaHoaDon == id);
            if (hd == null)
            {
                return HttpNotFound(); // hoặc trả view thông báo không có quyền
            }

            // Lấy chi tiết và tính tổng (nếu cần)
            hd.Items = dl.LayChiTietHoaDon(id);
            if (hd.TongTien == 0)
                hd.TongTien = hd.Items.Sum(it => it.DonGia * it.SoLuong);

            return View(hd);
        }


    }
}