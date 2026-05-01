using MySql.Data.MySqlClient;
using System.Data;

namespace ParkingLotDesktop;

public class ExitForm : Form
{
    private readonly ComboBox cboVehicle = new();
    private readonly NumericUpDown numRate = new();
    private readonly Label lblFee = new();
    private readonly MainForm main;

    public ExitForm(MainForm parent)
    {
        main = parent;
        Text = "Vehicle Exit";
        StartPosition = FormStartPosition.CenterParent;
        Size = new Size(500, 320);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "Parked Vehicle", Location = new Point(40, 45), AutoSize = true });
        cboVehicle.Location = new Point(170, 41); cboVehicle.Width = 250;
        Controls.Add(cboVehicle);

        Controls.Add(new Label { Text = "Rate Per Hour", Location = new Point(40, 95), AutoSize = true });
        numRate.Location = new Point(170, 91); numRate.Width = 250; numRate.Minimum = 1; numRate.Maximum = 10000; numRate.Value = 20;
        Controls.Add(numRate);

        var btnCalc = new Button { Text = "Calculate Fee", Location = new Point(170, 140), Width = 120, Height = 35 };
        btnCalc.Click += (_, _) => CalculateFee();
        Controls.Add(btnCalc);

        var btnExit = new Button { Text = "Save Exit", Location = new Point(300, 140), Width = 120, Height = 35 };
        btnExit.Click += SaveExit;
        Controls.Add(btnExit);

        lblFee.Text = "Fee: ₱0.00";
        lblFee.Location = new Point(170, 195);
        lblFee.AutoSize = true;
        lblFee.Font = new Font("Segoe UI", 14, FontStyle.Bold);
        Controls.Add(lblFee);

        Load += (_, _) => LoadVehicles();
    }

    private void LoadVehicles()
    {
        var table = Db.Query(@"SELECT v.id, CONCAT(v.plate_number, ' - ', s.slot_number, ' - ', v.time_in) AS label
                               FROM vehicles v
                               LEFT JOIN parking_slots s ON s.id=v.slot_id
                               WHERE v.status='parked'
                               ORDER BY v.time_in DESC");
        cboVehicle.DataSource = table;
        cboVehicle.DisplayMember = "label";
        cboVehicle.ValueMember = "id";
    }

    private decimal CalculateFee()
    {
        if (cboVehicle.SelectedValue == null) return 0;
        int vehicleId = Convert.ToInt32(cboVehicle.SelectedValue);
        var timeInObj = Db.Scalar("SELECT time_in FROM vehicles WHERE id=@id", new MySqlParameter("@id", vehicleId));
        var timeIn = Convert.ToDateTime(timeInObj);
        var hours = Math.Max(1, Math.Ceiling((DateTime.Now - timeIn).TotalHours));
        var fee = Convert.ToDecimal(hours) * numRate.Value;
        lblFee.Text = $"Fee: ₱{fee:N2}  ({hours} hour/s)";
        return fee;
    }

    private void SaveExit(object? sender, EventArgs e)
    {
        if (cboVehicle.SelectedValue == null)
        {
            MessageBox.Show("No parked vehicle selected.");
            return;
        }

        int vehicleId = Convert.ToInt32(cboVehicle.SelectedValue);
        decimal fee = CalculateFee();
        var slotId = Db.Scalar("SELECT slot_id FROM vehicles WHERE id=@id", new MySqlParameter("@id", vehicleId));

        Db.Execute("UPDATE vehicles SET time_out=NOW(), fee=@fee, status='exited' WHERE id=@id",
            new MySqlParameter("@fee", fee),
            new MySqlParameter("@id", vehicleId));

        Db.Execute("UPDATE parking_slots SET status='vacant' WHERE id=@slot", new MySqlParameter("@slot", slotId));
        MessageBox.Show("Vehicle exit saved.");
        main.LoadDashboard();
        Close();
    }
}
