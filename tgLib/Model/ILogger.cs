using System;
using System.Text;

namespace TGL.Model
{
    /// <summary>
    /// Severity enumerator.
    /// </summary>
    public enum Severiry { INFO, WARNING, ERROR };
    /// <summary>
    /// Logger interface. It provides a log method for handling log messages.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Handle log message given a severity.
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="msg"></param>
        void log(Severiry severity, string msg);
    }
}
