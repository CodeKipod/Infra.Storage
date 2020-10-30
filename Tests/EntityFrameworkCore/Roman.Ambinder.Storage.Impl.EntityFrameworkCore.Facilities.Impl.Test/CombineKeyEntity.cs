using System;

namespace Roman.Ambinder.Storage.Impl.EntityFrameworkCore.Facilities.Impl.Test
{
    public class SameValueTypeComposedKeysEntity
    {
        public int Key1 { get; set; }

        public int Key2 { get; set; }

        public int Key3 { get; set; }

        public string Name { get; set; }
    }

    public class DifferentValueTypesComposedKeysEntity
    {
        public int Key1 { get; set; }

        public DateTime Key2 { get; set; }

        public byte Key3 { get; set; }

        public string Name { get; set; }
    }

    public class SameRefTypeKeysComposedEntity
    {
        public string Key1 { get; set; }

        public string Key2 { get; set; }

        public string Key3 { get; set; }

        public string Name { get; set; }
    }

    public class SingleValueTypeKeyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SingleRefTypeKeyEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
