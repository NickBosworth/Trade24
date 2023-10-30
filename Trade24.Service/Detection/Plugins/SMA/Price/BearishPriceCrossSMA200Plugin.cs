namespace Trade24.Service.Detection.Plugins.SMA.Price
{
    using Trade24.Service.Data;
    public class BearishPriceCrossSMA200Plugin : BasePriceCrossSMAPlugin, IDetector
    {
        public BearishPriceCrossSMA200Plugin()
        {
            Id = Guid.Parse("1243ea4e-2931-4df3-b41b-17ed7878bc04");
            Name = "Bearish Price Cross 200 SMA";
            Description = "Bearish Price Cross SMA200";
            MinimumDays = 200;
            SignalType = SignalType.Bearish;
            Length = 200;
            BullishCheck = false;
        }
    }
}
