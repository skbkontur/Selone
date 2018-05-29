using System;

namespace Kontur.Selone.Properties
{
    public class Props<T1>
    {
        public Props(IProp<T1> prop1)
        {
            Prop1 = prop1;
        }

        public IProp<T1> Prop1 { get; }

        public ValueTuple<T1> Get()
        {
            return ValueTuple.Create(Prop1.Get());
        }
    }

    public class Props<T1, T2>
    {
        public Props(IProp<T1> prop1, IProp<T2> prop2)
        {
            Prop1 = prop1;
            Prop2 = prop2;
        }

        public IProp<T1> Prop1 { get; }
        public IProp<T2> Prop2 { get; }

        public (T1, T2) Get()
        {
            return (Prop1.Get(), Prop2.Get());
        }
    }
    

    public class Props<T1, T2, T3>
    {
        public Props(IProp<T1> prop1, IProp<T2> prop2, IProp<T3> prop3)
        {
            Prop1 = prop1;
            Prop2 = prop2;
            Prop3 = prop3;
        }

        public IProp<T1> Prop1 { get; }
        public IProp<T2> Prop2 { get; }
        public IProp<T3> Prop3 { get; }

        public (T1, T2, T3) Get()
        {
            return (Prop1.Get(), Prop2.Get(), Prop3.Get());
        }
    }

    public class Props<T1, T2, T3, T4>
    {
        public Props(IProp<T1> prop1, IProp<T2> prop2, IProp<T3> prop3, IProp<T4> prop4)
        {
            Prop1 = prop1;
            Prop2 = prop2;
            Prop3 = prop3;
            Prop4 = prop4;
        }

        public IProp<T1> Prop1 { get; }
        public IProp<T2> Prop2 { get; }
        public IProp<T3> Prop3 { get; }
        public IProp<T4> Prop4 { get; }

        public (T1, T2, T3, T4) Get()
        {
            return (Prop1.Get(), Prop2.Get(), Prop3.Get(), Prop4.Get());
        }
    }

    public class Props<T1, T2, T3, T4, T5>
    {
        public Props(IProp<T1> prop1, IProp<T2> prop2, IProp<T3> prop3, IProp<T4> prop4, IProp<T5> prop5)
        {
            Prop1 = prop1;
            Prop2 = prop2;
            Prop3 = prop3;
            Prop4 = prop4;
            Prop5 = prop5;
        }

        public IProp<T1> Prop1 { get; }
        public IProp<T2> Prop2 { get; }
        public IProp<T3> Prop3 { get; }
        public IProp<T4> Prop4 { get; }
        public IProp<T5> Prop5 { get; }

        public (T1, T2, T3, T4, T5) Get()
        {
            return (Prop1.Get(), Prop2.Get(), Prop3.Get(), Prop4.Get(), Prop5.Get());
        }
    }

    public class Props<T1, T2, T3, T4, T5, T6>
    {
        public Props(IProp<T1> prop1, IProp<T2> prop2, IProp<T3> prop3, IProp<T4> prop4, IProp<T5> prop5, IProp<T6> prop6)
        {
            Prop1 = prop1;
            Prop2 = prop2;
            Prop3 = prop3;
            Prop4 = prop4;
            Prop5 = prop5;
            Prop6 = prop6;
        }

        public IProp<T1> Prop1 { get; }
        public IProp<T2> Prop2 { get; }
        public IProp<T3> Prop3 { get; }
        public IProp<T4> Prop4 { get; }
        public IProp<T5> Prop5 { get; }
        public IProp<T6> Prop6 { get; }

        public (T1, T2, T3, T4, T5, T6) Get()
        {
            return (Prop1.Get(), Prop2.Get(), Prop3.Get(), Prop4.Get(), Prop5.Get(), Prop6.Get());
        }
    }

    public class Props<T1, T2, T3, T4, T5, T6, T7>
    {
        public Props(IProp<T1> prop1, IProp<T2> prop2, IProp<T3> prop3, IProp<T4> prop4, IProp<T5> prop5, IProp<T6> prop6, IProp<T7> prop7)
        {
            Prop1 = prop1;
            Prop2 = prop2;
            Prop3 = prop3;
            Prop4 = prop4;
            Prop5 = prop5;
            Prop6 = prop6;
            Prop7 = prop7;
        }

        public IProp<T1> Prop1 { get; }
        public IProp<T2> Prop2 { get; }
        public IProp<T3> Prop3 { get; }
        public IProp<T4> Prop4 { get; }
        public IProp<T5> Prop5 { get; }
        public IProp<T6> Prop6 { get; }
        public IProp<T7> Prop7 { get; }

        public (T1, T2, T3, T4, T5, T6, T7) Get()
        {
            return (Prop1.Get(), Prop2.Get(), Prop3.Get(), Prop4.Get(), Prop5.Get(), Prop6.Get(), Prop7.Get());
        }
    }

    public class Props
    {
        public static Props<T1> Create<T1>(IProp<T1> prop1)
        {
            return new Props<T1>(prop1);
        }
       
        public static Props<T1, T2> Create<T1, T2>(IProp<T1> prop1, IProp<T2> prop2)
        {
            return new Props<T1, T2>(prop1, prop2);
        }
       
        public static Props<T1, T2, T3> Create<T1, T2, T3>(IProp<T1> prop1, IProp<T2> prop2, IProp<T3> prop3)
        {
            return new Props<T1, T2, T3>(prop1, prop2, prop3);
        }

        public static Props<T1, T2, T3, T4> Create<T1, T2, T3, T4>(IProp<T1> prop1, IProp<T2> prop2, IProp<T3> prop3, IProp<T4> prop4)
        {
            return new Props<T1, T2, T3, T4>(prop1, prop2, prop3, prop4);
        }
        
        public static Props<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(IProp<T1> prop1, IProp<T2> prop2, IProp<T3> prop3, IProp<T4> prop4, IProp<T5> prop5)
        {
            return new Props<T1, T2, T3, T4, T5>(prop1, prop2, prop3, prop4, prop5);
        }
        
        public static Props<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(IProp<T1> prop1, IProp<T2> prop2, IProp<T3> prop3, IProp<T4> prop4, IProp<T5> prop5, IProp<T6> prop6)
        {
            return new Props<T1, T2, T3, T4, T5, T6>(prop1, prop2, prop3, prop4, prop5, prop6);
        }
       
        public static Props<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(IProp<T1> prop1, IProp<T2> prop2, IProp<T3> prop3, IProp<T4> prop4, IProp<T5> prop5, IProp<T6> prop6, IProp<T7> prop7)
        {
            return new Props<T1, T2, T3, T4, T5, T6, T7>(prop1, prop2, prop3, prop4, prop5, prop6, prop7);
        }
    }
}