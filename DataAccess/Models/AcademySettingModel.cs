using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class AcademySettingModel
    {
        public bool MembershipEnabled { get; set; }
        public bool PayatAacademyEnableForPlayer { get; set; }
        public bool PayByLinkEnableForPlayer { get; set; }
        public bool AddressEnabled { get; set; }
        public string TermsAndConditionContent { get; set; }
        public string PrivacyContent { get; set; }
        public string AcademyWeatherSetting { get; set; }
        public bool ThTimeFormat { get; set; }
        public bool IsMembershipShow { get; set; }
        public bool EnableAcademyMemberPricing { get; set; }
    }
}
