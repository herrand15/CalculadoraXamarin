using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CalculatorApp.Classes;

namespace CalculatorApp
{
    public interface OperationsInterface
    {
        Task<IEnumerable<Operation>> getOperationsAsync();
        Task<bool> addOperation(Operation newOp);

    }
}
