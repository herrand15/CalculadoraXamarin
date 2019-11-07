using CalculatorApp.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CalculatorApp.Views
{
    public class OperationsView : ContentPage
    {

        const int MAXROWS = 50;
        
        //Getting DB path
        static DataBase database;
        
        public static DataBase Database{
            get{
                if (database == null){
                    database = new DataBase(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "operationsDB.db"));
                }
                return database;
            }
        }

        

        private ListView recordsView;

        public OperationsView()
        {
            recordsView = new ListView();
            this.Title = "Historial";
            List<string> history = new List<string>();



            string newEntry;
            
            for(int i = 1; i < MAXROWS; i++){
                try{
                    newEntry = Database.getItem(i).Result.ToString();
                    history.Add(newEntry);
                }
                catch(NullReferenceException ex){
                    return;
                }
            }
            
            history.Add(Database.getItem(3).Result.ToString());
            recordsView.ItemsSource = history;
            

           
            
            Content = new StackLayout
            {
                Children = {
                    recordsView,


                }
            };
        }
    }
}