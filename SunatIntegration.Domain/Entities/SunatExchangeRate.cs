
namespace SunatIntegration.Domain.Entities
{
    public class SunatExchangeRate
    {
        public int Id { get; set; }
        public DateTime DatePublic { get; set; }
        public decimal? PriceSales { get; set; }
        public decimal? Pricepurchase { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public DateTime ModifiedDate { get; set; }= DateTime.Now;

    }
}
