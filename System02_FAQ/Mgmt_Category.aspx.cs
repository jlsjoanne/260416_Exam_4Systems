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
    public partial class Mgmt_Category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Role"] != null && (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "2"))
            {
                if(Session["Role"].ToString() == "1")
                {
                    AddCategory.Visible = true;
                    CommandField commandField = (CommandField)CategoryGrid.Columns[0];
                    commandField.ShowDeleteButton = true;
                    commandField.ShowEditButton = true;
                }
                if (!IsPostBack)
                {
                    BindCategoryGrid();
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

        protected void GoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Client_Category.aspx");
        }

        protected void AddCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect("Add_Category.aspx");
        }

        protected void AddFAQ_Click(object sender, EventArgs e)
        {
            Response.Redirect("Add_Content.aspx");
        }

        protected void CategoryGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CategoryGrid.EditIndex = e.NewEditIndex;
            BindCategoryGrid();
        }

        protected void CategoryGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CategoryGrid.EditIndex = -1;
            BindCategoryGrid();
        }

        protected void CategoryGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string categoryId = CategoryGrid.DataKeys[e.RowIndex].Value.ToString();
            DeleteCategory(categoryId);
            e.Cancel = true;
            BindCategoryGrid();
        }

        protected void CategoryGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string categoryId = CategoryGrid.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = CategoryGrid.Rows[e.RowIndex];
            string categoryName = ((TextBox)row.Cells[1].Controls[0]).Text;
            string categoryOrder = ((TextBox)row.Cells[2].Controls[0]).Text;
            bool isPublished = ((CheckBox)row.Cells[3].Controls[0]).Checked;

            if (string.IsNullOrEmpty(categoryName))
            {
                Response.Write("<script>alert('類別名稱不得為空')</script>");
                return;
            }
            if (string.IsNullOrEmpty(categoryOrder))
            {
                UpdateCategory(categoryId, categoryName, null, isPublished);
            }
            else if(int.TryParse(categoryOrder, out int orderNum))
            {
                UpdateCategory(categoryId, categoryName, orderNum, isPublished);
            }
            else
            {
                Response.Write("<script>alert('類別排序須為數字')</script>");
                return;
            }

            CategoryGrid.EditIndex = -1;
            BindCategoryGrid();

        }
        private void BindCategoryGrid()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string getCategoryQuery = "SELECT * FROM [FAQ_Category]";

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

                        CategoryGrid.DataSource = dt;
                        CategoryGrid.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                }
            }
        }

        private void DeleteCategory(string categoryId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string deleteCategoryQuery = "DELETE FROM [FAQ_Category] WHERE CategoryId = @CategoryId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteCategoryQuery, conn))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Response.Write("<script>alert('分類刪除失敗');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                }
            }
        }

        private void UpdateCategory(string categoryId, string categoryName, int? categoryOrder, bool isPublished)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string updateCategoryQuery = "UPDATE [FAQ_Category] " +
                "SET CategoryName = @CategoryName, CategoryOrder = @CategoryOrder, IsPublished = @IsPublished " +
                "WHERE CategoryId = @CategoryId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updateCategoryQuery, conn))
                {
                    command.Parameters.AddWithValue("@CategoryName", categoryName);
                    command.Parameters.AddWithValue("@CategoryOrder", categoryOrder);
                    command.Parameters.AddWithValue("@IsPublished", isPublished);
                    command.Parameters.AddWithValue("@CategoryId", categoryId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Response.Write("<script>alert('分類更新失敗');</script>");
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