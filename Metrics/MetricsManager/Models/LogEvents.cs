namespace MetricsManager.Models
{
    public class LogEvents
    {
        public const int ListMetrics = 1001;
        public const int GetMetrics = 1002;
        public const int InsertMetric = 1003;
        public const int UpdateMetric = 1004;
        public const int DeleteMetric = 1005;

        public const int TestMetric = 3000;

        public const int GetMetricNotFound = 4000;
        public const int UpdateMetricNotFound = 4001;
    }
}
