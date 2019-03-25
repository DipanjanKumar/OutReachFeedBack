using OutReachDataAccessLayer.Generic_Repository;
using OutReachDataAccessLayer.Models;
using System.Linq;

namespace OutReachDataAccessLayer.Repository
{
    public class RoleRepository
    {
        private readonly IGenericRepository<Role> IRoleRepository = null;
        public RoleRepository()
        {
            IRoleRepository = new GenericRepository<Role>();
        }
        public Role FindRoleId(string RoleName)
        {
            return IRoleRepository.SelectAll().Where(rl => rl.RoleName.Equals(RoleName)).FirstOrDefault();
        }
        public Role FindRoleName(int RoleId)
        {
            return IRoleRepository.SelectAll().Where(rl => rl.RoleID.Equals(RoleId)).FirstOrDefault();
        }
    }
}
