using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using log4net;
using log4net.Config;
// ReSharper disable InconsistentNaming
#pragma warning disable 1591
namespace hzerpdemo.hsLog
{
    public sealed class ILogger
    {
        /// <summary>
        /// 记录消息Queue
        /// </summary>
        private readonly ConcurrentQueue<ILogMessage> _que;

        /// <summary>
        /// 信号
        /// </summary>
        private readonly ManualResetEvent _mre;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// 日志
        /// </summary>
        private static readonly ILogger _iLogger = new ILogger();


        private ILogger()
        {
            var configFile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.xml"));
            if (!configFile.Exists)
            {
                throw new Exception("未配置log4net配置文件！");
            }

            // 设置日志配置文件路径
            XmlConfigurator.Configure(configFile);

            _que = new ConcurrentQueue<ILogMessage>();
            _mre = new ManualResetEvent(false);
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);
        }

        /// <summary>
        /// 实现单例
        /// </summary>
        /// <returns></returns>
        public static ILogger Instance()
        {
            return _iLogger;
        }

        /// <summary>
        /// 另一个线程记录日志，只在程序初始化时调用一次
        /// </summary>
        public void Register()
        {
            Thread t = new Thread(new ThreadStart(WriteLog))
            {
                IsBackground = false
            };
            t.Start();
        }

        /// <summary>
        /// 从队列中写日志至磁盘
        /// </summary>
        private void WriteLog()
        {
            while (true)
            {
                // 等待信号通知
                _mre.WaitOne();

                // 判断是否有内容需要如磁盘 从列队中获取内容，并删除列队中的内容
                while (_que.Count > 0 && _que.TryDequeue(out var msg))
                {
                    // 判断日志等级，然后写日志
                    switch (msg.Level)
                    {
                        case ILogLevel.Debug:
                            _log.Debug(msg.Message, msg.Exception);
                            break;
                        case ILogLevel.Info:
                            _log.Info(msg.Message, msg.Exception);
                            break;
                        case ILogLevel.Error:
                            _log.Error(msg.Message, msg.Exception);
                            break;
                        case ILogLevel.Warn:
                            _log.Warn(msg.Message, msg.Exception);
                            break;
                        case ILogLevel.Fatal:
                            _log.Fatal(msg.Message, msg.Exception);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                // 重新设置信号
                _mre.Reset();
                Thread.Sleep(1);
            }
        }


        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message">日志文本</param>
        /// <param name="level">等级</param>
        /// <param name="ex">Exception</param>
        public void EnqueueMessage(string message, ILogLevel level, Exception ex = null)
        {
            if ((level != ILogLevel.Debug || !_log.IsDebugEnabled)
                && (level != ILogLevel.Error || !_log.IsErrorEnabled)
                && (level != ILogLevel.Fatal || !_log.IsFatalEnabled)
                && (level != ILogLevel.Info || !_log.IsInfoEnabled)
                && (level != ILogLevel.Warn || !_log.IsWarnEnabled)) return;
            _que.Enqueue(new ILogMessage
            {
                Message =  message,
                Level = level,
                Exception = ex
            });

            // 通知线程往磁盘中写日志
            _mre.Set();
        }

        public static void Debug(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, ILogLevel.Debug, ex);
        }

        public static void Error(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, ILogLevel.Error, ex);
        }

        public static void Fatal(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, ILogLevel.Fatal, ex);
        }

        public static void Info(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, ILogLevel.Info, ex);
        }

        public static void Warn(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, ILogLevel.Warn, ex);
        }

    }
}