using System;
using System.Collections.Generic;
using BackIngE_N.Models.BD;
using Microsoft.EntityFrameworkCore;

namespace BackIngE_N.Context;

public partial class IngenieriaeynContext : DbContext {
    public IngenieriaeynContext() {
    }

    public IngenieriaeynContext(DbContextOptions<IngenieriaeynContext> options)
        : base(options) {
    }

    public virtual DbSet<BlockedIp> BlockedIps { get; set; }

    public virtual DbSet<Channel> Channels { get; set; }

    public virtual DbSet<PlayList> PlayLists { get; set; }

    public virtual DbSet<Security> Securities { get; set; }

    public virtual DbSet<Userr> Userrs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<BlockedIp>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__BlockedI__3213E83F4711E8FB");

            entity.ToTable("BlockedIP");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.BlockTime)
                .HasColumnType("datetime")
                .HasColumnName("block_time");
            entity.Property(e => e.Ip)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ip");
        });

        modelBuilder.Entity<Channel>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Channel__3213E83F8A6ECE67");
            entity.ToTable("Channel");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("logo");
            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.TvgChannelNumber).HasColumnName("tvg_channel_number");
            entity.Property(e => e.TvgId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tvg_id");
            entity.Property(e => e.TvgName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tvg_name");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("url");
            entity.Property(e => e.GroupTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("group_title");
            entity.Property(e => e.orderList)
                .HasColumnName("order_list");

            entity.HasOne(d => d.Playlist).WithMany(p => p.Channels)
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_playlist");
        });

        modelBuilder.Entity<PlayList>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__PlayList__3213E83F175CA317");

            entity.ToTable("PlayList");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.PlayLists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<Security>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Security__3213E83F930700BE");

            entity.ToTable("Security");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Ip)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ip");
            entity.Property(e => e.LoginTime)
                .HasColumnType("datetime")
                .HasColumnName("login_time");
            entity.Property(e => e.StatusLogin).HasColumnName("status_login");
        });

        modelBuilder.Entity<Userr>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Userr__3213E83F8F800657");

            entity.ToTable("Userr");

            entity.HasIndex(e => e.Email, "UQ_Userr_Email").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Token)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("token");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
