namespace OutReachFeedBack.Models
{
    public class UserMenuDTO
    {
        public string MainMenuName { get; set; }
        public int MainMenuId { get; set; }
        public string SubMenuName { get; set; }
        public int SubMenuId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}