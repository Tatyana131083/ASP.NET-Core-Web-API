using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherManager
{
    public struct Value
    {
        public DateTime DateTime { set; get; }
        public int Temperature { set; get; }

        public Value(DateTime dateTime, int temperature)
        {
            DateTime = dateTime;
            Temperature = temperature;
        }

        public override string ToString()
        {
            return DateTime + ": " + Temperature;
        }
    }

    public class WeatherHistory
    {
        private List<Value> _values;

        public WeatherHistory()
        {
            _values = new List<Value>();
        }

        public List<Value> Values
        {
            get { return _values; }
            set { _values = value; }
        }

        public void Add(DateTime dateTime, int temperature)
        {
            _values.Add(new Value(dateTime, temperature));
        }

        public string Get(DateTime From, DateTime To)
        {
            
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < _values.Count; i++)
            {
                //if (_values[i].DateTime.CompareTo(From) >= 0 && _values[i].DateTime.CompareTo(To) <= 0)
                if (_values[i].DateTime >= From && _values[i].DateTime <= To)
                {
                    result.Append( _values[i]);
                    result.Append('\t');
                }

            }
            return result.ToString();
        }

        public void Remove(DateTime From, DateTime To)
        {

            for (int i = 0; i < _values.Count; i++)
            {
                if (_values[i].DateTime >= From && _values[i].DateTime <= To)
                {
                    _values.RemoveAt(i);
                    i--;
                }

            }
        }
    }
}
