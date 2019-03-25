using OutReachDTO.DTO;
using OutReachDataAccessLayer.Models;
using OutReachDataAccessLayer.Repository;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OutReachBusinessLayer.Users
{
    public class UserAndRole
    {
        UserRepository userRepository = null;
        RoleMenuRepository RoleMenuRepository = null;
        public UserAndRole()
        {
            userRepository = new UserRepository();
            RoleMenuRepository = new RoleMenuRepository();
        }
        public List<UserDTO> GetUserListByAssociateID(string AssociateID)
        {
            try
            {
                List<UserDTO> userDTO = userRepository.GetUserListByAssociateID(AssociateID).Select(x => new UserDTO
                {
                    AssociateID = x.AssociateID,
                    AssociateName = x.AssociateName,
                    RoleID = x.RoleID,
                    EventId = x.EventId
                }).ToList();

                return userDTO;
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "UserAndRole",
                    ActionrName = "GetUserListByAssociateID()",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
                throw ex;
            }
        }
        public List<UserMenuDTO> GetUserMenu(int RoleId)
        {
            try
            {
                List<UserMenuDTO> _menus = RoleMenuRepository.GetMenuListByRoleId(RoleId).Select(x => new UserMenuDTO
                {
                    MainMenuId = x.SubMenu.MainMenu.MainMenuId,
                    MainMenuName = x.SubMenu.MainMenu.MainMenuName,
                    SubMenuId = x.Id,
                    SubMenuName = x.SubMenu.SubMenuName,
                    ControllerName = x.SubMenu.ControllerName,
                    ActionName = x.SubMenu.ActionrName,
                    RoleID = x.RoleID,
                    RoleName = x.Role.RoleName
                }).ToList();

                return _menus;
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "UserAndRole",
                    ActionrName = "GetUserMenu()",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
                throw ex;
            }
        }
        public DashboardDTO GetDashboardList(string associateID)
        {
            try
            {
                DashboardDTO dashboardDTO = new DashboardDTO();
                List<UserDTO> userDTOList = GetUserListByAssociateID(associateID);
                if (userDTOList != null && userDTOList.Count > 0)
                {
                    RoleRepository roleRepository = new RoleRepository();
                    Role role = roleRepository.FindRoleName(userDTOList[0].RoleID);
                    if (role != null)
                    {
                        dashboardDTO.LocationList = new List<string>();
                        dashboardDTO.EventwithID = new Dictionary<string, string>();
                        if (role.RoleName.Equals(ConstantValues.Admin) || role.RoleName.Equals(ConstantValues.PMO))
                        {
                            EventRepository eventRepository = new EventRepository();
                            List<Event> outreachEvents = eventRepository.GetEventList();
                            if (outreachEvents != null && outreachEvents.Count > 0)
                            {
                                dashboardDTO.LocationList = outreachEvents.Select(env => env.Location).Distinct().ToList();
                                foreach (Event evt in outreachEvents)
                                {
                                    if (!dashboardDTO.EventwithID.ContainsKey(evt.EventId))
                                    {
                                        dashboardDTO.EventwithID.Add(evt.EventId, evt.EventName);
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (UserDTO user in userDTOList)
                            {
                                EventRepository eventRepository = new EventRepository();
                                Event evt = eventRepository.FindEvent(user.EventId);
                                if (evt != null)
                                {
                                    dashboardDTO.LocationList.Add(evt.Location);
                                    dashboardDTO.EventwithID.Add(evt.EventId, evt.EventName);
                                }
                            }
                        }
                    }
                }                
                return dashboardDTO;
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "UserAndRole",
                    ActionrName = "GetDashboardList()",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
                throw ex;
            }            
        }
    }
}