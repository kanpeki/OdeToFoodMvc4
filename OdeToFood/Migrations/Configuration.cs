namespace OdeToFood.Migrations
{
	using Models;
	using System.Collections.Generic;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using System.Web.Security;
	using WebMatrix.WebData;

	internal sealed class Configuration : DbMigrationsConfiguration<OdeToFood.Models.OdeToFoodDb>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(OdeToFood.Models.OdeToFoodDb context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
			context.Restaurants.AddOrUpdate(r => r.Name,
				new Restaurant { Name = "Sabatino's", City = "Baltimore", Country = "USA" },
				new Restaurant { Name = "Great Lake", City = "Chicago", Country = "USA" },
				new Restaurant
				{
					Name = "Smaka",
					City = "Gothenburg",
					Country = "Sweden",
					Reviews =
						new List<RestaurantReview> {
							new RestaurantReview { Rating = 9, Body="Great food!", ReviewerName="Scott" }
						}
				});

			SeedMembership();
		}

		private void SeedMembership()
		{
			if (!WebSecurity.Initialized)
			{
				WebSecurity.InitializeDatabaseConnection("DefaultConnection",
					"UserProfile", "UserId", "UserName", autoCreateTables: true);
			}

			var roles = (SimpleRoleProvider)Roles.Provider;
			var membership = (SimpleMembershipProvider)Membership.Provider;

			if (!roles.RoleExists("Admin"))
			{
				roles.CreateRole("Admin");
			}
			if (membership.GetUser("sallen", false) == null)
			{
				membership.CreateUserAndAccount("sallen", "imalittleteapot");
			}
			if (!roles.GetRolesForUser("sallen").Contains("Admin"))
			{
				roles.AddUsersToRoles(new[] { "sallen" }, new[] { "admin" });
			}
		}
	}
}