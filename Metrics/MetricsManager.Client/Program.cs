using System;
using System.Net.Http;

namespace MetricsManager.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MetricsManagerClient metricsManagerClient = new MetricsManagerClient("https://localhost:44354", new HttpClient());
            while (true)
            {
                
                Console.WriteLine("Задачи");
                Console.WriteLine("1 - Получить метрики за последнюю минуту (CPU) по агенту");
                Console.WriteLine("2 - Получить метрики за последнюю минуту (CPU) по кластеру");
                Console.WriteLine("3 - Получить метрики за последнюю минуту (HDD) по агенту");
                Console.WriteLine("4 - Получить метрики за последнюю минуту (HDD) по кластеру");
                Console.WriteLine("5 - Получить метрики за последнюю минуту (RAM) по агенту");
                Console.WriteLine("6 - Получить метрики за последнюю минуту (RAM) по кластеру");
                Console.WriteLine("7 - Получить метрики за последнюю минуту (NetWork) по агенту");
                Console.WriteLine("8 - Получить метрики за последнюю минуту (NetWork) по кластеру");
                Console.WriteLine("9 - Получить метрики за последнюю минуту (DotNet) по агенту");
                Console.WriteLine("10 - Получить метрики за последнюю минуту (DotNet) по кластеру");
                Console.WriteLine("0 - Завершить приложение");

                Console.WriteLine("Введите номер задачи");
                if(int.TryParse(Console.ReadLine(), out int taskNumber))
                {

                    switch (taskNumber)
                    {
                        case 0:
                            Console.WriteLine("Завершение работы приложения");
                            return;

                        case 1:
                            GetCpuMetricsFromAgent(metricsManagerClient);
                            break;
                        case 2:
                            GetCpuMetricsFromCluster(metricsManagerClient);
                            break;

                        case 3:
                            GetHddMetricsFromAgent(metricsManagerClient);
                            break;
                        case 4:
                            GetHddMetricsFromCluster(metricsManagerClient);
                            break;

                        case 5:
                            GetRamMetricsFromAgent(metricsManagerClient);
                            break;
                        case 6:
                            GetRamMetricsFromCluster(metricsManagerClient);
                            break;

                        case 7:
                            GetNetworkMetricsFromAgent(metricsManagerClient);
                            break;
                        case 8:
                            GetNetworkMetricsFromCluster(metricsManagerClient);
                            break;

                        case 9:
                            GetDotNetMetricsFromAgent(metricsManagerClient);
                            break;
                        case 10:
                            GetDotNetMetricsFromCluster(metricsManagerClient);
                            break;
                        default: 
                            Console.WriteLine("Номер задачи введен некорректно. Повторите ввод данных.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Номер задачи введен некорректно. Повторите ввод данных.");
                }

            }
        }


        private static void PrintEndInfo()
        {
            Console.WriteLine("Для продолжения нажмитe любую кнопку.");
            Console.ReadKey();
            Console.Clear();
        }

        private static void GetCpuMetricsFromCluster(MetricsManagerClient metricsManagerClient)
        {
            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
            try
            {
                CpuMetricsAllResponse response = metricsManagerClient.GetCpuMetricsFromClusterAsync(fromTime.ToString("dd\\.hh\\:mm\\:ss"), toTime.ToString("dd\\.hh\\:mm\\:ss")).Result;
                foreach (CpuMetricsWithAgentResponse cpuMetrics in response.Metrics)
                {
                    Console.WriteLine($"AgentId - {cpuMetrics.AgentId}.");
                    foreach (CpuMetricDto cpuMetric in cpuMetrics.Metrics)
                        Console.WriteLine($"{TimeSpan.Parse(cpuMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {cpuMetric.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при попытке получить CPU метрики");
                Console.WriteLine(ex.Message);
            }
            PrintEndInfo();
            return;
        }

        private static void GetCpuMetricsFromAgent(MetricsManagerClient metricsManagerClient)
        {
            Console.WriteLine("Введите номер агента");
            if (int.TryParse(Console.ReadLine(), out int agentId))
            {
                try
                {
                    AgentInfo response = metricsManagerClient.GetByIdAsync(agentId).Result;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    PrintEndInfo();
                    return;
                }
                
            }
            else
            {
                Console.WriteLine("Введен некорректно id Агента.");
                PrintEndInfo();
                return;
            }
            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
            try
            {
                CpuMetricsWithAgentResponse response = metricsManagerClient.GetCpuMetricsFromAgentAsync(agentId, fromTime.ToString("dd\\.hh\\:mm\\:ss"), toTime.ToString("dd\\.hh\\:mm\\:ss")).Result;
                foreach (CpuMetricDto cpuMetric in response.Metrics)
                {
                    Console.WriteLine($"{TimeSpan.Parse(cpuMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {cpuMetric.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при попытке получить CPU метрики");
                Console.WriteLine(ex.Message);
            }
            PrintEndInfo();
            return;
        }

        private static void GetHddMetricsFromCluster(MetricsManagerClient metricsManagerClient)
        {
            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
            try
            {
                HddMetricsAllResponse response = metricsManagerClient.GetHddMetricsFromClusterAsync(fromTime.ToString("dd\\.hh\\:mm\\:ss"), toTime.ToString("dd\\.hh\\:mm\\:ss")).Result;
                foreach (HddMetricsWithAgentResponse hddMetrics in response.Metrics)
                {
                    Console.WriteLine($"AgentId - {hddMetrics.AgentId}.");
                    foreach (HddMetricDto hddMetric in hddMetrics.Metrics)
                        Console.WriteLine($"{TimeSpan.Parse(hddMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {hddMetric.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при попытке получить HDD метрики");
                Console.WriteLine(ex.Message);
            }
            PrintEndInfo();
            return;
        }

        private static void GetHddMetricsFromAgent(MetricsManagerClient metricsManagerClient)
        {
            Console.WriteLine("Введите номер агента");
            if (int.TryParse(Console.ReadLine(), out int agentId))
            {
                try
                {
                    AgentInfo response = metricsManagerClient.GetByIdAsync(agentId).Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    PrintEndInfo();
                    return;
                }

            }
            else
            {
                Console.WriteLine("Введен некорректно id Агента.");
                PrintEndInfo();
                return;
            }
            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
            try
            {
                HddMetricsWithAgentResponse response = metricsManagerClient.GetHddMetricsFromAgentAsync(agentId, fromTime.ToString("dd\\.hh\\:mm\\:ss"), toTime.ToString("dd\\.hh\\:mm\\:ss")).Result;
                foreach (HddMetricDto hddMetric in response.Metrics)
                {
                    Console.WriteLine($"{TimeSpan.Parse(hddMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {hddMetric.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при попытке получить Hdd метрики");
                Console.WriteLine(ex.Message);
            }
            PrintEndInfo();
            return;
        }

        private static void GetRamMetricsFromCluster(MetricsManagerClient metricsManagerClient)
        {
            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
            try
            {
                RamMetricsAllResponse response = metricsManagerClient.GetRamMetricsFromClusterAsync(fromTime.ToString("dd\\.hh\\:mm\\:ss"), toTime.ToString("dd\\.hh\\:mm\\:ss")).Result;
                foreach (RamMetricsWithAgentResponse ramMetrics in response.Metrics)
                {
                    Console.WriteLine($"AgentId - {ramMetrics.AgentId}.");
                    foreach (RamMetricDto ramMetric in ramMetrics.Metrics)
                        Console.WriteLine($"{TimeSpan.Parse(ramMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {ramMetric.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при попытке получить RAM метрики");
                Console.WriteLine(ex.Message);
            }
            PrintEndInfo();
            return;
        }

        private static void GetRamMetricsFromAgent(MetricsManagerClient metricsManagerClient)
        {
            Console.WriteLine("Введите номер агента");
            if (int.TryParse(Console.ReadLine(), out int agentId))
            {
                try
                {
                    AgentInfo response = metricsManagerClient.GetByIdAsync(agentId).Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    PrintEndInfo();
                    return;
                }

            }
            else
            {
                Console.WriteLine("Введен некорректно id Агента.");
                PrintEndInfo();
                return;
            }
            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
            try
            {
                RamMetricsWithAgentResponse response = metricsManagerClient.GetRamMetricsFromAgentAsync(agentId, fromTime.ToString("dd\\.hh\\:mm\\:ss"), toTime.ToString("dd\\.hh\\:mm\\:ss")).Result;
                foreach (RamMetricDto ramMetric in response.Metrics)
                {
                    Console.WriteLine($"{TimeSpan.Parse(ramMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {ramMetric.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при попытке получить RAM метрики");
                Console.WriteLine(ex.Message);
            }
            PrintEndInfo();
            return;
        }

        private static void GetNetworkMetricsFromCluster(MetricsManagerClient metricsManagerClient)
        {
            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
            try
            {
                NetworkMetricsAllResponse response = metricsManagerClient.GetNetworkMetricsFromClusterAsync(fromTime.ToString("dd\\.hh\\:mm\\:ss"), toTime.ToString("dd\\.hh\\:mm\\:ss")).Result;
                foreach (NetworkMetricsWithAgentResponse networkMetrics in response.Metrics)
                {
                    Console.WriteLine($"AgentId - {networkMetrics.AgentId}.");
                    foreach (NetworkMetricDto networkMetric in networkMetrics.Metrics)
                        Console.WriteLine($"{TimeSpan.Parse(networkMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {networkMetric.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при попытке получить Network метрики");
                Console.WriteLine(ex.Message);
            }
            PrintEndInfo();
            return;
        }

        private static void GetNetworkMetricsFromAgent(MetricsManagerClient metricsManagerClient)
        {
            Console.WriteLine("Введите номер агента");
            if (int.TryParse(Console.ReadLine(), out int agentId))
            {
                try
                {
                    AgentInfo response = metricsManagerClient.GetByIdAsync(agentId).Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    PrintEndInfo();
                    return;
                }

            }
            else
            {
                Console.WriteLine("Введен некорректно id Агента.");
                PrintEndInfo();
                return;
            }
            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
            try
            {
                NetworkMetricsWithAgentResponse response = metricsManagerClient.GetNetworkMetricsFromAgentAsync(agentId, fromTime.ToString("dd\\.hh\\:mm\\:ss"), toTime.ToString("dd\\.hh\\:mm\\:ss")).Result;
                foreach (NetworkMetricDto networkMetric in response.Metrics)
                {
                    Console.WriteLine($"{TimeSpan.Parse(networkMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {networkMetric.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при попытке получить Network метрики");
                Console.WriteLine(ex.Message);
            }
            PrintEndInfo();
            return;
        }

        private static void GetDotNetMetricsFromCluster(MetricsManagerClient metricsManagerClient)
        {
            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
            try
            {
                DotNetMetricsAllResponse response = metricsManagerClient.GetDotNetMetricsFromClusterAsync(fromTime.ToString("dd\\.hh\\:mm\\:ss"), toTime.ToString("dd\\.hh\\:mm\\:ss")).Result;
                foreach (DotNetMetricsWithAgentResponse dotNetMetrics in response.Metrics)
                {
                    Console.WriteLine($"AgentId - {dotNetMetrics.AgentId}.");
                    foreach (DotNetMetricDto dotNetMetric in dotNetMetrics.Metrics)
                        Console.WriteLine($"{TimeSpan.Parse(dotNetMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {dotNetMetric.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при попытке получить DotNet метрики");
                Console.WriteLine(ex.Message);
            }
            PrintEndInfo();
            return;
        }

        private static void GetDotNetMetricsFromAgent(MetricsManagerClient metricsManagerClient)
        {
            Console.WriteLine("Введите номер агента");
            if (int.TryParse(Console.ReadLine(), out int agentId))
            {
                try
                {
                    AgentInfo response = metricsManagerClient.GetByIdAsync(agentId).Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    PrintEndInfo();
                    return;
                }

            }
            else
            {
                Console.WriteLine("Введен некорректно id Агента.");
                PrintEndInfo();
                return;
            }
            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
            try
            {
                DotNetMetricsWithAgentResponse response = metricsManagerClient.GetDotNetMetricsFromAgentAsync(agentId, fromTime.ToString("dd\\.hh\\:mm\\:ss"), toTime.ToString("dd\\.hh\\:mm\\:ss")).Result;
                foreach (DotNetMetricDto dotNetMetric in response.Metrics)
                {
                    Console.WriteLine($"{TimeSpan.Parse(dotNetMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {dotNetMetric.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при попытке получить DotNet метрики");
                Console.WriteLine(ex.Message);
            }
            PrintEndInfo();
            return;
        }
    }
}
