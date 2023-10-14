﻿using Skender.Stock.Indicators;
using Trade24.Common.Data;

namespace Trade24.Detection
{
    public static class EnrichmentService
    {
        public static List<RichDailyStockData> Enrich(List<DailyStockData> data)
        {
            //Convert to rich data format.
            data = data.OrderBy(d => d.Date).ToList();
            var richData = new List<RichDailyStockData>();
            data.ForEach(d => richData.Add(new RichDailyStockData(d)));
            richData = richData.OrderBy(d => d.Date).ToList();

            //SMA
            var sma50Result = data.GetSma(50).ToList();
            sma50Result.ForEach(sma50 => richData.First(rd => rd.Date == sma50.Date).SMA50 = (decimal)sma50.Sma.GetValueOrDefault(0));

            var sma200Result = data.GetSma(200).ToList();
            sma200Result.ForEach(sma200 => richData.First(rd => rd.Date == sma200.Date).SMA200 = (decimal)sma200.Sma.GetValueOrDefault(0));

            var sma20Result = data.GetSma(20).ToList();
            sma20Result.ForEach(sma20 => richData.First(rd => rd.Date == sma20.Date).SMA20 = (decimal)sma20.Sma.GetValueOrDefault(0));

            var sma10Result = data.GetSma(10).ToList();
            sma10Result.ForEach(sma10 => richData.First(rd => rd.Date == sma10.Date).SMA10 = (decimal)sma10.Sma.GetValueOrDefault(0));

            var sma5Result = data.GetSma(5).ToList();
            sma5Result.ForEach(sma5 => richData.First(rd => rd.Date == sma5.Date).SMA5 = (decimal)sma5.Sma.GetValueOrDefault(0));

            var sma9Result = data.GetSma(9).ToList();
            sma9Result.ForEach(sma9 => richData.First(rd => rd.Date == sma9.Date).SMA9 = (decimal)sma9.Sma.GetValueOrDefault(0));

            var sma21Result = data.GetSma(21).ToList();
            sma21Result.ForEach(sma21 => richData.First(rd => rd.Date == sma21.Date).SMA21 = (decimal)sma21.Sma.GetValueOrDefault(0));

            var sma100Result = data.GetSma(100).ToList();
            sma100Result.ForEach(sma100 => richData.First(rd => rd.Date == sma100.Date).SMA100 = (decimal)sma100.Sma.GetValueOrDefault(0));

            //RSI
            var rsiResults = data.GetRsi(14).ToList();

            rsiResults.ForEach(rsi => richData.First(rd => rd.Date == rsi.Date).RSI14 = (decimal)rsi.Rsi.GetValueOrDefault(0));

            //MACD
            var macdResults = data.GetMacd(12, 26, 9).ToList();

            macdResults.ForEach(macd =>
            {
                richData.First(rd => rd.Date == macd.Date).MACD = (decimal)macd.Macd.GetValueOrDefault(0);
                richData.First(rd => rd.Date == macd.Date).MACDSignal = (decimal)macd.Signal.GetValueOrDefault(0);
                richData.First(rd => rd.Date == macd.Date).MACDHistogram = (decimal)macd.Histogram.GetValueOrDefault(0);
                richData.First(rd => rd.Date == macd.Date).MACDFastEMA = (decimal)macd.FastEma.GetValueOrDefault(0);
            });

            //Ichimoku Cloud
            var ichimokuResults = data.GetIchimoku(9, 26, 52).ToList();

            ichimokuResults.ForEach(ichimoku =>
            {
                richData.First(rd => rd.Date == ichimoku.Date).IchimokuConversionLine = (decimal)ichimoku.TenkanSen.GetValueOrDefault(0);
                richData.First(rd => rd.Date == ichimoku.Date).IchimokuBaseLine = (decimal)ichimoku.KijunSen.GetValueOrDefault(0);
                richData.First(rd => rd.Date == ichimoku.Date).IchimokuLeadingSpanA = (decimal)ichimoku.SenkouSpanA.GetValueOrDefault(0);
                richData.First(rd => rd.Date == ichimoku.Date).IchimokuLeadingSpanB = (decimal)ichimoku.SenkouSpanB.GetValueOrDefault(0);
                richData.First(rd => rd.Date == ichimoku.Date).IchimokuLaggingSpan = (decimal)ichimoku.ChikouSpan.GetValueOrDefault(0);
            });

            //TODO: Volume increase/decrease.

            //Average Directional Index.
            var adxResults = data.GetAdx(14).ToList();

            adxResults.ForEach(adx => richData.First(rd => rd.Date == adx.Date).ADX = (decimal)adx.Adx.GetValueOrDefault(0));

            //Parabolic Stop and Reverse.
            var psarResult = data.GetParabolicSar(0.02, 0.2).ToList();

            psarResult.ForEach(psa => richData.First(rd => rd.Date == psa.Date).ParabolicSAR = (decimal)psa.Sar.GetValueOrDefault(0));

            //Stochastic Oscillator.
            var stochasticResult = data.GetStoch(14, 3, 3).ToList();

            stochasticResult.ForEach(stochastic =>
            {
                richData.First(rd => rd.Date == stochastic.Date).StochasticOscillator = (decimal)stochastic.K.GetValueOrDefault(0);
                richData.First(rd => rd.Date == stochastic.Date).StochasticOscillatorD = (decimal)stochastic.D.GetValueOrDefault(0);
                richData.First(rd => rd.Date == stochastic.Date).StochasticOscillatorJ = (decimal)stochastic.PercentJ.GetValueOrDefault(0);
            });

            //On Balance Volume.
            var obvResult = data.GetObv().ToList();

            obvResult.ForEach(obv => richData.First(rd => rd.Date == obv.Date).OnBalanceVolume = (decimal)obv.Obv);

            //return the data.
            return richData;
        }

    }
}
