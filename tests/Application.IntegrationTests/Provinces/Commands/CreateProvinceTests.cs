using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Provinces.Commands.CreateProvince;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Application.IntegrationTests.Provinces.Commands;
using static Testing;

public class CreateProvinceTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateProvinceCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
    [Test]
    public async Task ShouldCreateProvince()
    {
        //var userId = await RunAsDefaultUserAsync();

        var res = await SendAsync(new CreateProvinceCommand
        {
            Name = "Ho Chi Minh",
            AliasName = "VN.HCM"
        });

        var item = await FindAsync<Province>(res.Data);

        item.Should().NotBeNull();
        //item!.CreatedBy.Should().Be(userId);
        item?.LastModifiedBy.Should().BeNull();
        item?.LastModified.Should().BeNull();
    }
}
