using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CalculatorApp.Classes
{
    public class Operation
    {
        [PrimaryKey,AutoIncrement]
        public int Id{
            get;
            set;
        }

        public double leftOperand
        {
            get;
            set;
        }

        public string Operator{
            get;
            set;
        }

        public double rightOperand{
            get;
            set;
        }

        public double operationResult{
            get;
            set;
        }

        public string dateOfOperation{
            get;
            set;
        }

        public override string ToString()
        {
            return $"{Id}) {leftOperand} {Operator} {rightOperand}    {dateOfOperation}";
        }


    }
}
