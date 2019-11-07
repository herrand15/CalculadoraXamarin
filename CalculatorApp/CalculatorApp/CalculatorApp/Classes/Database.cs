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

        public Task<int> insertItemAsync(Operation item)
        {
                return database.InsertAsync(item);
            
        }

        public Task<Operation> getItem(int id)
        {
            return database.Table<Operation>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }


        public Task<List<Operation>> getItems()
        {
            return database.QueryAsync<Operation>("SELECT * FROM [Operation] WHERE [Done] = 0");
        }


        
        public string getCount()
        {
            var task = database.Table<Operation>().CountAsync();
            string result = task.ToString();
            return result; 
        }


    }
}
