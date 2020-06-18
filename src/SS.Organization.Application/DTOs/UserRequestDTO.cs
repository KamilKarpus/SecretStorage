using System;

namespace SS.Organizations.Application.DTOs
{
    public class UserRequestDTO
    {
        public Guid Id { get; set; }
        public UserRequestDTO(Guid id)
        {
            Id = id;
        }
    }
}
