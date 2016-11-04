using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BWSC.Models;

namespace BWSC.Data
{
    public class SwimmingClubContext : DbContext
    {
        public SwimmingClubContext(DbContextOptions<SwimmingClubContext> options) : base(options)
        {

        }
        public DbSet<Swimmer> Swimmers { get; set; }
        public DbSet<Squad> Squads { get; set; }
        public DbSet<Coach> Coaches { get; set; }
    }
}
