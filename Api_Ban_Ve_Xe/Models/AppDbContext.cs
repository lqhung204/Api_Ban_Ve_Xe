using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Api_Ban_Ve_Xe.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChiTietVeXe> ChiTietVeXes { get; set; } = null!;
        public virtual DbSet<ChuyenXe> ChuyenXes { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<LoaiXe> LoaiXes { get; set; } = null!;
        public virtual DbSet<TaiXe> TaiXes { get; set; } = null!;
        public virtual DbSet<TuyenXe> TuyenXes { get; set; } = null!;
        public virtual DbSet<VeXe> VeXes { get; set; } = null!;
        public virtual DbSet<Xe> Xes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-14S8E6S\\HUNGLE;Initial Catalog=BANVEXE;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietVeXe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Chi_Tiet_Ve_Xe");

                entity.Property(e => e.MaVe).HasColumnName("Ma_Ve");
                entity.Property(e => e.NgayDat).HasColumnName("Ngay_Dat");
                entity.Property(e => e.SoLuongVe).HasColumnName("So_Luong_Ve");

                entity.HasOne(d => d.MaVeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaVe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Chi_Tiet_Ve_Xe_Ve_Xe");

                
            });

            modelBuilder.Entity<ChuyenXe>(entity =>
            {
                entity.HasKey(e => e.MaChuyenXe);

                entity.ToTable("Chuyen_Xe");

                entity.Property(e => e.MaChuyenXe)
                    .ValueGeneratedNever()
                    .HasColumnName("Ma_Chuyen_Xe");

                entity.Property(e => e.ChoTrong).HasColumnName("Cho_Trong");

                entity.Property(e => e.GioDen).HasColumnName("Gio_Den");

                entity.Property(e => e.GioDi).HasColumnName("Gio_Di");

                entity.Property(e => e.MaTaiXe).HasColumnName("Ma_Tai_Xe");

                entity.Property(e => e.MaTuyen).HasColumnName("Ma_Tuyen");
                entity.Property(e => e.NgayDi).HasColumnName("Ngay_Di");
                entity.Property(e => e.MaXe).HasColumnName("Ma_Xe");
                entity.Property(e => e.TenChuyenXe)
                    .HasMaxLength(10)
                    .HasColumnName("Ten_Chuyen_Xe")
                    .IsFixedLength();

                entity.HasOne(d => d.MaTaiXeNavigation)
                    .WithMany(p => p.ChuyenXes)
                    .HasForeignKey(d => d.MaTaiXe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Chuyen_Xe_Tai_Xe");

                entity.HasOne(d => d.MaTuyenNavigation)
                    .WithMany(p => p.ChuyenXes)
                    .HasForeignKey(d => d.MaTuyen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Chuyen_Xe_Tuyen_Xe");
                entity.HasOne(d => d.MaXeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaXe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Chuyen_Xe_Ma_Xe");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKhachHang)
                    .HasName("PK_KhachHang");

                entity.ToTable("Khach_Hang");

                entity.Property(e => e.MaKhachHang)
                    .ValueGeneratedNever()
                    .HasColumnName("Ma_Khach_Hang");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CCCD")
                    .IsFixedLength();

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(50)
                    .HasColumnName("Dia_Chi")
                    .IsFixedLength();

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Dien_Thoai")
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.GioiTinh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Gioi_Tinh")
                    .IsFixedLength();

                entity.Property(e => e.NgaySinh)
                    .HasColumnType("date")
                    .HasColumnName("Ngay_Sinh");

                entity.Property(e => e.TenKhachHang)
                    .HasMaxLength(50)
                    .HasColumnName("Ten_Khach_Hang");
            });

            modelBuilder.Entity<LoaiXe>(entity =>
            {
                entity.HasKey(e => e.MaLoaiXe);

                entity.ToTable("Loai_Xe");

                entity.Property(e => e.MaLoaiXe)
                    .ValueGeneratedNever()
                    .HasColumnName("Ma_Loai_Xe");

                entity.Property(e => e.TenLoaiXe)
                    .HasMaxLength(20)
                    .HasColumnName("Ten_Loai_Xe")
                    .IsFixedLength();
            });

            modelBuilder.Entity<TaiXe>(entity =>
            {
                entity.HasKey(e => e.MaTaiXe);

                entity.ToTable("Tai_Xe");

                entity.Property(e => e.MaTaiXe)
                    .ValueGeneratedNever()
                    .HasColumnName("Ma_Tai_Xe");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CCCD")
                    .IsFixedLength();

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(50)
                    .HasColumnName("Dia_Chi")
                    .IsFixedLength();

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Dien_Thoai")
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.GioiTinh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Gioi_Tinh")
                    .IsFixedLength();

                entity.Property(e => e.NgaySinh)
                    .HasColumnType("date")
                    .HasColumnName("Ngay_Sinh");

                entity.Property(e => e.TenTaiXe)
                    .HasMaxLength(50)
                    .HasColumnName("Ten_Tai_Xe");
            });

            modelBuilder.Entity<TuyenXe>(entity =>
            {
                entity.HasKey(e => e.MaTuyen);

                entity.ToTable("Tuyen_Xe");

                entity.Property(e => e.MaTuyen)
                    .ValueGeneratedNever()
                    .HasColumnName("Ma_Tuyen");

                entity.Property(e => e.BangGia).HasColumnName("Bang_Gia");

                entity.Property(e => e.DiemDen)
                    .HasMaxLength(50)
                    .HasColumnName("Diem_Den");

                entity.Property(e => e.DiemDi)
                    .HasMaxLength(50)
                    .HasColumnName("Diem_Di");

                entity.Property(e => e.TenTuyen)
                    .HasMaxLength(50)
                    .HasColumnName("Ten_Tuyen");
            });

            modelBuilder.Entity<VeXe>(entity =>
            {
                entity.HasKey(e => e.MaVe);

                entity.ToTable("Ve_Xe");

                entity.Property(e => e.MaVe)
                    .ValueGeneratedNever()
                    .HasColumnName("Ma_Ve");

                entity.Property(e => e.MaChuyenXe).HasColumnName("Ma_Chuyen_Xe");

                entity.Property(e => e.MaKhachHang).HasColumnName("Ma_Khach_Hang");
                entity.Property(e => e.NgayDat)
                    .HasColumnType("date")
                    .HasColumnName("Ngay_Dat");
                entity.Property(e => e.SoLuongVe).HasColumnName("So_Ghe");

                entity.Property(e => e.TenVe)
                    .HasMaxLength(50)
                    .HasColumnName("Ten_Ve")
                    .IsFixedLength();

                entity.HasOne(d => d.MaChuyenXeNavigation)
                    .WithMany(p => p.VeXes)
                    .HasForeignKey(d => d.MaChuyenXe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ve_Xe_Chuyen_Xe");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.VeXes)
                    .HasForeignKey(d => d.MaKhachHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ve_Xe_Khach_Hang");
            });

            modelBuilder.Entity<Xe>(entity =>
            {
                entity.HasKey(e => e.MaXe);

                entity.ToTable("Xe");

                entity.Property(e => e.MaXe)
                    .ValueGeneratedNever()
                    .HasColumnName("Ma_Xe");

                entity.Property(e => e.BienSo)
                    .HasColumnType("ntext")
                    .HasColumnName("Bien_So");

                entity.Property(e => e.MaLoaiXe).HasColumnName("Ma_Loai_Xe");

                entity.Property(e => e.SoGhe)
                    .HasMaxLength(10)
                    .HasColumnName("So_Ghe")
                    .IsFixedLength();

                entity.Property(e => e.TenXe)
                    .HasMaxLength(50)
                    .HasColumnName("Ten_Xe")
                    .IsFixedLength();

                entity.HasOne(d => d.MaLoaiXeNavigation)
                    .WithMany(p => p.Xes)
                    .HasForeignKey(d => d.MaLoaiXe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Xe_Loai_Xe");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
