using MySql.Data.MySqlClient;

namespace ParkingLotDesktop;

public partial class ExitForm : Form
{
    private readonly MainForm main;
    public ExitForm(MainForm parent)
    {
        main = parent; InitializeComponent(); btnCalc.Click += (_, _) => CalculateFee(); btnExit.Click += SaveExit; Load += (_, _) => LoadVehicles();
    }
    private void LoadVehicles()
    {
        var table = Db.Query(@"SELECT v.id, CONCAT(v.plate_number, ' - ', s.slot_number, ' - ', v.time_in) AS label FROM vehicles v LEFT JOIN parking_slots s ON s.id=v.slot_id WHERE v.status='parked' ORDER BY v.time_in DESC");
        cboVehicle.DataSource = table; cboVehicle.DisplayMember = "label"; cboVehicle.ValueMember = "id";
    }
    private decimal CalculateFee()
    {
        if (cboVehicle.SelectedValue == null) return 0;
        int vehicleId = Convert.ToInt32(cboVehicle.SelectedValue);
        var timeInObj = Db.Scalar("SELECT time_in FROM vehicles WHERE id=@id", new MySqlParameter("@id", vehicleId));
        var timeIn = Convert.ToDateTime(timeInObj); var hours = Math.Max(1, Math.Ceiling((DateTime.Now - timeIn).TotalHours)); var fee = Convert.ToDecimal(hours) * numRate.Value;
        lblFee.Text = $"Fee: ₱{fee:N2}  ({hours} hour/s)"; return fee;
    }
    private void SaveExit(object? sender, EventArgs e)
    {
        if (cboVehicle.SelectedValue == null) { MessageBox.Show("No parked vehicle selected."); return; }
        int vehicleId = Convert.ToInt32(cboVehicle.SelectedValue); decimal fee = CalculateFee(); var slotId = Db.Scalar("SELECT slot_id FROM vehicles WHERE id=@id", new MySqlParameter("@id", vehicleId));
        Db.Execute("UPDATE vehicles SET time_out=NOW(), fee=@fee, status='exited' WHERE id=@id", new MySqlParameter("@fee", fee), new MySqlParameter("@id", vehicleId));
        Db.Execute("UPDATE parking_slots SET status='vacant' WHERE id=@slot", new MySqlParameter("@slot", slotId));
        MessageBox.Show("Vehicle exit saved."); main.LoadDashboard(); Close();
    }
}
