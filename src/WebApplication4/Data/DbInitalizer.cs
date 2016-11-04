using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BWSC.Data;
using System.Threading.Tasks;
using BWSC.Models;

namespace BWSC.Data
{
    public class DbInitializer
    {
        public static void Initalize(SwimmingClubContext context)
        {
            context.Database.EnsureCreated();

            if (context.Swimmers.Any())
            {
                return;
            }

            var squads = new Squad[]
            {
                new Squad {Name="Youth Performance"},
                new Squad {Name="Youth Competitive" }
            };
            foreach (Squad sq in squads)
            {
                context.Squads.Add(sq);
            }
            context.SaveChanges();

            var swimmers = new Swimmer[]
            {
                new Swimmer {FirstName="Sinead",Surname="Clay",ASANumber="123456",DOB=DateTime.Parse("2001-01-01"),StartDate=DateTime.Parse("2008-01-01"),SquadID=1},
                new Swimmer {FirstName="Henry",Surname="Clay",ASANumber="123456",DOB=DateTime.Parse("2011-01-01"),StartDate=DateTime.Parse("2015-01-01"),SquadID=2}
            };
            foreach (Swimmer s in swimmers)
            {
                context.Swimmers.Add(s);
            }
            context.SaveChanges();

            var coaches = new Coach[]
            {
                new Coach {FirstName="Mike",Surname="Bloggs",StartDate=DateTime.Parse("2002-01-01"),SquadID=1},
                new Coach {FirstName="Andy",Surname="Smith",StartDate=DateTime.Parse("2002-01-01"),SquadID=2}
            };
            foreach (Coach c in coaches)
            {
                context.Coaches.Add(c);
            }
            context.SaveChanges();

        }
    }
}
