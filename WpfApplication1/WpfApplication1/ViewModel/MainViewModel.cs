using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using System.Data.SqlClient;
using System.Configuration;

namespace WpfAppViewModel
{
    public class MainViewModel
    {

        public static string constr = ConfigurationManager.ConnectionStrings["UserRegistrationConnectionString"].ConnectionString;

        //Get the default MemoryCache
        //ObjectCache cache = MemoryCache.Default;

        public DataTable GetCountryData()
        {
            DataTable data;

            //if (cache.GetCount() == 0)
            //{
                //Create a custom Timeout of 10 seconds
                //CacheItemPolicy policy = new CacheItemPolicy();
                //policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(30.0);

                data = GetCountryDataFromDB();

                //add the object to the cache
                //cache.Add("data", data, policy);
            //}
            //else
            //{
            //    data = (DataTable)cache.Get("data");
            //}

            return data;
        }

        private static DataTable GetCountryDataFromDB()
        {
            #region 
            //DataTable data;
            //data = new DataTable();
            //data.Columns.Add("Id");
            //data.Columns.Add("DisplayValue");

            //data.Rows.Add(1, "Val1");
            //data.Rows.Add(2, "Val2");
            //data.Rows.Add(3, "Val3");
            //data.Rows.Add(4, "Val4");
            //data.Rows.Add(5, "Val5");
            //return data;
            #endregion

            DataTable data;
            data = new DataTable();
            
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT CountryId, CountryName FROM Country"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;                        
                        sda.Fill(data);
                    }
                }
            }

            return data;
        }

        public DataTable GetStateData()
        {
            DataTable data = GetStateDataFromDB();

            return data;
        }

        private static DataTable GetStateDataFromDB()
        {         

            DataTable data;
            data = new DataTable();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT StateId, StateName FROM State"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(data);
                    }
                }
            }

            return data;
        }

        public DataTable GetCityData()
        {
            DataTable data = GetCityDataFromDB();

            return data;
        }

        private static DataTable GetCityDataFromDB()
        {

            DataTable data;
            data = new DataTable();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT CityId, CityName FROM City"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(data);
                    }
                }
            }

            return data;
        }

        internal DataTable GetUGEducationData()
        {
            DataTable data;
            data = new DataTable();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT UGEducationId, UGEducationName FROM UGEducation"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(data);
                    }
                }
            }

            return data;
        }

        internal DataTable GetGraduationEducationData()
        {
            DataTable data;
            data = new DataTable();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT GraduateEducationId, GraduateEducationName FROM GraduateEducation"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(data);
                    }
                }
            }

            return data;
        }

        internal DataTable GetPostGraduationEducationData()
        {
            DataTable data;
            data = new DataTable();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT PostGraduateEducationId, PostGraduateEducationName FROM PostGraduateEducation"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(data);
                    }
                }
            }

            return data;
        }

        internal DataTable GetCertificationData()
        {
            DataTable data;
            data = new DataTable();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT CertificationId, CertificationName FROM Certification"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(data);
                    }
                }
            }

            return data;
        }

        internal DataTable GetBankListData()
        {
            DataTable data;
            data = new DataTable();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT BankMasterId, BankName FROM BankMaster"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(data);
                    }
                }
            }

            return data;
        }

        internal DataTable GetBankBranchData(int? BankMasterId)
        {
            string strWhereClause = "";
            if (BankMasterId == null)
            { 
                strWhereClause = "";
            }
            else
            {
                strWhereClause = "where BankMasterId=" + BankMasterId;
            }

            DataTable data;
            data = new DataTable();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT BankBranchId, BankBranchName FROM BankBranchMaster " + strWhereClause))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(data);
                    }
                }
            }

            return data;
        }
    }
}
