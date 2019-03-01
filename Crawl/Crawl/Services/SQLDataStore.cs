using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.Services
{
    public sealed class SQLDataStore : IDataStore
    {

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static SQLDataStore _instance;

        public static SQLDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SQLDataStore();
                }
                return _instance;
            }
        }

        private SQLDataStore()
        {
            CreateTables();
        }

        public void InitializeDatabaseNewTables()
        {
            DeleteTables();
            CreateTables();

            // Populate them
            InitilizeSeedData();

            // Tell View Models they need to refresh
            NotifyViewModelsOfDataChange();
        }

        // Create the Database Tables
        private void CreateTables()
        {
            App.Database.CreateTableAsync<Item>().Wait();
            App.Database.CreateTableAsync<BaseCharacter>().Wait();
            App.Database.CreateTableAsync<BaseMonster>().Wait();
            App.Database.CreateTableAsync<Score>().Wait();
        }

        // Delete the Datbase Tables by dropping them
        private void DeleteTables()
        {
            App.Database.DropTableAsync<Item>().Wait();
            App.Database.DropTableAsync<BaseCharacter>().Wait();
            App.Database.DropTableAsync<BaseMonster>().Wait();
            App.Database.DropTableAsync<Score>().Wait();
        }

        // Tells the View Models to update themselves.
        private void NotifyViewModelsOfDataChange()
        {
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            MonstersViewModel.Instance.SetNeedsRefresh(true);
            CharactersViewModel.Instance.SetNeedsRefresh(true);
            //ScoresViewModel.Instance.SetNeedsRefresh(true);
        }

        private async void InitilizeSeedData()
        {
            // Load Items.
            await AddAsync_Item(new Item("SQL Gold Sword", "Sword made of Gold, really expensive looking",
                "http://www.clker.com/cliparts/e/L/A/m/I/c/sword-md.png", 0, 10, 10, ItemLocationEnum.PrimaryHand, AttributeEnum.Defense));

            await AddAsync_Item(new Item("SQL Strong Shield", "Enough to hide behind",
                "http://www.clipartbest.com/cliparts/4T9/LaR/4T9LaReTE.png", 0, 10, 0, ItemLocationEnum.OffHand, AttributeEnum.Attack));

            await AddAsync_Item(new Item("SQL Bunny Hat", "Pink hat with fluffy ears",
                "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png", 0, 10, -1, ItemLocationEnum.Head, AttributeEnum.Speed));

            // Implement Characters
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL First Character", Description = "This is an Character description.", Level = 1 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL Second Character", Description = "This is an Character description.", Level = 1 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL Third Character", Description = "This is an Character description.", Level = 2 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL Fourth Character", Description = "This is an Character description.", Level = 2 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL Fifth Character", Description = "This is an Character description.", Level = 3 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL Sixth Character", Description = "This is an Character description.", Level = 3 });

            // Implement Monsters
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQL First Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQL Second Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQL Third Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQL Fourth Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQL Fifth Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQL Sixth Monster", Description = "This is an Monster description." });

            //await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), NameBogus = "First Score", ScoreTotal = 111 });
            //await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), NameBogus = "Second Score", ScoreTotal = 222 });
            //await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), NameBogus = "Third Score", ScoreTotal = 333 });
            //await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), NameBogus = "Fourth Score", ScoreTotal = 444 });
            //await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), NameBogus = "Fifth Score", ScoreTotal = 555 });
            //await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), NameBogus = "Sixth Score", ScoreTotal = 666 });
        }

        #region Item
        // Item

        // Add InsertUpdateAsync_Item Method

        // Check to see if the item exists
        // Add your code here.

        // If it does not exist, then Insert it into the DB
        // Add your code here.
        // return true;

        // If it does exist, Update it into the DB
        // Add your code here
        // return true;

        // If you got to here then return false;

        //insert item if not in db, otherwise update
        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {
            // Implement
            var check = await GetAsync_Item(data.Id);
            //if item does not exist
            if(check == null)
            {
                //add item
                await AddAsync_Item(data);
                return true;
            }

            //update item if already in db
            var update = await UpdateAsync_Item(data);

            //if update successful
            if(update)
            {
                return true;
            }
            //if update failed
            return false;

        }

        //add it to db
        public async Task<bool> AddAsync_Item(Item data)
        {
            // Implement
            //call insertasync
            var result = await App.Database.InsertAsync(data);
            //if successful insert
            if (result == 1)
            {
                return true;
            }
            //if insert failed
            return false;
        }

        //update item
        public async Task<bool> UpdateAsync_Item(Item data)
        {
            // Implement
            //call updateasync
            var result = await App.Database.UpdateAsync(data);
            //if update successful
            if (result == 1)
            {
                return true;
            }
            //if update failed
            return false;
        }

        //delete item
        public async Task<bool> DeleteAsync_Item(Item data)
        {
            // Implement
            //call deleteasync
            var result = await App.Database.DeleteAsync(data);
            //if delete successful
            if (result == 1)
            {
                return true;
            }
            //if delete failed
            return false;
        }

        //get one item from db
        public async Task<Item> GetAsync_Item(string id)
        {
            // Implement
            try
            {
                //call getasync
                var result = await App.Database.GetAsync<Item>(id);
                //return item
                return result;
            }
            catch
            {
                return null;
            }
        }

        //get item list
        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            // Implement
            //get items & put into list
            var tempResult = await App.Database.Table<Item>().ToListAsync();
            //list to return
            var result = new List<Item>();
            //put items in return list
            foreach (var item in tempResult)
            {
                result.Add(item);
            }
            //return list of items
            return result;
        }
        #endregion Item

        #region Character
        // Character
        // Conver to BaseCharacter and then add it
        public async Task<bool> AddAsync_Character(Character data)
        {
            // Convert Character to CharacterBase before saving to Database
            var dataBase = new BaseCharacter(data);

            var result = await App.Database.InsertAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> InsertUpdateAsync_Character(Character data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Character(data.Id);
            if (oldData == null)
            {
                await AddAsync_Character(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Character(data);
            if (UpdateResult)
            {
                await AddAsync_Character(data);
                return true;
            }

            return false;
        }

        // Convert to BaseCharacter and then update it
        public async Task<bool> UpdateAsync_Character(Character data)
        {
            // Convert Character to CharacterBase before saving to Database
            var dataBase = new BaseCharacter(data);

            var result = await App.Database.UpdateAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        // Pass in the character and convert to Character to then delete it
        public async Task<bool> DeleteAsync_Character(Character data)
        {
            // Convert Character to CharacterBase before saving to Database
            var dataBase = new BaseCharacter(data);

            var result = await App.Database.DeleteAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        // Get the Character Base, and Load it back as Character
        public async Task<Character> GetAsync_Character(string id)
        {
            var tempResult = await App.Database.GetAsync<BaseCharacter>(id);

            var result = new Character(tempResult);

            return result;
        }

        // Load each character as the base character, 
        // Then then convert the list to characters to push up to the view model
        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            var tempResult = await App.Database.Table<BaseCharacter>().ToListAsync();

            var result = new List<Character>();
            foreach (var item in tempResult)
            {
                result.Add(new Character(item));
            }

            return result;
        }

        #endregion Character

        #region Monster
        //Monster
        // Conver to BaseMonster and then add it
        public async Task<bool> AddAsync_Monster(Monster data)
        {
            // Convert Monster to MonsterBase before saving to Database
            var dataBase = new BaseMonster(data);

            var result = await App.Database.InsertAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> InsertUpdateAsync_Monster(Monster data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Monster(data.Id);
            if (oldData == null)
            {
                await AddAsync_Monster(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Monster(data);
            if (UpdateResult)
            {
                await AddAsync_Monster(data);
                return true;
            }

            return false;
        }

        // Convert to BaseMonster and then update it
        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            // Convert Monster to MonsterBase before saving to Database
            var dataBase = new BaseMonster(data);

            var result = await App.Database.UpdateAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        // Pass in the Monster and convert to Monster to then delete it
        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            // Convert Monster to MonsterBase before saving to Database
            var dataBase = new BaseMonster(data);

            var result = await App.Database.DeleteAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        // Get the Monster Base, and Load it back as Monster
        public async Task<Monster> GetAsync_Monster(string id)
        {
            var tempResult = await App.Database.GetAsync<BaseMonster>(id);

            var result = new Monster(tempResult);

            return result;
        }

        // Load each Monster as the base Monster, 
        // Then then convert the list to Monsters to push up to the view model
        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            var tempResult = await App.Database.Table<BaseMonster>().ToListAsync();

            var result = new List<Monster>();
            foreach (var item in tempResult)
            {
                result.Add(new Monster(item));
            }

            return result;
        }

        #endregion Monster

        #region Score
        // Score
        public async Task<bool> AddAsync_Score(Score data)
        {
            // Implement
            return false;
        }

        public async Task<bool> UpdateAsync_Score(Score data)
        {
            // Implement
            return false;
        }

        public async Task<bool> DeleteAsync_Score(Score data)
        {
            // Implement
            return false;
        }

        public async Task<Score> GetAsync_Score(string id)
        {
            // Implement
            return null;
        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            // Implement
            return null ;

        }

#endregion Score
    }
}