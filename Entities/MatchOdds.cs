using System;

namespace Accepted_Assignment.Entities
{
	public class MatchOdds
	{
		public int ID { get; set; }
		public int MatchId { get; set; }
		public String Specifier { get; set; }
		public Decimal? Odd { get; set; }
		public Match Match { get; set; }
		public DateTime UpdatedAt { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
