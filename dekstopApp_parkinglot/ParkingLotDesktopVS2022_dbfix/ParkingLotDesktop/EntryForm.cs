using MySql.Data.MySqlClient;

namespace ParkingLotDesktop;

public partial class EntryForm : Form
{
    private readonly MainForm main;
    public EntryForm(MainForm parent)
    {
        main = parent;
        InitializeComponent();
        cboType.Items.AddRange(new object[] { "Car", "Motorcycle", "Van", "Truck" });
        cboType.SelectedIndex = 0;
        btnSave.Click += SaveEntry;
        Load += (_, _) => LoadSlots();
    }
    private void LoadSlots()
    {
        var slots = Db.Query("SELECT id, slot_number FROM parking_slots WHERE status='vacant' ORDER BY slot_number");
        cboSlot.DataSource = slots; cboSlot.DisplayMember = "slot_number"; cboSlot.ValueMember = "id";
    }
    private void SaveEntry(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPlate.Text) || cboSlot.SelectedValue == null) { MessageBox.Show("Please complete all fields."); return; }
        int slotId = Convert.ToInt32(cboSlot.SelectedValue);
        Db.Execute(@"INSERT INTO vehicles(plate_number, vehicle_type, slot_id, time_in, status) VALUES(@plate,@type,@slot,NOW(),'parked')", new MySqlParameter("@plate", txtPlate.Text.Trim().ToUpper()), new MySqlParameter("@type", cboType.Text), new MySqlParameter("@slot", slotId));
        Db.Execute("UPDATE parking_slots SET status='occupied' WHERE id=@id", new MySqlParameter("@id", slotId));
        MessageBox.Show("Vehicle entry saved."); main.LoadDashboard(); Close();
    }
}
