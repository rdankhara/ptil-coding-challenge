using Microsoft.Extensions.Logging;
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
        private ILogger<PositionProvider> _logger;
        public PositionProvider(IPowerService powerService, ILogger<PositionProvider> logger)
        {
            this._powerService = powerService;
            this._logger = logger;
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
            var date = dateProvider.GetDate();
            _logger.LogInformation($"GetPosition at {date}");
            return AggregatePowerTrades(_powerService.GetTrades(date));
        }

        public async Task<IEnumerable<Position>> GetPositionAsync(IDateProvider dateProvider)
        {
            var date = dateProvider.GetDate();
            _logger.LogInformation($"GetPosition at {date}");
            var powerTrades = await _powerService.GetTradesAsync(date);
            return await Task.FromResult(AggregatePowerTrades(powerTrades));
        }
    }
}
