using System;
using System.Collections.Generic;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace HKShoppingManage.Common
{
    public static class EmitMapperHelper
    {
        /// <summary>
        /// 将From映射到To
        /// </summary>
        /// <typeparam name="To">输出类型</typeparam>
        /// <typeparam name="From">输入类型</typeparam>
        /// <param name="from">输入类型实体</param>
        /// <returns></returns>
        public static To Map<To, From>(this From from) where From : class , new()
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<From, To>(
                new DefaultMapConfig()
                .DeepMap()
                .NullSubstitution<DateTime?, DateTime>((value) => DateTime.MinValue)
                );
            return mapper.Map(from);
        }

        /// <summary>
        /// 将From集合映射到To集合
        /// </summary>
        /// <typeparam name="To">输出类型</typeparam>
        /// <typeparam name="From">输入类型</typeparam>
        /// <param name="from">输入类型实体</param>
        /// <returns></returns>
        public static List<To> MapList<To, From>(this List<From> from) where From : class , new()
        {
            //var mapper = ObjectMapperManager.DefaultInstance.GetMapper<From, To>(
            //    new DefaultMapConfig()
            //    .DeepMap()
            //    .NullSubstitution<DateTime?, DateTime>((value) => DateTime.MinValue)
            //    );
            //List<To> list = new List<To>();
            //foreach (var item in from)
            //{
            //    list.Add(mapper.Map(item));
            //}
            //return list;
            return Map<List<To>, List<From>>(from);
        }

    }
}
