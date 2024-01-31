namespace BackInfinity.Models.Appis
{
    public class ModelMercadoPago
    {
        private string? categoryDescriptor1;

        public string title { get; set; }
        public string? description { get; set; }
        public string? pictureUrl { get; set; }
        public string? categoryId { get; set; }
        public int quantity { get; set; }
        public int unitPrice { get; set; }
        public string? currencyId { get; set; }
        public string? categoryDescriptor { get; set; }
        public string? warranty { get; set; }
        public string? eventDate { get; set; }
    }
}
