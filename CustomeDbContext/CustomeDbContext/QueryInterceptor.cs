using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Targets;
using NLog.Config;
using System.Data.Common;

namespace CustomeDbContext
{
    public class QueryInterceptor : IDbCommandInterceptor
    {
        private NLog.Logger _logger;

        public QueryInterceptor()
        {
            var config = new LoggingConfiguration();
            var layout = "${level} ${longdate} ${logger} ${exception:format=Method} ${exception:format=Type} ${exception:format=Message} ${message} ${newline}";
            var localLogLevels = new string[] { "Trace" };
            var baseDirectory = @"C:\logs\query"; 

            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            fileTarget.FileName = $"{baseDirectory}\\{DateTime.Now.ToString("yyyy-MM-dd")}.log";
            fileTarget.Layout = layout;
            fileTarget.KeepFileOpen = false;

            var filelogRule = new LoggingRule();
            filelogRule.EnableLoggingForLevel(LogLevel.Trace);
            filelogRule.Targets.Add(fileTarget);
            filelogRule.LoggerNamePattern = "*";

            config.LoggingRules.Add(filelogRule);
            LogManager.Configuration = config;
            _logger = LogManager.GetLogger(typeof(QueryInterceptor).ToString());
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext) =>
          _logger.Info($"IsAsync:{interceptionContext.IsAsync}, Command Text: {command.CommandText}, Command Type: {command.CommandType.ToString()}");

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext) =>
            _logger.Info($"IsAsync:{interceptionContext.IsAsync}, Command Text: {command.CommandText}, Command Type: {command.CommandType.ToString()}");

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext) =>
            _logger.Info($"IsAsync:{interceptionContext.IsAsync}, Command Text: {command.CommandText}, Command Type: {command.CommandType.ToString()}");

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext) =>
            _logger.Info($"IsAsync:{interceptionContext.IsAsync}, Command Text: {command.CommandText}, Command Type: {command.CommandType.ToString()}");

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext) =>
            _logger.Info($"IsAsync:{interceptionContext.IsAsync}, Command Text: {command.CommandText}, Command Type: {command.CommandType.ToString()}");

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext) =>
            _logger.Info($"IsAsync:{interceptionContext.IsAsync}, Command Text: {command.CommandText}, Command Type: {command.CommandType.ToString()}");
    }
}
