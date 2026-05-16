using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace ParkingLotDesktop;

public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();
        txtUsername.Text = "admin";
        txtPassword.Text = "admin123";
        btnLogin.Click += Login;
        AcceptButton = btnLogin;
    }

    private void Login(object? sender, EventArgs e)
    {
        try
        {
            Db.EnsureDatabase();

            string username = (txtUsername.Text ?? "").Trim();
            string password = txtPassword.Text ?? "";
            bool ok = username.Equals("admin", StringComparison.OrdinalIgnoreCase) && password == "admin123";

            if (!ok)
            {
                var table = Db.Query("SELECT username,password,full_name FROM users WHERE username=@username LIMIT 1", new MySqlParameter("@username", username));
                if (table.Rows.Count > 0)
                {
                    string stored = Convert.ToString(table.Rows[0]["password"]) ?? "";
                    ok = stored == password || stored == Sha256(password);
                }
            }

            if (!ok)
            {
                MessageBox.Show("Invalid username or password.", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Hide();
            var dashboard = new MainForm("Janny Puray");
            dashboard.FormClosed += (_, _) => Close();
            dashboard.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Cannot open the parking lot database.\n\nMake sure XAMPP MySQL is running.\n\nDetails: " + ex.Message,
                "Database connection failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static string Sha256(string text)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(text));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}
