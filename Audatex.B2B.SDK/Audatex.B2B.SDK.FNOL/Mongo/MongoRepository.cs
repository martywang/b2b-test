using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Audatex.B2B.SDK.FNOL.Mongo
{
	public class MongoRepository<T, TKey> : MongoRepositoryBase<T>, IRepository<T, TKey> where T : IEntity<TKey>
	{
		private readonly string _connectionString;

		public MongoRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public IMongoCollection<T> Collection => MongoCollection;

		public IMongoQueryable<T> AsQueryable()
		{
			return Collection.AsQueryable();
		}

		public virtual Task<T> GetAsync(TKey id)
		{
			return Collection.FindSync(x => x.Id.Equals(id)).FirstOrDefaultAsync();
		}

		public virtual async Task<T> AddAsync(T entity)
		{
			await Collection.InsertOneAsync(entity);
			return entity;
		}

		public virtual Task AddAsync(IEnumerable<T> entities)
		{
			return Collection.InsertManyAsync(entities);
		}

		public virtual async Task<T> UpdateAsync(T entity)
		{
			await Collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity);
			return entity;
		}

		public virtual Task DeleteAsync(FilterDefinition<T> filter)
		{
			return Collection.DeleteManyAsync(filter);
		}

		public virtual Task DeleteAsync(TKey id)
		{
			return Collection.DeleteOneAsync(x => x.Id.Equals(id));
		}

		public virtual Task DeleteAsync(T entity)
		{
			return DeleteAsync(entity.Id);
		}

		public virtual Task DeleteAsync(Expression<Func<T, bool>> predicate)
		{
			return Collection.DeleteManyAsync(predicate);
		}

		public virtual Task<long> CountAsync()
		{
			return CountAsync(x => true);
		}

		public virtual Task<long> CountAsync(Expression<Func<T, bool>> predicate)
		{
			return Collection.CountAsync(predicate);
		}

		public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
		{
			var cursor = await Collection.FindAsync(predicate);
			return await cursor.AnyAsync();
		}

		
		protected override string ConnectionString => _connectionString;
		protected override string CollectionName => $"{typeof(T).Name}";
	}

	public class MongoRepository<T> : MongoRepository<T, ObjectId>, IRepository<T> where T : IEntity<ObjectId>
	{
		public MongoRepository(string connectionString) : base(connectionString)
		{
		}
	}
}
