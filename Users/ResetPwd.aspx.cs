using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _260416_Exam_4Systems.Users
{
    public partial class ResetPwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["LoginStatus"] != null && Session["LoginStatus"].ToString() == "true")
            {

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

        protected void Submit_Click(object sender, EventArgs e)
        {
            string username = Session["Username"].ToString();
            string storedHash = GetStoredHash(username);
            if (!string.IsNullOrEmpty(storedHash) && SecurityHelper.VerifyPassword(OldPwd.Text, storedHash))
            {
                if(NewPwd.Text == ConfirmNewPwd.Text)
                {
                    UpdatePwd(username);
                    Response.Redirect("UserInfo.aspx");
                }
                else
                {
                    Response.Write("<script>alert('新密碼輸入不一致');</script>");
                    return;
                }
            }
            else
            {
                Response.Write("<script>alert('原密碼錯誤');</script>");
                return;
            }
        }

        private string GetStoredHash(string username)
        {
            string storedHash = "";
            string connectionString = WebConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
            string getPwdQuery = "SELECT PasswordHash FROM [UsersData] WHERE UserName = @UserName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getPwdQuery, conn))
                {
                    command.Parameters.AddWithValue("@UserName", username);

                    try
                    {
                        conn.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            storedHash = result.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                }
            }

            return storedHash;
        }

        private void UpdatePwd(string username)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
            string updatePwdQuery = "UPDATE [UsersData] " +
                "SET PasswordHash = @PasswordHash " +
                "WHERE UserName = @UserName";

            string hashedPwd = SecurityHelper.HashPassword(NewPwd.Text);

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updatePwdQuery, conn))
                {
                    command.Parameters.AddWithValue("@PasswordHash", hashedPwd);
                    command.Parameters.AddWithValue("@UserName", username);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('密碼更新失敗');</script>");
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