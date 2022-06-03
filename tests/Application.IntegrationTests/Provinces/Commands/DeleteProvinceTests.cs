using CleanArchitecture.Application.Provinces.Commands.CreateProvince;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Application.IntegrationTests.Provinces.Commands;
using static Testing;

public class DeleteProvinceTests : TestBase
{
    
    [Test]
    public async Task ShouldDeleteProvince()
    {
        var userId = await RunAsDefaultUserAsync();

        var res = await SendAsync(new CreateProvinceCommand
        {
            Name = "TEST DELETE",
            AliasName = "VN.TEST-DELETE"
        });
        await SendAsync(new DeleteProvinceCommand
        {
            Id = res.Data
        });

        var item = await FindAsync<Province>(res.Data);

        item.Should().BeNull();
    }
}
