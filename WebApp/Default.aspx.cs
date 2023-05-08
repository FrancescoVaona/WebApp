using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Recupera il nome utente dalla sessione
        string username = (string)Session["Username"];

        // Personalizza la visualizzazione in base al nome utente
        if (!string.IsNullOrEmpty(username))
        {
            // Esegui le operazioni di personalizzazione
            // ad esempio, puoi impostare il testo di un controllo Label
            WelcomeLabel.Text = "Benvenuto, " + username + "!";
        }
    }
}