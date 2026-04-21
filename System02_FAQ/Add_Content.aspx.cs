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
    public partial class Add_Content : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] != null && (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "2"))
            {
                if (!IsPostBack)
                {
                    GetCategoryData();
                    if (Request.QueryString["CategoryId"] != null)
                    {
                        CategoryDrop.SelectedValue = Request.QueryString["CategoryId"];
                    }
                }
            }
            else if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("Client_Category.aspx");
            }
        }

        private void GetCategoryData()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string getCategoryQuery = "SELECT * FROM [FAQ_Category] ORDER BY CategoryOrder ASC";

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
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(QInput.Text) || string.IsNullOrEmpty(AInput.Text))
            {
                Response.Write("<script>alert('問題與答案輸入值不得為空');</script>");
                return;
            }
            AddFAQData();
            string categoryId = CategoryDrop.SelectedValue;
            Response.Redirect($"Mgmt_Content.aspx?CategoryId={categoryId}");
        }

        private void AddFAQData()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string addFAQQuery = "INSERT INTO [FAQ_Content] (Question, Answer, CategoryId) " +
                "VALUES (@Question, @Answer, @CategoryId)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(addFAQQuery, conn))
                {
                    command.Parameters.AddWithValue("@Question", QInput.Text);
                    command.Parameters.AddWithValue("@Answer", AInput.Text);
                    command.Parameters.AddWithValue("@CategoryId", CategoryDrop.SelectedValue);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Response.Write("<script>alert('新增FAQ失敗');</script>");
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