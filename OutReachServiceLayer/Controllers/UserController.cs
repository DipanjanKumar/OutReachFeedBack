using OutReachBusinessLayer.Users;
using OutReachDTO.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OutReachServiceLayer.Controllers
{
    /// <summary>
    /// This API contains method related to Users and roles.
    /// </summary>
    [RoutePrefix("api/UserAndRole")]
    public class UserController : ApiController
    {
        UserAndRole userAndRole = null;
        /// <summary>
        /// 
        /// </summary>
        public UserController()
        {
            userAndRole = new UserAndRole();
        }
        /// <summary>
        /// Get user details of specific employee id
        /// </summary>
        /// <param name="AssociateID"></param>
        /// <returns></returns>
        [Route("GetUserDetails/{AssociateID}")]
        public HttpResponseMessage Get(string AssociateID)
        {
            try
            {
                List<UserDTO> userList = userAndRole.GetUserListByAssociateID(AssociateID);
                return Request.CreateResponse(HttpStatusCode.OK, userList);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        /// <summary>
        /// Get menu list of specific employee id
        /// </summary>
        /// <param name="AssociateID"></param>
        /// <returns></returns>
        [Route("GetUserMenu/{AssociateID}")]
        public HttpResponseMessage GetMenu(string AssociateID)
        {
            try
            {
                List<UserDTO> userList = userAndRole.GetUserListByAssociateID(AssociateID);
                List<UserMenuDTO> menuitemList = new List<UserMenuDTO>();
                if (userList != null && userList.Count > 0)
                {
                    menuitemList = userAndRole.GetUserMenu(userList[0].RoleID);
                }
                return Request.CreateResponse(HttpStatusCode.OK, menuitemList);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AssociateID"></param>
        /// <returns></returns>
        [Route("GetDashboardInfo/{AssociateID}")]
        public HttpResponseMessage Getdashboard(string AssociateID)
        {
            try
            {
                DashboardDTO dashboardDTO = userAndRole.GetDashboardList(AssociateID);
                return Request.CreateResponse(HttpStatusCode.OK, dashboardDTO);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
