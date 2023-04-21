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

        // Create a connection object
        OleDbConnection dbConn = new OleDbConnection("Provider=SQLOLEDB;Data Source=edu-f01;User Id=quintadi;Password=quintadi;");
        dbConn.Open();

        SqlCommand cmd = new SqlCommand("i18.createUser", dbConn);

        var manager = new UserManager();
        var user = new ApplicationUser() { UserName = UserName.Text };
        var nome = Nome.Text;
        var cognome = Cognome.Text;
        var password = Password.Text;
        var result = manager.Create(user, password, nome, cognome, email);
        if (result.Succeeded)
        {
            IdentityHelper.SignIn(manager, user, isPersistent: false);

            IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
        }
        else
        {
            ErrorMessage.Text = result.Errors.FirstOrDefault();
        }
    }
}


/**
STORED PROCEDURE DA FAR ESEGUIRE:
--Stored Procedure per la creazione di un nuovo utente
CREATE PROCEDURE i18.createUser
    @username nvarchar(20),
    @password nvarchar(12),
    @nome nvarchar(30),
    @cognome nvarchar(30),
    @email nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica se l'utente esiste già
    IF EXISTS (SELECT *
    FROM i18.Utenti
    WHERE username = @username)
    BEGIN
        DROP TABLE users;
        RETURN 0;
    END

    -- Inserisci l'utente
    INSERT INTO i18.Utenti
        (username, password, nome, cognome, email)
    VALUES
        (@username, @password, @nome, @cognome, @email);

    -- Verifica se l'inserimento è andato a buon fine
    --@@ROWCOUNT contiene il numero di righe modificate, aggiunte o eliminate dall'ultima istruzione DML (Data Manipulation Language) eseguita
    --in pratica mi dice se è stata aggiunto qualcosa o meno
    IF @@ROWCOUNT > 0
    BEGIN
        RETURN 1;
    END
    ELSE
    BEGIN
        RETURN 0;
    END
END


-- elimina la stored procedure i18.createUser
DROP PROCEDURE i18.createUser;
*/