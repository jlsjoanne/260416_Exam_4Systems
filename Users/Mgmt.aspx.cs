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
    public partial class Mgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Role"] != null && Session["Role"].ToString() == "1")
            {
                if (!IsPostBack)
                {
                    BindUserGrid();
                }
            }
            else if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }

        private void BindUserGrid()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
            string getUserQuery = "SELECT UserId, UserName, U.RoleId, CreatedAt, RoleName " +
                "FROM [UsersData] AS U " +
                "LEFT JOIN [Roles] AS R ON U.RoleId = R.RoleId " +
                "ORDER BY U.RoleId ASC, CreatedAt DESC";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getUserQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        UserGrid.DataSource = dt;
                        UserGrid.DataBind();
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