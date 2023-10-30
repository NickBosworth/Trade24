namespace Trade24.Service.Detection.Plugins.SMA.Price
{
    using Trade24.Service.Data;
    public class BearishPriceCrossSMA50Plugin : BasePriceCrossSMAPlugin, IDetector
    {
        public BearishPriceCrossSMA50Plugin()
        {
            Id = Guid.Parse("71a88d3e-74ef-41ec-90e6-2791acf65e9c");
            Name = "Bearish Price Cross 50 SMA";
            Description = "Bearish Price Cross SMA50";
            MinimumDays = 50;
            SignalType = SignalType.Bearish;
            Length = 50;
            BullishCheck = false;
        }
    }
}
