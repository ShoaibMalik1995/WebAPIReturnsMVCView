using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BAL
{
    public class LoginBAL
    {
        #region Properties
        private DataTable dt = new DataTable();
        private ParamCollection param;
        private DBConnection con = new DBConnection();

        #endregion

        #region Methods
        public bool IsEmailAlreadyExist(int AcademyId, string Email)
        {
            param = new ParamCollection();
            param.Add(new SqlParameter("@PersonalEmail", Email));
            param.Add(new SqlParameter("@AcademyId", AcademyId));
            dt = con.GetDataTable("SpEmailValidationUser", CommandType.StoredProcedure, param);
            if (dt != null &&dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public LoginModels LoginValidation(LoginModels model, bool isOwner = false)
        {
            param = new ParamCollection();
            param.Add(new SqlParameter("@UserName", model.UserName));
            param.Add(new SqlParameter("@Password", model.Password));
            param.Add(new SqlParameter("@IsOwner", isOwner));
            param.Add(new SqlParameter("@AcademyId", model.AcademyId));
            dt = con.GetDataTable("SpLoginValidation", CommandType.StoredProcedure, param);
            //noOfRoles = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                model.Userid = Convert.ToInt32(dt.Rows[0]["UserId"]);
                model.UserName = dt.Rows[0]["UserName"].ToString();
                model.Password = dt.Rows[0]["Password"].ToString();
                model.AcademyTitle = dt.Rows[0]["AcademyTitle"].ToString();
                model.RegionId = Convert.ToInt32(dt.Rows[0]["RegionId"]);
                model.UserRoleId = Convert.ToInt32(dt.Rows[0]["UserRoleId"]);
                model.Status = "Success";
                model.PersonalEmail = dt.Rows[0]["PersonalEmail"].ToString();
                model.AcademyId = Convert.ToInt32(dt.Rows[0]["AcademyId"]);
                model.CurrencySymbol = dt.Rows[0]["CurrencySymbol"].ToString();
                model.NoOfUnreadMessages = dt.Rows[0]["NoOfUnreadMessages"].ToString();
                model.firstName = dt.Rows[0]["firstName"].ToString();
                model.Lastname = dt.Rows[0]["LastName"].ToString();
                model.coachId = Convert.ToInt32(dt.Rows[0]["CoachId"]);
                model.playerId = Convert.ToInt32(dt.Rows[0]["PlayerId"]);
                model.RegDateMonth = dt.Rows[0]["RegDateMonth"].ToString();
                model.RegDateWeek = dt.Rows[0]["RegDateWeek"].ToString();
                model.RegDateDay = dt.Rows[0]["RegDateDay"].ToString();
                model.WeekStartDay = Convert.ToInt32(dt.Rows[0]["WeekStartDay"]);
                model.StartTime = Convert.ToInt32(dt.Rows[0]["StartTime"]);
                model.EndTime = Convert.ToInt32(dt.Rows[0]["EndTime"]);
                model.CalendarSlot = Convert.ToInt32(dt.Rows[0]["CalendarSlot"]);
                model.ParentAcademyId = Convert.ToInt32(dt.Rows[0]["ParentAcademyId"]);
                model.MembershipId = Convert.ToInt32(dt.Rows[0]["MembershipId"]);
                model.MemberShipExpiry = dt.Rows[0]["MemberShipExpiry"].ToString();
                model.TrailMemberShipId = Convert.ToInt32(dt.Rows[0]["TrailMemberShipId"]);
                model.TrailMemberShipExpiry = dt.Rows[0]["TrailMemberShipExpiry"].ToString();
                model.MemberShipName = dt.Rows[0]["MemberShipName"].ToString();
                model.EmailNotificationDays = Convert.ToInt32(dt.Rows[0]["EmailNotificationDays"]);
                model.NotificationText = dt.Rows[0]["NotificationText"].ToString();
                model.EmailTemplete = dt.Rows[0]["EmailTemplete"].ToString();
                model.ExpiryDays = Convert.ToInt32(dt.Rows[0]["ExpiryDays"]);
                model.TrailPeriodExpiryDays = Convert.ToInt32(dt.Rows[0]["TrailPeriodExpiryDays"]);
                //model.OnlinePaymentTypeWeb = Convert.ToInt32(dt.Rows[0]["OnlinePaymentTypeWeb"]);
            }
            else
            {
                model.UserName = null;
                model.Password = null;
                model.UserRoleId = 0;
                model.Status = "UnSuccessFull";
                model.PersonalEmail = null;
            }
            return model;

        }

        public LoginModels ValidateUserLogin(LoginModels model)
        {
            param = new ParamCollection();
            param.Add(new SqlParameter("@Filter", "FDGAMenuUserValidation"));
            param.Add(new SqlParameter("@UserId", model.Userid));
            param.Add(new SqlParameter("@AcademyId", model.AcademyId));
            dt = con.GetDataTable("SpLoginValidation", CommandType.StoredProcedure, param);
            
            if (dt.Rows.Count > 0)
            {
                model.Userid = Convert.ToInt32(dt.Rows[0]["UserId"]);
                model.UserName = dt.Rows[0]["UserName"].ToString();
                model.Password = dt.Rows[0]["Password"].ToString();
                model.AcademyTitle = dt.Rows[0]["AcademyTitle"].ToString();
                model.RegionId = Convert.ToInt32(dt.Rows[0]["RegionId"]);
                model.UserRoleId = Convert.ToInt32(dt.Rows[0]["UserRoleId"]);
                model.Status = "Success";
                model.PersonalEmail = dt.Rows[0]["PersonalEmail"].ToString();
                model.AcademyId = Convert.ToInt32(dt.Rows[0]["AcademyId"]);
                model.CurrencySymbol = dt.Rows[0]["CurrencySymbol"].ToString();
                model.NoOfUnreadMessages = dt.Rows[0]["NoOfUnreadMessages"].ToString();
                model.firstName = dt.Rows[0]["firstName"].ToString();
                model.Lastname = dt.Rows[0]["LastName"].ToString();
                model.coachId = Convert.ToInt32(dt.Rows[0]["CoachId"]);
                model.playerId = Convert.ToInt32(dt.Rows[0]["PlayerId"]);
                model.RegDateMonth = dt.Rows[0]["RegDateMonth"].ToString();
                model.RegDateWeek = dt.Rows[0]["RegDateWeek"].ToString();
                model.RegDateDay = dt.Rows[0]["RegDateDay"].ToString();
                model.WeekStartDay = Convert.ToInt32(dt.Rows[0]["WeekStartDay"]);
                model.StartTime = Convert.ToInt32(dt.Rows[0]["StartTime"]);
                model.EndTime = Convert.ToInt32(dt.Rows[0]["EndTime"]);
                model.CalendarSlot = Convert.ToInt32(dt.Rows[0]["CalendarSlot"]);
                model.ParentAcademyId = Convert.ToInt32(dt.Rows[0]["ParentAcademyId"]);
                model.MembershipId = Convert.ToInt32(dt.Rows[0]["MembershipId"]);
                model.MemberShipExpiry = dt.Rows[0]["MemberShipExpiry"].ToString();
                model.TrailMemberShipId = Convert.ToInt32(dt.Rows[0]["TrailMemberShipId"]);
                model.TrailMemberShipExpiry = dt.Rows[0]["TrailMemberShipExpiry"].ToString();
                model.MemberShipName = dt.Rows[0]["MemberShipName"].ToString();
                model.EmailNotificationDays = Convert.ToInt32(dt.Rows[0]["EmailNotificationDays"]);
                model.NotificationText = dt.Rows[0]["NotificationText"].ToString();
                model.EmailTemplete = dt.Rows[0]["EmailTemplete"].ToString();
                model.ExpiryDays = Convert.ToInt32(dt.Rows[0]["ExpiryDays"]);
                model.TrailPeriodExpiryDays = Convert.ToInt32(dt.Rows[0]["TrailPeriodExpiryDays"]);
            }
            else
            {
                model.UserName = null;
                model.Password = null;
                model.UserRoleId = 0;
                model.Status = "UnSuccessFull";
                model.PersonalEmail = null;
            }
            return model;
        }

        public UserModel PrepareLogin(UserModel _UserModel)
        {
            param = new ParamCollection();
            param.Add(new SqlParameter("@Filter", "HeaderMenuAPIUserValidation"));
            param.Add(new SqlParameter("@UserId", _UserModel.Userid));
            param.Add(new SqlParameter("@IsOwner", 0));
            param.Add(new SqlParameter("@AcademyId", _UserModel.AcademyId));

            dt = con.GetDataTable("SpLoginValidationNew", CommandType.StoredProcedure, param);
            _UserModel.noOfRoles = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                _UserModel.Userid = Convert.ToInt32(dt.Rows[0]["UserId"]);
                _UserModel.UserName = dt.Rows[0]["UserName"].ToString();
                _UserModel.Password = dt.Rows[0]["Password"].ToString();
                _UserModel.AcademyTitle = dt.Rows[0]["AcademyTitle"].ToString();
                _UserModel.UserRoleId = Convert.ToInt32(dt.Rows[0]["UserRoleId"]);
                _UserModel.Status = "Success";
                _UserModel.PersonalEmail = dt.Rows[0]["PersonalEmail"].ToString();
                _UserModel.AcademyId = Convert.ToInt32(dt.Rows[0]["AcademyId"]);
                _UserModel.CurrencySymbol = dt.Rows[0]["CurrencySymbol"].ToString();
                _UserModel.SymbolPlacement = dt.Rows[0]["IsSymbolPlacementRight"].ToString();
                _UserModel.NoOfUnreadMessages = dt.Rows[0]["NoOfUnreadMessages"].ToString();
                _UserModel.firstName = dt.Rows[0]["firstName"].ToString();
                _UserModel.Lastname = dt.Rows[0]["LastName"].ToString();
                _UserModel.coachId = Convert.ToInt32(dt.Rows[0]["CoachId"]);
                _UserModel.playerId = Convert.ToInt32(dt.Rows[0]["PlayerId"]);
                _UserModel.RegDateMonth = dt.Rows[0]["RegDateMonth"].ToString();
                _UserModel.RegDateWeek = dt.Rows[0]["RegDateWeek"].ToString();
                _UserModel.RegDateDay = dt.Rows[0]["RegDateDay"].ToString();
                _UserModel.WeekStartDay = Convert.ToInt32(dt.Rows[0]["WeekStartDay"]);
                _UserModel.StartTime = Convert.ToInt32(dt.Rows[0]["StartTime"]);
                _UserModel.EndTime = Convert.ToInt32(dt.Rows[0]["EndTime"]);
                _UserModel.CalendarSlot = Convert.ToInt32(dt.Rows[0]["CalendarSlot"]);
                _UserModel.ParentAcademyId = Convert.ToInt32(dt.Rows[0]["ParentAcademyId"]);
                _UserModel.IsPasswordReset = Convert.ToInt32(dt.Rows[0]["IsPasswordReset"]);
                _UserModel.IsBillingAddressVisible = Convert.ToBoolean(dt.Rows[0]["IsBillingAddressVisible"]);
                _UserModel.IsBillingAddressRequired = Convert.ToBoolean(dt.Rows[0]["IsBillingAddressRequired"]);
                _UserModel.IsPasswordChanged = Convert.ToBoolean(dt.Rows[0]["IsPasswordChanged"]);
                _UserModel.MembershipId = Convert.ToInt32(dt.Rows[0]["MembershipId"]);
                _UserModel.MemberShipExpiry = dt.Rows[0]["MemberShipExpiry"].ToString();
                _UserModel.TrailMemberShipId = Convert.ToInt32(dt.Rows[0]["TrailMemberShipId"]);
                _UserModel.TrailMemberShipExpiry = dt.Rows[0]["TrailMemberShipExpiry"].ToString();
                _UserModel.MemberShipName = dt.Rows[0]["MemberShipName"].ToString();
                _UserModel.EmailNotificationDays = Convert.ToInt32(dt.Rows[0]["EmailNotificationDays"]);
                _UserModel.NotificationText = dt.Rows[0]["NotificationText"].ToString();
                _UserModel.EmailTemplete = dt.Rows[0]["EmailTemplete"].ToString();
                _UserModel.ExpiryDays = Convert.ToInt32(dt.Rows[0]["ExpiryDays"]);
                _UserModel.TrailPeriodExpiryDays = Convert.ToInt32(dt.Rows[0]["TrailPeriodExpiryDays"]);
                _UserModel.StripeUserId = dt.Rows[0]["StripeUserId"].ToString();
                _UserModel.UserRoleName = dt.Rows[0]["UserRoleName"].ToString();
                _UserModel.AgreeTerms = Convert.ToBoolean(dt.Rows[0]["AgreeTerms"]);
                _UserModel.RecEmail = Convert.ToBoolean(dt.Rows[0]["RecEmail"]);
                _UserModel.AddressLine1 = dt.Rows[0]["AddressLine1"].ToString();
                _UserModel.AddressLine2 = dt.Rows[0]["AddressLine2"].ToString();
                _UserModel.Mobile = dt.Rows[0]["Mobile"].ToString();
                _UserModel.DateofBirth = dt.Rows[0]["DateofBirth"].ToString();
                _UserModel.MedicalIssues = dt.Rows[0]["MedicalIssues"].ToString();
                _UserModel.Injuries = dt.Rows[0]["Injuries"].ToString();
                _UserModel.LeftHanded = Convert.ToInt32(dt.Rows[0]["LeftHanded"]);
                _UserModel.HadGolfLessons = Convert.ToInt32(dt.Rows[0]["HadGolfLessons"]);
                _UserModel.HandiCapName = dt.Rows[0]["HandiCapName"].ToString();
                _UserModel.MissionStatement = dt.Rows[0]["MissionStatement"].ToString();
                _UserModel.PlayFrequency = dt.Rows[0]["PlayFrequency"].ToString();
                _UserModel.PracticeFrequency = dt.Rows[0]["PracticeFrequency"].ToString();
                _UserModel.PlayingStartDate = dt.Rows[0]["PlayingStartDate"].ToString();
                _UserModel.BillingAddress = dt.Rows[0]["BillingAddress"].ToString();
                _UserModel.FbId = dt.Rows[0]["FBID"].ToString();
                _UserModel.FbToken = dt.Rows[0]["FBToken"].ToString();
                _UserModel.TwitterID = dt.Rows[0]["TwitterID"].ToString();
                _UserModel.AccesTokenSecret = dt.Rows[0]["AccesTokenSecret"].ToString();
                _UserModel.PhotoUrl = dt.Rows[0]["PhotoUrl"].ToString();
                _UserModel.BackgroundImageUrl = dt.Rows[0]["BackGroundImageUrl"].ToString();
                _UserModel.PublicBackgroundImageUrl = dt.Rows[0]["PublicBackGroundImageUrl"].ToString();
                _UserModel.City = dt.Rows[0]["City"].ToString();
                _UserModel.Country = Convert.ToInt32(dt.Rows[0]["Country"]);
                _UserModel.FavouriteCourse = dt.Rows[0]["FavouriteCourse"].ToString();
                _UserModel.FavouritePlayer = dt.Rows[0]["FavouritePlayer"].ToString();
                _UserModel.PGAText = dt.Rows[0]["PGAText"].ToString();
                _UserModel.BestRound = dt.Rows[0]["BestRound"].ToString();
                _UserModel.DreamFourball = dt.Rows[0]["DreamFourball"].ToString();
                _UserModel.BriefProfile = dt.Rows[0]["BriefProfile"].ToString();
                _UserModel.HomeTel = dt.Rows[0]["HomeTel"].ToString();
                _UserModel.TwitterUrl = dt.Rows[0]["TwitterUrl"].ToString();
                _UserModel.FaceBookUrl = dt.Rows[0]["FBURL"].ToString();
                _UserModel.SkypeDetail = dt.Rows[0]["SkypeDetails"].ToString();
                _UserModel.IsClubMember = Convert.ToBoolean(dt.Rows[0]["IsClubMember"]);
                
            }
            else
            {
                _UserModel.UserName = null;
                _UserModel.Password = null;
                _UserModel.UserRoleId = 0;
                _UserModel.Status = "UnSuccessFull";
                _UserModel.PersonalEmail = null;
            }
            return _UserModel;
        }
        public List<Menu> GetMainMenus(string GroupName, int UserId = 0, int AcademyId = 0)
        {
            param = new ParamCollection();
            Menu menu = new Menu();
            menu.GroupName = GroupName;
            menu.AcademyId = AcademyId;
            List<Menu> lst = new List<Menu>();
            param.Add(new SqlParameter("@GroupName", menu.GroupName));
            param.Add(new SqlParameter("@AcademyId", menu.AcademyId));
            param.Add(new SqlParameter("@UserId", UserId));
            dt = con.GetDataTable("sp_GetMenus", CommandType.StoredProcedure, param);

            return dt.AsEnumerable().Select(row => new Menu()
            {
                ID = Convert.ToInt32(row["ID"]),
                AcademyId = Convert.ToInt32(row["AcademyId"]),
                Title = row["Title"].ToString(),
                MeneURL = row["MeneURL"].ToString(),
                MenuId = row["MenuId"].ToString(),
                Description = row["menuDesc"].ToString(),
                ParentID = Convert.ToInt32(row["ParentID"]),
                IsActive = Convert.ToBoolean(row["IsActive"])
            }).ToList();
        }

        public string GetUserGroup(int UserRoleId)
        {
            string group = "Manager";
            switch (UserRoleId)
            {
                case 1:
                    group = "Owner";
                    break;
                case 2:
                    group = "Manager";
                    break;
                case 3:
                    group = "Coach";
                    break;
                case 4:
                    group = "Player";
                    break;
                case 6:
                    group = "SuperOwner";
                    break;
                case 7:
                    group = "Proshop";
                    break;
                case 8:
                    group = "Proshop";
                    break;
                default:
                    group = "Manager";
                    break;

            }
            return group;
        }
        #endregion
    }
}
