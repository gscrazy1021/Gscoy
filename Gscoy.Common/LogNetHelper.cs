using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Common
{
    /// <summary>
    /// 日志处理
    /// </summary>
    public class LogNetHelper
    {
        /// <summary>
        /// 静态类
        /// </summary>
        private LogNetHelper() { }
        private const string LOG_REPOSITORY = "Default"; // this should likely be set in the web config.
        private static ILog m_log;
        private static bool Enable = false;
        /// <summary>
        /// 初始化日志系统
        /// 在系统运行开始初始化
        /// Global.asax Application_Start内
        /// </summary>
        public static void Init()
        {
            Enable = ConfigHelper.GetConfig("EnableLog", "1") == "1";
            if (Enable)
            {
                //设置读取配置文件的路径
                log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            }
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="messageType">日志类型</param>
        public static void Write(string message, LogMessageType messageType)
        {
            DoLog(message, messageType, null, Type.GetType("System.Object"));
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="messageType">日志类型</param>
        /// <param name="type"></param>
        public static void Write(string message, LogMessageType messageType, Type type)
        {
            DoLog(message, messageType, null, type);
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="messageType">日志类型</param>
        /// <param name="ex">异常</param>
        public static void Write(string message, LogMessageType messageType, Exception ex)
        {
            DoLog(message, messageType, ex, Type.GetType("System.Object"));
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="messageType">日志类型</param>
        /// <param name="ex">异常</param>
        /// <param name="type"></param>
        public static void Write(string message, LogMessageType messageType, Exception ex,
        Type type)
        {
            DoLog(message, messageType, ex, type);
        }
        /// <summary>
        /// 断言
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="message">日志信息</param>
        public static void Assert(bool condition, string message)
        {
            Assert(condition, message, Type.GetType("System.Object"));
        }
        /// <summary>
        /// 断言
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="message">日志信息</param>
        /// <param name="type">日志类型</param>
        public static void Assert(bool condition, string message, Type type)
        {
            if (condition == false)
                Write(message, LogMessageType.Info);
        }
        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="messageType">日志类型</param>
        /// <param name="ex">异常</param>
        /// <param name="type">日志类型</param>
        private static void DoLog(string message, LogMessageType messageType, Exception ex, Type type)
        {
            m_log = LogManager.GetLogger(type);
            switch (messageType)
            {
                case LogMessageType.Debug:
                    LogNetHelper.m_log.Debug(message, ex);
                    break;
                case LogMessageType.Info:
                    LogNetHelper.m_log.Info(message, ex);
                    break;
                case LogMessageType.Warn:
                    LogNetHelper.m_log.Warn(message, ex);
                    break;
                case LogMessageType.Error:
                    LogNetHelper.m_log.Error(message, ex);
                    break;
                case LogMessageType.Fatal:
                    LogNetHelper.m_log.Fatal(message, ex);
                    break;
            }
        }
        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogMessageType
        {
            /// <summary>
            /// 调试
            /// </summary>
            Debug,
            /// <summary>
            /// 信息
            /// </summary>
            Info,
            /// <summary>
            /// 警告
            /// </summary>
            Warn,
            /// <summary>
            /// 错误
            /// </summary>
            Error,
            /// <summary>
            /// 致命错误
            /// </summary>
            Fatal
        }
    }
}
