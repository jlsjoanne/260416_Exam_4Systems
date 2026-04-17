using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _260416_Exam_4Systems.System01_Links
{
    public partial class Links_Client : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] != null && (Session["Role"].ToString() == "1" || Session["Role"].ToString() == "2"))
            {
                Mgmt.Visible = true;
            }
            if (!IsPostBack)
            {

            }
        }

        protected void Mgmt_Click(object sender, EventArgs e)
        {
            Response.Redirect("Links_Mgmt.aspx");
        }
    }
}