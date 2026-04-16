using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _260416_Exam_4Systems
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["LoginStatus"] != null && Session["LoginStatus"].ToString() == "true")
            {
                Username.Text = Session["Username"].ToString();
                LogIn.Visible = false;
                SignUp.Visible = false;
                LogOut.Visible = true;
            }
        }

        protected void LogIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Users/LogIn.aspx");
        }

        protected void SignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Users/SignUp.aspx");
        }

        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Session["LoginStatus"] = null;
            Session["Role"] = null;
            Response.Redirect("Default.aspx");
        }
    }
}