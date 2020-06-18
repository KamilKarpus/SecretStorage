using System;
using System.Diagnostics.CodeAnalysis;

namespace SS.Collections.Domain
{
    public class Status : IEquatable<Status>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Status(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public const int CreatedId = 1;
        public const int ReadedId = 2;
        public const int EditedId = 3;

        public static Status Created
            => new Status(CreatedId, nameof(Created));

        public static Status Readed
            => new Status(ReadedId, nameof(Readed));

        public static Status Edited
            => new Status(EditedId, nameof(Edited));

        public static Status From(int id)
            => id switch
            {
                CreatedId => Created,
                ReadedId => Readed,
                EditedId => Edited,
                         _ => throw new ArgumentException("There is no status with this Id.")
            };
        public bool Equals([AllowNull] Status other)
        => other.Id == Id;
        
    }
}
