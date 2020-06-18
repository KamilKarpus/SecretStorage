using SS.Users.Application.Configuration.Commands;
using System.Reflection;

namespace SS.Users.Infrastructure.Configuration
{
        internal static class Assemblies
        {
            public static readonly Assembly Application = typeof(ICommand).Assembly;
        }
}
