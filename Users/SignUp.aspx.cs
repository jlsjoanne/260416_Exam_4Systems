using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace _260416_Exam_4Systems.Users
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Username.Text) || string.IsNullOrEmpty(Password.Text))
            {
                Response.Write("<script>alert('帳號或密碼不得為空');</script>");
                return;
            }
            if(Password.Text != PwdConfirm.Text)
            {
                Response.Write("<script>alert('密碼輸入不一致');</script>");
                return;
            }

            if (checkDuplicate())
            {
                Response.Write("<script>alert('帳號已存在');</script>");
                return;
            }
            else
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
                string registerQuery = "INSERT INTO [UsersData] (UserName, PasswordHash) " +
                "VALUES (@UserName, @PasswordHash)";

                string hashPwd = SecurityHelper.HashPassword(Password.Text);

                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(registerQuery, conn);

                command.Parameters.AddWithValue("@UserName", Username.Text);
                command.Parameters.AddWithValue("@PasswordHash", hashPwd);

                try
                {
                    conn.Open();
                    int result = command.ExecuteNonQuery();

                    command.Cancel();
                    conn.Close();

                    if(result < 0)
                    {
                        Response.Write("<script>alert('註冊失敗');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('註冊成功')</scrpipt>");
                        Response.Redirect("LogIn.aspx");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('{ex.Message}')</script>");
                }
            }
        }

        private bool checkDuplicate()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
            string checkQuery = "SELECT * FROM [UsersData] WHERE UserName = @UserName";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(checkQuery, conn))
                {
                    command.Parameters.AddWithValue("@UserName", Username.Text);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Close();
                            conn.Close();
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                        conn.Close();
                        return true;
                    }
                }
            }
            return false;
        }

        private void RegisterUser()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
            string registerQuery = "INSERT INTO [UsersData] (UserName, PasswordHash) " +
                "VALUES (@UserName, @PasswordHash)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(registerQuery, conn))
                {
                    string pwdHash = SecurityHelper.HashPassword(Password.Text);

                    command.Parameters.AddWithValue("@UserName", Username.Text);
                    command.Parameters.AddWithValue("@PasswordHash", pwdHash);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('註冊失敗')</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('註冊成功')</scrpipt>");
                            command.Cancel();
                            conn.Close();
                            Response.Redirect("LogIn.aspx");
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