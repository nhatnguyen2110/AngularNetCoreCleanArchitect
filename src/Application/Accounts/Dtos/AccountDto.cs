using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Accounts.Dtos;
public class AccountDto : IMapFrom<Account>
{
    public void Mapping(Profile profile)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        profile.CreateMap<Account, AccountDto>()
            .ForMember(d => d.Roles, opt => opt.MapFrom(s => s.AccountRoles.Select(x => x.Role.Name)))
            ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Gender { get; set; }
    public DateTime? BirthDay { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public bool IsFirstLogin { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}