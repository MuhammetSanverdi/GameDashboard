using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboard.Application.Abstractions
{
    public interface IMapper
    {
        TDestination Map<TDestination, TSource>(TSource source);
        IList<TDestination> Map<TDestination, TSource>(IList<TSource> sources);
        TDestination Map<TDestination>(object source);
        IList<TDestination> Map<TDestination>(IList<object> source);
    }
}
