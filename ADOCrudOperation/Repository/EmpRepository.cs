using ADOCrudOperation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ADOCrudOperation.Repository
{
    public class EmpRepository
    {
      public  SqlConnection con;

        public EmpRepository()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);
        }

        public List<EmpModel> GetAllEmployee()
        {
            List<EmpModel> EmpList = new List<EmpModel>();

            SqlCommand com = new SqlCommand("Select * from Employee", con);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

       

            foreach (DataRow dr in dt.Rows)
            {

                EmpList.Add(

                    new EmpModel
                    {

                        Empid = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        City = Convert.ToString(dr["City"]),
                        Address = Convert.ToString(dr["Address"])

                    }
                    );
            }

            return EmpList;

        }

       
    }
}