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
