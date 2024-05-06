CREATE DATABASE QuanLyNhaHang
GO

USE QuanLyNhaHang
GO

-- MonAn
-- Ban
-- DanhMucMonAn
-- TaiKhoan
-- HoaDon
-- ThongTinHoaDon


----------  Phần bàn ------------------
CREATE TABLE Ban
(
	id INT IDENTITY PRIMARY KEY,
	tenBan NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa có tên',
	trangThai NVARCHAR(100) DEFAULT N'Trống' NOT NULL -- Trống || Có người
)
GO
-- Thêm dữ liệu vào table Ban
DECLARE @i INT = 0

WHILE @i <= 10
BEGIN 
	INSERT dbo.Ban ( tenBan)VALUES ( N'Bàn ' + CAST(@i AS nvarchar(100)))
	SET @i = @i + 1
END
GO
-- cl
CREATE PROC USP_GetTableList
AS SELECT * FROM dbo.Ban
GO
EXEC dbo.USP_GetTableList
GO
-------------- Phần tài khoản ----------
CREATE TABLE TaiKhoan
(
	tenDangNhap NVARCHAR(100) PRIMARY KEY,
	tenHienThi NVARCHAR(100) NOT NULL DEFAULT N'Chưa có tên',
	matKhau NVARCHAR(1000) NOT NULL DEFAULT 123,
	loai INT NOT NULL DEFAULT 0 -- 1: admin || 0: nhanvien
)
GO
-- thêm dữ liệu vào table TaiKhoan
INSERT INTO Taikhoan (tenDangNhap, tenHienThi, matkhau, loai)
VALUES 
(N'admin', N'Admin', N'123', 1)
GO
INSERT INTO Taikhoan (tenDangNhap, tenHienThi, matkhau, loai)
VALUES 
(N'khaiminh', N'Khai Minh', N'123', 0)
GO
CREATE PROC USP_TaikhoantenDangNhap
@tenDangnhap nvarchar(100)
AS
BEGIN
	SELECT * FROM TaiKhoan WHERE tenDangNhap = @tenDangnhap
END
GO

CREATE PROC USP_DangNhap
@tenDangnhap nvarchar(100), @matKhau nvarchar(100)
AS
BEGIN
	SELECT * FROM TaiKhoan WHERE tenDangNhap = @tenDangnhap AND matKhau = @matKhau
END
GO


EXEC USP_TaikhoantenDangNhap @tenDangnhap = N'admin' -- nvarchar(100)

CREATE TABLE DanhMucMonAn
(
	id INT IDENTITY PRIMARY KEY,
	tenDanhMuc NVARCHAR(100) NOT NULL DEFAULT N'Danh mục chưa có tên',
)
GO
-- Thêm danh mục món ăn
INSERT INTO DanhMucMonAn(tenDanhMuc)
VALUES 
(N'Hải sản')
GO

INSERT INTO DanhMucMonAn(tenDanhMuc)
VALUES 
(N'Món nướng')
GO

INSERT INTO DanhMucMonAn(tenDanhMuc)
VALUES 
(N'Món kho')
GO

INSERT INTO DanhMucMonAn(tenDanhMuc)
VALUES 
(N'Món chay')
GO

INSERT INTO DanhMucMonAn(tenDanhMuc)
VALUES 
(N'Nước')
GO

--------- Phần món ăn -----------
CREATE TABLE MonAn
(
	id INT IDENTITY PRIMARY KEY,
	tenMonAn NVARCHAR(100) NOT NULL DEFAULT N'Món chưa có tên',
	idDanhMuc INT NOT NULL,
	giaMonAn FLOAT NOT NULL DEFAULT 0

	FOREIGN KEY (idDanhMuc) REFERENCES dbo.DanhMucMonAn(id)
)
GO
-- Thêm món ăn
INSERT INTO MonAn(tenMonAn, idDanhMuc, giaMonAn)
VALUES 
(N'Mực nướng sa tế' , 1, 70000)
GO

