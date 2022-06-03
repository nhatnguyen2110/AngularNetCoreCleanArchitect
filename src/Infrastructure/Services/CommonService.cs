﻿using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using OpenWeatherMapAPI.ApiClient;

namespace CleanArchitecture.Infrastructure.Services;

public class CommonService : ICommonService
{
    protected readonly IMapper _mapper;
    protected readonly ICurrentUserService _currentUserService;
    protected readonly IDateTime _dateTimeService;
    protected readonly IDomainEventService _domainEventService;
    protected readonly IWebsiteSettingsService _websiteSettingsService;
    protected readonly IApplicationDbContext _applicationDbContext;
    protected readonly IOpenWeatherMapClient _openWeatherMapClient;

    public CommonService(
        IMapper mapper
        , ICurrentUserService currentUserService
        , IDateTime dateTimeService
        , IDomainEventService domainEventService
        , IWebsiteSettingsService websiteSettingsService
        , IApplicationDbContext applicationDbContext
        , IOpenWeatherMapClient openWeatherMapClient
        )
    {
        _mapper = mapper;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
        _domainEventService = domainEventService;
        _websiteSettingsService = websiteSettingsService;
        _applicationDbContext = applicationDbContext;
        _openWeatherMapClient = openWeatherMapClient;
    }

    public IMapper Mapper => _mapper;

    public ICurrentUserService CurrentUserService => _currentUserService;

    public IDateTime DateTimeService => _dateTimeService;

    public IDomainEventService DomainEventService => _domainEventService;

    public IWebsiteSettingsService WebsiteSettingsService => _websiteSettingsService;

    public IApplicationDbContext ApplicationDBContext => _applicationDbContext;

    public IOpenWeatherMapClient OpenWeatherMapClient => _openWeatherMapClient;
}
