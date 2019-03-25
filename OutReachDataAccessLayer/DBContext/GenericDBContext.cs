using OutReachDataAccessLayer.Models;
using System.Data.Entity;

namespace OutReachDataAccessLayer.DBContext
{
    public class GenericDBContext : DbContext
    {
        public GenericDBContext() : base("name=FeedBackManagement")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GenericDBContext, OutReachDataAccessLayer.Migrations.Configuration>());
        }

        public DbSet<RoleMenu> RoleMenus { get; set; }
        public DbSet<SubMenu> SubMenus { get; set; }
        public DbSet<MainMenu> MainMenus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ExceptionLogger> ExceptionLoggers { get; set; }
        public DbSet<NotAttendedVolunteer> NotAttendedVolunteers { get; set; }
        public DbSet<RegisteredVolunteer> RegisteredVolunteers { get; set; }
        public DbSet<UnRegisteredVolunteer> UnRegisteredVolunteers { get; set; }
        public DbSet<FeedbackQuestion> FeedbackQuestions { get; set; }
        public DbSet<NotAttendedVolunteerFeedback> NotAttendedVolunteerFeedbacks { get; set; }
        public DbSet<RegisteredVolunteerFeedback> RegisteredVolunteerFeedbacks { get; set; }
        public DbSet<UnRegisteredVolunteerFeedback> UnRegisteredVolunteerFeedbacks { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
