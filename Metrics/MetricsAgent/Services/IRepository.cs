using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    public interface IRepository<T> where T : class
    {

        IList<T> GetByTimePeriod(TimeSpan from, TimeSpan to);

    }

}
