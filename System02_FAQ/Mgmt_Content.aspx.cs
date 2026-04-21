using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace _260416_Exam_4Systems.System02_FAQ
{
    public partial class Mgmt_Content : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["CategoryId"] != null && (Session["Role"] != null && (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "2")))
            {
                string categoryId = Request.QueryString["CategoryId"];
                GetCategory(categoryId);
                if (!IsPostBack)
                {
                    GetContentData(categoryId);
                }
            }
            else if(Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("Client_Category.aspx");
            }
        }

        protected void GoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Mgmt_Category.aspx");
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            string categoryId = Request.QueryString["CategoryId"];
            Response.Redirect($"Add_Content.aspx?CategoryId={categoryId}");
        }

        protected void FAQGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string categoryId = Request.QueryString["CategoryId"];
            FAQGrid.EditIndex = e.NewEditIndex;
            GetContentData(categoryId);
        }

        protected void FAQGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            string categoryId = Request.QueryString["CategoryId"];
            FAQGrid.EditIndex = -1;
            GetContentData(categoryId);
        }

        protected void FAQGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string categoryId = Request.QueryString["CategoryId"];
            string faqId = FAQGrid.DataKeys[e.RowIndex].Value.ToString();
            DeleteFAQ(faqId);
            e.Cancel = true;
            GetContentData(categoryId);
        }

        protected void FAQGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string categoryId = Request.QueryString["CategoryId"];
            string faqId = FAQGrid.DataKeys[e.RowIndex].Value.ToString();

            GridViewRow row = FAQGrid.Rows[e.RowIndex];
            string qInput = ((TextBox)row.Cells[1].Controls[0]).Text;
            string aInput = ((TextBox)row.Cells[2].Controls[0]).Text;
            bool isTop = ((CheckBox)row.Cells[4].Controls[0]).Checked;
            bool isPublished = ((CheckBox)row.Cells[5].Controls[0]).Checked;
            string orderInput = ((TextBox)row.Cells[6].Controls[0]).Text;

            if(string.IsNullOrEmpty(qInput) || string.IsNullOrEmpty(aInput))
            {
                Response.Write("<script>alert('問題及答案不得為空');</script>");
                return;
            }
            if (string.IsNullOrEmpty(orderInput))
            {
                UpdateFAQ(faqId, qInput, aInput, isTop, isPublished, null);
            }
            else if (int.TryParse(orderInput,out int orderNum))
            {
                UpdateFAQ(faqId, qInput, aInput, isTop, isPublished, orderNum);
            }
            else
            {
                Response.Write("<script>alert('FAQ排序須為數字');</script>");
                return;
            }

            FAQGrid.EditIndex = -1;
            GetContentData(categoryId);
        }

        private void GetCategory(string categoryId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string getCategoryQuery = "SELECT CategoryName FROM [FAQ_Category] WHERE CategoryId = @CategoryId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getCategoryQuery, conn))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            CName.Text = dr["CategoryName"].ToString();
                            
                        }
                        dr.Close();

                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                }
            }
        }

        private void GetContentData(string categoryId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string getContentQuery = "SELECT * FROM [FAQ_Content] WHERE CategoryId = @CategoryId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getContentQuery, conn))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);

                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        FAQGrid.DataSource = dt;
                        FAQGrid.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                }
            }
        }

        private void DeleteFAQ(string faqId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string deleteFAQQuery = "DELETE FROM [FAQ_Content] WHERE FAQId = @FAQId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteFAQQuery, conn))
                {
                    command.Parameters.AddWithValue("@FAQId", faqId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Response.Write("<script>alert('FAQ刪除失敗');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                }
            }
        }

        private void UpdateFAQ(string faqId, string qInput, string aInput, bool isTop, bool isPublished, int? orderNum)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string updateFAQQuery = "UPDATE [FAQ_Content] " +
                "SET Question = @Question , Answer = @Answer, IsTop = @IsTop, IsPublished = @IsPublished, FAQOrder = @FAQOrder " +
                "WHERE FAQId = @FAQId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updateFAQQuery, conn))
                {
                    command.Parameters.AddWithValue("@Question", qInput);
                    command.Parameters.AddWithValue("@Answer", aInput);
                    command.Parameters.AddWithValue("@IsTop", isTop);
                    command.Parameters.AddWithValue("@IsPublished", isPublished);
                    command.Parameters.AddWithValue("@FAQOrder", orderNum);
                    command.Parameters.AddWithValue("@FAQId", faqId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Response.Write("<script>alert('FAQ更新失敗');</script>");
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