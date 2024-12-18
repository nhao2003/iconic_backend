namespace API.DTOs
{
    public class ProductCustomOptionDto
    {
        public long Id { get; set; }
        public string OptionName { get; set; }

        public string OptionType { get; set; }

        public bool IsRequired { get; set; }


        public int? SortOrder { get; set; }

        public List<ProductCustomOptionValueDto> ProductCustomOptionValues { get; set; } = [];
    }

    public class ProductCustomOptionValueDto
    {
        public long Id { get; set; }
        public decimal? ExtraPrice { get; set; }

        public int? SortOrder { get; set; }

        public string Value { get; set; }
    }

    public class CreateProductCustomOptionDto
    {
        public string OptionName { get; set; }

        public string OptionType { get; set; }

        public bool IsRequired { get; set; }


        public int? SortOrder { get; set; }

        public List<CreateProductCustomOptionValueDto> ProductCustomOptionValues { get; set; } = [];
    }

    public class CreateProductCustomOptionValueDto
    {
        public decimal? ExtraPrice { get; set; }

        public int? SortOrder { get; set; }

        public string Value { get; set; }
    }
}
