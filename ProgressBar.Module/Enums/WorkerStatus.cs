using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ProgressBar.Module.Enums
{
    /// <summary>
    /// Execution status of a <see cref="BackgroundWorker"/>
    /// </summary>
    public enum WorkerStatus
    {
        Stopped,
        Running,
        Cancelled,
    }
}
