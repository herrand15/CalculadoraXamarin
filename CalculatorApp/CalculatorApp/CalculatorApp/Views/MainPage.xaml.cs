using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CalculatorApp.Classes;
using SQLite;
using System.IO;
using CalculatorApp.Views;

namespace CalculatorApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        //main buttons
        Button plus, minus, multiply, divide, calculate, history,clear;
        Button[] numbers = new Button[10];

        Label leftOperand = new Label();
        Label Operator = new Label();
        Label resultLabel = new Label(); 
        
        
        // current operation values
        private double stored = 0.0;
        private double rightSide = 0.0;
        private double result = 0.0;
        private string currentOperator = "";

        //database utilities
        Operation record;

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

        // stored value(left side), right side value and result value properties
        public double StoredValue{

            get{
                return stored;
            }
            set{
                stored = value;
                Change_Stored(stored.ToString());
            }
        }

        public double RightSideValue{

            get{
                return rightSide;
            }
            set{
                rightSide = value;
                Change_Result(rightSide.ToString());
            }

        }

        public double Result{

            get{
                return result;
            }
            set{
                result = value;
            }

        }

        public MainPage()
        {
            InitializeComponent();
            
            // initializing buttons
            plus = new Button() { Text = "+" };
            minus = new Button() { Text = "-" };
            multiply = new Button() { Text = "*" };
            divide = new Button() { Text = "/" };
            calculate = new Button() { Text = "=" };
            history = new Button() { Text = "Historial" };
            clear = new Button() { Text = "Clear" };


            plus.Pressed += Plus_Pressed;
            minus.Pressed += Minus_Pressed;
            multiply.Pressed += Multiply_Pressed;
            divide.Pressed += Divide_Pressed;
            calculate.Pressed += Calculate_Pressed;
            history.Pressed += History_Pressed;
            clear.Pressed += Clear_Pressed;

            //setting the text to each Keyboeard button (0,1,2...)
            for (int i = 0; i < numbers.Count(); i++){
                ref var currentButton = ref numbers[i];
                currentButton = new Button() { Text = i.ToString() };
                int index = i;
                currentButton.Pressed += (o, e) => { Number_Pressed(index); };
            }

            #region Style
            for(int i = 0; i <10; i++)
            {
                numbers[i].BackgroundColor = Xamarin.Forms.Color.DarkBlue;
                numbers[i].FontFamily = "Arial";
                numbers[i].FontSize = 20;
                numbers[i].TextColor = Xamarin.Forms.Color.White;
            }
            minus.BackgroundColor = Xamarin.Forms.Color.DarkBlue;
            minus.FontFamily = "Arial";
            minus.FontSize = 20;
            minus.TextColor = Xamarin.Forms.Color.White;

            plus.BackgroundColor = Xamarin.Forms.Color.DarkBlue;
            plus.FontFamily = "Arial";
            plus.FontSize = 20;
            plus.TextColor = Xamarin.Forms.Color.White;

            divide.BackgroundColor = Xamarin.Forms.Color.DarkBlue;
            divide.FontFamily = "Arial";
            divide.FontSize = 20;
            divide.TextColor = Xamarin.Forms.Color.White;

            multiply.BackgroundColor = Xamarin.Forms.Color.DarkBlue;
            multiply.FontFamily = "Arial";
            multiply.FontSize = 20;
            multiply.TextColor = Xamarin.Forms.Color.White;


            resultLabel.FontSize = 24;
            resultLabel.FontFamily = "Arial";
            resultLabel.HorizontalTextAlignment = TextAlignment.Center;
            resultLabel.VerticalTextAlignment = TextAlignment.Center;

            leftOperand.FontSize = 24;
            leftOperand.FontFamily = "Arial";
            leftOperand.HorizontalTextAlignment = TextAlignment.Center;
            leftOperand.VerticalTextAlignment = TextAlignment.Center;


            Operator.FontSize = 24;
            Operator.FontFamily = "Arial";
            Operator.HorizontalTextAlignment = TextAlignment.Center;
            Operator.VerticalTextAlignment = TextAlignment.Center;


            #endregion


            #region Setting Layout (using template)
            //we use a template (Grid template/AutoGrid.cs) to make the grid easier
            var layout = new rMultiplatform.AutoGrid();
            layout.DefineGrid(4, 6);

            layout.AutoAdd(leftOperand);
            layout.AutoAdd(Operator);
            layout.AutoAdd(resultLabel,2);
            layout.AutoAdd(numbers[7]);
            layout.AutoAdd(numbers[8]);
            layout.AutoAdd(numbers[9]);
            layout.AutoAdd(multiply);


            layout.AutoAdd(numbers[4]);
            layout.AutoAdd(numbers[5]);
            layout.AutoAdd(numbers[6]);
            layout.AutoAdd(divide);

            layout.AutoAdd(numbers[1]);
            layout.AutoAdd(numbers[2]);
            layout.AutoAdd(numbers[3]);
            layout.AutoAdd(plus);

            layout.AutoAdd(clear);
            layout.AutoAdd(numbers[0]);
            layout.AutoAdd(calculate);
            layout.AutoAdd(minus);
            layout.AutoAdd(history,4);

            Content = layout;

            #endregion
        }

        //handlears for button pressed actions
        private void Clear_Pressed(object sender, EventArgs e)
        {
            StoredValue = 0.0;
            Result = 0.0;
            RightSideValue = 0.0;
            currentOperator = "";
            Change_Result("");
            Change_Stored("");
            Set_Operator("");

        }

        // button actions
        private void History_Pressed(object sender, EventArgs e){
            //navigates to operations history
            Navigation.PushAsync(new OperationsView());
        }

        private void Calculate_Pressed(object sender, EventArgs e){

            switch (currentOperator)
            {
                case ("+"):
                    Result = StoredValue + rightSide;
                    break;
                case ("-"):
                    Result = StoredValue - rightSide;
                    break;
                case ("/"):
                    Result = StoredValue / rightSide;
                    break;
                case ("X"):
                    Result = StoredValue * rightSide;
                    break;
            }

            //creating and inserting the new record
            record = new Operation();
            record.Id = 1;
            record.leftOperand = StoredValue;
            record.rightOperand = rightSide;
            record.Operator = currentOperator;
            record.operationResult = Result;
            record.dateOfOperation = DateTime.Now.ToString();
            Database.insertItemAsync(record);
            
            StoredValue = Result;
            RightSideValue = 0.0;
            Change_Result("");
            Change_Stored(Result.ToString());
            Set_Operator("");
            

        }

        private void Divide_Pressed(object sender, EventArgs e){
            currentOperator = "/";
            Set_Operator(currentOperator);
            
        }

        private void Multiply_Pressed(object sender, EventArgs e){
            currentOperator = "X";
            Set_Operator(currentOperator);
            
        }

        private void Minus_Pressed(object sender, EventArgs e)
        {
            currentOperator = "-";
            Set_Operator(currentOperator);
            
            
        }

        private void Plus_Pressed(object sender, EventArgs e){
            currentOperator = "+";
            Set_Operator(currentOperator);
            
            
        }


        private void Number_Pressed(int index){
            if (currentOperator.Equals("")){
                StoredValue *= 10.0;
                StoredValue += (double)index;
            }
            else{
                RightSideValue *= 10.0;
                RightSideValue += (double)index;
            }

        }

        private void Set_Operator(string op)
        {
            Operator.Text = op;
        }

        private void Change_Result(string op)
        {
            resultLabel.Text = op;
        }

        private void Change_Stored(string op)
        {
            leftOperand.Text = op;
        }


    }
}