INSERT INTO MonAn(tenMonAn, idDanhMuc, giaMonAn)
VALUES 
(N'Bò lá lốt' , 2, 40000)
GO

INSERT INTO MonAn(tenMonAn, idDanhMuc, giaMonAn)
VALUES 
(N'Cá kho tiêu' , 3, 20000)
GO

INSERT INTO MonAn(tenMonAn, idDanhMuc, giaMonAn)
VALUES 
(N'Đậu phụ chiên xả ớt' , 4, 25000)
GO

INSERT INTO MonAn(tenMonAn, idDanhMuc, giaMonAn)
VALUES 
(N'Sting đâu' , 5, 15000)
GO





---------- Phần hóa đơn ------------
CREATE TABLE HoaDon
(
	id INT IDENTITY PRIMARY KEY,
	thoiGianVao DATE NOT NULL DEFAULT GETDATE(),
	thoiGianRa DATE,
	idBan INT NOT NULL,
	trangThai INT NOT NULL DEFAULT 0  -- 1: đã thanh toán || 0: Chưa thanh toán
	
	FOREIGN KEY (idBan) REFERENCES dbo.Ban(id)
)
GO
-- Thêm dữ liệu cho hóa đơn
INSERT INTO HoaDon(thoiGianVao, thoiGianRa, idBan, trangThai)
VALUES 
(GETDATE() , NULL, 1, 0) -- thoi gian vao. thoi gian ra . bàn . thanh toan chua
GO

INSERT INTO HoaDon(thoiGianVao, thoiGianRa, idBan, trangThai)
VALUES 
(GETDATE() , NULL, 2, 0)
GO

INSERT INTO HoaDon(thoiGianVao, thoiGianRa, idBan, trangThai)
VALUES 
(GETDATE() , GETDATE(), 2, 1)
GO

CREATE TABLE ThongTinHoaDon
(
	id INT IDENTITY PRIMARY KEY,
	idHoaDon INT NOT NULL,
	idMonAn INT NOT NULL,
	soLuongMon INT NOT NULL DEFAULT 0

	FOREIGN KEY (idHoaDon) REFERENCES dbo.HoaDon(id),
	FOREIGN KEY (idMonAn) REFERENCES dbo.MonAn(id)
)
GO
-- Thêm dữ liệu cho thông tin hóa đơn
INSERT INTO ThongTinHoaDon(idHoaDon, idMonAn, soLuongMon)
VALUES 
(1 , 1, 2)
GO
INSERT INTO ThongTinHoaDon(idHoaDon, idMonAn, soLuongMon)
VALUES 
(1 , 3, 1)
GO
INSERT INTO ThongTinHoaDon(idHoaDon, idMonAn, soLuongMon)
VALUES 
(1 , 5, 1)
GO

INSERT INTO ThongTinHoaDon(idHoaDon, idMonAn, soLuongMon)
VALUES 
(2 , 2, 2)
GO

INSERT INTO ThongTinHoaDon(idHoaDon, idMonAn, soLuongMon)
VALUES 
(3 , 2, 2)
GO

ALTER TABLE dbo.HoaDon 
ADD giamGia INT
go 

CREATE PROC USP_InsertBill
@idBan INT
AS
BEGIN
	INSERT HoaDon 
			(thoiGianVao ,
			 thoiGianRa ,
			 idBan ,
			 trangThai ,
			 giamGia
			 )
	VALUES (GETDATE() ,
			NULL ,
			@idBan ,
			0,
			0
			)
END
GO

