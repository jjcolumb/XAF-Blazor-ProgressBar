using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafCustomComponents
{
    public class ProgressIndicatorState
    {
        public double OperationNum { get; set; }
        public double TotalOperations { get; set; }
        public long ExpectedCompletionTime { get; set; }
    }
}
