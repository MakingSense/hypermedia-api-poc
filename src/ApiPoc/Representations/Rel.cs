using System;

namespace ApiPoc.Representations
{
    [Flags]
    public enum Rel
    {
        None = 0,                               //0000000000000
        Self = 0x1,                             //0000000000001
        Parent = 0x2,                           //0000000000010
        Home = 0x4,                             //0000000000100
        AccountItem = 0x8,                      //0000000001000
        AccountCollection = 0x10,               //0000000010000
        SubscriberItem = 0x20,                  //0000000100000
        SubscriberCollection = 0x40,            //0000001000000
        SubscriberDetailCollection = 0x80,      //0000010000000
        Unsafe = 0x800,                         //0100000000000
        Delete = 0x801,                         //0100000000001
        Suggested = 0x1000                      //1000000000000
    }
}
