using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barclays.ToDo.Data.Contracts;
using Barclays.ToDo.Data.Models;

namespace Barclays.ToDo.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly IList<Item> _items = new List<Item>();
        private int _id;

        public Task<IEnumerable<Item>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Item>>(_items);
        }

        public Task<IEnumerable<Item>> FindAsync(Func<Item, bool> expression)
        {
            return Task.FromResult(_items.Where(expression));
        }

        public Task<Item> GetAsync(int id)
        {
            return Task.FromResult(_items.SingleOrDefault(item => item.Id == id));
        }

        public Task<Item> Create(Item entity)
        {
            entity.Id = _id++;
            _items.Add(entity);
            return Task.FromResult(entity);
        }

        public void Update(Item entity)
        {
            var item = _items.First(record => record.Id == entity.Id);
            var indexOf = _items.IndexOf(item);
            if (indexOf != -1) _items[indexOf] = entity;
        }

        public void Delete(Item entity)
        {
            _items.Remove(entity);
        }
    }
}