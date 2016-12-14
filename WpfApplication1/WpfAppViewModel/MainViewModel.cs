using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using System.Data.SqlClient;

namespace WpfAppViewModel
{
    public class MainViewModel
    {
        //Get the default MemoryCache
        ObjectCache cache = MemoryCache.Default;

        public DataTable GetDataFromDataTable()
        {
            DataTable data;

            if (cache.GetCount() == 0)
            {
                //Create a custom Timeout of 10 seconds
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(30.0);

                data = BuildDataTable();

                //add the object to the cache
                cache.Add("data", data, policy);
            }
            else
            {
                data = (DataTable)cache.Get("data");
            }

            return data;
        }

        private static DataTable BuildDataTable()
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


            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT CountryId, CountryName FROM Country"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        DataTable data = new DataTable();
                        sda.Fill(data);
                    }
                }
            }
        }
    }
}
