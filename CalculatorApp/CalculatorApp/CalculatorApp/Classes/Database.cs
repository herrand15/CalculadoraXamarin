using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorApp.Classes
{
    public class DataBase
    {

        public SQLiteAsyncConnection database;


        public DataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Operation>().Wait();
        }


        // Database operations
        public Task<int> insertItemAsync(Operation item){
            return database.InsertAsync(item);
        }

        public Task<Operation> getItem(int id){
            return database.Table<Operation>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }


        public Task<List<Operation>> getItems(){
            return database.QueryAsync<Operation>("SELECT * FROM [Operation] WHERE [Done] = 0");
        }


        //returns row count
        public int getCount(){
            var allItems = database.Table<Operation>().ToListAsync();
            int count = allItems.Result.Count();
            return count; 
        }


    }
}
