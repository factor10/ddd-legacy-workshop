using System;
using System.Collections.Generic;
using LiteDB;
using factor10.TimeRegistration.DomainModel;

namespace factor10.TimeRegistration.Repositories
{
    public abstract class BaseRepository<T> where T : IEntity
    {
        private readonly LiteDatabase _db;
        private readonly string _collectionName;
        protected LiteCollection<T> Collection;

        public BaseRepository(LiteDatabase db)
        {
            _collectionName = typeof(T).Name;
            _db = db;
            Collection = _db.GetCollection<T>(_collectionName);
        }

        protected void BaseSave(T entity)
        {
            Collection.Upsert(entity);
        }

        protected T BaseGetById(Guid id)
        {
            var e = Collection.FindById(id);
            if (e == null) throw new Exception("Entity not found");
            return e;
        }

        protected LiteCollection<T> BaseAll()
        {
            //return Collection.FindAll();
            return Collection.IncludeAll();
        }
    }
}
