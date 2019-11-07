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
            string a = Database.getItem(1).Result.ToString();

            Label r = new Label();
            r.Text = a;
            Content = new StackLayout
            {
                Children = {
                    r,


                }
            };
        }
    }
}