CREATE DATABASE QL_NHASACH

CREATE TABLE tbl_NhaSanXuat (
    MaNSX INT PRIMARY KEY,
    TenNSX NVARCHAR(100),
);

CREATE TABLE tbl_Loai (
    MaLoai INT PRIMARY KEY,
    TenLoai NVARCHAR(100)
);

CREATE TABLE tbl_SanPham (
    MaSP INT PRIMARY KEY, 
    TenSP NVARCHAR(150),
    MaNSX INT,
    MaLoai INT,
    Gia FLOAT, 
    GhiChu NVARCHAR(MAX),
    Hinh VARCHAR(255),
    FOREIGN KEY (MaNSX) REFERENCES tbl_NhaSanXuat(MaNSX),
    FOREIGN KEY (MaLoai) REFERENCES tbl_Loai(MaLoai)
);

CREATE TABLE tbl_KhachHang (
    MaKH INT PRIMARY KEY, 
    TenKH NVARCHAR(100),
    SDT VARCHAR(15),
    MatKhau VARCHAR(50)
);

CREATE TABLE tbl_HoaDon (
    MaHD INT PRIMARY KEY, 
    MaKH INT,
    NgayTao DATE,
    FOREIGN KEY (MaKH) REFERENCES tbl_KhachHang(MaKH)
);

CREATE TABLE tbl_ChiTiet (
    MaHD INT,
    MaSP INT,
    SoLuong INT,

    PRIMARY KEY (MaHD, MaSP),
    FOREIGN KEY (MaHD) REFERENCES tbl_HoaDon(MaHD),
    FOREIGN KEY (MaSP) REFERENCES tbl_SanPham(MaSP)
);

INSERT INTO tbl_NhaSanXuat (MaNSX, TenNSX) VALUES
(1, N'Nhà Xuất Bản Trẻ'),
(2, N'Nhà Xuất Bản Kim Đồng'),
(3, N'Nhà Sách FAHASA');

INSERT INTO tbl_Loai (MaLoai, TenLoai) VALUES
(10, N'Sách Văn Học'),
(20, N'Sách Khoa Học Kỹ Thuật'),
(30, N'Truyện Tranh'),
(40, N'Văn Phòng Phẩm');

INSERT INTO tbl_SanPham (MaSP, TenSP, MaNSX, MaLoai, Gia, GhiChu, Hinh) VALUES
(1001, N'Đắc Nhân Tâm', 1, 10, 120000.00, N'Sách kỹ năng sống bán chạy nhất', 'dnt.jpg'),
(1002, N'Lập Trình C# Cơ Bản', 2, 20, 185500.50, N'Sách giáo trình chuyên ngành', 'csharp.png'),
(1003, N'Thám Tử Conan Tập 1', 2, 30, 25000.00, N'Truyện trinh thám kinh điển', 'conan01.webp'),
(1004, N'Bút Bi Thiên Long', 3, 40, 5000.00, N'Dụng cụ học tập', 'butbi.jpg');

INSERT INTO tbl_KhachHang (MaKH, TenKH, SDT, MatKhau) VALUES
(1, N'Nguyễn Văn A', '0901234567', 'pass123'),
(2, N'Trần Thị B', '0909876543', 'bmatkhau'),
(3, N'Lê Văn C', '0912345678', 'cstrongpass');

INSERT INTO tbl_HoaDon (MaHD, MaKH, NgayTao) VALUES
(501, 1, '2025-09-01'),
(502, 2, '2025-09-05'),
(503, 1, '2025-09-10'),
(504, 3, '2025-09-15');

INSERT INTO tbl_ChiTiet (MaHD, MaSP, SoLuong) VALUES
-- Hóa đơn 501
(501, 1001, 2), -- 2 cuốn Đắc Nhân Tâm
(501, 1004, 5), -- 5 cái Bút Bi

-- Hóa đơn 502
(502, 1003, 10), -- 10 cuốn Conan
(502, 1002, 1), -- 1 cuốn C#

-- Hóa đơn 503
(503, 1001, 1), -- 1 cuốn Đắc Nhân Tâm

-- Hóa đơn 504
(504, 1004, 3) -- 3 cái Bút Bi