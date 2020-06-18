using SS.Collections.Application.Configuration.Commands;
using System.Reflection;

namespace SS.Collections.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(ICommand).Assembly;
    }
}
