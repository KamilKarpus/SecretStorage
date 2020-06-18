using SS.Organizations.Application.Commands;
using System.Reflection;

namespace SS.Organizations.Infrastructure.Configuration
{
    internal static class Assemblies
        {
            public static readonly Assembly Application = typeof(ICommand).Assembly;
        }
}
