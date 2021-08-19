﻿using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Recipes.WebApi.IntegrationTests.Logging
{
    internal sealed class XUnitLogger<T> : XUnitLogger, ILogger<T>
    {
        public XUnitLogger(LoggerExternalScopeProvider scopeProvider)
            : base(scopeProvider, typeof(T).FullName)
        {
        }
    }

    internal class XUnitLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly LoggerExternalScopeProvider _scopeProvider;

        public XUnitLogger(LoggerExternalScopeProvider scopeProvider, string categoryName)
        {
            _scopeProvider = scopeProvider;
            _categoryName = categoryName;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return _scopeProvider.Push(state);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (logLevel < LogLevel.Error)
                return;
            var sb = new StringBuilder();
            sb.Append(GetLogLevelString(logLevel))
                .Append(" [").Append(_categoryName).Append("] ")
                .Append(formatter(state, exception));

            if (exception != null) sb.Append('\n').Append(exception);

            // Append scopes
            _scopeProvider.ForEachScope((scope, state) =>
            {
                state.Append("\n => ");
                state.Append(scope);
            }, sb);

            File.AppendAllText("logs.txt", sb + "\r\n\r\n\r\n\r\n");
        }

        private static string GetLogLevelString(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace => "trce",
                LogLevel.Debug => "dbug",
                LogLevel.Information => "info",
                LogLevel.Warning => "warn",
                LogLevel.Error => "fail",
                LogLevel.Critical => "crit",
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel))
            };
        }
    }
}