using BitAI.Data;
using BitAI.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BitAI.DataService
{
    public class DataCrawlingService
    {
        private BittrexClient _client;
        private BitAIContext _dbContext;

        private IRepository<Coin> _coinRepository;
        private IRepository<Market> _marketRepository;
        private IRepository<MarketHistory> _marketHistoryRepository;

        public DataCrawlingService(BittrexClient client)
        {
            _client = client;
            _dbContext = new BitAIContext();
            _coinRepository = new GenericRepository<Coin>(_dbContext);
            _marketRepository = new GenericRepository<Market>(_dbContext);
            _marketHistoryRepository = new GenericRepository<MarketHistory>(_dbContext);

        }

        public async Task<int> GetCurrencies()
        {
            var result = await _client.GetCurrencies()
                .ConfigureAwait(false);

            foreach (var item in result.Result)
            {
                if (!_coinRepository.GetList().Any(x => x.CoinCodeName.Equals(item.Currency)))
                {
                    var coin = new Coin
                    {
                        CoinCodeName = item.Currency,
                        CoinName = item.CurrencyLong,
                        CoinType = item.CoinType,
                        TxFee = item.TxFee,
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    };
                
                    _coinRepository.Insert(coin);
                }
            }

            var numRecords = _coinRepository.Save();
            return numRecords;
        }

        public async Task<int> GetMarkets()
        {
            var result = await _client.GetMarkets()
                .ConfigureAwait(false);

            foreach (var item in result.Result)
            {
                if (!_marketRepository.GetList().Any(x => x.MarketName.Equals(item.MarketName)))
                {
                    var market = new Market
                    {
                        MarketName = item.MarketName,
                        BaseCoin = _dbContext.Coins.FirstOrDefault(x => x.CoinCodeName.Equals(item.BaseCurrency)),
                        Coin = _dbContext.Coins.FirstOrDefault(x => x.CoinCodeName.Equals(item.MarketCurrency)),
                        MinTradeSize = item.MinTradeSize,
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    };

                    _marketRepository.Insert(market);
                }
            }

            var numRecords = _marketRepository.Save();
            return numRecords;
        }

        public async Task<int> GetMarketHistory(string marketName)
        {
            var result = await _client.GetMarketHistory(marketName)
                .ConfigureAwait(false);

            var market = _marketRepository.GetList(x => x.MarketName.Equals(marketName)).FirstOrDefault();

            foreach (var item in result.Result)
            {
                if (!_marketHistoryRepository.GetList().Any(x => x.TransactionId.Equals(item.Id)))
                {
                    var marketHistory = new MarketHistory
                    {
                        TransactionId = item.Id,
                        Market = market,
                        Timestamp = item.TimeStamp,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        FillType = (short) ((item.FillType == "FILL")? FillType.Fill: FillType.PartialFill),
                        OrderType = (short)((item.FillType == "BUY") ? OrderType.Buy : OrderType.Sell),
                        CreatedDate = DateTime.Now
                    };

                    _marketHistoryRepository.Insert(marketHistory);
                }
            }

            var numRecords = _marketHistoryRepository.Save();
            return numRecords;
        }
    }
}
