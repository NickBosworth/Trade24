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
        public decimal SMA50 { get; set; }
        public decimal SMA200 { get; set; }
        public decimal SMA20 { get; set; }
        public decimal SMA10 { get; set; }
        public decimal SMA5 { get; set; }
        public decimal SMA9 { get; set; }
        public decimal SMA21 { get; set; }
        public decimal SMA100 { get; set; }

        //RSI
        public decimal RSI14 { get; set; }

        //MACD
        public decimal MACD { get; set; }
        public decimal MACDSignal { get; set; }
        public decimal MACDHistogram { get; set; }
        public decimal MACDFastEMA { get; set; }

        //Ichimoku Cloud
        public decimal IchimokuConversionLine { get; set; }
        public decimal IchimokuBaseLine { get; set; }
        public decimal IchimokuLeadingSpanA { get; set; }
        public decimal IchimokuLeadingSpanB { get; set; }
        public decimal IchimokuLaggingSpan { get; set; }

        //ADX
        public decimal ADX { get; set; }

        //Parabolic SAR
        public decimal ParabolicSAR { get; set; }

        //Stochastic Oscillator
        public decimal StochasticOscillator { get; set; }
        public decimal StochasticOscillatorD { get; set; }
        public decimal StochasticOscillatorJ { get; set; }

        //On Balance Volume
        public decimal OnBalanceVolume { get; set; }

    }
}
