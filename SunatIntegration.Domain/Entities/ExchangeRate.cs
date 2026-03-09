
namespace SunatIntegration.Domain.Entities
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public DateTime DatePublic { get; set; }
        public double? PriceSales { get; set; }
        public double? Pricepurchase { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
    }
}
