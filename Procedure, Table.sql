CREATE PROCEDURE i18.CreateUser
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
        -- Se l'utente esiste già, restituisci 0 e interrompi l'esecuzione della stored procedure
        RETURN 0;
    END

    -- Se l'utente non esiste già, inseriscilo nella tabella i18.Utenti
    INSERT INTO i18.Utenti
        (username, password, nome, cognome, email)
    VALUES
        (@username, @password, @nome, @cognome, @email);

    -- Controlla se l'inserimento è andato a buon fine
    IF @@ROWCOUNT > 0
    BEGIN
        -- Se l'inserimento è andato a buon fine, restituisci 1
        RETURN 1;
    END
    ELSE
    BEGIN
        -- Se l'inserimento non è andato a buon fine, restituisci 0
        RETURN 0;
    END
END


CREATE TABLE i18.Utenti
(
    Username nvarchar(20) NOT NULL PRIMARY KEY,
    Password nvarchar(12) NOT NULL,
    Nome nvarchar(30) NOT NULL,
    Cognome nvarchar(30) NOT NULL,
    Email nvarchar(50) NOT NULL,
    isAdministrator bit DEFAULT 0,
    isSuspended bit DEFAULT 0
);

---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------

--creazione stored procedure per il login(return 0 se è sbagliato e 1 se è corretto)
CREATE PROCEDURE i18.loginUser
    @username nvarchar(20),
    @password nvarchar(12)
AS
BEGIN
    SET NOCOUNT ON;
    -- Verifica se l'utente esiste già
    IF EXISTS (SELECT *
    FROM i18.Utenti
    WHERE username = @username AND password = @password AND isAdministrator = 1)
BEGIN
        SELECT 3;
        RETURN;
    END
ELSE IF EXISTS (SELECT *
    FROM i18.Utenti
    WHERE username = @username AND password = @password AND isSuspended = 1)
BEGIN
        SELECT 4;
        RETURN;
    END
ELSE IF EXISTS (SELECT *
    FROM i18.Utenti
    WHERE username = @username AND password = @password)
BEGIN
        SELECT 1;
    END
ELSE
BEGIN
        SELECT 0;
        RETURN;
    END

END
GO
