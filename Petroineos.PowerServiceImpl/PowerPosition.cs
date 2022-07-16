using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petroineos.PowerServiceImpl
{
    public class PositionProvider : IPostionProvider
    {
        private IPowerService _powerService;

        public PositionProvider(IPowerService powerService)
        {
            _powerService = powerService;
        }

        private IEnumerable<Position> AggregatePowerTrades(IEnumerable<PowerTrade> powerTrades)
        {
            return powerTrades.SelectMany((t => t.Periods), (t, period) => new { period.Period, period.Volume })
                    .GroupBy((pv) => pv.Period,
                        pv => pv.Volume,
                        (period, volumes) => new Position(period, volumes.Sum())
                        );
        }
        public IEnumerable<Position> GetPosition(IDateProvider dateProvider)
        {
            return AggregatePowerTrades(_powerService.GetTrades(dateProvider.GetDate()));
        }

        public async Task<IEnumerable<Position>> GetPositionAsync(IDateProvider dateProvider)
        {
            var powerTrades = await _powerService.GetTradesAsync(dateProvider.GetDate());

            return await Task.FromResult(AggregatePowerTrades(powerTrades));
        }
    }
}
