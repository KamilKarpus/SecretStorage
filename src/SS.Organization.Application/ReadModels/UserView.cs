using System;

namespace SS.Organizations.Application.ReadModels.Organizations
{
    public class UserView
    {
       public Guid Id { get; set; }
       public string DisplayName { get; set; }
       public string Role { get; set; }
    }
}
