using FluentAssertions;
using HardCode.Dal.Entites;
using HardCode.Dal.Repositories;
using HardCode.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HardCode.Dal.Test;

public class RepositoryTests
{
    private readonly ApplicationContext _applicationContext;
    private readonly ICrudRepository<ValueEntity> _repository;

    public RepositoryTests()
    {
        var builder = new DbContextOptionsBuilder<ApplicationContext>();
        builder.UseInMemoryDatabase(GetType().Name);

        _applicationContext = new ApplicationContext(builder.Options);
        _applicationContext.Database.EnsureDeleted();
        _applicationContext.Database.EnsureCreated();
        _repository = new Repository<ValueEntity>(_applicationContext);
    }

    [Fact]
    public async Task GetAll_ShouldGetAllEntities_PositiveTest()
    {
        // Arrange
        ValueEntity[] listToAdd =
        {
            new() { Value = "Starks" },
            new() { Value = "Lanisters" },
            new() { Value = "Martels" }
        };

        await _applicationContext.ValueEntities.AddRangeAsync(listToAdd);
        await _applicationContext.SaveChangesAsync();

        // Act
        var getAllResult = await _repository.GetAll();

        // Assert
        getAllResult.Should().BeEquivalentTo(listToAdd);
    }

    [Fact]
    public async Task GetById_ShouldGetEntityByItsId_PositiveTest()
    {
        // Arrange
        var expected = new ValueEntity() { Value = "Unsullied" };
        await _applicationContext.AddAsync(expected);
        await _applicationContext.SaveChangesAsync();

        // Act
        var actual = await _repository.GetById(expected.Id);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Create_ShouldCreateEntity_PositiveTest()
    {
        // Arrange
        var expected = new ValueEntity { Value = "Unsullied" };

        // Act
        await _repository.Create(expected);

        // Assert
        var actual = await _applicationContext.ValueEntities
            .FirstAsync(x => x.Id == expected.Id);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Update_ShouldUpdateEntity_PositiveTest()
    {
        // Arrange
        var initial = new ValueEntity() { Value = "Unsullied" };
        await _applicationContext.AddAsync(initial);
        await _applicationContext.SaveChangesAsync();

        var afterChangeExpected = await _applicationContext.ValueEntities
            .FirstAsync(x => x.Id == initial.Id);

        // Act
        afterChangeExpected.Value = "Sullied";
        await _repository.Update(afterChangeExpected);

        // Assert
        var afterChangeActual = await _applicationContext.ValueEntities
            .FirstAsync(x => x.Id == initial.Id);

        afterChangeActual.Should().BeEquivalentTo(afterChangeExpected);
    }

    [Fact]
    public async Task Delete_ShouldDeleteEntityByItsId_PositiveTest()
    {
        // Arrange
        var initial = new ValueEntity() { Value = "Unsullied" };
        await _applicationContext.AddAsync(initial);
        await _applicationContext.SaveChangesAsync();

        // Act
        await _repository.Delete(initial.Id);

        // Assert
        var ifExistAfterDelete = _applicationContext.ValueEntities
            .Any(x => x.Id == initial.Id);

        Assert.False(ifExistAfterDelete);
    }
}