using BitAI.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitAI.DataCrawler
{
    class Program
    {

        static void Main(string[] args)
        {
            // trigger async evaluation
            Scheduler.RunScheduler().GetAwaiter().GetResult();

            //var client = new BittrexClient("a1b69f60294f4bee9148f2575f292fbe", "d305491dd146407393ab4c4e9217495b");
            //var service = new DataCrawlingService(client);

            //GetMarketHistory(client).GetAwaiter().GetResult();
            //GetMarkets(client).GetAwaiter().GetResult();
            //var n = service.GetCurrencies().GetAwaiter().GetResult();
            //var n = service.GetMarkets().GetAwaiter().GetResult();
            //var n = service.GetMarketHistory("BTC-ADA").GetAwaiter().GetResult();

            //Console.WriteLine($"Result: {n}");
            Console.ReadLine();
        }

        static async Task GetMarketHistory(BittrexClient client)
        {
            var result = await client.GetMarketHistory("BTC-ADA")
                .ConfigureAwait(false);

            foreach (var item in result.Result)
            {
                await Console.Out.WriteLineAsync($"Id: {item.Id}, TimeStamp: {item.TimeStamp.ToString()}, Quantity: {item.Quantity}, Price: {item.Price}, Total: {item.Total}, Fill type: {item.FillType}")
                    .ConfigureAwait(false);
            }
        }

        static async Task GetMarkets(BittrexClient client)
        {
            var result = await client.GetMarkets()
                .ConfigureAwait(false);

            foreach (var item in result.Result)
            {
                await Console.Out.WriteLineAsync($"Market: {item.MarketName}, Base currency: {item.BaseCurrency}, Market currency: {item.MarketCurrency}")
                    .ConfigureAwait(false);
            }
        }

        static async Task GetCurrencies(BittrexClient client)
        {
            var result = await client.GetCurrencies()
                .ConfigureAwait(false);

            foreach (var item in result.Result)
            {
                await Console.Out.WriteLineAsync($"Currency: {item.Currency}, Currency Long: {item.CurrencyLong}, Tx fee: {item.TxFee}")
                    .ConfigureAwait(false);
            }
        }
    }
}
