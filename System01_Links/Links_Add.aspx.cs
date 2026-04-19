using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace _260416_Exam_4Systems.System01_Links
{
    public partial class Links_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] != null && (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "2"))
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
            if(string.IsNullOrEmpty(LName.Text) || string.IsNullOrEmpty(LUrl.Text))
            {
                Response.Write("<script>alert('連結名稱及位址不得為空');</script>");
                return;
            }
            if (!ImgUpload.HasFile)
            {
                Response.Write("<script>alert('無上傳連結圖檔');</script>");
                return;
            }

            AddLinks();
            Response.Redirect("Links_Mgmt.aspx");
        }

        private void AddLinks()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["SystemsDB"].ConnectionString;
            string insertLinkQuery = "INSERT INTO [Link_Content] (LinkName, LinkUrl, ImgName, CategoryId) " +
                "VALUES (@LinkName, @LinkUrl, @ImgName, @CategoryId)";

            // store img file to Images folder
            string folderPath = Server.MapPath("~/Images/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUpload.FileName);
            ImgUpload.SaveAs(Path.Combine(folderPath, fileName));

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertLinkQuery, conn))
                {
                    command.Parameters.AddWithValue("@LinkName", LName.Text);
                    command.Parameters.AddWithValue("@LinkUrl", LUrl.Text);
                    command.Parameters.AddWithValue("@ImgName", fileName);
                    command.Parameters.AddWithValue("@CategoryId", CategoryDrop.SelectedValue);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                        {
                            Response.Write("<script>alert('新增連結失敗');</script>");
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