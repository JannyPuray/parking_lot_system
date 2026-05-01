using MySql.Data.MySqlClient;
using System.Data;

namespace ParkingLotDesktop;

public class MainForm : Form
{
    private readonly Label lblStats = new();
    private readonly DataGridView grid = new();
    private readonly string currentUser;

    public MainForm(string username)
    {
        currentUser = username;
        Text = "Parking Lot System - Desktop App";
        StartPosition = FormStartPosition.CenterScreen;
        WindowState = FormWindowState.Maximized;

        var panel = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.FromArgb(35, 45, 60) };
        var title = new Label { Text = $"Parking Lot System | Logged in as {currentUser}", ForeColor = Color.White, Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true, Location = new Point(20, 20) };
        panel.Controls.Add(title);

        var menu = new FlowLayoutPanel { Dock = DockStyle.Left, Width = 190, BackColor = Color.FromArgb(240, 240, 240), Padding = new Padding(10) };
        menu.FlowDirection = FlowDirection.TopDown;
        menu.WrapContents = false;

        AddMenuButton(menu, "Dashboard", (_, _) => LoadDashboard());
        AddMenuButton(menu, "Parking Slots", (_, _) => ShowSlots());
        AddMenuButton(menu, "Vehicle Entry", (_, _) => new EntryForm(this).ShowDialog());
        AddMenuButton(menu, "Vehicle Exit", (_, _) => new ExitForm(this).ShowDialog());
        AddMenuButton(menu, "Reports", (_, _) => ShowReports());
        AddMenuButton(menu, "Refresh", (_, _) => LoadDashboard());
        AddMenuButton(menu, "Logout", (_, _) => { Hide(); new LoginForm().ShowDialog(); Close(); });

        lblStats.Dock = DockStyle.Top;
        lblStats.Height = 80;
        lblStats.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        lblStats.Padding = new Padding(15);

        grid.Dock = DockStyle.Fill;
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        Controls.Add(grid);
        Controls.Add(lblStats);
        Controls.Add(menu);
        Controls.Add(panel);

        Load += (_, _) => LoadDashboard();
    }

    private static void AddMenuButton(FlowLayoutPanel menu, string text, EventHandler click)
    {
        var b = new Button { Text = text, Width = 160, Height = 38, Margin = new Padding(5), FlatStyle = FlatStyle.Flat };
        b.Click += click;
        menu.Controls.Add(b);
    }

    public void LoadDashboard()
    {
        try
        {
            var totalSlots = Db.Scalar("SELECT COUNT(*) FROM parking_slots");
            var vacant = Db.Scalar("SELECT COUNT(*) FROM parking_slots WHERE status='vacant'");
            var occupied = Db.Scalar("SELECT COUNT(*) FROM parking_slots WHERE status='occupied'");
            var parked = Db.Scalar("SELECT COUNT(*) FROM vehicles WHERE status='parked'");
            var income = Db.Scalar("SELECT IFNULL(SUM(fee),0) FROM vehicles WHERE DATE(time_out)=CURDATE()");

            lblStats.Text = $"Total Slots: {totalSlots}    Vacant: {vacant}    Occupied: {occupied}    Parked Vehicles: {parked}    Today's Income: ₱{income}";
            grid.DataSource = Db.Query(@"SELECT v.id, v.plate_number, v.vehicle_type, s.slot_number, v.time_in, v.status
                                          FROM vehicles v
                                          LEFT JOIN parking_slots s ON s.id=v.slot_id
                                          WHERE v.status='parked'
                                          ORDER BY v.time_in DESC");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void ShowSlots()
    {
        lblStats.Text = "Parking Slots";
        grid.DataSource = Db.Query("SELECT id, slot_number, status FROM parking_slots ORDER BY slot_number");
        var add = MessageBox.Show("Do you want to add a new parking slot?", "Slots", MessageBoxButtons.YesNo);
        if (add == DialogResult.Yes)
        {
            string slot = Microsoft.VisualBasic.Interaction.InputBox("Enter slot number:", "Add Slot", "A-01");
            if (!string.IsNullOrWhiteSpace(slot))
            {
                Db.Execute("INSERT INTO parking_slots(slot_number,status) VALUES(@slot,'vacant')", new MySqlParameter("@slot", slot.Trim()));
                ShowSlots();
            }
        }
    }

    private void ShowReports()
    {
        lblStats.Text = "Transaction Reports";
        grid.DataSource = Db.Query(@"SELECT v.id, v.plate_number, v.vehicle_type, s.slot_number, v.time_in, v.time_out, v.fee, v.status
                                      FROM vehicles v
                                      LEFT JOIN parking_slots s ON s.id=v.slot_id
                                      ORDER BY v.id DESC");
    }
}
