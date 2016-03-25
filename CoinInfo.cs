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
            /***    Sample delimited JSON object received.
                    Some manual string manipulation saves depending 
                    on the JSON dll for such a small operation.

            {"symbol":"eth"
            "position":"2"
            "name":"Ethereum"
            "market_cap":{"usd":849802766.804
            "eur":760264148.0824634
            "cny":5535045855.107497
            "gbp":600443441.3351687
            "cad":1123265897.95046
            "rub":58288226715.916405
            "hkd":6592706979.460689
            "jpy":95827330006.2424
            "aud":1129121039.0137396
            "btc":2039292.94104}
            "price":{"usd":10.8362
            "eur":9.6944546232
            "cny":70.579746546
            "gbp":7.6565121616
            "cad":14.323245815199998
            "rub":743.25820886
            "hkd":84.06643772119999
            "jpy":1221.9354348762001
            "aud":14.3979072332
            "btc":0.0260039}
            "supply":"78422580"
            "volume":{"usd":22363900
            "eur":20007550.040400002
            "cny":145663460.787
            "gbp":15801616.0952
            "cad":29560513.5644
            "rub":1533946610.17
            "hkd":173497481.2714
            "jpy":2521847314.7439003
            "aud":29714600.8354
            "btc":53667.5}
            "change":"-8.05"
            "timestamp":"1458876324.147"}

            ***/
            /* TODO: Include support for the new currencies offered by the coinmarketcap API */


            string[] splitJson = json.Split(',');


            string str;
            str = splitJson[13].TrimStart("\"usd\": ".ToCharArray()).TrimEnd("\",".ToCharArray());
            str = CleanString(str);
            PriceUSD = Convert.ToDecimal(str);

            str = splitJson[14].TrimStart("\"eur\": ".ToCharArray()).TrimEnd("\",".ToCharArray());
            str = CleanString(str);
            PriceEUR = Convert.ToDecimal(str);
      
            str = splitJson[15].TrimStart("\"cny\": ".ToCharArray()).TrimEnd("\",".ToCharArray());
            str = CleanString(str);
            PriceCNY = Convert.ToDecimal(str);

            str = splitJson[17].TrimStart("\"cad\": ".ToCharArray()).TrimEnd("\",".ToCharArray());
            str = CleanString(str);
            PriceCAD = Convert.ToDecimal(str);

            str = splitJson[18].TrimStart("\"rub\": ".ToCharArray()).TrimEnd("\",".ToCharArray());
            str = CleanString(str);
            PriceRUB = Convert.ToDecimal(str);

            str = splitJson[22].TrimStart("\"btc\": ".ToCharArray()).TrimEnd("\",".ToCharArray());
            str = CleanString(str);
            PriceBTC = Convert.ToDecimal(str);

        }


        private string CleanString(string dirtyString)
        {
            string cleanString = String.Empty;

            foreach (char s in dirtyString)
            {
                if (Char.IsDigit(s) || s.Equals('.'))
                    cleanString += s;
            }

            return cleanString;

        }






    }
}
