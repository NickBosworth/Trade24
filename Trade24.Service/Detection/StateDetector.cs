using Trade24.Service.Data;

namespace Trade24.Service.Detection
{
    public static class StateDetector
    {
        //Handle EMA12 Events.
        public static void UpdateState(List<RichDailyStockData> data, List<DailyStockIndicatorState> dailyStates)
        {
            if (!data.Any(d => d.Date >= new DateTime(2023, 6, 1)))
            {
                return;
            }


            var firstItem = data.OrderBy(d => d.Date).First();

            Parallel.ForEach(data.Where(d => d.Date < new DateTime(2023, 1, 1)), item =>
            {
                if (item == data.First())
                {
                    return;
                }

                var index = data.IndexOf(item);
                var previousItem = data[data.IndexOf(item) - 1];

                var state = dailyStates.First(d => d.Symbol == item.Symbol && d.Date == item.Date);


                //ADX
                //Neutral
                state.NeutralAdxWeak_20_25 = item.ADX >= 20 && item.ADX < 25 ? true : false;
                state.NeutralAdxModerate_25_30 = item.ADX >= 25 && item.ADX < 30 ? true : false;
                state.NeutralAdxStrong_30_40 = item.ADX >= 30 && item.ADX < 40 ? true : false;
                state.NeutralAdxVeryStrong_40_50 = item.ADX >= 40 && item.ADX < 50 ? true : false;
                state.NeutralAdxExtremelyStrong_50_N = item.ADX >= 50 ? true : false;

                //SMA
                //Bullish
                state.BullishAbove50Sma = item.Close >= item.SMA50 ? true : false;
                state.BullishAbove200Sma = item.Close >= item.SMA200 ? true : false;
                state.BullishAbove20Sma = item.Close >= item.SMA20 ? true : false;
                state.BullishAbove100Sma = item.Close >= item.SMA100 ? true : false;
                state.BullishAbove5Sma = item.Close >= item.SMA5 ? true : false;
                state.BullishAbove9Sma = item.Close >= item.SMA9 ? true : false;
                state.BullishAbove21Sma = item.Close >= item.SMA21 ? true : false;
                state.BullishAbove100Sma = item.Close >= item.SMA100 ? true : false;

                //Bearish
                state.BearishBelow50Sma = item.Close < item.SMA50 ? true : false;
                state.BearishBelow200Sma = item.Close < item.SMA200 ? true : false;
                state.BearishBelow20Sma = item.Close < item.SMA20 ? true : false;
                state.BearishBelow100Sma = item.Close < item.SMA100 ? true : false;
                state.BearishBelow5Sma = item.Close < item.SMA5 ? true : false;
                state.BearishBelow9Sma = item.Close < item.SMA9 ? true : false;
                state.BearishBelow21Sma = item.Close < item.SMA21 ? true : false;
                state.BearishBelow100Sma = item.Close < item.SMA100 ? true : false;

                //EMA 
                //Bullish
                state.BullishAbove12Ema = item.Close >= item.EMA12 ? true : false;
                state.BullishAbove26Ema = item.Close >= item.EMA26 ? true : false;
                state.BullishAbove50Ema = item.Close >= item.EMA50 ? true : false;
                state.BullishAbove100Ema = item.Close >= item.EMA100 ? true : false;
                state.BullishAbove200Ema = item.Close >= item.EMA200 ? true : false;
                state.BullishAbove9Ema = item.Close >= item.EMA9 ? true : false;
                state.BullishAbove21Ema = item.Close >= item.EMA21 ? true : false;
                state.BullishAbove8Ema = item.Close >= item.EMA8 ? true : false;
                state.BullishAbove5Ema = item.Close >= item.EMA5 ? true : false;
                state.BullishAbove10Ema = item.Close >= item.EMA10 ? true : false;

                //Bearish
                state.BearishBelow12Ema = item.Close < item.EMA12 ? true : false;
                state.BearishBelow26Ema = item.Close < item.EMA26 ? true : false;
                state.BearishBelow50Ema = item.Close < item.EMA50 ? true : false;
                state.BearishBelow100Ema = item.Close < item.EMA100 ? true : false;
                state.BearishBelow200Ema = item.Close < item.EMA200 ? true : false;
                state.BearishBelow9Ema = item.Close < item.EMA9 ? true : false;
                state.BearishBelow21Ema = item.Close < item.EMA21 ? true : false;
                state.BearishBelow8Ema = item.Close < item.EMA8 ? true : false;
                state.BearishBelow5Ema = item.Close < item.EMA5 ? true : false;
                state.BearishBelow10Ema = item.Close < item.EMA10 ? true : false;

                //RSI
                //Bullish
                state.BullishRsi14Oversold = item.RSI14 <= 30 ? true : false;
                state.BullishRsiBullishDivergence = item.Low < previousItem.Low && item.RSI14 > previousItem.RSI14 ? true : false;

                //Bearish
                state.BearishRsi14Overbought = item.RSI14 >= 70 ? true : false;
                state.BearishRsi14Overbought = item.High > previousItem.High && item.RSI14 < previousItem.RSI14 ? true : false;

                //MACD
                //Bullish
                state.BullishMacdBullishCrossover = item.MACD > item.MACDSignal ? true : false;
                state.BullishMacdBullishHistogram = item.MACDHistogram > 0 && item.MACDHistogram > previousItem.MACDHistogram ? true : false;

                //Bearish
                state.BearishMacdBearishCrossover = item.MACD < item.MACDSignal ? true : false;
                state.BearishMacdBearishHistogram = item.MACDHistogram < 0 && item.MACDHistogram < previousItem.MACDHistogram ? true : false;


                //Ichimoku cloud
                // Bullish
                state.BullishAboveConversionLine = item.Close >= item.IchimokuConversionLine ? true : false;
                state.BullishAboveBaseLine = item.Close >= item.IchimokuBaseLine ? true : false;
                state.BullishAboveLeadingSpanA = item.Close >= item.IchimokuLeadingSpanA ? true : false;
                state.BullishAboveLeadingSpanB = item.Close >= item.IchimokuLeadingSpanB ? true : false;

                // Bearish
                state.BearishBelowConversionLine = item.Close < item.IchimokuConversionLine ? true : false;
                state.BearishBelowBaseLine = item.Close < item.IchimokuBaseLine ? true : false;
                state.BearishBelowLeadingSpanA = item.Close < item.IchimokuLeadingSpanA ? true : false;
                state.BearishBelowLeadingSpanB = item.Close < item.IchimokuLeadingSpanB ? true : false;

                // Parabolic SAR
                // Bullish
                state.BullishAboveParabolicSAR = item.Close > item.ParabolicSAR ? true : false;

                // Bearish
                state.BearishBelowParabolicSAR = item.Close <= item.ParabolicSAR ? true : false;

                // Stochastic Oscillator
                // Bullish
                state.BullishAboveStochasticOscillator = item.StochasticOscillator > item.StochasticOscillatorD ? true : false;
                state.BullishStochasticOscillatorBullishCross = item.StochasticOscillator > item.StochasticOscillatorD && previousItem.StochasticOscillator <= previousItem.StochasticOscillatorD ? true : false;

                // Bearish
                state.BearishBelowStochasticOscillator = item.StochasticOscillator < item.StochasticOscillatorD ? true : false;
                state.BearishStochasticOscillatorBearishCross = item.StochasticOscillator < item.StochasticOscillatorD && previousItem.StochasticOscillator >= previousItem.StochasticOscillatorD ? true : false;

                // On-Balance Volume (OBV)
                // Bullish
                state.BullishOBVIncreasing = item.OnBalanceVolume > previousItem.OnBalanceVolume ? true : false;

                // Bearish
                state.BearishOBVDecreasing = item.OnBalanceVolume < previousItem.OnBalanceVolume ? true : false;


                if (item.Close == 0)
                {
                    state.ValidForTraining = false;
                }
                else
                {
                    state.ValidForTraining = true;
                    //Future closing price factors.
                    state.WeekChange = data.OrderBy(d => d.Date).FirstOrDefault(d => d.Date >= item.Date.AddDays(7)).Close / item.Close;
                    state.MonthChange = data.OrderBy(d => d.Date).FirstOrDefault(d => d.Date >= item.Date.AddDays(30)).Close / item.Close;
                    state.QuarterChange = data.OrderBy(d => d.Date).FirstOrDefault(d => d.Date >= item.Date.AddDays(90)).Close / item.Close;
                    state.HalfYearChange = data.OrderBy(d => d.Date).FirstOrDefault(d => d.Date >= item.Date.AddDays(180)).Close / item.Close;

                }




            });
        }


    }
}
