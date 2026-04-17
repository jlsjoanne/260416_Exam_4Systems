using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _260416_Exam_4Systems.Users
{
    public partial class Mgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Role"] != null && Session["Role"].ToString() == "1")
            {
                if (!IsPostBack)
                {
                    BindUserGrid();
                }
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

        private void BindUserGrid()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
            string getUserQuery = "SELECT UserId, UserName, U.RoleId, CreatedAt, RoleName " +
                "FROM [UsersData] AS U " +
                "LEFT JOIN [Roles] AS R ON U.RoleId = R.RoleId " +
                "ORDER BY U.RoleId ASC, CreatedAt DESC";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(getUserQuery, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        UserGrid.DataSource = dt;
                        UserGrid.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                }
            }
        }

        protected void UserGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            UserGrid.EditIndex = e.NewEditIndex;
            BindUserGrid();
        }

        protected void UserGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            UserGrid.EditIndex = -1;
            BindUserGrid();
        }

        protected void UserGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string userId = UserGrid.DataKeys[e.RowIndex].Value.ToString();

            GridViewRow row = UserGrid.Rows[e.RowIndex];
            string username = ((TextBox)row.Cells[1].Controls[0]).Text;
            string pwd = ((TextBox)row.FindControl("TBPwd")).Text;
            string roleId = ((TextBox)row.Cells[3].Controls[0]).Text;

            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pwd))
            {
                Response.Write("<script>alert('帳號或密碼不得為空');</script>");
                return;
            }
            if(roleId == "1" || roleId == "2" || roleId == "3")
            {
                UpdateUser(userId, username, pwd, roleId);
                UserGrid.EditIndex = -1;
                BindUserGrid();
            }
            else
            {
                Response.Write("<script>alert('權限代號輸入錯誤');</script>");
                return;
            }

            
        }

        protected void UserGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string userId = UserGrid.DataKeys[e.RowIndex].Value.ToString();
            DeleteUser(userId);
            e.Cancel = true;
            BindUserGrid();
        }

        private void DeleteUser(string userId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
            string deleteUserQuery = "DELETE FROM [UsersData] WHERE UserId = @UserId";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteUserQuery, conn))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('帳號刪除失敗');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"<script>alert('{ex.Message}')</script>");
                    }
                }
            }
        }

        private void UpdateUser(string userId, string username, string pwd, string roleId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
            string updateUserQuery = "UPDATE [UsersData] " +
                "SET UserName = @UserName, PasswordHash = @PasswordHash, RoleId = @RoleId " +
                "WHERE UserId = @UserId";

            string hashPwd = SecurityHelper.HashPassword(pwd);

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand(updateUserQuery, conn))
                {
                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@PasswordHash",hashPwd);
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    command.Parameters.AddWithValue("@UserId",userId);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if(result < 0)
                        {
                            Response.Write("<script>alert('帳號更新失敗');</script>");
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