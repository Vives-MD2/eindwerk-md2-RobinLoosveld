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
        public static string OnlineConnectionString { get; set; } = @"Data Source=SQL5108.site4now.net;Initial Catalog=db_a7d32f_robinloosveld;User Id=db_a7d32f_robinloosveld_admin;Password=xamarin5";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(LocalConnectionString,
            //     x => x.UseNetTopologySuite());
            optionsBuilder.UseSqlServer(OnlineConnectionString,x => x.UseNetTopologySuite());
            //optionsBuilder.EnableSensitiveDataLogging(true);
        }
        #region DbSets
        //
        public DbSet<User> User { get; set; }
        public DbSet<Achievement> Achievement { get; set; }
        public DbSet<LocationData> LocationData { get; set; }
        //
        public DbSet<UserAchievement> UserAchievement { get; set; }
        public DbSet<UserLocationData> UsersLocationData { get; set; }
        //
        public DbSet<AchievementRain> AchievementFirstRain { get; set; }
        public DbSet<AchievementHighVoltage> AchievementHighVoltage { get; set; }
        public DbSet<AchievementListening> AchievementListening { get; set; }
        public DbSet<AchievementSpeed> AchievementSpeed { get; set; }

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
            modelBuilder.Entity<LocationData>()
                .HasKey(ld => ld.Id);
            #endregion

            #region USER
            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .HasMaxLength(50);
            #endregion

            #region USER UNIQUE KEYS
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
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

            #region ACHIEVEMENTRAIN

            modelBuilder.Entity<AchievementRain>()
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

            #region ONE TO ONE INHERITANCE

            // source: https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/inheritance?view=aspnetcore-5.0
            // -> upgrade EF core to 5 or up, this doesn't work with EF core 3
            // -> ignore .net cli command, use package manager console "add-migration inheritance"
            // -> you don't need to replace the "Up" method in the migration file 

            modelBuilder.Entity<Achievement>().ToTable("Achievement");
            modelBuilder.Entity<AchievementRain>().ToTable("AchievementRain");
            modelBuilder.Entity<AchievementHighVoltage>().ToTable("AchievementHighVoltage");
            modelBuilder.Entity<AchievementListening>().ToTable("AchievementListening");
            modelBuilder.Entity<AchievementSpeed>().ToTable("AchievementSpeed");

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
