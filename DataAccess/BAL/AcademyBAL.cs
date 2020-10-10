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
    public class AcademyBAL
    {
        #region Properties
        private DataTable dt = new DataTable();
        private ParamCollection param;
        private DBConnection con = new DBConnection();

        #endregion

        #region Methods
        public DataTable getAcademyOwnerInfo(int OwnerId)
        {
            param = new ParamCollection();
            param.Add(new SqlParameter("@OwnerID", OwnerId));
            dt = con.GetDataTable("SpgetAcademyOwnerInfo", CommandType.StoredProcedure, param);
            return dt;
        }

        public int getAcademyByName(string Academyname)
        {
            param = new ParamCollection();
            param.Add(new SqlParameter("@AcademyName", Academyname));
            dt = con.GetDataTable("spGetAcademyIdByName", CommandType.StoredProcedure, param);
            if (dt == null || dt.Rows.Count == 0)
                return 0;
            else
                return Convert.ToInt32(dt.Rows[0][0]);
        }

        public DataTable GetAcademyInfo(int AcademyId)
        {
            param = new ParamCollection();
            param.Add(new SqlParameter("@AcademyId", AcademyId));
            dt = con.GetDataTable("SpGetAcademyInformation", CommandType.StoredProcedure, param);
            return dt;
        }

        public DateTime GetDateByAcademyTimeZone(int academyId)
        {
            param.Add(new SqlParameter("@AcademyId", academyId));
            DataTable dt = con.GetDataTable("SpGetDateByAcademyTimeZone", CommandType.StoredProcedure, param);
            if (dt == null) return DateTime.Now;
            return Convert.ToDateTime(dt.Rows[0][0]);
        }

        public DataTable GetSocialMediaLinks(int academyId)
        {
            param.Add(new SqlParameter("@AcademyId", academyId));
            return con.GetDataTable("spGetSocialMediaLinks", CommandType.StoredProcedure, param);
        }

        public bool IsGateWayChargeFee(int AcademyId)
        {
            param = new ParamCollection();
            param.Add(new SqlParameter("@AcademyId", AcademyId));
            var result = con.GetSingleRecord("SpIsGateWayChargeFee", CommandType.StoredProcedure, param);
            return Convert.ToInt32(result) == 1 ? true : false;
        }

        public AcademyModels getAcademyByAcademyId(int AcademyId)
        {
            AcademyModels acm = new AcademyModels();
            param = new ParamCollection();
            param.Add(new SqlParameter("@AcademyId", AcademyId));
            dt = con.GetDataTable("SpGetAcademyById", CommandType.StoredProcedure, param);
            if (dt.Rows.Count > 0)
            {
                acm.AcademyName = dt.Rows[0]["AcademyName"].ToString();
                acm.GolfFaciliityName = dt.Rows[0]["GolfFaciliityName"].ToString();
                acm.AcademyId = Convert.ToInt32(dt.Rows[0]["AcademyId"]);
                acm.AcademyOwnerId = Convert.ToInt32(dt.Rows[0]["AcademyOwnerId"]);
                acm.AcademyLogo = dt.Rows[0]["AcademyLogo"].ToString();
                acm.Address = dt.Rows[0]["Address"].ToString();
                acm.Address2 = dt.Rows[0]["Address2"].ToString();
                acm.City = dt.Rows[0]["City"].ToString();
                acm.CountryId = Convert.ToInt32(dt.Rows[0]["CountryId"]);
                acm.AcademyManagerId = Convert.ToInt32(dt.Rows[0]["AcademyManagerId"]);
                acm.CountyId = Convert.ToInt32(dt.Rows[0]["CountyId"]);
                acm.AcademyPhone = dt.Rows[0]["AcademyPhone"].ToString();
                acm.ClubPhone = dt.Rows[0]["ClubPhone"].ToString();
                acm.Email = dt.Rows[0]["Email"].ToString();
                acm.PostCode = dt.Rows[0]["PostCode"].ToString();
                acm.CurrencySymbol = dt.Rows[0]["CurrencySymbol"].ToString();
                acm.AcademyPicture = dt.Rows[0]["AcademyPicture"].ToString();
                acm.RegionId = Convert.ToInt32(dt.Rows[0]["RegionId"]);
                acm.AcademyDescription = getAcademyDescription(AcademyId);
                acm.RegDateMonth = dt.Rows[0]["RegDateMonth"].ToString();
                acm.RegDateWeek = dt.Rows[0]["RegDateWeek"].ToString();
                acm.RegDateDay = dt.Rows[0]["RegDateDay"].ToString();
                acm.DateFormat = dt.Rows[0]["DateFormat"].ToString();
                acm.WeekStartDay = Convert.ToInt32(dt.Rows[0]["WeekStartDay"]);
                acm.StartTime = Convert.ToInt32(dt.Rows[0]["StartTime"]);
                acm.EndTime = Convert.ToInt32(dt.Rows[0]["EndTime"]);
                acm.CalendarSlot = Convert.ToInt32(dt.Rows[0]["CalendarSlot"]);
                acm.TwitterId = dt.Rows[0]["TwitterId"].ToString();
                acm.AcademyTitle = dt.Rows[0]["AcademyTitle"].ToString();
                acm.AcademyNumber = dt.Rows[0]["AcademyNumber"].ToString();
                acm.AuthenticationType = Convert.ToInt32(dt.Rows[0]["AuthType"]);
                acm.TimeZoneId = Convert.ToInt32(dt.Rows[0]["TimeZoneId"]);
                acm.TimeZoneKey = dt.Rows[0]["TimeZoneKey"].ToString();
                acm.TimeZoneOffSet = dt.Rows[0]["TimeZoneOffSet"].ToString();
                acm.ParentAcademyId = Convert.ToInt32(dt.Rows[0]["ParentAcademyId"]);
                acm.EnableFrontEndWebsite = Convert.ToBoolean(dt.Rows[0]["EnableFrontEndWebsite"]);
                acm.VenueUrl = dt.Rows[0]["VenueUrl"].ToString();
                //acm.LogoUrl = dt.Rows[0]["LogoUrl"].ToString();
                //acm.PrivatePolicyUrl = dt.Rows[0]["PrivatePolicyUrl"].ToString();
                //acm.TermsAndConditionsUrl = dt.Rows[0]["TermsAndConditionsUrl"].ToString();

            }

            return acm;
        }

        public string getAcademyDescription(int AcademyId)
        {
            param = new ParamCollection();
            param.Add(new SqlParameter("@AcademyId", AcademyId));
            DataTable dt = con.GetDataTable("SpGetAcademyDetailById", CommandType.StoredProcedure, param);
            String academyDescription = "";
            foreach (DataRow dr in dt.Rows)
            {
                academyDescription += dr["AcademyDescription"].ToString();
            }
            return academyDescription;
        }

        public AcademySettingModel GetAcademySettings(int AcademyId)
        {
            AcademySettingModel _Settings = new AcademySettingModel();
            try
            {
                param = new ParamCollection();
                param.Add(new SqlParameter("@AcademyId", AcademyId));
                dt = con.GetDataTable("spGetSettingsByAcademy", CommandType.StoredProcedure, param);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Select("[SettingName] = 'MembershipEnabled'").FirstOrDefault();
                    _Settings.MembershipEnabled = dr == null ? false : dr["Setting"].ToString() == "True" ? true : false;
                    dr = dt.Select("[SettingName] = 'PayatAacademy.EnableForPlayer'").FirstOrDefault();
                    _Settings.PayatAacademyEnableForPlayer = dr == null ? true : dr["Setting"].ToString() == "True" ? true : false;
                    dr = dt.Select("[SettingName] = 'PayByLink.EnableForPlayer'").FirstOrDefault();
                    _Settings.PayByLinkEnableForPlayer = dr == null ? false : dr["Setting"].ToString() == "True" ? true : false;
                    dr = dt.Select("[SettingName] = 'EnableAddress'").FirstOrDefault();
                    _Settings.AddressEnabled = dr == null ? false : dr["Setting"].ToString() == "True" ? true : false;
                    dr = dt.Select("[SettingName] = 'Academy.TermsContent'").FirstOrDefault();
                    _Settings.TermsAndConditionContent = dr == null ? "" : dr["Setting"].ToString();
                    dr = dt.Select("[SettingName] = 'Academy.PrivacyContent'").FirstOrDefault();
                    _Settings.PrivacyContent = dr == null ? "" : dr["Setting"].ToString();
                    dr = dt.Select("[SettingName] = 'AcademyWeatherSettings'").FirstOrDefault();
                    _Settings.AcademyWeatherSetting = dr == null ? "" : dr["Setting"].ToString();
                    dr = dt.Select("[SettingName] = '12HoursTimeFormat'").FirstOrDefault();
                    _Settings.ThTimeFormat = dr == null ? false : dr["Setting"].ToString() == "True" ? true : false;
                    dr = dt.Select("[SettingName] = 'Manager.IsMembershipShow'").FirstOrDefault();
                    _Settings.IsMembershipShow = dr == null ? false : dr["Setting"].ToString() == "True" ? true : false;
                    dr = dt.Select("[SettingName] = 'Academy.EnableAcademyMemberPricing'").FirstOrDefault();
                    _Settings.EnableAcademyMemberPricing = dr == null ? false : dr["Setting"].ToString() == "True" ? true : false;
                }
            }
            catch { }
            return _Settings;
        }

        public DataTable GetAcademyData()
        {
            DataTable dtAcademyHash = new DataTable();
            dtAcademyHash.Columns.Add("AcademyId", typeof(Int32));
            dtAcademyHash.Columns.Add("AcademyName", typeof(string));
            dtAcademyHash.Columns.Add("AcademyTitle", typeof(string));
            dtAcademyHash.Columns.Add("HostMDHash", typeof(string)); //AcademyTitle + (glfbeta.com | glflocker.com)

            DataTable dtResult = con.GetDataTable("SELECT AcademyId, AcademyName, AcademyTitle, '' HostMDHash  FROM tblAcademy WHERE CreatedBy=639", CommandType.Text);
            
            foreach(DataRow row in dtResult.Rows)
            {
                DataRow dtRow = dtAcademyHash.NewRow();
                dtRow["AcademyId"] = Convert.ToInt32(row["AcademyId"].ToString());
                dtRow["AcademyName"] = row["AcademyName"].ToString();
                dtRow["AcademyTitle"] = row["AcademyTitle"].ToString();
                dtRow["HostMDHash"] = DataAccess.Common.Common.EncryptStringMD5HASH(row["AcademyId"].ToString() + "_" + row["AcademyTitle"].ToString() + ".glflocker.com"); //For Live
                //dtRow["HostMDHash"] = DataAccess.Common.Common.EncryptStringMD5HASH(row["AcademyId"].ToString() + "_" + row["AcademyTitle"].ToString()+ ".glfbeta.com"); // For Beta

                dtAcademyHash.Rows.Add(dtRow);
            }
            
            return dtAcademyHash;
        }
        #endregion
    }
}
