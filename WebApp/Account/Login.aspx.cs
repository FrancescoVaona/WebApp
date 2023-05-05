using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.UI;
using WebApp;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using System.Windows.Forms;
//clausula using per usare le stored procedure
using System.Data.SqlClient;
//clausula using per usare le sqlCommand


public partial class Account_Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterHyperLink.NavigateUrl = "Register";
        OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
        var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        if (!String.IsNullOrEmpty(returnUrl))
        {
            RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
        }
    }

    protected void LogIn(object sender, EventArgs e)
    {

        var password = Password.Text;
        var username = UserName.Text;

        string connectionString = "Provider=SQLOLEDB;Data Source=edu-f01;User Id=quintadi;Password=quintadi;"; // sostituire con la stringa di connessione corretta

        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            connection.Open();
            OleDbCommand comando = connection.CreateCommand();
            comando.CommandText = $"EXEC i18.loginUser @username = '{username}', @password = '{password}'";
            //esegui la lettura di "Comando"
            OleDbDataReader reader = comando.ExecuteReader();
            //chiusura del data reader
            reader.Close();
            int result = (int)comando.ExecuteScalar();

            //se login fallito
            if (result == 0)
            {
                // Fai qualcosa se il risultato è 1

            }
            //se login effettuato
            else if (result == 1)
            {
                UserName.Text = "dasdsa";
                Response.Redirect("~/Default.aspx");
                // Fai qualcos'altro se il risultato è 0
            }
            //se sospeso
            else if (result == 2)
            {
                // mostra una finestra a comprarsa che dice che l'utente è sospeso

            }
            //se amministratore
            else if (result == 3)
            {
                // Fai qualcos'altro se il risultato è 0
            }
        }
        // Reinizializza i campi del form a vuoti
        UserName.Text = "";
        Password.Text = "";


    }
}