using DataAccess.Context;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Seeds
{
	public class SeedService
	{
		private BestApiDbContext _db;

		public SeedService(BestApiDbContext db)
		{
			_db = db;
		}
		private static string CreatePasswordHash(AppUser user, string password)
		{
			var passwordHasher = new PasswordHasher<AppUser>();
			return passwordHasher.HashPassword(user, password);
		}
		public void SeedData()
		{
			if (!_db.Roles.Any())
			{
				var adminrole = new AppRole
				{
					Name = "Admin",
					NormalizedName = "Admin".ToUpper(CultureInfo.InvariantCulture)
				};
				var customerrole = new AppRole
				{
					Name = "Customer",
					NormalizedName = "Customer".ToUpper(CultureInfo.InvariantCulture)
				};
				_db.Roles.Add(adminrole);
				_db.Roles.Add(customerrole);
				_db.SaveChanges();
				var user = new AppUser
				{
					UserName = "firstuser",
					Email = "firstuser@gmail.com",
					EmailConfirmed = true,
					NormalizedEmail = "firstuser@gmail.com".ToUpper(CultureInfo.InvariantCulture),
					NormalizedUserName = "firstuser".ToUpper(CultureInfo.InvariantCulture),
					AccessFailedCount = 5,
					SecurityStamp=Guid.NewGuid().ToString()
				};
				user.PasswordHash = CreatePasswordHash(user, "123123");
				_db.Users.Add(user);
				_db.SaveChanges();
				_db.UserRoles.Add(new IdentityUserRole<long> { RoleId=adminrole.Id,UserId=user.Id});
				_db.SaveChanges();
			}
		}
	}
}
