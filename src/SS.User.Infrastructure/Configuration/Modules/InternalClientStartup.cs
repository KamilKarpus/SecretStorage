using Autofac;
using MediatR;
using SS.Infrastructure.ModuleClient;
using SS.Infrastructure.ModuleClient.Shared;
using SS.Users.Application.GetUserInfo;
using SS.Users.Application.GetUserShortInfo;
using SS.Users.Application.ReadModels;

namespace SS.Users.Infrastructure.Configuration.Modules
{
    public static class InternalClientStartup
    {
        public static void Initialize()
        {
            var client = UserCompositionRoot.BeginLifetimeScope().Resolve<IModuleClient>();
            var mediator = UserCompositionRoot.BeginLifetimeScope().Resolve<IMediator>();
            client.AddEndpointDefination<GetUserShortInfobyEmailQuery, UserShortInfo>("internal/users", RequestMethod.GET);
            client.AddEndpointDefination<GetUserInfoQuery, UserInfo>("internal/usersInfo", RequestMethod.GET);
            client.AddEndpointDispatcher("internal/users", mediator);
        }

    }
}
