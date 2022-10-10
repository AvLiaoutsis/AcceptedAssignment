using Accepted_Assignment.Helpers.Enum;
using Accepted_Assignment.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accepted_Assignment.Entities
{
	public class Match
	{
		public int ID { get; set; }
		public String Description { get; set; }
		public DateTime MatchDate { get; set; }

		[NotMapped]
		public String MatchTime => this.MatchDate.ToString("HH:mm"); 
		public String TeamA { get; set; }
		public String TeamB { get; set; }
		public Sport Sport { get; set; }
		public DateTime UpdatedAt { get; set; }
		public DateTime CreatedAt { get; set; }

	}
}
