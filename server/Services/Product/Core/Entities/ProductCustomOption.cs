namespace Core.Entities
{
    public class ProductCustomOption : BaseEntity
    {
        public required string OptionName { get; set; }

        public required string OptionType { get; set; }

        public bool IsRequired { get; set; }


        public int? SortOrder { get; set; } // Nullable integer for SortOrder

        public required Product Product { get; set; }

        public List<ProductCustomOptionValue> ProductCustomOptionValues { get; set; } = [];
    }
}
