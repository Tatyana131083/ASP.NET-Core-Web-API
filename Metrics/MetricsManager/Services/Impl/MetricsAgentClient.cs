using MetricsLib.Models.Request;
using MetricsLib.Models.Response;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace MetricsManager.Services.Impl
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly IAgentRepository _repository;
        private readonly ILogger<MetricsAgentClient> _logger;

        public MetricsAgentClient(HttpClient httpClient, IAgentRepository repository, ILogger<MetricsAgentClient> logger)
        {
            _httpClient = httpClient;
            _repository = repository;
            _logger = logger;
        }

        public CpuMetricsWithAgentResponse GetCpuMetrics(CpuMetricsRequest request)
        {
            try
            {
                AgentInfo agentInfo = _repository.GetById(request.AgentId);
                if (agentInfo == null)
                {
                    return null;
                }
                string requestQuery = $"{agentInfo.Url}api/metrics/cpu/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);
                httpRequestMessage.Headers.Add("Accept", "application/json");
                HttpResponseMessage httpResponseMessage = _httpClient.SendAsync(httpRequestMessage).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string response = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    CpuMetricsWithAgentResponse metrics = (CpuMetricsWithAgentResponse)JsonConvert.DeserializeObject(response, typeof(CpuMetricsWithAgentResponse));
                    metrics.AgentId = request.AgentId;
                    return metrics;
                }
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null; 
        }

        public CpuMetricsAllResponse GetCpuMetricsFromAllAgents(CpuMetricsAllRequest request)
        {
            IList<AgentInfo> agentsInfo =  _repository.GetAll();
            CpuMetricsAllResponse allResponse = new CpuMetricsAllResponse();
            foreach (AgentInfo agentInfo in agentsInfo)
            {
                CpuMetricsWithAgentResponse response = GetCpuMetrics(new CpuMetricsRequest()
                {
                    AgentId = agentInfo.Id,
                    ToTime = request.ToTime,
                    FromTime = request.FromTime,
                }) ;
                allResponse.Metrics.Add(response);
            }
            return allResponse;
        }

        public DotNetMetricsWithAgentResponse GetDotNetMetrics(DotNetMetricsRequest request)
        {
            try
            {
                AgentInfo agentInfo = _repository.GetById(request.AgentId);
                if (agentInfo == null)
                {
                    return null;
                }
                string requestQuery = $"{agentInfo.Url}api/metrics/dotnet/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);
                httpRequestMessage.Headers.Add("Accept", "application/json");
                HttpResponseMessage httpResponseMessage = _httpClient.SendAsync(httpRequestMessage).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string response = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    DotNetMetricsWithAgentResponse metrics = (DotNetMetricsWithAgentResponse)JsonConvert.DeserializeObject(response, typeof(DotNetMetricsWithAgentResponse));
                    metrics.AgentId = request.AgentId;
                    return metrics;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public DotNetMetricsAllResponse GetDotNetMetricsFromAllAgents(DotNetMetricsAllRequest request)
        {
            IList<AgentInfo> agentsInfo = _repository.GetAll();
            DotNetMetricsAllResponse allResponse = new DotNetMetricsAllResponse();
            foreach (AgentInfo agentInfo in agentsInfo)
            {
                DotNetMetricsWithAgentResponse response = GetDotNetMetrics(new DotNetMetricsRequest()
                {
                    AgentId = agentInfo.Id,
                    ToTime = request.ToTime,
                    FromTime = request.FromTime,
                });
                allResponse.Metrics.Add(response);
            }
            return allResponse;
        }

        public HddMetricsWithAgentResponse GetHddMetrics(HddMetricsRequest request)
        {
            try
            {
                AgentInfo agentInfo = _repository.GetById(request.AgentId);
                if (agentInfo == null)
                {
                    return null;
                }
                string requestQuery = $"{agentInfo.Url}api/metrics/hdd/left/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);
                httpRequestMessage.Headers.Add("Accept", "application/json");
                HttpResponseMessage httpResponseMessage = _httpClient.SendAsync(httpRequestMessage).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string response = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    HddMetricsWithAgentResponse metrics = (HddMetricsWithAgentResponse)JsonConvert.DeserializeObject(response, typeof(HddMetricsWithAgentResponse));
                    metrics.AgentId = request.AgentId;
                    return metrics;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public HddMetricsAllResponse GetHddMetricsFromAllAgents(HddMetricsAllRequest request)
        {
            IList<AgentInfo> agentsInfo = _repository.GetAll();
            HddMetricsAllResponse allResponse = new HddMetricsAllResponse();
            foreach (AgentInfo agentInfo in agentsInfo)
            {
                HddMetricsWithAgentResponse response = GetHddMetrics(new HddMetricsRequest()
                {
                    AgentId = agentInfo.Id,
                    ToTime = request.ToTime,
                    FromTime = request.FromTime,
                });
                allResponse.Metrics.Add(response);
            }
            return allResponse;
        }

        public NetworkMetricsWithAgentResponse GetNetworkMetrics(NetworkMetricsRequest request)
        {
            try
            {
                AgentInfo agentInfo = _repository.GetById(request.AgentId);
                if (agentInfo == null)
                {
                    return null;
                }
                string requestQuery = $"{agentInfo.Url}api/metrics/network/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);
                httpRequestMessage.Headers.Add("Accept", "application/json");
                HttpResponseMessage httpResponseMessage = _httpClient.SendAsync(httpRequestMessage).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string response = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    NetworkMetricsWithAgentResponse metrics = (NetworkMetricsWithAgentResponse)JsonConvert.DeserializeObject(response, typeof(NetworkMetricsWithAgentResponse));
                    metrics.AgentId = request.AgentId;
                    return metrics;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public NetworkMetricsAllResponse GetNetworkMetricsFromAllAgents(NetworkMetricsAllRequest request)
        {
            IList<AgentInfo> agentsInfo = _repository.GetAll();
            NetworkMetricsAllResponse allResponse = new NetworkMetricsAllResponse();
            foreach (AgentInfo agentInfo in agentsInfo)
            {
                NetworkMetricsWithAgentResponse response = GetNetworkMetrics(new NetworkMetricsRequest()
                {
                    AgentId = agentInfo.Id,
                    ToTime = request.ToTime,
                    FromTime = request.FromTime,
                });
                allResponse.Metrics.Add(response);
            }
            return allResponse;
        }

        public RamMetricsWithAgentResponse GetRamMetrics(RamMetricsRequest request)
        {
            try
            {
                AgentInfo agentInfo = _repository.GetById(request.AgentId);
                if (agentInfo == null)
                {
                    return null;
                }
                string requestQuery = $"{agentInfo.Url}api/metrics/ram/available/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);
                httpRequestMessage.Headers.Add("Accept", "application/json");
                HttpResponseMessage httpResponseMessage = _httpClient.SendAsync(httpRequestMessage).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string response = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    RamMetricsWithAgentResponse metrics = (RamMetricsWithAgentResponse)JsonConvert.DeserializeObject(response, typeof(RamMetricsWithAgentResponse));
                    metrics.AgentId = request.AgentId;
                    return metrics;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public RamMetricsAllResponse GetRamMetricsFromAllAgents(RamMetricsAllRequest request)
        {
            IList<AgentInfo> agentsInfo = _repository.GetAll();
            RamMetricsAllResponse allResponse = new RamMetricsAllResponse();
            foreach (AgentInfo agentInfo in agentsInfo)
            {
                RamMetricsWithAgentResponse response = GetRamMetrics(new RamMetricsRequest()
                {
                    AgentId = agentInfo.Id,
                    ToTime = request.ToTime,
                    FromTime = request.FromTime,
                });
                allResponse.Metrics.Add(response);
            }
            return allResponse;
        }
    }
}
