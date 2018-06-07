using AutoMapper;

namespace Stone.Sdk.Extensions
{
    public static class ObjectExtensions
    {
        public static TDestination MapTo<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}