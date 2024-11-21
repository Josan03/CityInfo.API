﻿using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("connectionString");

            base.OnConfiguring(optionsBuilder);
        }*/

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                    new City("New York City")
                    {
                        Id = 1,
                        Description = "The one with that big park."
                    },
                    new City("Antwerp")
                    {
                        Id = 2,
                        Description = "The one with the cathedral that was never really finished."
                    },
                    new City("Paris")
                    {
                        Id = 3,
                        Description = "The one with that big tower."
                    });

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                    new PointOfInterest("Central Park")
                    {
                        Id = 1,
                        CityId = 1,
                        Description = "The most visited urban park in the United States."
                    },
                    new PointOfInterest("Empire State Building")
                    {
                        Id = 2,
                        CityId = 1,
                        Description = "A 102-story skyscraper located in Midtown Manhattan."
                    },
                    new PointOfInterest("Cathedral")
                    {
                        Id = 3,
                        CityId = 2,
                        Description = "A cathedral church of Saint John the Divine."
                    },
                    new PointOfInterest("Antwerp Central Station")
                    {
                        Id = 4,
                        CityId = 2,
                        Description = "The the finest example of railway architecture in Belgium."
                    },
                    new PointOfInterest("Eiffel Tower")
                    {
                        Id = 5,
                        CityId = 3,
                        Description = "A wrought-iron lattice tower on the Champ de Mars in Paris."
                    });

            base.OnModelCreating(modelBuilder);
        }
    }
}
