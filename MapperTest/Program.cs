using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            //As an alternative, you can also use Attribute Mapping: https://docs.automapper.org/en/latest/Attribute-mapping.html
            //Mapper.CustomMappings.Add(typeof(MyCustomSubclass), typeof(MyCustomSubclassViewModel)); //or:            
            //Mapper.CustomMappings.Add<MyCustomSubclass, MyCustomSubclassViewModel>();

            //Optional advanced configuration; see below
            Mapper.OnConfiguring += AdvancedConf;

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

        static void AdvancedConf(object sender, MapperConfiguringEventArgs e)
        {
            //e.Configuration.CreateMap<MyViewModel, My>().ForMember(my => my.MyEquipment, opt => opt.MapFrom(dto => MyUnflattenMethod<MyEquipment>(dto.Id, dto.Equipment)));
            //e.Configuration.CreateMap<My, MyViewModel>().ForMember(dto => dto.Equipment, opt => opt.MapFrom(my => MyFlattenMethod(my.MyEquipment)));
        }


   
    }

   
}
