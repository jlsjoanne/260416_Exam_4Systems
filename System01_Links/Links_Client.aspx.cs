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
    public partial class Links_Client : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] != null && (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "2"))
            {
                Mgmt.Visible = true;
            }
            if (!IsPostBack)
            {
                GetCategoryDrop();
                BindCategoryRepeater();
            }
        }

        protected void Mgmt_Click(object sender, EventArgs e)
        {
            Response.Redirect("Links_Mgmt.aspx");
        }

        private void GetCategoryDrop()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string getCategoryQuery = "SELECT * FROM [Link_Category] WHERE IsPublished = 1 ORDER BY [CategoryOrder] ASC";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getCategoryQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        CategoryDrop.DataSource = dt;
                        CategoryDrop.DataTextField = "CategoryName";
                        CategoryDrop.DataValueField = "CategoryId";
                        CategoryDrop.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                }
            }
            CategoryDrop.Items.Insert(0, new ListItem("--分類篩選--", "0"));
        }

        private void BindCategoryRepeater(string categoryId = "")
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            if (string.IsNullOrEmpty(categoryId) || categoryId == "0")
            {
                string getAllCategoryQuery = "SELECT * FROM [Link_Category] WHERE IsPublished = 1 ORDER BY CategoryOrder ASC";
                
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    using(SqlCommand command = new SqlCommand(getAllCategoryQuery, conn))
                    {
                        try
                        {
                            conn.Open();
                            SqlDataAdapter da = new SqlDataAdapter(command);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            RPLinks.DataSource = dt;
                            RPLinks.DataBind();
                        }
                        catch (Exception ex)
                        {
                            Response.Write($"<script>alert('{ex.Message}')</script>");
                        }
                    }
                }
            }
            else
            {
                string getCategoryQuery = "SELECT * FROM [Link_Category] WHERE CategoryId = @CategoryId";

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    using(SqlCommand command = new SqlCommand(getCategoryQuery, conn))
                    {
                        command.Parameters.AddWithValue("@CategoryId", categoryId);

                        try
                        {
                            conn.Open();
                            SqlDataAdapter da = new SqlDataAdapter(command);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            RPLinks.DataSource = dt;
                            RPLinks.DataBind();
                        }
                        catch (Exception ex)
                        {
                            Response.Write($"<script>alert('{ex.Message}')</script>");
                        }
                    }
                }
            }
        }

        protected void CategoryDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoryId = CategoryDrop.SelectedValue;
            BindCategoryRepeater(categoryId);
        }

        protected void RPLinks_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string categoryId = DataBinder.Eval(e.Item.DataItem, "CategoryId").ToString();
            ListView linksListView = (ListView)e.Item.FindControl("LVLinks");

            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string getLinksQuery = "SELECT * FROM [Link_Content] " +
                "WHERE CategoryId = @CategoryId AND IsPublished = 1 " +
                "ORDER BY LinkOrder ASC, PostDate DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getLinksQuery, conn))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);

                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        linksListView.DataSource = dt;
                        linksListView.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                }
            }
        }

        protected void LVLinks_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if(e.CommandName == "ToUrl")
            {
                string linkUrl = e.CommandArgument.ToString();
                Response.Redirect(linkUrl);
            }
        }
    }
}