using MySql.Data.MySqlClient;
using System.Data;

namespace ParkingLotDesktop;

public class EntryForm : Form
{
    private readonly TextBox txtPlate = new();
    private readonly ComboBox cboType = new();
    private readonly ComboBox cboSlot = new();
    private readonly MainForm main;

    public EntryForm(MainForm parent)
    {
        main = parent;
        Text = "Vehicle Entry";
        StartPosition = FormStartPosition.CenterParent;
        Size = new Size(420, 300);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "Plate Number", Location = new Point(40, 40), AutoSize = true });
        txtPlate.Location = new Point(160, 36); txtPlate.Width = 190;
        Controls.Add(txtPlate);

        Controls.Add(new Label { Text = "Vehicle Type", Location = new Point(40, 85), AutoSize = true });
        cboType.Location = new Point(160, 81); cboType.Width = 190;
        cboType.Items.AddRange(new object[] { "Car", "Motorcycle", "Van", "Truck" });
        cboType.SelectedIndex = 0;
        Controls.Add(cboType);

        Controls.Add(new Label { Text = "Parking Slot", Location = new Point(40, 130), AutoSize = true });
        cboSlot.Location = new Point(160, 126); cboSlot.Width = 190;
        Controls.Add(cboSlot);

        var btn = new Button { Text = "Save Entry", Location = new Point(160, 180), Width = 190, Height = 35 };
        btn.Click += SaveEntry;
        Controls.Add(btn);

        Load += (_, _) => LoadSlots();
    }

    private void LoadSlots()
    {
        var slots = Db.Query("SELECT id, slot_number FROM parking_slots WHERE status='vacant' ORDER BY slot_number");
        cboSlot.DataSource = slots;
        cboSlot.DisplayMember = "slot_number";
        cboSlot.ValueMember = "id";
    }

    private void SaveEntry(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPlate.Text) || cboSlot.SelectedValue == null)
        {
            MessageBox.Show("Please complete all fields.");
            return;
        }

        int slotId = Convert.ToInt32(cboSlot.SelectedValue);
        Db.Execute(@"INSERT INTO vehicles(plate_number, vehicle_type, slot_id, time_in, status) 
                     VALUES(@plate,@type,@slot,NOW(),'parked')",
            new MySqlParameter("@plate", txtPlate.Text.Trim().ToUpper()),
            new MySqlParameter("@type", cboType.Text),
            new MySqlParameter("@slot", slotId));

        Db.Execute("UPDATE parking_slots SET status='occupied' WHERE id=@id", new MySqlParameter("@id", slotId));
        MessageBox.Show("Vehicle entry saved.");
        main.LoadDashboard();
        Close();
    }
}
