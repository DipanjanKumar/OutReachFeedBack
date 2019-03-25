using OutReachDataAccessLayer.Generic_Repository;
using OutReachDataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;
namespace OutReachDataAccessLayer.Repository
{
    public class RoleMenuRepository
    {
        private readonly IGenericRepository<RoleMenu> RoleMenuRepo = null;
        public RoleMenuRepository()
        {
            RoleMenuRepo = new GenericRepository<RoleMenu>();
        }
        public List<RoleMenu> GetRoleMenyList()
        {
            return RoleMenuRepo.SelectAll().ToList();
        }
        public List<RoleMenu> GetMenuListByRoleId(int RoleID)
        {
            return RoleMenuRepo.SelectAll().Where(rl => rl.RoleID.Equals(RoleID)).ToList();
        }
    }
}
