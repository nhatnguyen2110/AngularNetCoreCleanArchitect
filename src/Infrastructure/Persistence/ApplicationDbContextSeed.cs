using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    //public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    //{
    //    var administratorRole = new IdentityRole("Administrator");

    //    if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
    //    {
    //        await roleManager.CreateAsync(administratorRole);
    //    }

    //    var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

    //    if (userManager.Users.All(u => u.UserName != administrator.UserName))
    //    {
    //        await userManager.CreateAsync(administrator, "Administrator1!");
    //        await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
    //    }
    //}

    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        //Add roles: Admin, Member, Guest
        //if (!context.SysRoles.Any())
        //{
        //    context.SysRoles.AddRange(
        //         new SysRole()
        //         {
        //             Name = "Administrator"
        //         },
        //        new SysRole()
        //        {
        //            Name = "Member"
        //        },
        //        new SysRole()
        //        {
        //            Name = "Guest"
        //        }
        //    );
        //}

        if (!context.Countries.Any())
        {
            context.Countries.Add(new Country
            {
                Name = "Việt Nam",
                Priority = 0,
                LanguageCode = "vi-VN",
                UserDefined1 = "Tiếng Việt",
                UserDefined2 = "flag-icon flag-icon-vn",
                UserDefined3 = "Asia/Ho_Chi_Minh",
                Created = DateTime.Now,
                Provinces =
                {
                    new Province { Name = "Bắc Giang", Longitude=106.333,Latitude=21.333,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Bắc Kạn", Longitude=105.833,Latitude=22.167,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Cao Bằng", Longitude=106,Latitude=22.667,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Hà Giang", Longitude=105,Latitude=22.75,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Lạng Sơn", Longitude=106.5,Latitude=21.75,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Phú Thọ", Longitude=105.167,Latitude=21.333,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Quảng Ninh", Longitude=107.333,Latitude=21.25,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Thái Nguyên", Longitude=105.833,Latitude=21.667,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Tuyên Quang", Longitude=105.25,Latitude=22.117,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Lào Cai", Longitude=104,Latitude=22.333,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Yên Bái", Longitude=104.667,Latitude=21.5,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Điện Biên", Longitude=103.017,Latitude=21.383,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Hòa Bình", Longitude=105.25,Latitude=20.333,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Lai Châu", Longitude=103,Latitude=22,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Sơn La", Longitude=104,Latitude=21.167,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Bắc Ninh", Longitude=106.167,Latitude=21.083,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Hà Nam", Longitude=106,Latitude=20.583,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Hải Dương", Longitude=106.333,Latitude=20.917,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Hưng Yên", Longitude=106.083,Latitude=20.833,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Nam Định", Longitude=106.25,Latitude=20.25,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Ninh Bình", Longitude=105.833,Latitude=20.25,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Thái Bình", Longitude=106.333,Latitude=20.5,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Vĩnh Phúc", Longitude=105.6,Latitude=21.3,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Hà Nội", Longitude=105.85417,Latitude=21.02833,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Hải Phòng", Longitude=106.683833,Latitude=20.865139,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Hà Tĩnh", Longitude=105.9,Latitude=18.333,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Nghệ An", Longitude=104.833,Latitude=19.333,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Quảng Bình", Longitude=106.333,Latitude=17.5,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Quảng Trị", Longitude=107,Latitude=16.75,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Thanh Hóa", Longitude=105.5,Latitude=20,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Thừa Thiên-Huế", Longitude=107.583,Latitude=16.333,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Đắk Lắk", Longitude=108.05,Latitude=12.667,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Đắk Nông", Longitude=107.7,Latitude=11.983,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Gia Lai", Longitude=108.25,Latitude=13.75,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Kon Tum", Longitude=107.917,Latitude=14.75,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Lâm Đồng", Longitude=108.433,Latitude=11.95,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Bình Định", Longitude=109,Latitude=14.167,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Bình Thuận", Longitude=108.1,Latitude=10.933,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Khánh Hòa", Longitude=109.2,Latitude=12.25,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Ninh Thuận", Longitude=108.833,Latitude=11.75,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Phú Yên", Longitude=109.167,Latitude=13.167,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Quảng Nam", Longitude=107.91667,Latitude=15.58333,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Quảng Ngãi", Longitude=108.667,Latitude=15,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Đà Nẵng", Longitude=108.20972,Latitude=16.06944,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Bà Rịa-Vũng Tàu", Longitude=107.25,Latitude=10.583,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Bình Dương", Longitude=106.667,Latitude=11.167,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Bình Phước", Longitude=106.917,Latitude=11.75,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Đồng Nai", Longitude=107.183,Latitude=11.117,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Tây Ninh", Longitude=106.167,Latitude=11.333,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Hồ Chí Minh", Longitude=106.65,Latitude=10.8,Priority=-1,Created=DateTime.Now }
                    ,new Province { Name = "An Giang", Longitude=105.167,Latitude=10.5,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Bạc Liêu", Longitude=105.75,Latitude=9.25,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Bến Tre", Longitude=106.5,Latitude=10.167,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Cà Mau", Longitude=105.083,Latitude=9.083,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Đồng Tháp", Longitude=105.667,Latitude=10.667,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Hậu Giang", Longitude=105.467,Latitude=9.783,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Kiên Giang", Longitude=105.167,Latitude=10,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Long An", Longitude=106.167,Latitude=10.667,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Sóc Trăng", Longitude=105.833,Latitude=9.667,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Tiền Giang", Longitude=106.167,Latitude=10.417,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Trà Vinh", Longitude=106.25,Latitude=9.833,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Vĩnh Long", Longitude=106,Latitude=10.167,Priority=0,Created=DateTime.Now }
                    ,new Province { Name = "Cần Thơ", Longitude=105.783,Latitude=10.033,Priority=0,Created=DateTime.Now }
                }
            });
            await context.SaveChangesAsync();
        }
    }
}
