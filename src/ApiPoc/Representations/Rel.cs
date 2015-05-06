using System;

namespace ApiPoc.Representations
{
    [Flags]
    public enum Rel
    {
        None = 0,                               //000000000000
        Self = 0x1,                             //000000000001
        Parent = 0x2,                           //000000000010
        Home = 0x4,                             //000000000100
        AccountItem = 0x8,                      //000000001000
        AccountCollection = 0x10,               //000000010000
        SubscriberItem = 0x20,                  //000000100000
        SubscriberCollection = 0x40,            //000001000000
        SubscriberDetailCollection = 0x80,      //000010000000
        Unsafe = 0x800,                         //100000000000
        Delete = 0x801,                         //100000000001
    }
}
