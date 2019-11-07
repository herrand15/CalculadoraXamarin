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

        /*
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool createDataBase()
        {
            
            {
                using (var connection = new SQLiteAsyncConnection(System.IO.Path.Combine(folder, "operationsDB.db")))
                {
                    connection.CreateTableAsync<Operation>().Wait(); ;
                    return true;
                }
            }
            catch(SQLiteException ex)
            {   
                return false;
                throw (ex);
            }
        }

       



        public bool insertIntoOperations(Operation oper)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "operationsDB.db")))
                {
                    connection.Insert(oper);
                    return true;
                }
            }
            catch (SQLiteException ex){    
                return false;
                throw (ex);
            }
        }

        public List<Operation> selectTableOperations()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "operationsDB.db")))
                {
                    return connection.Table<Operation>().ToList();

                }
            }
            catch (SQLiteException ex)
            {
                throw (ex);
                return null;
            }
        }

    */



    }
}
