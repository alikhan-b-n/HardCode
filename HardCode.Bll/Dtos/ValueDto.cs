using HardCode.Dal.AdditionalModels;
using HardCode.Dal.Entites;

namespace HardCode.Bll.Dtos;

public class ValueDto
{
    public string Value { get; set; }
    public TypeEnum Type { get; set; }
    public PropertyEntity PropertyEntity { get; set; }
    public Guid PropertyId { get; set; }
    public ProductDto ProductDto { get; set; }
    public Guid ProductId { get; set; }
}