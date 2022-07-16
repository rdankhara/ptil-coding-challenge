using Services;
using System;
using System.Collections.Generic;

namespace Petroineos.PowerServiceImpl.Tests
{
    public class FakePowerTrades
    {
        public static IEnumerable<PowerTrade> GetFakePowerTradesResponse(DateTime dateTime)
        {
            List<PowerTrade> powerTrades = new List<PowerTrade>();

            var trade1 = PowerTrade.Create(dateTime, 5);
            var trade2 = PowerTrade.Create(dateTime, 5);
            powerTrades.Add(trade1);
            powerTrades.Add(trade2);

            return powerTrades;
        }
    }
}
