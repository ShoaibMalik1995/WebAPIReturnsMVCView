using System.Collections.Generic;

namespace DataAccess.Models
{
    public class AcademyModels
    {
        #region Properties
        public int AcademyId { set; get; }
        public int AcademyOwnerId { set; get; }
        public string AcademyName { set; get; }
        public string AcademyDescription { set; get; }
        public string AcademyLogo { set; get; }
        public int AcademyManagerId { set; get; }
        public string GolfFaciliityName { set; get; }
        public string Address { set; get; }
        public string Address2 { set; get; }
        public string City { set; get; }
        public int CountryId { set; get; }
        public int RegionId { set; get; }
        public int TimeZoneId { set; get; }
        public string TimeZoneKey { set; get; }
        public string TimeZoneOffSet { set; get; }
        public string RegDateFormat { set; get; }
        public int WeekStartDay { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int CalendarSlot { get; set; }
        public int CountyId { set; get; }
        public string PostCode { set; get; }
        public string AcademyPhone { set; get; }
        public string AcademyTitle { set; get; }
        public string AcademyNumber { set; get; }
        public int AuthenticationType { set; get; }
        public string ClubPhone { set; get; }
        public string Email { set; get; }
        public string academyOwnerUsername { get; set; }
        public string academyOwnerPassword { get; set; }
        public int CreatedBy { set; get; }
        public string AcademyPicture { set; get; }
        public string CurrencySymbol { get; set; }
        public int ParentAcademyId { get; set; }
        public string returnUrl { get; set; }
        public string VenueUrl { get; set; }
        public string LogoUrl { get; set; }
        public string PrivatePolicyUrl { get; set; }
        public string TermsAndConditionsUrl { get; set; }
        public bool EnableFrontEndWebsite { get; set; }
        public string RegDateMonth { get; set; }
        public string RegDateWeek { get; set; }
        public string RegDateDay { get; set; }
        public string DateFormat { get; set; }
        public string twConsumerKey { get; set; }
        public string twConsumerSecret { get; set; }
        public string TwitterId { get; set; }

        #endregion
    }
}
