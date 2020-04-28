using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataBase.Models
{
    public partial class jingshenContext : DbContext
    {
        public jingshenContext()
        {
        }

        public jingshenContext(DbContextOptions<jingshenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistory { get; set; }
        public virtual DbSet<Efmigrationshistory1> Efmigrationshistory1 { get; set; }
        public virtual DbSet<HmAd> HmAd { get; set; }
        public virtual DbSet<HmAdPosition> HmAdPosition { get; set; }
        public virtual DbSet<HmAdmin> HmAdmin { get; set; }
        public virtual DbSet<HmAdminLog> HmAdminLog { get; set; }
        public virtual DbSet<HmArticle> HmArticle { get; set; }
        public virtual DbSet<HmArticleCategory> HmArticleCategory { get; set; }
        public virtual DbSet<HmDoctor> HmDoctor { get; set; }
        public virtual DbSet<HmGonggao> HmGonggao { get; set; }
        public virtual DbSet<HmRbacPower> HmRbacPower { get; set; }
        public virtual DbSet<HmRbacPowerRole> HmRbacPowerRole { get; set; }
        public virtual DbSet<HmRbacRole> HmRbacRole { get; set; }
        public virtual DbSet<HmRbacRoleAdmin> HmRbacRoleAdmin { get; set; }
        public virtual DbSet<HmReview> HmReview { get; set; }
        public virtual DbSet<HmSystemfoot> HmSystemfoot { get; set; }
        public virtual DbSet<HmSystemfriendlink> HmSystemfriendlink { get; set; }
        public virtual DbSet<HmUserLog> HmUserLog { get; set; }
        public virtual DbSet<Userclass> Userclass { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=134.175.147.106;userid=jingshen;pwd=HG8XpGtThkRk3EeE;port=3306;database=jingshen;sslmode=none;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EfmigrationsHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__EFMigrationsHistory");

                entity.Property(e => e.MigrationId).HasColumnType("varchar(95)");

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasColumnType("varchar(32)");
            });
            
            modelBuilder.Entity<Efmigrationshistory1>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasColumnType("varchar(95)");

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasColumnType("varchar(32)");
            });

            modelBuilder.Entity<HmAd>(entity =>
            {
                entity.ToTable("hm_ad");

                entity.HasIndex(e => e.PositionId)
                    .HasName("position_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnName("image")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PositionId)
                    .HasColumnName("position_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<HmAdPosition>(entity =>
            {
                entity.ToTable("hm_ad_position");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<HmAdmin>(entity =>
            {
                entity.ToTable("hm_admin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.Lasttime)
                    .HasColumnName("lasttime")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(64)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(32)");
            });

            modelBuilder.Entity<HmAdminLog>(entity =>
            {
                entity.ToTable("hm_admin_log");

                entity.HasIndex(e => e.AdminId)
                    .HasName("admin_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdminId)
                    .HasColumnName("admin_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LogInfo)
                    .IsRequired()
                    .HasColumnName("log_info")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LogIp)
                    .IsRequired()
                    .HasColumnName("log_ip")
                    .HasColumnType("varchar(30)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LogTime)
                    .HasColumnName("log_time")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<HmArticle>(entity =>
            {
                entity.ToTable("hm_article");

                entity.HasIndex(e => e.CateId)
                    .HasName("cate_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Addtime)
                    .HasColumnName("addtime")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CateId)
                    .HasColumnName("cate_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnName("image")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.IsShow)
                    .HasColumnName("is_show")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Sort)
                    .HasColumnName("sort")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'50'");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<HmArticleCategory>(entity =>
            {
                entity.ToTable("hm_article_category");

                entity.HasIndex(e => e.ParentId)
                    .HasName("parent_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.ParentId)
                    .HasColumnName("parent_id")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<HmDoctor>(entity =>
            {
                entity.ToTable("hm_doctor");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Edu)
                    .HasColumnName("edu")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnName("image")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Job)
                    .IsRequired()
                    .HasColumnName("job")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.OpRoom)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.OpTime)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Room)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<HmGonggao>(entity =>
            {
                entity.ToTable("hm_gonggao");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Addtime)
                    .HasColumnName("addtime")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Dianji)
                    .HasColumnName("dianji")
                    .HasColumnType("int(50)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IsShow)
                    .HasColumnName("is_show")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Shenpi)
                    .HasColumnName("shenpi")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Tougao)
                    .HasColumnName("tougao")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<HmRbacPower>(entity =>
            {
                entity.ToTable("hm_rbac_power");

                entity.HasIndex(e => e.ParentId)
                    .HasName("parent_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasColumnName("action")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Controller)
                    .IsRequired()
                    .HasColumnName("controller")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasColumnName("icon")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.ParentId)
                    .HasColumnName("parent_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Sort)
                    .HasColumnName("sort")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'50'");
            });

            modelBuilder.Entity<HmRbacPowerRole>(entity =>
            {
                entity.ToTable("hm_rbac_power_role");

                entity.HasIndex(e => e.PowerId)
                    .HasName("power_id");

                entity.HasIndex(e => e.RoleId)
                    .HasName("role_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PowerId)
                    .HasColumnName("power_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<HmRbacRole>(entity =>
            {
                entity.ToTable("hm_rbac_role");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<HmRbacRoleAdmin>(entity =>
            {
                entity.ToTable("hm_rbac_role_admin");

                entity.HasIndex(e => e.AdminId)
                    .HasName("admin_id");

                entity.HasIndex(e => e.RoleId)
                    .HasName("role_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AdminId)
                    .HasColumnName("admin_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<HmReview>(entity =>
            {
                entity.ToTable("hm_review");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Adminid)
                    .HasColumnName("adminid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Review)
                    .HasColumnName("review")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Reviewtime)
                    .HasColumnName("reviewtime")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<HmSystemfoot>(entity =>
            {
                entity.ToTable("hm_systemfoot");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Faxphone)
                    .IsRequired()
                    .HasColumnName("faxphone")
                    .HasColumnType("varchar(30)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Footstr)
                    .IsRequired()
                    .HasColumnName("footstr")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Logo)
                    .IsRequired()
                    .HasColumnName("logo")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Zipcode)
                    .IsRequired()
                    .HasColumnName("zipcode")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<HmSystemfriendlink>(entity =>
            {
                entity.ToTable("hm_systemfriendlink");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Linkname)
                    .IsRequired()
                    .HasColumnName("linkname")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<HmUserLog>(entity =>
            {
                entity.ToTable("hm_UserLog");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("ip")
                    .HasColumnType("varchar(30)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("date")
                    .HasDefaultValueSql("'2000-01-01'");
            });

            modelBuilder.Entity<Userclass>(entity =>
            {
                entity.ToTable("userclass");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Power).HasColumnType("varchar(16)");

                entity.Property(e => e.Pwd).HasColumnType("varchar(16)");
            });
        }
    }
}
