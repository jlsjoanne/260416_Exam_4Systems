using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _260416_Exam_4Systems.System02_FAQ
{
    public partial class Client_Category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Role"] != null && (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "2"))
            {
                ToMgmt.Visible = true;
            }
            if (!IsPostBack)
            {
                BindCategoryData();
            }
        }

        protected void ToMgmt_Click(object sender, EventArgs e)
        {
            Response.Redirect("Mgmt_Category.aspx");
        }

        protected void CategoryRP_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "ToContent")
            {
                string categoryId = e.CommandArgument.ToString();
                Response.Redirect($"Client_Content.aspx?CategoryId={categoryId}");
            }
        }

        private void BindCategoryData()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string getCategoryQuery = "SELECT C.CategoryId, CategoryName, COALESCE(COUNT(FAQId),0) AS Cnt " +
                "FROM [FAQ_Category] AS C " +
                "LEFT JOIN [FAQ_Content] AS F ON C.CategoryId = F.CategoryId " +
                "WHERE C.IsPublished = 1 AND F.IsPublished = 1 " +
                "GROUP BY C.CategoryId, CategoryName, CategoryOrder " +
                "ORDER BY CategoryOrder ASC";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getCategoryQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        CategoryRP.DataSource = dt;
                        CategoryRP.DataBind();
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