using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtheriumWatcher
{

    class CoinInfo
    {
        public enum Currency { USD, EUR, CNY, CAD, RUB, BTC };
        public decimal PriceUSD { get; private set; }
        public decimal PriceEUR { get; private set; }
        public decimal PriceCNY { get; private set; }
        public decimal PriceCAD { get; private set; }
        public decimal PriceRUB { get; private set; }
        public decimal PriceBTC { get; private set; }



        public void ParseJson(string json)
        {
/*
        {
          "symbol": "eth",
          "position": "2",
          "market_cap": {
            "usd": "303703533.731",
            "eur": "268944056.8884196",
            "cny": "1996663045.4974792",
            "cad": "421465186.3422628",
            "rub": "24078396975.978905",
            "btc": "807406.749184"
          },
          "price": {
            "usd": "3.94879",
            "eur": "3.49684308692",
            "cny": "25.960853897779998",
            "cad": "5.479941220080001",
            "rub": "313.07022353908997",
            "btc": "0.010498"
          },
          "supply": "76910530",
          "volume": {
            "usd": "18112500",
            "eur": "16039488.15",
            "cny": "119078493.975",
            "cad": "25135658.100000005",
            "rub": "1436005567.2375",
            "btc": "48152.8"
          },
          "change": "23.98",
          "timestamp": 1455063011.855
        }
*/

            string[] splitJson = json.Split('\n');

            string str;
            str = splitJson[12].TrimStart("\"usd\": ".ToCharArray()).TrimEnd("\",".ToCharArray());
            PriceUSD = Convert.ToDecimal(str);




            str = splitJson[13].TrimStart("\"eur\": ".ToCharArray()).TrimEnd("\",".ToCharArray());
            PriceEUR = Convert.ToDecimal(str);

            str = splitJson[14].TrimStart("\"cny\": ".ToCharArray()).TrimEnd("\",".ToCharArray());
            PriceCNY = Convert.ToDecimal(str);
            str = splitJson[15].TrimStart("\"cad\": ".ToCharArray()).TrimEnd("\",".ToCharArray());
            PriceCAD = Convert.ToDecimal(str);
            str = splitJson[16].TrimStart("\"rub\": ".ToCharArray()).TrimEnd("\",".ToCharArray());
            PriceRUB = Convert.ToDecimal(str);
            str = splitJson[17].TrimStart("\"btc\": ".ToCharArray()).TrimEnd("\",".ToCharArray());
            PriceBTC = Convert.ToDecimal(str);

            





            Console.WriteLine(str);

        }






    }
}
