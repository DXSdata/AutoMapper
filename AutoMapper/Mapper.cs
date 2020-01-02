using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoMapper.DXSdata
{
    public static class Mapper
    {
        /// <summary>
        /// The assembly which contains the EF DB context classes (and maybe AutoMap attributes)
        /// </summary>
        public static string Assembly { get; set; } = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;


        /// <summary>
        /// Custom assignments, e.g. for nested object types
        /// </summary>
        public static List<(Type TSource, Type TDestination)> CustomMappings { get; set; } = new List<(Type TSource, Type TDestination)>();
        

        private static IMapper Create(Type TSource, Type TDestination)
        {
            var config = new MapperConfiguration(cfg =>
            {
                //Add attributes e.g. set in ViewModel classes, e.g. for resolving nested classes
                cfg.AddMaps(Assembly);

                //the current main class types to be mapped
                cfg.CreateMap(TSource, TDestination);

                //apply static/global custom assignments
                foreach (var cm in CustomMappings)
                    cfg.CreateMap(cm.TSource, cm.TDestination);
            });
            return config.CreateMapper();
        }


        /// <summary>
        /// Map list of objects
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination Map<TDestination>(this IEnumerable<object> source)
        {
            var sourceType = source.GetType().GetGenericArguments()[0];

            //check if array (not list)
            if (typeof(TDestination).IsArray)
                return Create(sourceType, typeof(TDestination).GetElementType()).Map<TDestination>(source);
            
            //should be list from here
            var destArgs = typeof(TDestination).GetGenericArguments();
            if (destArgs.Length == 0)
                throw new ArrayTypeMismatchException("Please check your destination type; should be IEnumerable (or use MapL())");

            return Create(sourceType,destArgs[0]) //could be optimized: https://stackoverflow.com/questions/906499/getting-type-t-from-ienumerablet
                .Map<TDestination>(source);
        }

        /// <summary>
        /// Similar to Map(), but using inner List type as parameter instead of List with type
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<TDestination> MapL<TDestination>(this IEnumerable<object> source) 
        {
            return Map<List<TDestination>>(source);
        }


        /// <summary>
        /// Map single object
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination Map<TDestination>(this object source)
        {
            return Create(source.GetType(), typeof(TDestination))
                .Map<TDestination>(source);
        }

        /// <summary>
        /// Project DB query to ViewModel
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source)
        {
            return Create(source.ElementType, typeof(TDestination))
                .ProjectTo<TDestination>(source);
        }
    }
}
