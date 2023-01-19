﻿using Mapster;
using MockBookingSystem.Entities;
using MockBookingSystem.Objects.DestinationOption;
using MockBookingSystem.ServiceLayer.Contracts;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MockBookingSystem.DAL
{
    public class InMemoryDAL : IDAL, IRegistrable
    {
        private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, object>> KeyDictionaryCombinationsDictionary = new();

        public async Task RegisterAsync()
        {
            var assembly = typeof(BaseEntity).Assembly;

            var typesToAdd = assembly.GetTypes().Where(x => typeof(BaseEntity).IsAssignableFrom(x) && x != typeof(BaseEntity));

            foreach (var type in typesToAdd)
            {
                KeyDictionaryCombinationsDictionary.TryAdd(type.FullName, new ConcurrentDictionary<string, object>());
            }
        }

        public T FirstOrDefault<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            KeyDictionaryCombinationsDictionary.TryGetValue(typeof(T).FullName, out var objectsDictionary);

            var value = objectsDictionary.Values.AsQueryable().FirstOrDefault(expression);

            return TypeAdapter.Adapt<T>(value);
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            KeyDictionaryCombinationsDictionary.TryGetValue(typeof(T).FullName, out var objectsDictionary);

            return objectsDictionary.Values.AsQueryable().Select(x => (T)x).Where(expression);
        }

        public IQueryable<T> GetAll<T>() where T : BaseEntity
        {
            KeyDictionaryCombinationsDictionary.TryGetValue(typeof(T).FullName, out var objectsDictionary);

            return objectsDictionary.Values.AsQueryable().Select(x => (T)x);
        }

        public T GetById<T>(string id) where T : BaseEntity
        {
            KeyDictionaryCombinationsDictionary.TryGetValue(typeof(T).FullName, out var objectsDictionary);

            objectsDictionary.TryGetValue(id, out var objectToReturn);

            return TypeAdapter.Adapt<T>(objectToReturn);
        }

        public void Insert<T>(T entity) where T : BaseEntity
        {
            KeyDictionaryCombinationsDictionary.TryGetValue(typeof(T).FullName, out var objectsDictionary);

            objectsDictionary.TryAdd(entity.Id, entity);
        }

        public void Replace<T>(T entity) where T : BaseEntity
        {
            KeyDictionaryCombinationsDictionary.TryGetValue(typeof(T).FullName, out var objectsDictionary);

            objectsDictionary.TryGetValue(entity.Id, out var objectToUpdate);
            objectsDictionary.TryUpdate(entity.Id, entity, objectToUpdate);
        }
        public void Delete<T>(string objectId) where T : BaseEntity
        {
            KeyDictionaryCombinationsDictionary.TryGetValue(typeof(T).FullName, out var objectsDictionary);

            objectsDictionary.TryRemove(objectId, out var objectToDelete);
        }

        public void AddOrUpdate<T>(T entity) where T : BaseEntity
        {
            KeyDictionaryCombinationsDictionary.TryGetValue(typeof(T).FullName, out var objectsDictionary);

            if (objectsDictionary.ContainsKey(entity.Id))
            {
                objectsDictionary.TryGetValue(entity.Id, out var objectToUpdate);
                objectsDictionary.TryUpdate(entity.Id, entity, objectToUpdate);
                return;
            }

            objectsDictionary.TryAdd(entity.Id, entity);
        }
    }
}