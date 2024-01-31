using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressBar.Module.Helpers
{
    public static class ProgressIndicatorHelper
    {
        public static int GetIntegerPercentage(double value, double total)
        {
            int result = 0;
            result = (int)Math.Floor(value / total * 100);
            return result;
        }

        public static int GetCompletionTime(long execTime, double value, double total)
        {
            int result = 0;
            double pendingOperations = total - value;
            double expectedCompletionTimeInMs = pendingOperations * execTime;
            result = (int)Math.Ceiling(expectedCompletionTimeInMs / 1000);
            return result;
        }
    }
}
