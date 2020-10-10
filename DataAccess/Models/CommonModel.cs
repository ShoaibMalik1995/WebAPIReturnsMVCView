using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataAccess.Models
{
    public class CommonModel
    {
        public string AbsPathURL { get; set; }
        public string AcademyTitle { get; set; }
        public string AcademyLogo { get; set; }
        public string HomeUrl { get; set; }
        public string StoreUrl { get; set; }
        public int LoggedInUserId { get; set; }
        public bool FrontEndEnabled { get; set; }
        public string AcademyName { get; set; }
        public int UserRoleId { get; set; }
        public int CartItemCount { get; set; }
        public string TermsContent { get; set; }
        public string PrivacyContent { get; set; }
        public int RescheduleHourLimit { get; set; }
        public List<Menu> MenuList { get; set; }
        public List<UserRole> UserRoleList { get; set; }
        public UserModel CurrentUser { get; set; }
        public AcademySettingModel AcademySettings { get; set; }
        public int AcademyId { get; set; }
        public string DateFormat { get; set; }
        public List<SelectListItem> CountryList { get; set; }

    }
}
