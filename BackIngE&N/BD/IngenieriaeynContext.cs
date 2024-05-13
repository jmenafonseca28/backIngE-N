using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackIngE_N.BD;

public partial class IngenieriaeynContext : DbContext {
    public IngenieriaeynContext() {
    }

    public IngenieriaeynContext(DbContextOptions<IngenieriaeynContext> options)
        : base(options) {
    }

    public virtual DbSet<Channel> Channels { get; set; }

    public virtual DbSet<ChannelPlayList> ChannelPlayLists { get; set; }

    public virtual DbSet<PlayList> PlayLists { get; set; }

    public virtual DbSet<Userr> Userrs { get; set; }

    public virtual DbSet<Security> Securities { get; set; }

    public virtual DbSet<BlockedIP> BlockedIPs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Channel>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Channel__3213E83FF5D86981");

            entity.ToTable("Channel");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("logo");
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
        });

        modelBuilder.Entity<ChannelPlayList>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Channel___3213E83F96E7E537");

            entity.ToTable("Channel_PlayList");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.ChannelId).HasColumnName("channel_id");
            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");

            entity.HasOne(d => d.Channel).WithMany(p => p.ChannelPlayLists)
                .HasForeignKey(d => d.ChannelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_channel");

            entity.HasOne(d => d.Playlist).WithMany(p => p.ChannelPlayLists)
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
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.User).WithMany(p => p.PlayLists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<Userr>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Userr__3213E83F8F800657");

            entity.ToTable("Userr");

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

            entity.HasIndex(e => e.Email, "UQ_Userr_Email").IsUnique();
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
            entity.Property(e => e.StatusLogin)
            .HasColumnName("status_login");

        });

        modelBuilder.Entity<BlockedIP>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__BlockedI__3213E83F4711E8FB");

            entity.ToTable("BlockedIP");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Ip)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ip");
            entity.Property(e => e.BlockTime)
                .HasColumnType("datetime")
                .HasColumnName("block_time");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
