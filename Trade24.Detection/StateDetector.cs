using Trade24.Common.Data;

namespace Trade24.Detection
{
    public static class StateDetector
    {
        //Handle EMA12 Events.
        public static void UpdateState(List<RichDailyStockData> data, List<DailyBullishStockIndicatorState> dailyBullishStates, List<DailyNeutralStockIndicatorState> dailyNeutralStates, List<DailyBearishStockIndicatorState> dailyBearishStates)
        {

            foreach (var item in data)
            {
                if (item == data.First())
                {
                    continue;
                }

                var previousItem = data[data.IndexOf(item) - 1];

                var bullishState = dailyBullishStates.First(d => d.Symbol == item.Symbol && d.Date == item.Date);
                var neutralState = dailyNeutralStates.First(d => d.Symbol == item.Symbol && d.Date == item.Date);
                var bearishState = dailyBearishStates.First(d => d.Symbol == item.Symbol && d.Date == item.Date);

                //ADX
                //Neutral
                neutralState.AdxWeak_20_25 = item.ADX >= 20 && item.ADX < 25 ? true : false;
                neutralState.AdxModerate_25_30 = item.ADX >= 25 && item.ADX < 30 ? true : false;
                neutralState.AdxStrong_30_40 = item.ADX >= 30 && item.ADX < 40 ? true : false;
                neutralState.AdxVeryStrong_40_50 = item.ADX >= 40 && item.ADX < 50 ? true : false;
                neutralState.AdxExtremelyStrong_50_N = item.ADX >= 50 ? true : false;

                //SMA
                //Bullish
                bullishState.Above50Sma = item.Close >= item.SMA50 ? true : false;
                bullishState.Above200Sma = item.Close >= item.SMA200 ? true : false;
                bullishState.Above20Sma = item.Close >= item.SMA20 ? true : false;
                bullishState.Above100Sma = item.Close >= item.SMA100 ? true : false;
                bullishState.Above5Sma = item.Close >= item.SMA5 ? true : false;
                bullishState.Above9Sma = item.Close >= item.SMA9 ? true : false;
                bullishState.Above21Sma = item.Close >= item.SMA21 ? true : false;
                bullishState.Above100Sma = item.Close >= item.SMA100 ? true : false;

                //Bearish
                bearishState.Below50Sma = item.Close < item.SMA50 ? true : false;
                bearishState.Below200Sma = item.Close < item.SMA200 ? true : false;
                bearishState.Below20Sma = item.Close < item.SMA20 ? true : false;
                bearishState.Below100Sma = item.Close < item.SMA100 ? true : false;
                bearishState.Below5Sma = item.Close < item.SMA5 ? true : false;
                bearishState.Below9Sma = item.Close < item.SMA9 ? true : false;
                bearishState.Below21Sma = item.Close < item.SMA21 ? true : false;
                bearishState.Below100Sma = item.Close < item.SMA100 ? true : false;

                //EMA 
                //Bullish
                bullishState.Above12Ema = item.Close >= item.EMA12 ? true : false;
                bullishState.Above26Ema = item.Close >= item.EMA26 ? true : false;
                bullishState.Above50Ema = item.Close >= item.EMA50 ? true : false;
                bullishState.Above100Ema = item.Close >= item.EMA100 ? true : false;
                bullishState.Above200Ema = item.Close >= item.EMA200 ? true : false;
                bullishState.Above9Ema = item.Close >= item.EMA9 ? true : false;
                bullishState.Above21Ema = item.Close >= item.EMA21 ? true : false;
                bullishState.Above8Ema = item.Close >= item.EMA8 ? true : false;
                bullishState.Above5Ema = item.Close >= item.EMA5 ? true : false;
                bullishState.Above10Ema = item.Close >= item.EMA10 ? true : false;

                //Bearish
                bearishState.Below12Ema = item.Close < item.EMA12 ? true : false;
                bearishState.Below26Ema = item.Close < item.EMA26 ? true : false;
                bearishState.Below50Ema = item.Close < item.EMA50 ? true : false;
                bearishState.Below100Ema = item.Close < item.EMA100 ? true : false;
                bearishState.Below200Ema = item.Close < item.EMA200 ? true : false;
                bearishState.Below9Ema = item.Close < item.EMA9 ? true : false;
                bearishState.Below21Ema = item.Close < item.EMA21 ? true : false;
                bearishState.Below8Ema = item.Close < item.EMA8 ? true : false;
                bearishState.Below5Ema = item.Close < item.EMA5 ? true : false;
                bearishState.Below10Ema = item.Close < item.EMA10 ? true : false;

                //RSI
                //Bullish
                bullishState.Rsi14Oversold = item.RSI14 <= 30 ? true : false;
                bullishState.RsiBullishDivergence = (item.Low < previousItem.Low) && (item.RSI14 > previousItem.RSI14) ? true : false;

                //Bearish
                bearishState.Rsi14Overbought = item.RSI14 >= 70 ? true : false;
                bearishState.Rsi14Overbought = (item.High > previousItem.High) && (item.RSI14 < previousItem.RSI14) ? true : false;

                //MACD
                //Bullish
                bullishState.MacdBullishCrossover = item.MACD > item.MACDSignal ? true : false;
                bullishState.MacdBullishHistogram = (item.MACDHistogram > 0 && item.MACDHistogram > previousItem.MACDHistogram) ? true : false;

                //Bearish
                bearishState.MacdBearishCrossover = item.MACD < item.MACDSignal ? true : false;
                bearishState.MacdBearishHistogram = (item.MACDHistogram < 0 && item.MACDHistogram < previousItem.MACDHistogram) ? true : false;


                //Ichimoku cloud
                // Bullish
                bullishState.AboveConversionLine = item.Close >= item.IchimokuConversionLine ? true : false;
                bullishState.AboveBaseLine = item.Close >= item.IchimokuBaseLine ? true : false;
                bullishState.AboveLeadingSpanA = item.Close >= item.IchimokuLeadingSpanA ? true : false;
                bullishState.AboveLeadingSpanB = item.Close >= item.IchimokuLeadingSpanB ? true : false;

                // Bearish
                bearishState.BelowConversionLine = item.Close < item.IchimokuConversionLine ? true : false;
                bearishState.BelowBaseLine = item.Close < item.IchimokuBaseLine ? true : false;
                bearishState.BelowLeadingSpanA = item.Close < item.IchimokuLeadingSpanA ? true : false;
                bearishState.BelowLeadingSpanB = item.Close < item.IchimokuLeadingSpanB ? true : false;

                // Parabolic SAR
                // Bullish
                bullishState.AboveParabolicSAR = item.Close > item.ParabolicSAR ? true : false;

                // Bearish
                bearishState.BelowParabolicSAR = item.Close <= item.ParabolicSAR ? true : false;

                // Stochastic Oscillator
                // Bullish
                bullishState.AboveStochasticOscillator = item.StochasticOscillator > item.StochasticOscillatorD ? true : false;
                bullishState.StochasticOscillatorBullishCross = (item.StochasticOscillator > item.StochasticOscillatorD) && (previousItem.StochasticOscillator <= previousItem.StochasticOscillatorD) ? true : false;

                // Bearish
                bearishState.BelowStochasticOscillator = item.StochasticOscillator < item.StochasticOscillatorD ? true : false;
                bearishState.StochasticOscillatorBearishCross = (item.StochasticOscillator < item.StochasticOscillatorD) && (previousItem.StochasticOscillator >= previousItem.StochasticOscillatorD) ? true : false;

                // On-Balance Volume (OBV)
                // Bullish
                bullishState.OBVIncreasing = item.OnBalanceVolume > previousItem.OnBalanceVolume ? true : false;

                // Bearish
                bearishState.OBVDecreasing = item.OnBalanceVolume < previousItem.OnBalanceVolume ? true : false;



            }
        }


    }
}
