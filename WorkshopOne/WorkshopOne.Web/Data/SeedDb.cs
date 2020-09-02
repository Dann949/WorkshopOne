using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopOne.Common.Entities;

namespace WorkshopOne.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountrysideAsync();

        }

        private async Task CheckCountrysideAsync()
        {
            if (!_context.countrysides.Any())
            {
                if (!_context.countrysides.Any())
                {
                    _context.countrysides.Add(new Countryside
                    {
                        Name = "Campo 1",
                        Districts = new List<District>
                {
                    new District
                    {
                        Name = "Distrito A",
                        Churches = new List<church>
                        {
                            new  church { Name = "Iglesia el calvario" },
                            new church { Name = "Iglesia San Juan" },
                            new church { Name = "Iglesia la Candelaria" }
                        }
                    },
                    new District
                    {
                        Name = "Distrito B",
                        Churches = new List<church>
                        {
                            new church { Name = "Iglesia santa Lucia" },
                            new church { Name = "Iglesia san Marcos" }
                        }
                    },
                    new District
                    {
                        Name = "Distrito C",
                        Churches = new List<church>
                        {
                             new church { Name = "Iglesia Botero" },
                             new church { Name = "Iglesia san sepulcro" },
                             new church { Name = "Iglesia Mariana" },
                        }
                    }
                }
                    });

                    await _context.SaveChangesAsync();

                }

            }
        }
    }
}
