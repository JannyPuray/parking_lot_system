namespace ParkingLotDesktop;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel topPanel; private Panel sidePanel; private Panel contentPanel; private Label lblBrand; private Label lblUser; private Label lblHeader; private Label lblWelcome; private Label lblStats; private DataGridView grid;
    private Button btnDashboard; private Button btnSlots; private Button btnEntry; private Button btnExit; private Button btnReports; private Button btnRefresh; private Button btnLogout;
    protected override void Dispose(bool disposing){ if(disposing && components != null) components.Dispose(); base.Dispose(disposing); }
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container(); topPanel = new Panel(); sidePanel = new Panel(); contentPanel = new Panel(); lblBrand = new Label(); lblUser = new Label(); lblHeader = new Label(); lblWelcome = new Label(); lblStats = new Label(); grid = new DataGridView();
        btnDashboard = MakeButton("Dashboard"); btnSlots = MakeButton("Parking Slots"); btnEntry = MakeButton("Vehicle Entry"); btnExit = MakeButton("Vehicle Exit"); btnReports = MakeButton("Reports"); btnRefresh = MakeButton("Refresh"); btnLogout = MakeButton("Logout");
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit(); SuspendLayout();
        Text = "Parking Lot System - Desktop App"; WindowState = FormWindowState.Maximized; BackColor = Color.FromArgb(241,245,249); MinimumSize = new Size(1000,650);
        topPanel.Dock = DockStyle.Top; topPanel.Height = 76; topPanel.BackColor = Color.FromArgb(15,23,42);
        lblBrand.Text = "⚡ Parking Lot System"; lblBrand.ForeColor = Color.White; lblBrand.Font = new Font("Segoe UI",18F,FontStyle.Bold); lblBrand.AutoSize = true; lblBrand.Location = new Point(24,20);
        lblUser.ForeColor = Color.White; lblUser.Font = new Font("Segoe UI",10.5F,FontStyle.Bold); lblUser.AutoSize = true; lblUser.Anchor = AnchorStyles.Top | AnchorStyles.Right; lblUser.Location = new Point(780,26); topPanel.Controls.AddRange(new Control[]{lblBrand,lblUser});
        sidePanel.Dock = DockStyle.Left; sidePanel.Width = 220; sidePanel.BackColor = Color.White; sidePanel.Padding = new Padding(16,22,16,16);
        var menu = new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.TopDown,WrapContents=false}; menu.Controls.AddRange(new Control[]{btnDashboard,btnSlots,btnEntry,btnExit,btnReports,btnRefresh,btnLogout}); sidePanel.Controls.Add(menu);
        contentPanel.Dock = DockStyle.Fill; contentPanel.Padding = new Padding(28); contentPanel.BackColor = Color.FromArgb(241,245,249);
        lblHeader.Text = "⚡ Dashboard"; lblHeader.Font = new Font("Segoe UI",28F,FontStyle.Bold); lblHeader.ForeColor = Color.FromArgb(15,23,42); lblHeader.Dock = DockStyle.Top; lblHeader.Height = 58;
        lblWelcome.Font = new Font("Segoe UI",12F); lblWelcome.ForeColor = Color.FromArgb(100,116,139); lblWelcome.Dock = DockStyle.Top; lblWelcome.Height = 34;
        lblStats.Font = new Font("Segoe UI",12F,FontStyle.Bold); lblStats.ForeColor = Color.White; lblStats.BackColor = Color.FromArgb(37,99,235); lblStats.Dock = DockStyle.Top; lblStats.Height = 58; lblStats.Padding = new Padding(18,16,18,10);
        grid.Dock = DockStyle.Fill; grid.ReadOnly = true; grid.AllowUserToAddRows = false; grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect; grid.BackgroundColor = Color.White; grid.BorderStyle = BorderStyle.None; grid.RowHeadersVisible = false; grid.EnableHeadersVisualStyles = false; grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248,250,252); grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(51,65,85); grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI",10F,FontStyle.Bold); grid.DefaultCellStyle.Font = new Font("Segoe UI",10F); grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219,234,254); grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15,23,42);
        contentPanel.Controls.Add(grid); contentPanel.Controls.Add(lblStats); contentPanel.Controls.Add(lblWelcome); contentPanel.Controls.Add(lblHeader); Controls.Add(contentPanel); Controls.Add(sidePanel); Controls.Add(topPanel); ((System.ComponentModel.ISupportInitialize)grid).EndInit(); ResumeLayout(false);
    }
    private Button MakeButton(string text){ var b = new Button(); b.Text = text; b.Width = 185; b.Height = 44; b.Margin = new Padding(0,0,0,12); b.FlatStyle = FlatStyle.Flat; b.FlatAppearance.BorderSize = 0; b.BackColor = Color.FromArgb(239,246,255); b.ForeColor = Color.FromArgb(30,64,175); b.Font = new Font("Segoe UI",10.5F,FontStyle.Bold); return b; }
}
