using Accepted_Assignment.Entities;
using Accepted_Assignment.Helpers.Enum;
using System;

namespace Accepted_Assignment.Models
{
	public class MatchModel
	{
		public int? ID { get; set; }
		public String Description { get; set; }
		public DateTime MatchDate { get; set; }
		public String MatchTime => this.MatchDate.ToString("HH:mm");
		public String TeamA { get; set; }
		public String TeamB { get; set; }
		public Sport? Sport { get; set; }

		public MatchModel()
		{

		}

		public MatchModel(Match match)
		{
			this.ID = match.ID;
			this.Description = match.Description;
			this.MatchDate = match.MatchDate;
			this.TeamA = match.TeamA;
			this.TeamB = match.TeamB;
			this.Sport = match.Sport;
		}
	}
}
