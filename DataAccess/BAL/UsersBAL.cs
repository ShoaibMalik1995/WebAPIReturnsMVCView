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
    public class UsersBAL
    {
        #region Properties
        private DataTable dt = new DataTable();
        private ParamCollection param;
        private DBConnection con = new DBConnection();

        #endregion

        #region Methods
        public IEnumerable<UsersModels> GetManagerByAcademyId(int academyId, int userRoleId = 2)
        {
            param = new ParamCollection();
            param.Add(new SqlParameter("@UserRoleId", userRoleId));
            param.Add(new SqlParameter("@AcademyId", academyId));
            dt = con.GetDataTable("SpGetUsersByRoleID", CommandType.StoredProcedure, param);

            return dt.AsEnumerable().Select(row => new UsersModels {
                UserId = Convert.ToInt32(row["UserId"]),
                UserRoleId = Convert.ToInt32(row["UserRoleId"]),
                UserName = row["UserName"].ToString(),
                Password = row["Password"].ToString(),
                FirstName = row["LastName"].ToString(),
                LastName = row["LastName"].ToString(),
                FullName = row["FirstName"].ToString() + " " + row["LastName"].ToString(),
                WorkEmail = row["WorkEmail"].ToString(),
                PersonalEmail = row["PersonalEmail"].ToString(),
                PasswordQuestion = row["PasswordQuestion"].ToString(),
                PasswordAnswer = row["PasswordAnswer"].ToString(),
                IsActive = Convert.ToBoolean(row["IsActive"]),
                UserRole = "NotAssigned"
            });
        }
        public string GetSuperOwnerEmail()
        {
            param = new ParamCollection();
            return con.GetSingleRecord("spGetSuperownersEmail", CommandType.StoredProcedure).ToString();
        }
        public List<UserRole> GetUserRoles(int UserId)
        {
            param = new ParamCollection();
            param.Add(new SqlParameter("@UserId", UserId));
            dt = con.GetDataTable("sp_GetUserLockerUrls", CommandType.StoredProcedure, param);
            return dt.AsEnumerable().Select(row => new UserRole() { 
                UserRoleId = Convert.ToInt32(row["UserRoleId"].ToString()),
                UserRoleName = row["UserRoleName"].ToString(),
                LockerURL = row["LockerURL"].ToString().Split('-'),
                Controller = row["LockerURL"].ToString().Split('-').Length > 0 ? row["LockerURL"].ToString().Split('-')[0] : "",
                Action = row["LockerURL"].ToString().Split('-').Length > 0 ? row["LockerURL"].ToString().Split('-')[1] : "",
            }).ToList();
        }
        public DataTable GetPlayerCountryList()
        {
            dt = con.GetDataTable("spGetCountryList", CommandType.StoredProcedure);
            return dt;
        }
        #endregion
    }
}
