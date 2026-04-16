using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace _260416_Exam_4Systems.Users
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string storedHash = GetStoredHash();
            if(!string.IsNullOrEmpty(storedHash) && SecurityHelper.VerifyPassword(Password.Text, storedHash))
            {
                Session["Username"] = Username.Text;
                Session["LoginStatus"] = "true";
                Session["Role"] = GetRole();
                Response.Redirect("../Default.aspx");
            }
            else
            {
                Response.Write("<script>alert('帳號或密碼錯誤');</script>");
            }
        }

        private string GetStoredHash()
        {
            string storedHash = "";
            
            string connectionString = WebConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
            string getPwdQuery = "SELECT PasswordHash FROM [UsersData] WHERE UserName = @UserName";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getPwdQuery, conn))
                {
                    command.Parameters.AddWithValue("@UserName", Username.Text);

                    try
                    {
                        conn.Open();
                        object result = command.ExecuteScalar();
                        if(result != null)
                        {
                            storedHash = result.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                }
            }

            return storedHash;
        }

        private string GetRole()
        {
            string roleId = "";

            string connectionString = WebConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
            string getRoleQuery = "SELECT RoleId FROM [UsersData] WHERE UserName = @UserName";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getRoleQuery, conn))
                {
                    command.Parameters.AddWithValue("@UserName", Username.Text);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            roleId = dr["RoleId"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                }
            }
            return roleId;
        }

        protected void ToSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignUp.aspx");
        }
    }
}