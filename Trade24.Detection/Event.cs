namespace Trade24.Detection
{
    public class Event
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public Direction Direction { get; set; }
        public Guid EventTypeId { get; set; }
        public string Description { get; set; }


    }

    public enum Direction
    {
        Bullish,
        Bearish
    }


}
