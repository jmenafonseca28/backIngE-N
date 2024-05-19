using System;
using System.Collections.Generic;
using BackIngE_N.BD;
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

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Channel>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Channel__3213E83FF5D86981");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasMany(d => d.Playlists).WithMany(p => p.Channels)
                .UsingEntity<Dictionary<string, object>>(
                    "ChannelPlayList",
                    r => r.HasOne<PlayList>().WithMany()
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_playlist"),
                    l => l.HasOne<Channel>().WithMany()
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_channel"),
                    j => {
                        j.HasKey("ChannelId", "PlaylistId").HasName("pk_channel_playlist");
                        j.ToTable("Channel_PlayList");
                        j.IndexerProperty<Guid>("ChannelId").HasColumnName("channel_id");
                        j.IndexerProperty<Guid>("PlaylistId").HasColumnName("playlist_id");
                    });
        });

        modelBuilder.Entity<PlayList>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__PlayList__3213E83F175CA317");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.User).WithMany(p => p.PlayLists)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<Security>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Security__3213E83F930700BE");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Userr>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Userr__3213E83F8F800657");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
