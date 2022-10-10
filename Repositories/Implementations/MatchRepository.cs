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
	public class MatchRepository : IMatchRepository
	{
		private readonly AppDbContext _context;
		public MatchRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task Delete(int id)
		{
			Match data = await _context.Matches.FirstOrDefaultAsync(x => x.ID == id);

			if(data == null)
			{
				throw new NullReferenceException("Not found");
			}
			else
			{
				_context.Matches.Remove(data);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<MatchModel> GetSingle(int id)
		{
			Match data = await _context.Matches
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.ID == id);

			if (data == null)
			{
				throw new NullReferenceException("Not found");
			}
			else
			{
				return new MatchModel(data);
			}
		}

		public async Task<List<MatchModel>> Query(Lookup lookup)
		{
			if(lookup == null) return null;

			IQueryable<Match> query = _context.Matches;

			if (lookup.Like != null) query = query.Where(x => x.TeamA.Contains(lookup.Like) || x.TeamB.Contains(lookup.Like));

			if (lookup.Limit != null) query = query.Take(lookup.Limit.Value);

			if (lookup.Offset != null) query = query.Skip(lookup.Offset.Value);



			List<Match> data = await query.ToListAsync();

			List<MatchModel> models = new List<MatchModel>();

			data.ForEach(d => models.Add(new MatchModel(d)));

			return models;
		}

		public async Task<MatchModel> Upsert(MatchModel model)
		{
			if(model.ID == null)
			{
				Match dataToBeAdded = new Match()
				{
					TeamA = model.TeamA,
					Description = model.Description,
					MatchDate = model.MatchDate,
					Sport = model.Sport.HasValue ? model.Sport.Value : 0,
					TeamB = model.TeamB,
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.Now,
				};

				_context.Matches.Add(dataToBeAdded);
				await _context.SaveChangesAsync();

				model.ID = dataToBeAdded.ID;
			}
			else
			{
				Match datum = await _context.Matches.FirstOrDefaultAsync(x => x.ID == model.ID);
				if(datum != null)
				{
					datum.MatchDate = model.MatchDate != null ? model.MatchDate : datum.MatchDate;
					datum.Sport = model.Sport != null ? model.Sport.Value : datum.Sport;
					datum.TeamB = model.TeamB != null ? model.TeamB : datum.TeamB;
					datum.TeamA = model.TeamA != null ? model.TeamA : datum.TeamA;
					datum.Description = model.Description != null ? model.Description : datum.Description;
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
	}
}
