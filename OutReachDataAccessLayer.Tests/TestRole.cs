using NUnit.Framework;
using OutReachDataAccessLayer.Models;
using OutReachDataAccessLayer.Repository;

namespace OutReachDataAccessLayer.Tests
{
    [TestFixture]
    public class TestRole
    {
        RoleRepository roleRepository = null;
        public TestRole()
        {
            roleRepository = new RoleRepository();
        }
        [Test]
        public void FindRoleName()
        {
            Role role = roleRepository.FindRoleName(3);
            Assert.That(role.RoleName.Equals("POC"));
        }
        [Test]
        public void FindRoleID()
        {
            Role role = roleRepository.FindRoleId("PMO");
            Assert.That(role.RoleID.Equals(2));
        }
    }
}
