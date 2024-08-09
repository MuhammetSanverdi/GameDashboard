using GameDashboard.Application.Abstractions;
using Mapster;


namespace GameDashboard.Mapper
{
    public class Mapper : IMapper
    {
        public TDestination Map<TDestination, TSource>(TSource source)
        {
            return source.Adapt<TDestination>();
        }

        public IList<TDestination> Map<TDestination, TSource>(IList<TSource> sources)
        {
            return sources.Adapt<IList<TDestination>>();
        }

        public TDestination Map<TDestination>(object source)
        {
            return source.Adapt<TDestination>();
        }

        public IList<TDestination> Map<TDestination>(IList<object> source)
        {
            return source.Adapt<IList<TDestination>>();
        }
    }
}
