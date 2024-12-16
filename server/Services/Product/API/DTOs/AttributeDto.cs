using Core.Entities;

namespace API.DTOs
{
    public class AttributeDto
    {
        public int Id { get; set; }
        public string AttributeCode { get; set; }

        public string AttributeName { get; set; }

        public string Type { get; set; }

        public bool IsRequired { get; set; } = false;

        public bool DisplayOnFrontend { get; set; } = false;

        public int SortOrder { get; set; } = 0;

        public bool IsFilterable { get; set; } = false;

        public List<AttributeOptionDto> AttributeOptions { get; set; } = [];
    }

    public class AttributeOptionDto
    {
        public int Id { get; set; }
        public string AttributeCode { get; set; }

        public string OptionText { get; set; }

        public string? Description { get; set; }
    }

    public class CreateAttributeDto
    {
        public string AttributeCode { get; set; }

        public string AttributeName { get; set; }

        public string Type { get; set; }

        public bool IsRequired { get; set; } = false;

        public bool DisplayOnFrontend { get; set; } = false;

        public int SortOrder { get; set; } = 0;

        public bool IsFilterable { get; set; } = false;

        public List<CreateAttributeOptionDto> AttributeOptions { get; set; } = [];
    }

    public class CreateAttributeOptionDto
    {
        public string OptionText { get; set; }

        public string? Description { get; set; }
    }

    public class UpdateAttributeDto
    {
        public int Id { get; set; }
        public string AttributeCode { get; set; }

        public string AttributeName { get; set; }

        public string Type { get; set; }

        public bool IsRequired { get; set; } = false;

        public bool DisplayOnFrontend { get; set; } = false;

        public int SortOrder { get; set; } = 0;

        public bool IsFilterable { get; set; } = false;

        public List<UpdateAttributeOptionDto> AttributeOptions { get; set; } = [];
    }

    public class UpdateAttributeOptionDto
    {
        public int Id { get; set; }
        public string AttributeCode { get; set; }

        public string OptionText { get; set; }

        public string? Description { get; set; }
    }
}
