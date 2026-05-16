namespace ParkingLotDesktop;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Button btnLogin;
    private Label titleLabel;
    private Label subLabel;
    private Panel cardPanel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        cardPanel = new Panel();
        titleLabel = new Label();
        subLabel = new Label();
        txtUsername = new TextBox();
        txtPassword = new TextBox();
        btnLogin = new Button();
        cardPanel.SuspendLayout();
        SuspendLayout();
        // 
        // cardPanel
        // 
        cardPanel.Anchor = AnchorStyles.None;
        cardPanel.BackColor = Color.White;
        cardPanel.Controls.Add(titleLabel);
        cardPanel.Controls.Add(subLabel);
        cardPanel.Controls.Add(txtUsername);
        cardPanel.Controls.Add(txtPassword);
        cardPanel.Controls.Add(btnLogin);
        cardPanel.Location = new Point(92, 41);
        cardPanel.Name = "cardPanel";
        cardPanel.Size = new Size(448, 320);
        cardPanel.TabIndex = 0;
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
        titleLabel.Location = new Point(21, 30);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(382, 46);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "⚡ Parking Lot System";
        // 
        // subLabel
        // 
        subLabel.AutoSize = true;
        subLabel.Font = new Font("Segoe UI", 10F);
        subLabel.ForeColor = Color.FromArgb(100, 116, 139);
        subLabel.Location = new Point(75, 76);
        subLabel.Name = "subLabel";
        subLabel.Size = new Size(270, 23);
        subLabel.TabIndex = 1;
        subLabel.Text = "Admin login • Owner: Janny Puray";
        // 
        // txtUsername
        // 
        txtUsername.Font = new Font("Segoe UI", 11F);
        txtUsername.Location = new Point(90, 125);
        txtUsername.Name = "txtUsername";
        txtUsername.PlaceholderText = "Username";
        txtUsername.Size = new Size(290, 32);
        txtUsername.TabIndex = 2;
        // 
        // txtPassword
        // 
        txtPassword.Font = new Font("Segoe UI", 11F);
        txtPassword.Location = new Point(90, 175);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '●';
        txtPassword.PlaceholderText = "Password";
        txtPassword.Size = new Size(290, 32);
        txtPassword.TabIndex = 3;
        // 
        // btnLogin
        // 
        btnLogin.BackColor = Color.FromArgb(37, 99, 235);
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnLogin.ForeColor = Color.White;
        btnLogin.Location = new Point(90, 230);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(290, 44);
        btnLogin.TabIndex = 4;
        btnLogin.Text = "Login";
        btnLogin.UseVisualStyleBackColor = false;
        // 
        // LoginForm
        // 
        BackColor = Color.Black;
        ClientSize = new Size(597, 413);
        Controls.Add(cardPanel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Parking Lot System - Login";
        cardPanel.ResumeLayout(false);
        cardPanel.PerformLayout();
        ResumeLayout(false);
    }
}
