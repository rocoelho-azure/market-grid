

namespace RabbIT.MarketGrid.Core.Model
{
    public class Stock
    {

        public Stock(string code, float price, StatusStock status)
        {
            Code = code;
            Price = price;
            Status = status;
        }
        public string Code
        {
            get;
            set;
        } = string.Empty;

        public float Price
        {
            get;
            set;
        }

        public StatusStock Status
        {
            get;
            set;
        }
    }
}
