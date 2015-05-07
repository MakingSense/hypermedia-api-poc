using System;

namespace ApiPoc.Representations
{
    [Flags]
    public enum Rel
    {
        _None = 0,                                  //0000 0000 0000 0000 0000 0000
        Self = 0x1,                                 //0000 0000 0000 0000 0000 0001
        Parent = 0x2,                               //0000 0000 0000 0000 0000 0010
        Home = 0x4,                                 //0000 0000 0000 0000 0000 0100
        Alternate = 0x8,                            //0000 0000 0000 0000 0000 1000
        _Collection = 0x10,                         //0000 0000 0000 0000 0001 0000
        _Detail = 0x20,                             //0000 0000 0000 0000 0010 0000
        //Reserved                                  //0000 0000 0000 0000 xx00 0000
        Suggested = 0x100,                          //0000 0000 0000 0001 0000 0000
        _Unsafe = 0x200,                            //0000 0000 0000 0010 0000 0000
        //Reserved                                  //0000 0000 0000 xx00 0000 0000
        _Post = 0x1200,                             //0000 0000 0001 0010 0000 0000
        _Put = 0x2200,                              //0000 0000 0010 0010 0000 0000
        _Delete = 0x4200,                           //0000 0000 0100 0010 0000 0000
        //Domain                                    
        _Account = 0x100000,                        //0001 0000 0000 0000 0000 0000
        _Subscriber = 0x200000,                     //0010 0000 0000 0000 0000 0000
        //Reserved                                  //xx00 0000 0000 0000 0000 0000
        //Representations                                 
        AccountCollection = 0x100010,               //0001 0000 0000 0000 0001 0000
        AccountDetail = 0x100020,                   //0001 0000 0000 0000 0010 0000
        SubscriberCollection = 0x200010,            //0010 0000 0000 0000 0001 0000
        SubscriberDetailedCollection = 0x200030,    //0010 0000 0000 0000 0011 0000
        SubscriberDetail = 0x200020,                //0010 0000 0000 0000 0010 0000
        //Operations                                      
        Unsubscribe = 0x204200,                     //0010 0000 0100 0010 0000 0000
        EditSubscriber = 0x202200,                  //0010 0000 0010 0010 0000 0000
    }
}
