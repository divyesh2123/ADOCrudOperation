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

        public bool AddUpdateEmployee(EmpModel empModel)
        {
            string spName = "AddNewEmpDetails";

            if(empModel.Empid >0)
            {
                spName = "UpdateEmpDetails";
            }

            SqlCommand com = new SqlCommand(spName, con);
            com.CommandType = CommandType.StoredProcedure;

            if(empModel.Empid > 0)
            {
                com.Parameters.AddWithValue("@EmpId", empModel.Empid);
            }

            com.Parameters.AddWithValue("@Name", empModel.Name);
            com.Parameters.AddWithValue("@City", empModel.City);
            com.Parameters.AddWithValue("@Address", empModel.Address);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }
            
        }


        public EmpModel GetEmployeeByEmployeeId(int employeeId)
        {
            EmpModel empModel = new EmpModel(); 

            SqlCommand com = new SqlCommand("GetEmployeeByID", con);
            com.Parameters.AddWithValue("@EmployeeID", employeeId);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);

            empModel.Empid = Convert.ToInt32(dt.Rows[0]["Id"]);
            empModel.Name = Convert.ToString(dt.Rows[0]["Name"]);
            empModel.City = Convert.ToString(dt.Rows[0]["City"]);
            empModel.Address = Convert.ToString(dt.Rows[0]["Address"]);


            return empModel;

        }

        public bool DeleteEmployee(int employeeId)
        {
            SqlCommand com = new SqlCommand("DeleteEmpById", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@EmpId", employeeId);
           
            con.Open();

            int i = com.ExecuteNonQuery();

            if (i > 0)
                return true;
            else
                return false;


        }
       
    }
}