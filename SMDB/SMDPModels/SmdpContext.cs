using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SMDP.SMDPModels;

public partial class SmdpContext : DbContext
{
    private IConfiguration _configuration;

    public SmdpContext()
    {
        _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    }

    public SmdpContext(DbContextOptions<SmdpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DailyPrice> DailyPrices { get; set; }

    public virtual DbSet<Fund> Funds { get; set; }

    public virtual DbSet<Industry> Industries { get; set; }

    public virtual DbSet<Instrument> Instruments { get; set; }

    public virtual DbSet<LetterType> LetterTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)       
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("connectionString"));


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Persian_100_CI_AI");

        modelBuilder.Entity<DailyPrice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Companie__3214EC070F429F2A");
        });

        modelBuilder.Entity<Fund>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Indices__40BC8AA10C8C24C8");

            entity.Property(e => e.InstituteType).UseCollation("Persian_100_BIN");
        });

        modelBuilder.Entity<Industry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Industri__3214EC074F81A4CF");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IndustryName).UseCollation("Persian_100_BIN");
            entity.Property(e => e.IndustryNameEnglish).UseCollation("Persian_100_BIN");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.StatusId).HasDefaultValueSql("((1))");
        });
        modelBuilder.HasSequence("hibernate_sequence");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
