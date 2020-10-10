using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataAccess.Models
{
    public class UsersModels
    {
        #region Constr
        public UsersModels()
        {
            populateRoles();
        }
        #endregion

        #region Properties
        private List<SelectListItem> lstItems = new List<SelectListItem>();
        public string signupBannerUrl { get; set; }
        public string ChildName { get; set; }
        public string ParentName { get; set; }
        public DateTime Dob { get; set; }
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string BreifProfile { get; set; }
        public string WorkEmail { get; set; }
        public string PersonalEmail { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public Boolean IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string Title { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public int Country { get; set; }
        public int AcademyId { get; set; }
        public string CountyState { get; set; }
        public string CountyName { get; set; }
        public string CountryName { get; set; }
        public string PostCode { get; set; }
        public string HomeTel { get; set; }
        public string WorkTel { get; set; }
        public string Mobile { get; set; }
        public string AddressCode { get; set; }
        public string FaceBookID { get; set; }
        public string FaceBookToken { get; set; }
        public string AccesToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public string SkypeId { get; set; }
        public string TwitterID { get; set; }
        public string TwitterPublicId { get; set; }
        public string FacebookPublicId { get; set; }
        //User public account details
        public string TwitterPublicUrl { get; set; }
        public string FacebookPublicUrl { get; set; }
        public string Consumer { get; set; }
        public string ConsumerSecret { get; set; }
        public int TotalRecords { get; set; }
        public DateTime DateofBirth { get; set; }
        public string MemberShipExpiry { get; set; }
        public int MemRDays { get; set; }
        // must be correct by string.formate("{1} {2}")
        public string FullName { get; set; }
        public string PhotoURL { get; set; }
        public string AltURL { get; set; }
        public string ZipCode { get; set; }
        public string Gender { get; set; }
        public bool IsDeleted { get; set; }
        public int? Membershiptype { get; set; }
        public string MangoPayUserId { get; set; }
        public string MangoPayUserWalletId { get; set; }
        public string LegalRepresentativeCountryOfResidence { get; set; }
        public string LegalRepresentativeNationality { get; set; }
        public bool AgreeTerms { get; set; }
        public bool RecEmail { get; set; }
        public bool RecSMS { get; set; }
        public int ClubMember { get; set; }
        public string StripeUserId { get; set; }
        public int Membership_Id { get; set; }
        public int TrailMemberShip_Id { get; set; }
        public int noOfRoles { get; set; }

        #endregion

        #region Populate Roles
        public void populateRoles()
        {
            lstItems.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
            lstItems.Add(new SelectListItem() { Text = "Role 1", Value = "1", Selected = false });
        }
        public IEnumerable<SelectListItem> Roles { get { return lstItems; } }
        #endregion
    }
}
