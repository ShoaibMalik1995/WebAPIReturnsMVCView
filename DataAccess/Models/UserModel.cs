using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserModel
    {
        public int Userid { get; set; }
        public string firstName { get; set; }
        public string Lastname { get; set; }
        public string Mobile { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string DateofBirth { get; set; }
        public DateTime dtDateofBirth { get; set; }
        public string AddressLine2 { get; set; }
        public string NoOfUnreadMessages { get; set; }
        public int UserRoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordToken { get; set; }
        public string AcademyTitle { get; set; }
        public int AcademyId { get; set; }
        public string CurrencySymbol { get; set; }
        public string SymbolPlacement { get; set; }
        public string Status { get; set; }
        public string RegDateFormat { get; set; }
        public string RegDateMonth { get; set; }
        public string RegDateWeek { get; set; }
        public string RegDateDay { get; set; }
        public int WeekStartDay { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int CalendarSlot { get; set; }
        public string PersonalEmail { get; set; }
        public int noOfRoles { get; set; }
        public bool IsValid { get; set; }
        public int RegionId { get; set; }
        public string DateFormat { get; set; }
        public string TimeZoneKey { get; set; }
        public string TimeZoneOffSet { get; set; }
        public int AuthenticationType { get; set; }
        public int coachId { get; set; }
        public int playerId { get; set; }
        public int PayByPlayerId { get; set; }
        public int SelPaymentType { get; set; }
        public decimal PlayerPrice { get; set; }
        public bool SendConfirmation { get; set; }
        public bool PayForOthers { get; set; }
        public int ParentAcademyId { get; set; }
        public int OnlinePaymentTypeWeb { get; set; }
        public int StripePaymentWeb { get; set; }
        public int IsPasswordReset { get; set; }
        public bool IsBillingAddressVisible { get; set; }
        public bool IsBillingAddressRequired { get; set; }
        public bool IsBraintreeAcademy { get; set; }
        public int MembershipId { get; set; }
        public string MemberShipExpiry { get; set; }
        public int TrailMemberShipId { get; set; }
        public string TrailMemberShipExpiry { get; set; }
        public string MemberShipName { get; set; }
        public int EmailNotificationDays { get; set; }
        public string NotificationText { get; set; }
        public string EmailTemplete { get; set; }
        public int ExpiryDays { get; set; }
        public int TrailPeriodExpiryDays { get; set; }
        public bool IsPasswordChanged { get; set; }
        public string StoreLoginURL { get; set; }
        public string StripeUserId { get; set; }
        public int CartItemCount { get; set; }
        public string FullName { get; set; }
        public int LoggedInUserId { get; set; }
        public string ChildDob { get; set; }
        public DateTime dtChildDob { get; set; }
        public string ParentName { get; set; }
        public bool PApproval { get; set; }
        public bool IsUnerEighteen { get; set; }
        public string encUserId { get; set; }
        public string EncPassword { get; set; }
        public string UserRoleName { get; set; }
        public bool AgreeTerms { get; set; }
        public bool RecEmail { get; set; }
        public string MedicalIssues { get; set; }
        public string Injuries { get; set; }
        public int LeftHanded { get; set; }
        public int HadGolfLessons { get; set; }
        public string HandiCapName { get; set; }
        public string MissionStatement { get; set; }
        public string PlayFrequency { get; set; }
        public string PracticeFrequency { get; set; }
        public string PlayingStartDate { get; set; }
        public string BillingAddress { get; set; }
        public string FbId { get; set; }
        public string FbToken { get; set; }
        public string TwitterID { get; set; }
        public string AccesTokenSecret { get; set; }
        public string PhotoUrl { get; set; }
        public string BackgroundImageUrl { get; set; }
        public string PublicBackgroundImageUrl { get; set; }
        public int Country { get; set; }
        public string FavouriteCourse { get; set; }
        public string FavouritePlayer { get; set; }
        public string PGAText { get; set; }
        public string BestRound { get; set; }
        public string DreamFourball { get; set; }
        public string BriefProfile { get; set; }
        public string FaceBookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string SkypeDetail { get; set; }
        public string HomeTel { get; set; }
        public string MemberShipText { get; set; }
        public bool IsClubMember { get; set; }
        public string StudentNotes { get; set; }
        public string InternalNotes { get; set; }
    }
}
