using Microsoft.EntityFrameworkCore;

namespace Accepted_Assignment.Entities
{
	public class AppDbContext : DbContext
	{

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Match> Matches { get; set; }
		public DbSet<MatchOdds> MatchOdds { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Match>()
				.Ignore(x => x.MatchTime);

			modelBuilder.Entity<MatchOdds>()
				.HasOne(x => x.Match)
				.WithMany()
				.HasForeignKey(x => x.MatchId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<MatchOdds>()
			  .Property(p => p.Odd)
			  .HasColumnType("decimal(4,2)");
		}
	}
}
