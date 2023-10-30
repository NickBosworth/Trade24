namespace Trade24.Service.Detection.Plugins.SMA.Price
{
    using Trade24.Service.Data;
    public class BearishPriceCrossSMA100Plugin : BasePriceCrossSMAPlugin, IDetector
    {
        public BearishPriceCrossSMA100Plugin()
        {
            Id = Guid.Parse("20e50630-ce2e-4328-87f7-3baec1d250e2");
            Name = "Bearish Price Cross 100 SMA";
            Description = "Bearish Price Cross SMA100";
            MinimumDays = 100;
            SignalType = SignalType.Bearish;
            Length = 100;
            BullishCheck = false;
        }
    }
}
