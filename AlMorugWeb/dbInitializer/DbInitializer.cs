using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using AlMorugWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace AlMorugWeb.dbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;
        public DbInitializer(
            UserManager<IdentityUser> userManager,
            ApplicationDbContext db
            )
        {
            _userManager = userManager;
            _db= db;
        }



        public async void Initialize()
        {
            // Migrations If They Are Not Applied.
            try{
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }

            }catch( Exception ex)
            {

            }


            //Create Admin user.
            if (await _userManager.FindByEmailAsync("admin@gmail.com") == null)
            { 
                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed= true,

                }, "Admin@123").GetAwaiter().GetResult();



            }
            IdentityUser user;
            user = await _userManager.FindByEmailAsync("admin@gmail.com");
            return;

        }
    }
}
