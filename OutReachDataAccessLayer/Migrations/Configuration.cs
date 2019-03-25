namespace OutReachDataAccessLayer.Migrations
{
    using OutReachDataAccessLayer.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<OutReachDataAccessLayer.DBContext.GenericDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "OutReachDataAccessLayer.DBContext.GenericDBContext";
        }

        protected override void Seed(OutReachDataAccessLayer.DBContext.GenericDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            IList<FeedbackQuestion> defaultQuestions = new List<FeedbackQuestion>();

            defaultQuestions.Add(new FeedbackQuestion() { QuestionNumber = 1, QuestionText = "Rating" });
            defaultQuestions.Add(new FeedbackQuestion() { QuestionNumber = 2, QuestionText = "What did you like about this volunteering activity" });
            defaultQuestions.Add(new FeedbackQuestion() { QuestionNumber = 3, QuestionText = "What can be improved for this volunteering activity" });

            foreach (FeedbackQuestion feedbackQuestion in defaultQuestions)
                context.FeedbackQuestions.AddOrUpdate(feedbackQuestion);

            IList<Role> roles = new List<Role>();
            roles.Add(new Role() { RoleID = 1, RoleName = "Admin" });
            roles.Add(new Role() { RoleID = 2, RoleName = "PMO" });
            roles.Add(new Role() { RoleID = 3, RoleName = "POC" });

            foreach (Role role in roles)
                context.Roles.AddOrUpdate(role);

            IList<MainMenu> mainMenus = new List<MainMenu>();
            mainMenus.Add(new MainMenu() { MainMenuId = 1, MainMenuName = "Dashboard" });
            mainMenus.Add(new MainMenu() { MainMenuId = 2, MainMenuName = "Admin" });

            foreach (MainMenu mainMenu in mainMenus)
                context.MainMenus.AddOrUpdate(mainMenu);

            IList<SubMenu> subMenus = new List<SubMenu>();
            subMenus.Add(new SubMenu() { SubMenuId = 1, SubMenuName = "User", ControllerName = "User", ActionrName = "Index", MainMenuId = 2 });
            subMenus.Add(new SubMenu() { SubMenuId = 2, SubMenuName = "Mail Template", ControllerName = "MailTemplate", ActionrName = "Index", MainMenuId = 2 });
            subMenus.Add(new SubMenu() { SubMenuId = 3, SubMenuName = "Mail Reminder", ControllerName = "MailReminder", ActionrName = "Index", MainMenuId = 2 });
            subMenus.Add(new SubMenu() { SubMenuId = 4, SubMenuName = "Dashboard", ControllerName = "Dashboard", ActionrName = "Index", MainMenuId = 1 });

            foreach (SubMenu subMenu in subMenus)
                context.SubMenus.AddOrUpdate(subMenu);

            IList<RoleMenu> roleMenus = new List<RoleMenu>();
            roleMenus.Add(new RoleMenu() { Id = 1, SubMenuId = 1, RoleID = 1 });
            roleMenus.Add(new RoleMenu() { Id = 2, SubMenuId = 2, RoleID = 1 });
            roleMenus.Add(new RoleMenu() { Id = 3, SubMenuId = 3, RoleID = 1 });
            roleMenus.Add(new RoleMenu() { Id = 4, SubMenuId = 4, RoleID = 1 });
            roleMenus.Add(new RoleMenu() { Id = 5, SubMenuId = 4, RoleID = 2 });
            roleMenus.Add(new RoleMenu() { Id = 6, SubMenuId = 4, RoleID = 3 });

            foreach (RoleMenu roleMenu in roleMenus)
                context.RoleMenus.AddOrUpdate(roleMenu);

            base.Seed(context);
        }
    }
}
