/*
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CalculatorApp;
using CalculatorApp.Classes;
using Microsoft.EntityFrameworkCore;

namespace SQLiteApp
{
    public class operationsRepo : OperationsInterface
    {
        private readonly DBcontext _databaseContext;
        public operationsRepo(string dbPath)
        {
            _databaseContext = new DBcontext(dbPath);
        }
        public async Task<IEnumerable<Operation>> getOperationsAsync()
        {
            try
            {
                var products = await _databaseContext.ops.ToListAsync();
                return products;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> addOperation(Operation element)
        {
            try
            {
                var tracking = await _databaseContext.ops.AddAsync(element);
                await _databaseContext.SaveChangesAsync();
                var isAdded = tracking.State == EntityState.Added;
                return isAdded;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
        
*/
     
        
