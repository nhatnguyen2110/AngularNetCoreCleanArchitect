﻿using AutoMapper;
using OpenWeatherMapAPI.ApiClient;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface ICommonService
{
    IMapper Mapper { get; }
    IDateTime DateTimeService { get; }
    IDomainEventService DomainEventService { get; }
    IWebsiteSettingsService WebsiteSettingsService { get; }
    IApplicationDbContext ApplicationDBContext { get; }
    IOpenWeatherMapClient OpenWeatherMapClient { get; }
}
