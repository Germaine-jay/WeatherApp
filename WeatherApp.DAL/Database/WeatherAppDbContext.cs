using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DAL.Entities;

namespace WeatherApp.DAL.Database
{
    public class WeatherAppDbContext: DbContext
    {
        public WeatherAppDbContext(DbContextOptions<WeatherAppDbContext> options) : base(options)
        {

        }

        public DbSet<City> Cities { get; set; }

    }
}
