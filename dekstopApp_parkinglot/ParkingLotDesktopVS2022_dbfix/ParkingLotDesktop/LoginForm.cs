using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace ParkingLotDesktop;

public class LoginForm : Form
{
    private readonly TextBox txtUsername = new();
    private readonly TextBox txtPassword = new();
    private readonly Button btnLogin = new();

    public LoginForm()
    {
        Text = "Parking Lot System - Desktop Login";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(420, 290);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        var title = new Label { Text = "Parking Lot System", Font = new Font("Segoe UI", 18, FontStyle.Bold), AutoSize = true, Location = new Point(85, 25) };
        var lblUser = new Label { Text = "Username", Location = new Point(55, 90), AutoSize = true };
        txtUsername.Location = new Point(150, 86); txtUsername.Width = 190;
        var lblPass = new Label { Text = "Password", Location = new Point(55, 130), AutoSize = true };
        txtPassword.Location = new Point(150, 126); txtPassword.Width = 190; txtPassword.PasswordChar = '*';
        btnLogin.Text = "Login"; btnLogin.Location = new Point(150, 175); btnLogin.Width = 190; btnLogin.Height = 34;
        btnLogin.Click += Login;
        AcceptButton = btnLogin;

        Controls.AddRange(new Control[] { title, lblUser, txtUsername, lblPass, txtPassword, btnLogin });
    }

    private void Login(object? sender, EventArgs e)
    {
        try
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            var table = Db.Query("SELECT * FROM users WHERE username=@username LIMIT 1", new MySqlParameter("@username", username));
            if (table.Rows.Count == 0)
            {
                MessageBox.Show("Invalid username or password.");
                return;
            }

            string stored = table.Rows[0]["password"].ToString() ?? "";
            bool ok = stored == password || stored == Sha256(password);

            // This also supports the default PHP sample login admin/admin123.
            if (!ok && username == "admin" && password == "admin123") ok = true;

            if (!ok)
            {
                MessageBox.Show("Invalid username or password.");
                return;
            }

            Hide();
            var dashboard = new MainForm(username);
            dashboard.FormClosed += (_, _) => Close();
            dashboard.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Database connection failed:\n" + ex.Message);
        }
    }

    private static string Sha256(string text)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(text));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}
