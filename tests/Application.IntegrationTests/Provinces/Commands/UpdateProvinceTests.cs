using CleanArchitecture.Application.Provinces.Commands.CreateProvince;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using CleanArchitecture.Application.Common.Exceptions;

namespace CleanArchitecture.Application.IntegrationTests.Provinces.Commands;
using static Testing;

public class UpdateProvinceTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateProvinceCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
    [Test]
    public async Task ShouldUpdateProvince()
    {
        var userId = await RunAsDefaultUserAsync();

        var res = await SendAsync(new CreateProvinceCommand
        {
            Name = "TEST UPDATE",
            AliasName = "VN.TEST"
        });

        var command = new UpdateProvinceCommand
        {
            Id = res.Data,
            Name = "Updated Test Name",
            AliasName = "VN.UPDATED-TEST",
            Latitude = 1,
            Longitude = 2,
            Priority = 1
        };

        await SendAsync(command);

        var item = await FindAsync<Province>(res.Data);

        item.Should().NotBeNull();
        item!.Name.Should().Be(command.Name);
        item.AliasName.Should().Be(command.AliasName);
        item.Latitude.Should().Be(command.Latitude);
        item.Longitude.Should().Be(command.Longitude);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().NotBeNull();
    }
}
