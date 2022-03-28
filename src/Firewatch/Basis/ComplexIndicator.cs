namespace Firewatch
{
    public class ComplexIndicator
    {
        public int WeatherStationId { get; private set; }

        public double Value { get; private set; }

        public ComplexIndicator(int weatherStationId, double value)
        {
            WeatherStationId = weatherStationId;
            Value = value;
        }
    }
}