﻿using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MetricsManager.Wpf.Client
{
    /// <summary>
    /// Interaction logic for RamChartControl.xaml
    /// </summary>
    public partial class RamChartControl : UserControl, INotifyPropertyChanged
    {
        private MetricsManagerClient _metricsManagerClient;
        private SeriesCollection _columnSeriesValues;
        private string _persentText;
        private string _persentTextDesciption;

        public RamChartControl()
        {
            InitializeComponent();
            _metricsManagerClient = new MetricsManagerClient("https://localhost:44354", new HttpClient());
            DataContext = this;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public SeriesCollection ColumnSeriesValues
        {
            get
            {
                return _columnSeriesValues;
            }
            set
            {
                _columnSeriesValues = value;
                OnPropertyChanged("ColumnSeriesValues");
            }
        }

        public string PersentText
        {
            get
            {
                return _persentText;
            }
            set
            {
                _persentText = value;
                OnPropertyChanged("PersentText");
            }
        }

        public string PersentTextDesciption
        {
            get
            {
                return _persentTextDesciption;
            }
            set
            {
                _persentTextDesciption = value;
                OnPropertyChanged("PersentTextDesciption");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        private void UpdateOnСlick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => {
                while (true)
                {
                    TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()); // new
                    TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
                    try
                    {
                        RamMetricsWithAgentResponse ramMetricsResponse =
                               _metricsManagerClient.GetRamMetricsFromAgentAsync(1, fromTime.ToString("dd\\.hh\\:mm\\:ss"), toTime.ToString("dd\\.hh\\:mm\\:ss")).Result;
                        RamMetricDto[] metrics = ramMetricsResponse.Metrics.ToArray();

                        Dispatcher.Invoke(() =>
                        {
                            if (ramMetricsResponse.Metrics.Count > 0)
                            {
                                TimeSpan del = TimeSpan.Parse(metrics[metrics.Count() - 1].Time) - TimeSpan.Parse(metrics[0].Time);
                                PersentTextDesciption = $"За последние {del.TotalSeconds} сек. средняя загрузка";
                                double sum = (double)metrics.Where(x => x != null).Select(x => x.Value).ToArray().Sum(x => x);
                                PersentText = $"{sum / metrics.Count():F2}";
                            }

                            ColumnSeriesValues = new SeriesCollection
                            {
                                new ColumnSeries
                                {
                                    Values = new ChartValues<int>(metrics.Where(x => x != null).Select(x => x.Value).ToArray())
                                }
                            };

                            TimePowerChart.Update(true);
                        });
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Произошла ошибка при попытке получить RAM метрики.\n{ex.Message}");
                    }

                    Thread.Sleep(5000);

                }
            });

        }
    }
}
