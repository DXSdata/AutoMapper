# AutoMapper
 Based on the [original .NET AutoMapper](https://github.com/AutoMapper/AutoMapper), keeping a workaround for the static mapping methods which were removed with v9.

# Installation
 NuGet: https://www.nuget.org/packages/DXSdata.AutoMapper

# Usage

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.DXSdata;

namespace MapperTest
{
    class Program
    {
        static void Main()
        {

            //Mapper.Assembly = nameof(MyAlternativeDbContext);

            var list1 = new List<MyClass1>
            {
                new MyClass1(),
                new MyClass1(),
                new MyClass1()
            };

            var dto = list1.Map<List<MyClass2>>();
            var dto2 = Mapper.Map<List<MyClass2>>(list1);

            var dto3 = list1.MapL<MyClass2>(); //for convenience
            var dto4 = Mapper.MapL<MyClass2>(list1);

            var dto5 = list1.AsQueryable().ProjectTo<List<MyClass2>>(); //for DB queries
            var dto6 = Mapper.ProjectTo<List<MyClass2>>(list1.AsQueryable());
            
            
        }
    }
}

```

# Notes

If you use nested objects, like MyClass1Object.MyClass2Object.Var1, please use the approach "[Attribute Mapping](https://docs.automapper.org/en/latest/Attribute-mapping.html)". Otherwise you will get a "missing maps" error.

If the attributed classes are not within your default project assembly, please set Mapper.Assembly manually.

E.g. if your DB context classes reside in another project called MyContext, set:
```<language>
Mapper.Assembly = nameof(MyContext);
```

# Links

Website: https://www.dxsdata.com/2019/12/automapper-with-static-extensionmethods-for-net-standard