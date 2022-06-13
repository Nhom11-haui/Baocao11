create database QLCuaHangMayTinh
use QLCuaHangMayTinh
--Bang 1
create table ThongTinMayTinh(MaMT char(10) primary key, tenmt varchar(50), MaNCC char(10) foreign key (MaNCC) references NhaCungCap(MaNCC) on delete cascade on update cascade
, soluong int, giaban int)
--Bang 2
create table NhanVien(MaNV char(10) primary key, TenNV varchar(50), GioiTinh nvarchar(50), DiaChi nvarchar(50),sdt int)
--Bang 3
create table NhaCungCap(MaNCC char(10) primary key, TenNCC varchar(50),Diachi nvarchar(50),sdt int)
--Bang 4
create table KhachHang(MaKH char(10) primary key, TenKH varchar(50), GioiTinh nvarchar(50), DiaChi nvarchar(50), sdt int)
--Bang 5
create table Hoadonnhap(MaHDN char(10) primary key,MaNV char(10) foreign key (MaNV) references NhanVien(MaNV)
,MaMT char(10) foreign key (MaMT) references ThongTinMayTinh(MaMT)
,MaNCC char(10) foreign key (MaNCC) references NhaCungCap(MaNCC)
,Soluong int,Ngaynhap date,Diachi varchar(50),sdt int, Dongia float,Tongtien float)
--Bang 6
create table Hoadonban(MaHDB char(10) primary key,MaNV char(10) foreign key (MaNV)references NhanVien(MaNV)
,MaKH char(10) foreign key (MaKH) references KhachHang(MaKH)
,MaMT char(10)foreign key (MaMT) references ThongTinMayTinh(MaMT)
,Soluong int,Ngayban date,Diachi varchar(50),sdt int, Dongia float,Tongtien float)

--Them thong tin nha cung cap
insert into NhaCungCap values('NCC01','TRANTUYEN','HY','1659939186')
insert into NhaCungCap values('NCC02','TRINHTHUY','HY','122345')
---Them nhan vien
insert into NhanVien values('NV01', 'Nguyen Van A', 'Nam', 'Ha Noi', '0123456789')
insert into NhanVien values('NV02', 'Nguyen Van B', 'Nam', 'Ha Noi', '0123456789')
---Them khach hang
insert into KhachHang values('KH01', 'Nguyen Van A', 'Nam', 'Ha Noi', '0123456789')
insert into KhachHang values('KH02', 'Nguyen Van B', 'Nam', 'Ha Noi', '0123456789')
--Them thong tin may tinh
insert into ThongTinMayTinh values('MT01','DELL','NCC01','2','1200')
insert into ThongTinMayTinh values('MT02','ASUS','NCC01','2','1200')

--Them thong tin hoa don ban
insert into Hoadonban values ('HDB01','NV01','KH01','MT01','1200','2014-01-01','HY','1659939285','120','144000')
insert into Hoadonban values ('HDB02','NV01','KH01','MT01','12','2014-01-03','HY','1356789','100','1200')

--Them thong tin hoa don nhap
insert into Hoadonnhap values ('HDN01','NV01','MT01','NCC01','125','2014-01-10','HN','1465676778','1005','125625')
insert into Hoadonnhap values ('HDN02','NV01','MT01','NCC01','23','2014-01-01','HY','2132456','12000','276000')








