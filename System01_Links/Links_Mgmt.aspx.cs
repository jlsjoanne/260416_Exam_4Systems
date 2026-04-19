using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace _260416_Exam_4Systems.System01_Links
{
    public partial class Links_Mgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] != null && (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "2"))
            {
                if(Session["Role"].ToString() == "1")
                {
                    CategoryMgmt.Visible = true;
                }
                if (!IsPostBack)
                {
                    GetCategory();
                    BindLinkGrid();
                }
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

        protected void GoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Links_Client.aspx");
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("Links_Add.aspx");
        }

        protected void CategoryMgmt_Click(object sender, EventArgs e)
        {
            Response.Redirect("Links_Mgmt_Category.aspx");
        }

        protected void CategoryDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = CategoryDrop.SelectedValue;
            BindLinkGrid(selectedCategory);
        }

        protected void LinksGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LinksGrid.EditIndex = e.NewEditIndex;
            BindLinkGrid(CategoryDrop.SelectedValue);
        }

        protected void LinksGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            LinksGrid.EditIndex = -1;
            BindLinkGrid(CategoryDrop.SelectedValue);
        }

        protected void LinksGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string linkId = LinksGrid.DataKeys[e.RowIndex].Value.ToString();
            DeleteFile(linkId);
            DeleteLink(linkId);
            e.Cancel = true;
            BindLinkGrid(CategoryDrop.SelectedValue);
        }

        protected void LinksGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string linkId = LinksGrid.DataKeys[e.RowIndex].Value.ToString();

            GridViewRow row = LinksGrid.Rows[e.RowIndex];
            string linkName = ((TextBox)row.Cells[2].Controls[0]).Text;
            string linkUrl = ((TextBox)row.Cells[3].Controls[0]).Text;
            string linkOrder = ((TextBox)row.Cells[5].Controls[0]).Text;
            bool isPublished = ((CheckBox)row.Cells[7].Controls[0]).Checked;

            if(string.IsNullOrEmpty(linkName) || string.IsNullOrEmpty(linkUrl))
            {
                Response.Write("<script>alert('連結名稱及位址(Url)不得為空');</script>");
                return;
            }
            if (string.IsNullOrEmpty(linkOrder))
            {
                UpdateLink(linkId, linkName, linkUrl, null, isPublished);
            }
            else
            {
                bool isInt = int.TryParse(linkOrder, out int orderNum);
                if (!isInt)
                {
                    Response.Write("<script>alert('排序須為數字或空值');</script>");
                    return;
                }
                UpdateLink(linkId, linkName, linkUrl, orderNum, isPublished);
            }

            LinksGrid.EditIndex = -1;
            BindLinkGrid(CategoryDrop.SelectedValue);
        }

        private void GetCategory()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string getCategoryQuery = "SELECT * FROM [Link_Category] ORDER BY [CategoryOrder] ASC";

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
            CategoryDrop.Items.Insert(0, new ListItem("--分類篩選--", "0"));
        }

        private void BindLinkGrid(string categoryId = "")
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            if(categoryId == "" || categoryId == "0")
            {
                string getLinksQuery = "SELECT LinkId, LinkName, LinkUrl, CategoryName, ImgName, LinkOrder, PostDate, DT.IsPublished " +
                    "FROM [Link_Content] AS DT " +
                    "LEFT JOIN [Link_Category] AS C ON DT.CategoryId = C.CategoryId " +
                    "ORDER BY PostDate DESC";

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    using(SqlCommand command = new SqlCommand(getLinksQuery, conn))
                    {
                        try
                        {
                            conn.Open();
                            SqlDataAdapter da = new SqlDataAdapter(command);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            LinksGrid.DataSource = dt;
                            LinksGrid.DataBind();
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
                string getLinksQuery = "SELECT LinkId, LinkName, LinkUrl, CategoryName, ImgName, LinkOrder, PostDate, DT.IsPublished " +
                    "FROM [Link_Content] AS DT " +
                    "LEFT JOIN [Link_Category] AS C ON DT.CategoryId = C.CategoryId " +
                    "WHERE DT.CategoryId = @CategoryId " +
                    "ORDER BY PostDate DESC";

                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    using(SqlCommand command = new SqlCommand(getLinksQuery, conn))
                    {
                        command.Parameters.AddWithValue("@CategoryId", categoryId);

                        try
                        {
                            conn.Open();
                            SqlDataAdapter da = new SqlDataAdapter(command);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            LinksGrid.DataSource = dt;
                            LinksGrid.DataBind();
                        }
                        catch (Exception ex)
                        {
                            Response.Write($"<script>alert('{ex.Message}')</script>");
                        }
                    }
                }
            }
        }

        private void DeleteFile(string linkId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string getImgQuery = "SELECT ImgName FROM [Link_Content] WHERE LinkId = @LinkId";
            string imgFilename = "";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getImgQuery, conn))
                {
                    command.Parameters.AddWithValue("@LinkId", linkId);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            imgFilename = dr["ImgName"].ToString();
                        }
                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                }
            }

            if (!string.IsNullOrEmpty(imgFilename))
            {
                string folderPath = Server.MapPath("~/Images/");
                string filePath = Path.Combine(folderPath, imgFilename);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        private void DeleteLink(string linkId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string deleteLinkQuery = "DELETE FROM [Link_Content] WHERE LinkId = @LinkId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(deleteLinkQuery, conn))
                {
                    command.Parameters.AddWithValue("@LinkId", linkId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Response.Write("<script>alert('連結刪除失敗');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                }
            }
        }

        private void UpdateLink(string linkId, string linkName, string linkUrl, int? linkOrder, bool isPublished)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string updateLinkQuery = "UPDATE [Link_Content] " +
                "SET LinkName = @LinkName, LinkUrl = @LinkUrl, LinkOrder = @LinkOrder, IsPublished = @IsPublished " +
                "WHERE LinkId = @LinkId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updateLinkQuery, conn))
                {
                    command.Parameters.AddWithValue("@LinkName", linkName);
                    command.Parameters.AddWithValue("@LinkUrl", linkUrl);
                    command.Parameters.AddWithValue("@LinkOrder", linkOrder);
                    command.Parameters.AddWithValue("@IsPublished", isPublished);
                    command.Parameters.AddWithValue("@LinkId", linkId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Response.Write("<script>alert('連結更新失敗');</script>");
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