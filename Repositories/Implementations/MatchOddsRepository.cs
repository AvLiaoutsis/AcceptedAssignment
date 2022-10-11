using Accepted_Assignment.Entities;
using Accepted_Assignment.Helpers;
using Accepted_Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accepted_Assignment.Repositories.Implementations
{
	public class MatchOddsRepository : IMatchOddsRepository
	{
		private readonly AppDbContext _context;
		public MatchOddsRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task Delete(int id)
		{
			MatchOdds data = await _context.MatchOdds.FirstOrDefaultAsync(x => x.ID == id);
			if (data == null)
			{
				throw new NullReferenceException("Not found");
			}
			else
			{
				_context.MatchOdds.Remove(data);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<MatchOddsModel> GetSingle(int id)
		{
			MatchOdds data = await _context.MatchOdds
				.Include(x => x.Match)
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.ID == id);

			if (data == null)
			{
				throw new NullReferenceException("Not found");
			}
			else
			{
				return new MatchOddsModel(data);
			}
		}

		public async Task<List<MatchOddsModel>> Query(Lookup lookup)
		{
			if (lookup == null) return null;

			IQueryable<MatchOdds> query = _context.MatchOdds.Include(x => x.Match);

			if (lookup.Limit != null) query =  query.Take(lookup.Limit.Value);

			if (lookup.Offset != null) query = query.Skip(lookup.Offset.Value);

			List<MatchOdds> data = await query.ToListAsync();

			List<MatchOddsModel> models = new List<MatchOddsModel>();

			data.ForEach(d => models.Add(new MatchOddsModel(d)));

			return models;
		}

		public async Task<MatchOddsModel> Upsert(MatchOddsModel model)
		{
			if (model.ID == null)
			{
				if(model.MatchId == null)
				{
					throw new ArgumentException("Match id property is required");

				}

				this.CheckExistingMatch(model.MatchId.Value);

				MatchOdds dataToBeAdded = new MatchOdds()
				{
					Odd = model.Odd,
					Specifier = model.Specifier,
					MatchId = model.MatchId.Value,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now
				};


				_context.MatchOdds.Add(dataToBeAdded);
				await _context.SaveChangesAsync();

				model.ID = dataToBeAdded.ID;
			}
			else
			{
				MatchOdds datum = await _context.MatchOdds.FirstOrDefaultAsync(x => x.ID == model.ID);
				if (datum != null)
				{
					if(model.MatchId != null)
					{
						this.CheckExistingMatch(model.MatchId.Value);
					}
					datum.Odd = model.Odd != null ? model.Odd : datum.Odd;
					datum.Specifier = model.Specifier != null ? model.Specifier : datum.Specifier;
					datum.MatchId = model.MatchId != null ? model.MatchId.Value : datum.MatchId;
					datum.UpdatedAt = DateTime.Now;

					await _context.SaveChangesAsync();
				}
				else
				{
					throw new NullReferenceException("NotFound");
				}
			}
			return model;
		}

		private void CheckExistingMatch(int matchId)
		{
			bool matchFound = _context.Matches.Any(x => x.ID == matchId);

			if (!matchFound)
			{
				throw new ArgumentException("This match is not available");
			}
		}
	}


}
