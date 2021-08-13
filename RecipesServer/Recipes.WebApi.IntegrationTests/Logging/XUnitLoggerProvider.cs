using Microsoft.Extensions.Logging;

namespace Recipes.WebApi.IntegrationTests.Logging
{
    internal sealed class XUnitLoggerProvider : ILoggerProvider
    {
        private readonly LoggerExternalScopeProvider _scopeProvider = new();
        
        public ILogger CreateLogger(string categoryName)
        {
            return new XUnitLogger(_scopeProvider, categoryName);
        }

        public void Dispose()
        {
        }
    }
}