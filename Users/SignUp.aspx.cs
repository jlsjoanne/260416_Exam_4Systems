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
            if (IsPostBack)
            {
                if(Password.Text == PwdConfirm.Text)
                {
                    CheckPwd.ForeColor = System.Drawing.Color.Green;
                    CheckPwd.Text = "密碼輸入一致";
                }
                else
                {
                    CheckPwd.ForeColor = System.Drawing.Color.Red;
                    CheckPwd.Text = "密碼輸入不一致";
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            // check if password and confirm password are the same
            if(CheckPwd.Text == "密碼輸入一致")
            {
                // check if DB already have this username
                if (!CheckRepeatUsername())
                {
                    // insert user info (username, password) into DB
                    if (RegisterUser())
                    {
                        Response.Write("<script>alert('註冊成功')</script>");
                        Response.Redirect("LogIn.aspx");
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('密碼輸入不一致')</script>");
            }
        }

        private bool CheckRepeatUsername()
        {
            string username = Username.Text.Trim();
            string connectionString = WebConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
            string checkUNameQuery = "SELECT * FROM [UsersData] WHERE UserName = @UserName";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(checkUNameQuery, conn))
                {
                    command.Parameters.AddWithValue("@UserName", username);
                    SqlDataReader dr = null;

                    try
                    {
                        conn.Open();
                        dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Close();
                            Response.Write("<script>alert('帳號已存在')</script>");
                            return true;
                        }
                    }
                    catch(Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                        return true;
                    }
                }
                return false;
            }
        }

        private bool RegisterUser()
        {
            string username = Username.Text.Trim();
            string pwd = Password.Text.Trim();

            string pwdHash = SecurityHelper.HashPassword(pwd);

            string connectionString = WebConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
            string insertUserQuery = "INSERT INTO [UsersData] (UserName, PasswordHash) " +
                "VALUES(@UserName, @PasswordHash)";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(insertUserQuery, conn))
                {
                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@PasswordHash", pwdHash);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('註冊失敗')</script>");
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                        return false;
                    }
                }
            }
            return true;
        }
    }
}