CREATE PROC USP_InsertBillInfo  -- CREATE || ALTER(nếu có rồi)
@idHoaDon INT,@idMonAn INT,@soLuongMon INT
AS
BEGIN
	DECLARE @isExitsBillInfo INT;
	DECLARE @foodCount INT = 1
	SELECT @isExitsBillInfo = id, @foodCount = soLuongMon 
	FROM ThongTinHoaDon 
	WHERE idHoaDon =@idHoaDon AND idMonAn = @idMonAn

	IF (@isExitsBillInfo > 0)
	BEGIN 
		DECLARE @newCount INT =@foodCount + @soLuongMon
		IF (@newCount > 0)
			UPDATE ThongTinHoaDon SET soLuongMon = @foodCount + @soLuongMon WHERE idHoaDon = @idHoaDon
		ELSE
			DELETE ThongTinHoaDon WHERE idHoaDon = @idHoaDon AND idMonAn = @idMonAn
	END
	ELSE 
	BEGIN 
		INSERT ThongTinHoaDon 
				(idHoaDon,idMonAn,soLuongMon)
		VALUES (
				@idHoaDon,
				@idMonAn,
				@soLuongMon)
	END
END
GO


--Phần mới thêm:

CREATE TRIGGER UTF_UpdateBillInfo
ON ThongTinHoaDon FOR INSERT, UPDATE
AS 
BEGIN
	DECLARE @idBill INT
	SELECT  @idBill = idHoaDon FROM Inserted

	DECLARE @idTable INT
	SELECT @idTable = idBan FROM dbo.HoaDon WHERE id = @idBill AND trangThai = 0
	
	UPDATE dbo.Ban SET trangThai = N'Có người' WHERE id = @idTable
END
GO

create TRIGGER UTF_UpdateBill --alter
ON dbo.HoaDon FOR  UPDATE
AS 
BEGIN
	DECLARE @idBill INT
	SELECT @idBill = id FROM Inserted

	DECLARE @idTable INT
	SELECT @idTable = idBan FROM dbo.HoaDon WHERE id = @idBill

	DECLARE @count INT = 0
	SELECT @count = COUNT(*) FROM dbo.HoaDon WHERE idBan = @idTable AND trangThai = 0

	if(@count=0) 
		UPDATE dbo.Ban SET trangThai = N'Trống' where id = @idTable
END
GO

delete dbo.ThongTinHoaDon 
GO
delete dbo.HoaDon
GO

SELECT * FROM HoaDon
SELECT * FROM ThongTinHoaDon 
SELECT * FROM TaiKhoan

alter table dbo.HoaDon add tongTien FLOAT
go

CREATE PROC USP_GetListBillDate
@checkIn DATE, @checkOut DATE
AS
BEGIN
    SELECT
        t.tenBan AS [Tên bàn],
        SUM(b.tongTien) AS [Tổng tiền],
        MIN(thoiGianVao) AS [Thời gian vào],
        MAX(thoiGianRa) AS [Thời gian ra],
        MAX(giamGia) AS [Giảm giá]
    FROM
        HoaDon AS b
        INNER JOIN Ban AS t ON b.idBan = t.id
        INNER JOIN ThongTinHoaDon AS bi ON bi.idHoaDon = b.id
        INNER JOIN MonAn AS f ON f.id = bi.idMonAn
        INNER JOIN DanhMucMonAn AS d ON d.id = f.idDanhMuc
    WHERE
        thoiGianVao >= @checkIn
        AND thoiGianRa <= @checkOut
        AND b.trangThai = 1
    -- Thêm điều kiện để chỉ lấy thông tin từ một danh mục món cụ thể, nếu cần
        -- AND d.id = 1
    GROUP BY t.tenBan
END
GO
-- phan  moi
CREATE PROC USP_UpdateAccount
@userName NVARCHAR(100), @displayName NVARCHAR(100), @password NVARCHAR(100), @newPassword NVARCHAR(100)
AS
BEGIN
	DECLARE @isRightPass INT = 0
	SELECT @isRightPass = COUNT(*) FROM TaiKhoan WHERE tenDangNhap = @userName AND matKhau = @password

	IF (@isRightPass = 1)
	BEGIN
		IF (@newPassword = NULL OR @newPassword = '')
		BEGIN
			UPDATE TaiKhoan SET tenHienThi = @displayName WHERE tenDangNhap = @userName 
		END
		ELSE
		UPDATE TaiKhoan SET tenHienThi = @displayName, matKhau = @newPassword WHERE tenDangNhap = @userName 

	END
