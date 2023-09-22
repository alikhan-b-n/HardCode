using HardCode.Bll.Dtos;

namespace HardCode.Bll.Services.Interfaces;

public interface IPropertyManager
{
    public Task Create(PropertyDto propertyDto);

    public Task Delete(Guid id);

}