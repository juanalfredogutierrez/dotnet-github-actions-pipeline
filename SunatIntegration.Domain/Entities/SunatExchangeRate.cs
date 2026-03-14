namespace SunatIntegration.Domain.Entities
{
    public class SunatExchangeRate: BaseEntity
    {
        public int Id { get; set; }
        public DateTime DatePublic { get; set; }
        public decimal? PriceSales { get; set; }
        public decimal? Pricepurchase { get; set; }

    }
}
