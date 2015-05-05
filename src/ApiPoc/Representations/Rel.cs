using System;

namespace ApiPoc.Representations
{
    [Flags]
    public enum Rel
    {
        None = 0,
        Self = 1,
        Parent = 2,
        Home = 4,
        AccountItem = 8,
        AccountCollection = 16,
        SubscriberItem = 32,
        SubscriberCollection = 64,
        SubscriberDetailCollection = 128,
    }
}
