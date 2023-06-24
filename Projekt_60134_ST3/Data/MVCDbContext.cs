using Microsoft.EntityFrameworkCore;
using Projekt_60134_ST3.Models.Domain;

namespace Projekt_60134_ST3.Data
{
	public class MVCDbContext : DbContext
	{
		public MVCDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
	}
}
