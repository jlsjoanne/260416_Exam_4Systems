using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _260416_Exam_4Systems.Users
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginStatus"] != null && Session["LoginStatus"].ToString() == "true")
            {
                Username.Visible = true;
                PwdReset.Visible = true;
                LogOut.Visible = true;
                if (Session["Role"] != null && Session["Role"].ToString() == "1")
                {
                    Management.Visible = true;
                }

                if (!IsPostBack)
                {
                    Username.Text += Session["Username"].ToString();
                    GetRolename(Session["Role"].ToString());
                }
            }
            else
            {
                Role.Text += "尚未登入";
            }
        }

        protected void GoHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void PwdReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResetPwd.aspx");
        }

        protected void Management_Click(object sender, EventArgs e)
        {
            Response.Redirect("Mgmt.aspx");
        }

        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Session["LoginStatus"] = null;
            Session["Role"] = null;
            Response.Redirect("../Default.aspx");
        }

        private void GetRolename(string roleId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
            string getRoleQuery = "SELECT RoleName FROM [Roles] WHERE RoleId = @RoleId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getRoleQuery, conn))
                {
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            Role.Text += dr["RoleName"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                }
            }
        }
    }
}