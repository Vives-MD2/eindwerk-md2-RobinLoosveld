using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Thunderstruck.DOMAIN.Helpers;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.DAL
{
    public class ThunderstruckContext : DbContext
    {
        public static string LocalConnectionString { get; set; } = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ThunderstruckDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string OnlineConnectionString { get; set; } = @"";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(LocalConnectionString,
                x => x.UseNetTopologySuite());
            //optionsBuilder.UseSqlServer(OnlineConnectionString);
            //optionsBuilder.EnableSensitiveDataLogging(true);
        }
        #region DbSets
        //
        public DbSet<User> Users { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<LocationData> LocationsData { get; set; }
        //
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<UserLocationData> UsersLocationsData { get; set; }
        //
        public DbSet<AchievementFirstRain> AchievementFirstRains { get; set; }
        public DbSet<AchievementHighVoltage> AchievementHighVoltages { get; set; }
        public DbSet<AchievementListening> AchievementListenings { get; set; }
        public DbSet<AchievementSpeed> AchievementsSpeeds { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region IGNORE  
            modelBuilder.Ignore<GObject>();
            modelBuilder.Ignore<ThunderstruckException>();
            modelBuilder.Ignore<Exception>();
            #endregion

            #region PRIMARY KEYS

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<Achievement>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<LocationData>()
                .HasKey(ld => ld.Id);

            modelBuilder.Entity<AchievementFirstRain>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<AchievementHighVoltage>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<AchievementListening>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<AchievementSpeed>()
                .HasKey(a => a.Id);

            #endregion

            #region USER
            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .HasMaxLength(50);
            #endregion
            #region UNIQUE KEYS USER
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
            #endregion

            #region ACHIEVEMENT
            modelBuilder.Entity<Achievement>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Achievement>()
                .Property(u => u.Description)
                .IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<Achievement>()
                .Property(u => u.Reward)
                .IsRequired();
            modelBuilder.Entity<Achievement>()
                .Property(u => u.IconImageLink)
                .HasMaxLength(255);
            #endregion

            #region LOCATIONDATA
            modelBuilder.Entity<LocationData>()
                .Property(l =>l.LocationName)
                .HasMaxLength(50);
            modelBuilder.Entity<LocationData>()
                .Property(l => l.TimeStamp)
                .HasColumnType("datetime")
                .IsRequired();
            modelBuilder.Entity<LocationData>()
                .Property(l => l.Location)
                .IsRequired()
                .HasColumnType("geography");
            #endregion

            #region ACHIEVEMENTFIRSTRAIN

            modelBuilder.Entity<AchievementFirstRain>()
                .Property(u => u.IsRaining)
                .IsRequired()
                .HasColumnType("bit");
            #endregion

            #region ACHIEVEMENTHIGHVOLTAGE
            modelBuilder.Entity<AchievementHighVoltage>()
                .Property(u => u.IsThunderstorm)
                .IsRequired()
                .HasColumnType("bit");
            #endregion

            #region ACHIEVEMENTLISTENING
            modelBuilder.Entity<AchievementListening>()
                .Property(u => u.TotalPlayTime)
                .IsRequired();
            #endregion

            #region ACHIEVEMENTSPEED
            modelBuilder.Entity<AchievementSpeed>()
                .Property(u => u.Speed)
                .IsRequired();
            #endregion

            #region ONE TO ONE
            //AchievementFirstRain
            modelBuilder.Entity<Achievement>()
                .HasOne<AchievementFirstRain>(a => a.AchievementFirstRain)
                .WithOne(ad => ad.Achievement)
                .HasForeignKey<AchievementFirstRain>(a => a.AchievementId);

            //AchievementHightVoltage
            modelBuilder.Entity<Achievement>()
                .HasOne<AchievementHighVoltage>(a => a.AchievementHighVoltage)
                .WithOne(ad => ad.Achievement)
                .HasForeignKey<AchievementHighVoltage>(a => a.AchievementId);

            //AchievementListening
            modelBuilder.Entity<Achievement>()
                .HasOne<AchievementListening>(a => a.AchievementListening)
                .WithOne(ad => ad.Achievement)
                .HasForeignKey<AchievementListening>(a => a.AchievementId);

            //AchievementSpeed
            modelBuilder.Entity<Achievement>()
                .HasOne<AchievementSpeed>(a => a.AchievementSpeed)
                .WithOne(ad => ad.Achievement)
                .HasForeignKey<AchievementSpeed>(a => a.AchievementId);

            #endregion

            #region MANY TO MANY
            //UserAchievement
            modelBuilder.Entity<UserAchievement>()
                    .HasKey(ua => new { ua.UserId, ua.AchievementId });

            modelBuilder.Entity<UserAchievement>()
                .HasOne(u => u.User)
                .WithMany(l => l.UserAchievements)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserAchievement>()
                .HasOne(l => l.Achievement)
                .WithMany(u => u.UserAchievements)
                .HasForeignKey(l => l.AchievementId);

            //UserLocationData
            modelBuilder.Entity<UserLocationData>()
                .HasKey(ul => new { ul.UserId, ul.LocationDataId });

            modelBuilder.Entity<UserLocationData>()
                .HasOne(ld => ld.LocationData)
                .WithMany(ld => ld.UserLocationsData)
                .HasForeignKey(ld => ld.LocationDataId);

            modelBuilder.Entity<UserLocationData>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserLocationsData)
                .HasForeignKey(u => u.LocationDataId);
            #endregion
        }

    }
}
