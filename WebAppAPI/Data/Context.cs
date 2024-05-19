using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Models;


namespace WebAppAPI.Data
{
	public partial class Context : IdentityDbContext<Users>
	{

		public Context(DbContextOptions<Context> options) : base(options) { }

		public virtual DbSet<Message> Messages { get; set; }
		public virtual DbSet<Users> Users { get; set ; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("TestAppUser");

			modelBuilder.Entity<IdentityUserLogin<string>>()
				.HasKey(login => new { login.LoginProvider, login.ProviderKey });

			modelBuilder.Entity<Message>(entity =>
			{
				entity.Property(e => e.Budget)
					  .HasColumnType("decimal(18, 2)");
			});
			base.OnModelCreating(modelBuilder);
		}

	}
}
