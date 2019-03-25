using OutReachDataAccessLayer.Generic_Repository;
using OutReachDataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace OutReachDataAccessLayer.Repository
{
    public class SubMenuRepository
    {
        private readonly IGenericRepository<SubMenu> SubMenuRepo = null;
        public SubMenuRepository()
        {
            SubMenuRepo = new GenericRepository<SubMenu>();
        }
        //public List<SubMenu> GetSubMenuListByRoleId(int RoleID)
        //{
        //    return SubMenuRepo.SelectAll().Where(rl => rl.RoleID.Equals(RoleID)).ToList();
        //}
        public List<SubMenu> GetSubMenuList()
        {
            return SubMenuRepo.SelectAll().ToList();
        }
    }
}
