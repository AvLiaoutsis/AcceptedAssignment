using Accepted_Assignment.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accepted_Assignment.Repositories
{
	public interface IBasicRepository<T> where T : class
	{
		public Task<T> Upsert(T model);
		public Task<List<T>> Query(Lookup lookup);
		public Task Delete(int id);
		public Task<T> GetSingle(int id);
	}
}
