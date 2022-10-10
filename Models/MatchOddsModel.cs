using Accepted_Assignment.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Accepted_Assignment.Models
{
	public class MatchOddsModel
	{
		public int? ID { get; set; }

		[Required]
		public int? MatchId { get; set; }
		public String Specifier { get; set; }
		public Decimal? Odd { get; set; }
		public MatchModel Match { get; set; }

		public MatchOddsModel()
		{

		}

		public MatchOddsModel(MatchOdds matchOdds)
		{
			this.ID = matchOdds.ID;
			this.MatchId = matchOdds.MatchId;
			this.Specifier = matchOdds.Specifier;
			this.Odd = matchOdds.Odd;
			this.Match = matchOdds.Match != null ? new MatchModel(matchOdds.Match) : null;
		}
	}
}
