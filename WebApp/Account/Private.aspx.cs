using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String user = (String)HttpContext.Current.Session["UserLogged"];
        if (user != null)
        {
            // Fai qualcosa con user (è una stringa)
        }
        else
        {
            Response.Redirect("/");
        }
    }
}