END
GO

select * from DanhMucMonAn
delete from DanhMucMonAn where id = 10

exec USP_GetListBillDate '01-04-2024' , '01-30-2024'
GO
CREATE PROC USP_rpBill
@checkIn DATE, @checkOut DATE
AS
BEGIN
    SELECT
        t.tenBan,
        SUM(b.tongTien),
        MIN(thoiGianVao),
        MAX(thoiGianRa),
        MAX(giamGia)
    FROM
        HoaDon AS b
        INNER JOIN Ban AS t ON b.idBan = t.id
        INNER JOIN ThongTinHoaDon AS bi ON bi.idHoaDon = b.id
        INNER JOIN MonAn AS f ON f.id = bi.idMonAn
        INNER JOIN DanhMucMonAn AS d ON d.id = f.idDanhMuc
    WHERE
        thoiGianVao >= @checkIn
        AND thoiGianRa <= @checkOut
        AND b.trangThai = 1
    -- Thêm điều kiện để chỉ lấy thông tin từ một danh mục món cụ thể, nếu cần
        -- AND d.id = 1
    GROUP BY t.tenBan
END
GO


ALTER PROC USP_rpBill
@checkIn DATE, @checkOut DATE
AS
BEGIN
    SELECT DISTINCT
        t.tenBan,
        b.tongTien,
        thoiGianVao,
        thoiGianRa,
        giamGia
    FROM
        HoaDon AS b
        INNER JOIN Ban AS t ON t.id = b.idBan
        INNER JOIN ThongTinHoaDon AS bi ON bi.idHoaDon = b.id
        INNER JOIN MonAn AS f ON f.id = bi.idMonAn
    WHERE
        thoiGianVao >= @checkIn
        AND thoiGianRa <= @checkOut
        AND b.trangThai = 1
    -- Thêm điều kiện bổ sung nếu cần
END
GO

select * from ban

SELECT f.tenMonAn, bi.soLuongMon, f.giaMonAn, f.giaMonAn*bi.soLuongMon as thanhTien FROM dbo.ThongTinHoaDon as bi, dbo.HoaDon as b, dbo.MonAn as f where bi.idHoaDon = b.id and bi.idMonAn = f.id and b.trangThai = 0 and b.idBan = 8
GO
create PROC USP_xuatHD
@id INT
AS
BEGIN
    SELECT f.tenMonAn, bi.soLuongMon, f.giaMonAn, f.giaMonAn*bi.soLuongMon as thanhTien FROM dbo.ThongTinHoaDon as bi, dbo.HoaDon as b, dbo.MonAn as f where bi.idHoaDon = b.id and bi.idMonAn = f.id and b.trangThai = 0 and b.idBan = @id
END
GO

ALTER PROC USP_xuatHD
@id INT
AS
BEGIN
    SELECT b.tongTien, f.tenMonAn, bi.soLuongMon, f.giaMonAn, f.giaMonAn*bi.soLuongMon as thanhTien FROM dbo.ThongTinHoaDon as bi, dbo.HoaDon as b, dbo.MonAn as f where bi.idHoaDon = b.id and bi.idMonAn = f.id and b.trangThai = 0 and b.idBan = @id
END
GO

SELECT b.giamGia, f.tenMonAn, bi.soLuongMon, f.giaMonAn, f.giaMonAn*bi.soLuongMon as thanhTien FROM dbo.ThongTinHoaDon as bi, dbo.HoaDon as b, dbo.MonAn as f where bi.idHoaDon = b.id and bi.idMonAn = f.id and b.trangThai = 0 and b.idBan = 8