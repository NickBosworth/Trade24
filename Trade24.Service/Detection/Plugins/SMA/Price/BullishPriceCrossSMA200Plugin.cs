namespace Trade24.Service.Detection.Plugins.SMA.Price
{
    using Trade24.Service.Data;
    public class BullishPriceCrossSMA200Plugin : BasePriceCrossSMAPlugin, IDetector
    {
        public BullishPriceCrossSMA200Plugin()
        {
            Id = Guid.Parse("4d22d9f9-1c26-477b-99a3-431314203d36");
            Name = "Bullish Price Cross 200 SMA";
            Description = "Bullish Price Cross SMA200";
            MinimumDays = 200;
            LastDayIsMatch = true;
            SignalType = SignalType.Bullish;
            Length = 200;
            BullishCheck = true;
        }
    }
}
