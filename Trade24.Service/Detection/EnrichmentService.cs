using Skender.Stock.Indicators;
using Trade24.Service.Data;

namespace Trade24.Service.Detection
{
    public static class EnrichmentService
    {
        public static List<RichDailyStockData> Enrich(List<DailyStockData> data)
        {
            //Convert to rich data format.
            data = data.OrderBy(d => d.Date).ToList();
            var richData = new List<RichDailyStockData>();
            data.ForEach(d => richData.Add(new RichDailyStockData(d)));
            var richDataDict = richData.ToDictionary(rd => rd.Date);


            //SMA
            Action<int, Action<dynamic, decimal>> setSmaValue = (smaValue, action) =>
            {
                var smaResults = data.GetSma(smaValue).ToList();
                foreach (var sma in smaResults)
                {
                    if (richDataDict.TryGetValue(sma.Date, out var rd))
                    {
                        action(rd, (decimal)sma.Sma.GetValueOrDefault(0));
                    }
                }
            };

            // Use the helper to set each SMA value
            setSmaValue(50, (rd, value) => rd.SMA50 = value);
            setSmaValue(200, (rd, value) => rd.SMA200 = value);
            setSmaValue(20, (rd, value) => rd.SMA20 = value);
            setSmaValue(10, (rd, value) => rd.SMA10 = value);
            setSmaValue(5, (rd, value) => rd.SMA5 = value);
            setSmaValue(9, (rd, value) => rd.SMA9 = value);
            setSmaValue(21, (rd, value) => rd.SMA21 = value);
            setSmaValue(100, (rd, value) => rd.SMA100 = value);

            // Helper action to set the EMA value
            Action<int, Action<dynamic, decimal>> setEmaValue = (emaValue, action) =>
            {
                var emaResults = data.GetEma(emaValue).ToList();
                foreach (var ema in emaResults)
                {
                    if (richDataDict.TryGetValue(ema.Date, out var rd))
                    {
                        action(rd, (decimal)ema.Ema.GetValueOrDefault(0));
                    }
                }
            };

            // Use the helper to set each EMA value
            setEmaValue(12, (rd, value) => rd.EMA12 = value);
            setEmaValue(26, (rd, value) => rd.EMA26 = value);
            setEmaValue(50, (rd, value) => rd.EMA50 = value);
            setEmaValue(100, (rd, value) => rd.EMA100 = value);
            setEmaValue(200, (rd, value) => rd.EMA200 = value);
            setEmaValue(9, (rd, value) => rd.EMA9 = value);
            setEmaValue(21, (rd, value) => rd.EMA21 = value);
            setEmaValue(8, (rd, value) => rd.EMA8 = value);
            setEmaValue(5, (rd, value) => rd.EMA5 = value);
            setEmaValue(10, (rd, value) => rd.EMA10 = value);


            // RSI
            var rsiResults = data.GetRsi(14).ToList();
            foreach (var rsi in rsiResults)
            {
                if (richDataDict.TryGetValue(rsi.Date, out var rd))
                {
                    rd.RSI14 = (decimal)rsi.Rsi.GetValueOrDefault(0);
                }
            }

            // MACD
            var macdResults = data.GetMacd(12, 26, 9).ToList();
            foreach (var macd in macdResults)
            {
                if (richDataDict.TryGetValue(macd.Date, out var rd))
                {
                    rd.MACD = (decimal)macd.Macd.GetValueOrDefault(0);
                    rd.MACDSignal = (decimal)macd.Signal.GetValueOrDefault(0);
                    rd.MACDHistogram = (decimal)macd.Histogram.GetValueOrDefault(0);
                    rd.MACDFastEMA = (decimal)macd.FastEma.GetValueOrDefault(0);
                    rd.MACDSlowEMA = (decimal)macd.SlowEma.GetValueOrDefault(0);
                }
            }

            // Ichimoku Cloud
            var ichimokuResults = data.GetIchimoku(9, 26, 52).ToList();
            foreach (var ichimoku in ichimokuResults)
            {
                if (richDataDict.TryGetValue(ichimoku.Date, out var rd))
                {
                    rd.IchimokuConversionLine = (decimal)ichimoku.TenkanSen.GetValueOrDefault(0);
                    rd.IchimokuBaseLine = (decimal)ichimoku.KijunSen.GetValueOrDefault(0);
                    rd.IchimokuLeadingSpanA = (decimal)ichimoku.SenkouSpanA.GetValueOrDefault(0);
                    rd.IchimokuLeadingSpanB = (decimal)ichimoku.SenkouSpanB.GetValueOrDefault(0);
                    rd.IchimokuLaggingSpan = (decimal)ichimoku.ChikouSpan.GetValueOrDefault(0);
                }
            }

            // Average Directional Index
            var adxResults = data.GetAdx(14).ToList();
            foreach (var adx in adxResults)
            {
                if (richDataDict.TryGetValue(adx.Date, out var rd))
                {
                    rd.ADX = (decimal)adx.Adx.GetValueOrDefault(0);
                }
            }

            // Parabolic Stop and Reverse
            var psarResult = data.GetParabolicSar(0.02, 0.2).ToList();
            foreach (var psa in psarResult)
            {
                if (richDataDict.TryGetValue(psa.Date, out var rd))
                {
                    rd.ParabolicSAR = (decimal)psa.Sar.GetValueOrDefault(0);
                }
            }

            // Stochastic Oscillator
            var stochasticResult = data.GetStoch(14, 3, 3).ToList();
            foreach (var stochastic in stochasticResult)
            {
                if (richDataDict.TryGetValue(stochastic.Date, out var rd))
                {
                    rd.StochasticOscillator = (decimal)stochastic.K.GetValueOrDefault(0);
                    rd.StochasticOscillatorD = (decimal)stochastic.D.GetValueOrDefault(0);
                    rd.StochasticOscillatorJ = (decimal)stochastic.PercentJ.GetValueOrDefault(0);
                }
            }

            // On Balance Volume
            var obvResult = data.GetObv().ToList();
            foreach (var obv in obvResult)
            {
                if (richDataDict.TryGetValue(obv.Date, out var rd))
                {
                    rd.OnBalanceVolume = (decimal)obv.Obv;
                }
            }


            //return the data.
            return richData;
        }

    }
}
