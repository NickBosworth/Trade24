namespace Trade24.Service.Detection.Plugins.SMA.Price
{
    using Trade24.Service.Data;
    public class BullishPriceCrossSMA20Plugin : BasePriceCrossSMAPlugin, IDetector
    {
        public BullishPriceCrossSMA20Plugin()
        {
            Id = Guid.Parse("4811b9b8-d3b2-4f5d-90d1-752aafcc21fa");
            Name = "Bullish Price Cross 20 SMA";
            Description = "Bullish Price Cross SMA20";
            MinimumDays = 20;
            SignalType = SignalType.Bullish;
            Length = 20;
            BullishCheck = true;
        }

    }
}
