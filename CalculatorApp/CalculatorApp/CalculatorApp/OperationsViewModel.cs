/*
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using CalculatorApp.Classes;
using Xamarin.Forms;

namespace CalculatorApp
{
    public class ProductsViewModel
    {
        private readonly OperationsInterface _operationsRepository;
        private IEnumerable<Operation> _operations;
        public IEnumerable<Operation> Operations
        {
            get
            {
                return _operations;
            }
            set
            {
                _operations = value;
               
            }
        }
        public double leftOperand { get; set; }
        public string Operator { get; set; }
        public double rightOperand { get; set; }
        public double operationResult { get; set; }


        public ICommand AddCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var newOperation = new Operation
                    {
                        leftOperand = this.leftOperand,
                        rightOperand = this.rightOperand,
                        Operator = this.Operator,
                        operationResult = this.operationResult
                    };
                    await _operationsRepository.addOperation(newOperation);
                });
            }
        }
        public ProductsViewModel(OperationsInterface operationsRepository)
        {
            _operationsRepository = operationsRepository;
        }
        
    }
}
*/