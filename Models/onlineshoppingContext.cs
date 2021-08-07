using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Skartwebapi.Models
{
    public partial class onlineshoppingContext : DbContext
    {
        public onlineshoppingContext()
        {
        }

        public onlineshoppingContext(DbContextOptions<onlineshoppingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCart> TblCarts { get; set; }
        public virtual DbSet<TblCategory> TblCategories { get; set; }
        public virtual DbSet<TblOrder> TblOrders { get; set; }
        public virtual DbSet<TblOrderdetail> TblOrderdetails { get; set; }
        public virtual DbSet<TblProduct> TblProducts { get; set; }
        public virtual DbSet<TblRetailer> TblRetailers { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-PL3O6N3;Database=onlineshopping;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblCart>(entity =>
            {
                entity.HasKey(e => new { e.Cartid, e.Productid })
                    .HasName("PK__tblCart__73B74D134E4A0672");

                entity.ToTable("tblCart");

                entity.Property(e => e.Cartid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("cartid");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Cartquantity).HasColumnName("cartquantity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblCarts)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblCart__product__440B1D61");
            });

            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.HasKey(e => e.Categoryid)
                    .HasName("PK__tblCateg__23CDE590FCE017FD");

                entity.ToTable("tblCategory");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Categorydescription).HasColumnName("categorydescription");

                entity.Property(e => e.Categoryname)
                    .IsRequired()
                    .HasColumnName("categoryname");
            });

            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.HasKey(e => e.Orderid)
                    .HasName("PK__tblOrder__080E37759EB425C2");

                entity.ToTable("tblOrder");

                entity.Property(e => e.Orderid).HasColumnName("orderid");

                entity.Property(e => e.Orderdate)
                    .HasColumnType("date")
                    .HasColumnName("orderdate");

                entity.Property(e => e.Orderprice).HasColumnName("orderprice");

                entity.Property(e => e.Orderquantity).HasColumnName("orderquantity");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblOrders)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("FK__tblOrder__produc__47DBAE45");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblOrders)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__tblOrder__userid__46E78A0C");
            });

            modelBuilder.Entity<TblOrderdetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblOrderdetails");

                entity.Property(e => e.Orderid).HasColumnName("orderid");

                entity.Property(e => e.Orderprice).HasColumnName("orderprice");

                entity.Property(e => e.Orderquantity).HasColumnName("orderquantity");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasForeignKey(d => d.Orderid)
                    .HasConstraintName("FK__tblOrderd__order__49C3F6B7");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("FK__tblOrderd__produ__4AB81AF0");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__tblOrderd__useri__4BAC3F29");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.Productid)
                    .HasName("PK__tblProdu__2D172D32C03F18E2");

                entity.ToTable("tblProduct");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Productbrand)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("productbrand");

                entity.Property(e => e.Productdescription).HasColumnName("productdescription");

                entity.Property(e => e.Productimage).HasColumnName("productimage");

                entity.Property(e => e.Productname).HasColumnName("productname");

                entity.Property(e => e.Productprice).HasColumnName("productprice");

                entity.Property(e => e.Productquantity).HasColumnName("productquantity");

                entity.Property(e => e.Retailerid).HasColumnName("retailerid");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TblProducts)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("FK__tblProduc__categ__412EB0B6");

                entity.HasOne(d => d.Retailer)
                    .WithMany(p => p.TblProducts)
                    .HasForeignKey(d => d.Retailerid)
                    .HasConstraintName("FK__tblProduc__retai__403A8C7D");
            });

            modelBuilder.Entity<TblRetailer>(entity =>
            {
                entity.HasKey(e => e.Retailerid)
                    .HasName("PK__tblRetai__7A12C3E0EB380616");

                entity.ToTable("tblRetailer");

                entity.HasIndex(e => e.Retaileremail, "UQ__tblRetai__F3B1033324DA38C3")
                    .IsUnique();

                entity.Property(e => e.Retailerid).HasColumnName("retailerid");

                entity.Property(e => e.Approved)
                    .HasColumnName("approved")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Retaileremail)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("retaileremail");

                entity.Property(e => e.Retailername)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("retailername");

                entity.Property(e => e.Retailerpassword)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("retailerpassword");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("PK__tblUser__CBA1B257C3FE5000");

                entity.ToTable("tblUser");

                entity.HasIndex(e => e.Userphone, "UQ__tblUser__310EC0A19C6C6554")
                    .IsUnique();

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Useraddress)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("useraddress");

                entity.Property(e => e.Useremail)
                    .HasMaxLength(255)
                    .HasColumnName("useremail");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Userpassword)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("userpassword");

                entity.Property(e => e.Userphone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("userphone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
