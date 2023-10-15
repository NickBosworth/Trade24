namespace Trade24.Common.Data
{
    public class RichDailyStockData : DailyStockData
    {
        public RichDailyStockData(DailyStockData data)
        {
            Symbol = data.Symbol;
            Date = data.Date;
            Open = data.Open;
            High = data.High;
            Low = data.Low;
            Close = data.Close;
            AdjustedClose = data.AdjustedClose;
            Volume = data.Volume;
        }


        //SMA
        public decimal? SMA50 { get; set; } = null;
        public decimal? SMA200 { get; set; } = null;
        public decimal? SMA20 { get; set; } = null;
        public decimal? SMA10 { get; set; } = null;
        public decimal? SMA5 { get; set; } = null;
        public decimal? SMA9 { get; set; } = null;
        public decimal? SMA21 { get; set; } = null;
        public decimal? SMA100 { get; set; } = null;

        //EMA
        public decimal? EMA12 { get; set; } = null;
        public decimal? EMA26 { get; set; } = null;
        public decimal? EMA50 { get; set; } = null;
        public decimal? EMA100 { get; set; } = null;
        public decimal? EMA200 { get; set; } = null;
        public decimal? EMA9 { get; set; } = null;
        public decimal? EMA21 { get; set; } = null;
        public decimal? EMA8 { get; set; } = null;
        public decimal? EMA5 { get; set; } = null;
        public decimal? EMA10 { get; set; } = null;

        //RSI
        public decimal? RSI14 { get; set; } = null;

        //MACD
        public decimal? MACD { get; set; } = null;
        public decimal? MACDSignal { get; set; } = null;
        public decimal? MACDHistogram { get; set; } = null;
        public decimal? MACDFastEMA { get; set; } = null;
        public decimal? MACDSlowEMA { get; set; } = null;

        //Ichimoku Cloud
        public decimal? IchimokuConversionLine { get; set; } = null;
        public decimal? IchimokuBaseLine { get; set; } = null;
        public decimal? IchimokuLeadingSpanA { get; set; } = null;
        public decimal? IchimokuLeadingSpanB { get; set; } = null;
        public decimal? IchimokuLaggingSpan { get; set; } = null;

        //ADX
        public decimal? ADX { get; set; } = null;

        //Parabolic SAR
        public decimal? ParabolicSAR { get; set; } = null;

        //Stochastic Oscillator
        public decimal? StochasticOscillator { get; set; } = null;
        public decimal? StochasticOscillatorD { get; set; } = null;
        public decimal? StochasticOscillatorJ { get; set; } = null;

        //On Balance Volume
        public decimal? OnBalanceVolume { get; set; } = null;

    }
}
