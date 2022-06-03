using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Common.Handlers;

public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected readonly ICommonService _commonService;
    public BaseHandler(ICommonService commonService)
    {
        _commonService = commonService;
    }
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
