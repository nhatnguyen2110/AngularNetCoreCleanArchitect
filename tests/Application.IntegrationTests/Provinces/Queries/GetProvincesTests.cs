using CleanArchitecture.Application.Provinces.Commands.CreateProvince;
using CleanArchitecture.Application.Provinces.Queries.GetProvincesWithPagination;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Application.IntegrationTests.Provinces.Queries;
using static Testing;

public class GetProvincesTests : TestBase
{
    [Test]
    public async Task ShouldReturnProvinces()
    {
        //var userId = await RunAsDefaultUserAsync();

        var res = await SendAsync(new CreateProvinceCommand
        {
            Name = "Ho Chi Minh",
            AliasName = "VN.HCM"
        });
        var query = new GetProvincesWithPaginationQuery();

        var result = await SendAsync(query);

        result.Data?.TotalPages.Should().BeGreaterThanOrEqualTo(1);
    }
}
