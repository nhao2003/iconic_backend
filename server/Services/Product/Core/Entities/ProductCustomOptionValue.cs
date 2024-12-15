namespace Core.Entities
{
    public class ProductCustomOptionValue : BaseEntity
    {
        public decimal? ExtraPrice { get; set; }

        public int? SortOrder { get; set; }

        public required string Value { get; set; }

        public required ProductCustomOption ProductCustomOption { get; set; }
    }
}
