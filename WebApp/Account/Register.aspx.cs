using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.UI;
using WebApp;
using System.Data.OleDb;
//clausula using per usare le stored procedure
using System.Data.SqlClient;
//clausula using per usare le sqlCommand


public partial class Account_Register : Page
{
    public void CreateUser_Click(object sender, EventArgs e)
    {
        var nome = Nome.Text;
        var cognome = Cognome.Text;
        var password = Password.Text;
        var username = UserName.Text;
        var email = Email.Text;

        string connectionString = "Provider=SQLOLEDB;Data Source=edu-f01;User Id=quintadi;Password=quintadi;"; // sostituire con la stringa di connessione corretta

        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {
            connection.Open();
            OleDbCommand comando = connection.CreateCommand();
            comando.CommandText = $"EXEC i18.createUser @username = '{username}', @password = '{password}', @nome = '{nome}', @cognome = '{cognome}', @email = '{email}'";
            //esegui la lettura di "Comando"
            OleDbDataReader reader = comando.ExecuteReader();

        }
        // Reinizializza i campi del form a vuoti
        Nome.Text = "";
        Cognome.Text = "";
        UserName.Text = "";
        Email.Text = "";
        Password.Text = "";
        ConfirmPassword.Text = "";

    }
}
