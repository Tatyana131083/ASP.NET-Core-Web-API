﻿using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    public interface IRepository<T> where T : class
    {

        IList<T> GetByTimePeriod(TimeSpan from, TimeSpan to);
        IList<T> GetAll();
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);

    }

}
