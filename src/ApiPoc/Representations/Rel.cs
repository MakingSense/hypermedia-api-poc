using System;
using System.ComponentModel;

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
        SubscriptorItem = 32,
        SubscriptorCollection = 64,
        SubscriptorDetailCollection = 128,
    }
}