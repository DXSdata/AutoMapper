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

            //If DB classes reside in other namespace, assembly etc.
            //Mapper.Assembly = nameof(MyAlternativeDbContext);

            //For more complex types, e.g nested custom objects which AutoMapper cannot determine automatically
            //Mapper.CustomMappings.Add(typeof(MyCustomSubclass), typeof(MyCustomSubclassViewModel)); //or:            
            //Mapper.CustomMappings.Add<MyCustomSubclass, MyCustomSubclassViewModel>();

            var list1 = new List<MyClass1>
            {
                new MyClass1(),
                new MyClass1(),
                new MyClass1()
            };

            var dto = list1.Map<List<MyClass2>>();
            var dto2 = Mapper.Map<List<MyClass2>>(list1);
            var dto3 = Mapper.Map<MyClass2[]>(list1);

            var dto4 = list1.MapL<MyClass2>(); //for convenience
            var dto5 = Mapper.MapL<MyClass2>(list1);

            var dto6 = list1.AsQueryable().ProjectTo<List<MyClass2>>(); //for DB queries
            var dto7 = Mapper.ProjectTo<List<MyClass2>>(list1.AsQueryable());

            

        }
    }
}
