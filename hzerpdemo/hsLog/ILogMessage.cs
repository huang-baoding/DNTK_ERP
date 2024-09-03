using System;
// ReSharper disable InconsistentNaming
#pragma warning disable 1591
namespace hzerpdemo.hsLog
{
    public class ILogMessage
    {

        public string Message { get; set; }
        public ILogLevel Level { get; set; }
        public Exception Exception { get; set; }
    }
}