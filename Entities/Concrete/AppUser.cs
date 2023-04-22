using Microsoft.AspNetCore.Identity;

namespace Entities.Concrete
{
	public class AppUser : IdentityUser<long>
	{
		public virtual Wallet Wallet { get; set; }
		public string City { get; set; }
	}
}
