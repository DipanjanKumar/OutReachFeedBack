using OutReachDataAccessLayer.Generic_Repository;
using OutReachDataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace OutReachDataAccessLayer.Repository
{
   public class UserRepository
    {
        private readonly IGenericRepository<User> IUserRepository = null;
        public UserRepository()
        {
            IUserRepository = new GenericRepository<User>();
        }
        public void AddUser(User user)
        {
            IUserRepository.Insert(user);
            IUserRepository.Save();
        }
        public List<User> GetUserListByAssociateID(string AssociateID)
        {
            return IUserRepository.SelectAll().Where(rl => rl.AssociateID.Equals(AssociateID)).ToList();
        }
    }
}
