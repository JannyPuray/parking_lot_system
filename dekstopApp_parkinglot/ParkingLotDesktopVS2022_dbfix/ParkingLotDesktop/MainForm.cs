namespace ParkingLotDesktop;

public partial class MainForm : Form
{
    private readonly string currentUser;

    public MainForm(string username)
    {
        currentUser = string.IsNullOrWhiteSpace(username) ? "Janny Puray" : username;
        InitializeComponent();
        lblUser.Text = "Owner: Janny Puray";
        lblWelcome.Text = "Welcome back, Janny Puray";

        btnDashboard.Click += (_, _) => LoadDashboard();
        btnSlots.Click += (_, _) => ShowSlots();
        btnEntry.Click += (_, _) => new EntryForm(this).ShowDialog();
        btnExit.Click += (_, _) => new ExitForm(this).ShowDialog();
        btnReports.Click += (_, _) => ShowReports();
        btnRefresh.Click += (_, _) => LoadDashboard();
        btnLogout.Click += (_, _) => Close();
        Load += (_, _) => LoadDashboard();
    }

    public void LoadDashboard()
    {
        try
        {
            Db.EnsureDatabase();
            var total = Db.Scalar("SELECT COUNT(*) FROM parking_slots") ?? 0;
            var vacant = Db.Scalar("SELECT COUNT(*) FROM parking_slots WHERE status='vacant'") ?? 0;
            var occupied = Db.Scalar("SELECT COUNT(*) FROM parking_slots WHERE status='occupied'") ?? 0;
            var income = Db.Scalar("SELECT IFNULL(SUM(fee),0) FROM vehicles WHERE DATE(time_out)=CURDATE()") ?? 0;

            lblHeader.Text = "⚡ Dashboard";
            lblWelcome.Text = "Welcome back, Janny Puray";
            lblStats.Text = $"Total Slots: {total}     Vacant: {vacant}     Occupied: {occupied}     Today's Income: ₱{Convert.ToDecimal(income):N2}";
            grid.DataSource = Db.Query(@"SELECT v.plate_number AS Plate, v.vehicle_type AS Type, s.slot_number AS Slot, v.time_in AS Time_In, v.time_out AS Time_Out, v.fee AS Fee, v.status AS Status FROM vehicles v LEFT JOIN parking_slots s ON s.id=v.slot_id ORDER BY v.id DESC LIMIT 20");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Dashboard failed to load.\n\n" + ex.Message, "Parking Lot System", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ShowSlots()
    {
        try
        {
            lblHeader.Text = "⚡ Parking Slots";
            lblWelcome.Text = "Manage live slot availability";
            lblStats.Text = "Desktop app is connected to the same MySQL database as the web system.";
            grid.DataSource = Db.Query("SELECT id, slot_number, status FROM parking_slots ORDER BY slot_number");
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Parking Slots"); }
    }

    private void ShowReports()
    {
        try
        {
            lblHeader.Text = "⚡ Reports";
            lblWelcome.Text = "All parking transactions";
            lblStats.Text = "Connected to the same MySQL database as the web system.";
            grid.DataSource = Db.Query(@"SELECT v.id, v.plate_number, v.vehicle_type, s.slot_number, v.time_in, v.time_out, v.fee, v.status FROM vehicles v LEFT JOIN parking_slots s ON s.id=v.slot_id ORDER BY v.id DESC");
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Reports"); }
    }
}
