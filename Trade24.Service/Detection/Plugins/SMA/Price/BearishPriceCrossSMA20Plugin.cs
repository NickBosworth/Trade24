namespace Trade24.Service.Detection.Plugins.SMA.Price
{
    using Trade24.Service.Data;
    public class BearishPriceCrossSMA20Plugin : BasePriceCrossSMAPlugin, IDetector
    {
        public BearishPriceCrossSMA20Plugin()
        {
            Id = Guid.Parse("91562819-94b0-444e-aa2f-41bb2d23d443");
            Name = "Bearish Price Cross 20 SMA";
            Description = "Bearish Price Cross SMA20";
            MinimumDays = 20;
            SignalType = SignalType.Bearish;
            Length = 20;
            BullishCheck = false;
        }
    }
}
