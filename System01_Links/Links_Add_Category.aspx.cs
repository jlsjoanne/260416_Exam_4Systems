using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _260416_Exam_4Systems.System01_Links
{
    public partial class Links_Add_Category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "1")
            {

            }
            else if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("Links_Client.aspx");
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CName.Text))
            {
                Response.Write("<script>alert('分類名稱不得為空');</script>");
                return;
            }
            AddCategory();
            Response.Redirect("Links_Mgmt_Category.aspx");
        }

        private void AddCategory()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string insertCategoryQuery = "INSERT INTO [Link_Category] (CategoryName) VALUES (@CategoryName)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertCategoryQuery, conn))
                {
                    command.Parameters.AddWithValue("@CategoryName", CName.Text);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('分類新增失敗');</script>");
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