﻿using System;

namespace MetricsAgent.DAL.Response
{
    public class NetworkMetricDto
    {
        public TimeSpan Time { get; set; }

        public int Value { get; set; }

        public int Id { get; set; }
    }
}
