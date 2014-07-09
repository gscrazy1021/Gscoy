using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Gscoy.Common
{
    /// <summary>
    /// 日志类
    /// </summary>
    public sealed class LogHelper
    {
        #region Member Variables

        /// <summary>
        /// 用于Trace的组织输出的类别名称
        /// </summary>
        private const string trace_sql = "\r\n***********************TRACE_SQL {0}*****************************\r\nTRACE_SQL";

        /// <summary>
        /// 用于Trace的组织输出的类别名称
        /// </summary>
        private const string trace_exception = "\r\n***********************TRACE_EXCEPTION {0}***********************";

        /// <summary>
        /// 用于Trace的组织输出的类别名称
        /// </summary>
        private const string trace_memo = "\r\n***********************TRACE_MEMO {0}***********************";

        /// <summary>
        /// 当前日志的日期
        /// </summary>
        private static DateTime CurrentLogFileDate = DateTime.Now;

        /// <summary>
        /// 日志对象
        /// </summary>
        private static TextWriterTraceListener twtl;

        /// <summary>
        /// 日志根目录
        /// </summary>
        private static string log_root_directory = AppDomain.CurrentDomain.BaseDirectory + "log";

        /// <summary>
        /// 日志子目录
        /// </summary>
        private static string log_subdir;


        /// <summary>
        /// "      {0} = {1}"
        /// </summary>
        private const string FORMAT_TRACE_PARAM = "      {0} = {1}";

        /// <summary>
        /// 1   仅控制台输出
        /// 2   仅日志输出
        /// 3   控制台+日志输出
        /// </summary>
#if DEBUG
        private static readonly int flag = 3;
#else
	private static readonly int flag = 2;         //可以修改成从配置文件读取
#endif
        #endregion

        #region Constructor

        static LogHelper()
        {
            System.Diagnostics.Trace.AutoFlush = true;

            switch (flag)
            {
                case 1:
                    System.Diagnostics.Trace.Listeners.Add(new ConsoleTraceListener());
                    break;
                case 2:
                    System.Diagnostics.Trace.Listeners.Add(TWTL);
                    break;
                case 3:
                    System.Diagnostics.Trace.Listeners.Add(new ConsoleTraceListener());
                    System.Diagnostics.Trace.Listeners.Add(TWTL);
                    break;
            }
        }

        #endregion

        #region Method

        #region trace

        /// <summary>
        /// 异步错误日志
        /// </summary>
        /// <param name="value"></param>
        public static void Trace(Exception ex)
        {
            new AsyncLogException(BeginTraceError).BeginInvoke(ex, null, null);
        }

        /// <summary>
        /// 异步SQL日志
        /// </summary>
        /// <param name="cmd"></param>
        public static void Trace(SqlCommand cmd)
        {
            new AsyncLogSqlCommand(BeginTraceSqlCommand).BeginInvoke(cmd, null, null);
        }

        /// <summary>
        /// 异步SQL日志
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        public static void Trace(string sql, params SqlParameter[] parameter)
        {
            new AsyncLogSql(BeginTraceSql).BeginInvoke(sql, parameter, null, null);
        }

        /// <summary>
        /// 异步备注日志
        /// </summary>
        /// <param name="value"></param>
        public static void Trace(string memo)
        {
            new AsyncLogMemo(BeginAsyncLogMemo).BeginInvoke(memo, null, null);
        }

        #endregion

        #region delegate

        private delegate void AsyncLogException(Exception ex);
        private delegate void AsyncLogSqlCommand(SqlCommand cmd);
        private delegate void AsyncLogSql(string sql, params SqlParameter[] parameter);
        private delegate void AsyncLogMemo(string memo);

        private static void BeginTraceError(Exception ex)
        {
            if (null != ex)
            {
                //检测日志日期
                StrategyLog();

                //输出日志头
                //System.Diagnostics.Trace.WriteLine(string.Format(trace_exception, DateTime.Now));
                WriteLog(string.Format(trace_exception, DateTime.Now));
                while (null != ex)
                {
                    //System.Diagnostics.Trace.WriteLine(string.Format("{0} {1}\r\n{2}\r\nSource:{3}", DateTime.Now.ToString(), ex.GetType().Name, ex.Message, ex.StackTrace, ex.Source));
                    WriteLog(string.Format("{0} {1}\r\n{2}\r\nSource:{3}", DateTime.Now.ToString(), ex.GetType().Name, ex.Message, ex.StackTrace, ex.Source));
                    ex = ex.InnerException;
                }
            }
        }

        private static void BeginTraceSqlCommand(SqlCommand cmd)
        {
            if (null != cmd)
            {
                SqlParameter[] parameter = new SqlParameter[cmd.Parameters.Count];
                cmd.Parameters.CopyTo(parameter, 0);
                BeginTraceSql(cmd.CommandText, parameter);
            }
        }

        private static void BeginTraceSql(string sql, params SqlParameter[] parameter)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                //检测日志日期
                StrategyLog();

                //System.Diagnostics.Trace.WriteLine(sql, string.Format(trace_sql, DateTime.Now));
                WriteLog(sql, string.Format(trace_sql, DateTime.Now));
                if (parameter != null)
                {
                    foreach (SqlParameter param in parameter)
                    {
                        //System.Diagnostics.Trace.WriteLine(string.Format(FORMAT_TRACE_PARAM, param.ParameterName, param.Value));
                        WriteLog(string.Format(FORMAT_TRACE_PARAM, param.ParameterName, param.Value));
                    }
                }
            }
        }

        private static void BeginAsyncLogMemo(string memo)
        {
            memo = memo.Trim();
            if (!string.IsNullOrEmpty(memo))
            {
                //检测日志日期
                StrategyLog();

                //System.Diagnostics.Trace.WriteLine(string.Format(trace_memo, DateTime.Now));
                //System.Diagnostics.Trace.WriteLine(memo);
                WriteLog(string.Format(trace_memo, DateTime.Now));
                WriteLog(memo);

            }
        }

        #endregion

        #region helper

        /// <summary>
        /// 根据日志策略生成日志
        /// </summary>
        private static void StrategyLog()
        {
            //判断日志日期
            if (DateTime.Compare(DateTime.Now.Date, CurrentLogFileDate.Date) != 0)
            {
                DateTime currentDate = DateTime.Now.Date;

                //生成子目录
                BuiderDir(currentDate);
                //更新当前日志日期
                CurrentLogFileDate = currentDate;

                System.Diagnostics.Trace.Flush();

                //更改输出
                if (twtl != null)
                    System.Diagnostics.Trace.Listeners.Remove(twtl);

                System.Diagnostics.Trace.Listeners.Add(TWTL);
            }
        }

        /// <summary>
        /// 根据年月生成子目录
        /// </summary>
        /// <param name="currentDate"></param>
        private static void BuiderDir(DateTime currentDate)
        {
            int year = currentDate.Year;
            int month = currentDate.Month;
            //年/月
            string subdir = string.Concat(year, '_', month);
            string path = Path.Combine(log_root_directory, subdir);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            log_subdir = subdir;
        }

        #region 写日志
        /// <summary>
        /// 将对象的 System.Object.ToString() 方法的值写入 System.Diagnostics.Trace.Listeners 集合中的跟踪侦听器中。
        /// </summary>
        /// <param name="value">一个 System.Object，其名称被发送到 System.Diagnostics.Trace.Listeners。</param>
        private static void WriteLog(object value)
        {
            if (IsWriteLog)
                System.Diagnostics.Trace.WriteLine(value);
        }

        /// <summary>
        /// 一个 System.Object，其名称被发送到 System.Diagnostics.Trace.Listeners。
        /// </summary>
        /// <param name="message">要写入的消息。</param>
        private static void WriteLog(string message)
        {
            if (IsWriteLog)
                System.Diagnostics.Trace.WriteLine(message);
        }

        /// <summary>
        /// 将类别名称和对象的 System.Object.ToString() 方法值写入 System.Diagnostics.Trace.Listeners集合中的跟踪侦听器。
        /// </summary>
        /// <param name="value">一个 System.Object，其名称被发送到 System.Diagnostics.Trace.Listeners。</param>
        /// <param name="category">用于组织输出的类别名称。</param>
        private static void WriteLog(object value, string category)
        {
            if (IsWriteLog)
                System.Diagnostics.Trace.WriteLine(value, category);
        }

        /// <summary>
        /// 将类别名称和消息写入 System.Diagnostics.Trace.Listeners 集合中的跟踪侦听器。
        /// </summary>
        /// <param name="message"> 要写入的消息。</param>
        /// <param name="category"> 用于组织输出的类别名称。</param>
        private static void WriteLog(string message, string category)
        {
            if (IsWriteLog)
                System.Diagnostics.Trace.WriteLine(message, category);
        }
        #endregion

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// 日志文件路径
        /// </summary>
        /// <returns></returns>
        private static string GetLogFullPath
        {
            get
            {
                return string.Concat(log_root_directory, '\\', string.Concat(log_subdir, @"\log", CurrentLogFileDate.ToString("yyyyMMdd"), ".txt"));
            }
        }

        /// <summary>
        /// 跟踪输出日志文件
        /// </summary>
        private static TextWriterTraceListener TWTL
        {
            get
            {
                if (twtl == null)
                {
                    if (string.IsNullOrEmpty(log_subdir))
                        BuiderDir(DateTime.Now);
                    else
                    {
                        string logPath = GetLogFullPath;
                        if (!Directory.Exists(Path.GetDirectoryName(logPath)))
                            BuiderDir(DateTime.Now);
                    }
                    twtl = new TextWriterTraceListener(GetLogFullPath);
                }
                return twtl;
            }
        }

        /// <summary>
        /// 是否写日志
        /// </summary>
        private static bool IsWriteLog
        {
            get
            {
                return ConfigHelper.GetConfig("IsWriteLog", "1") == "1";
            }
        }

        #endregion

        #region
        /// <summary>
        /// 用于侦听Application的ThreadException事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void AppThreadException(object source, System.Threading.ThreadExceptionEventArgs e)
        {
            Trace(e.Exception);
        }

        /// <summary>
        /// 用于侦听AppDomain的UnhandledException事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public static void AppDomainUnhandledException(object sender, System.UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Trace(e);

        }


        #endregion

    }
}
