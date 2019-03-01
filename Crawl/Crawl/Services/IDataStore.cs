using System.Collections.Generic;
using System.Threading.Tasks;
using Crawl.Models;

namespace Crawl.Services
{
    //public interface IDataStore<T> where T : class
    //{
    //    Task<bool> AddAsync(T item);
    //    Task<bool> UpdateAsync(T item);
    //    Task<bool> DeleteAsync(T item);
    //    Task<T> GetAsync(string id);
    //    Task<IEnumerable<T>> GetAllAsync(bool forceRefresh = false);

    //}

    public interface IDataStore
    {
        Task<bool> InsertUpdateAsync_Item(Item data); 
        Task<bool> AddAsync_Item(Item data);
        Task<bool> UpdateAsync_Item(Item data);
        Task<bool> DeleteAsync_Item(Item data);
        Task<Item> GetAsync_Item(string id);
        Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false);

        //Character
        Task<bool> AddAsync_Character(Character character);
        Task<bool> InsertUpdateAsync_Character(Character item);
        Task<bool> UpdateAsync_Character(Character character);
        Task<bool> DeleteAsync_Character(Character id);
        Task<Character> GetAsync_Character(string id);
        Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false);

        //Monster
        Task<bool> AddAsync_Monster(Monster monster);
        Task<bool> InsertUpdateAsync_Monster(Monster item);
        Task<bool> UpdateAsync_Monster(Monster monster);
        Task<bool> DeleteAsync_Monster(Monster id);
        Task<Monster> GetAsync_Monster(string id);
        Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false);

        // Implement Score

    }
}
