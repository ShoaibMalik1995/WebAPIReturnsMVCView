using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataAccess.Models
{
    public class LoginModels
    {
        #region Properties

        private DBConnection con = new DBConnection();
        private List<SelectListItem> lstItems = new List<SelectListItem>();

        public string loginBannerUrl { get; set; }
        public string forgotPasswBannerUrl { get; set; }
        public int Userid { get; set; }
        public string firstName { get; set; }
        public string Lastname { get; set; }
        public string NoOfUnreadMessages { get; set; }
        public int MembershipId { get; set; }
        public string MemberShipExpiry { get; set; }
        public int TrailMemberShipId { get; set; }
        public string TrailMemberShipExpiry { get; set; }
        public string MemberShipName { get; set; }
        public int EmailNotificationDays { get; set; }
        public string NotificationText { get; set; }
        public string EmailTemplete { get; set; }
        public int ExpiryDays { get; set; }
        public bool ResetStatus { get; set; }
        public int TrailPeriodExpiryDays { get; set; }
        public int UserRoleId { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string PasswordToken { set; get; }
        public string AcademyTitle { get; set; }
        public int AcademyId { get; set; }
        public string CurrencySymbol { get; set; }
        public string Status { set; get; }
        public string RegDateFormat { set; get; }
        public string RegDateMonth { get; set; }
        public string RegDateWeek { get; set; }
        public string RegDateDay { get; set; }
        public int WeekStartDay { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int CalendarSlot { get; set; }
        public String PersonalEmail { set; get; }
        public int noOfRoles { get; set; }
        public bool IsValid { get; set; }
        public int RegionId { set; get; }
        public IList<SelectListItem> Academies { get; set; }
        public string DateFormat { get; set; }
        public string TimeZoneKey { set; get; }
        public string TimeZoneOffSet { set; get; }
        public int AuthenticationType { set; get; }
        public int coachId { get; set; }
        public int playerId { get; set; }
        public int UserNameOrPassWord { get; set; }
        public List<SelectListItem> AcademiesList { get; set; }
        public int ParentAcademyId { get; set; }
        public string returnUrl { get; set; }
        public List<Menu> UsersMenu { get; set; }
        #endregion
    }
}
