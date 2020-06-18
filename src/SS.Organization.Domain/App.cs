using System;

namespace SS.Organizations.Domain
{
    public class App
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Key { get; private set; }

        public App(Guid id, string name, string key)
        {
            Id = id;
            Name = name;
            Key = key;
        }
    }
}
