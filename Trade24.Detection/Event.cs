namespace Trade24.Detection
{
    public class Event
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public Direction Direction { get; set; }
        public EventType Type { get; set; }


    }

    public enum Direction
    {
        Bullish,
        Bearish
    }

    public enum EventType
    {
        CloseCross50Sma,

    }

}
