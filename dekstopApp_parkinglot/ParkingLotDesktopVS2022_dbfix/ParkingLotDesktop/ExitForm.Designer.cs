namespace ParkingLotDesktop;
partial class ExitForm
{
    private System.ComponentModel.IContainer components = null; private ComboBox cboVehicle; private NumericUpDown numRate; private Label lblFee; private Button btnCalc; private Button btnExit;
    protected override void Dispose(bool disposing){ if(disposing && components != null) components.Dispose(); base.Dispose(disposing); }
    private void InitializeComponent()
    {
        cboVehicle = new ComboBox();
        numRate = new NumericUpDown();
        lblFee = new Label();
        btnCalc = new Button();
        btnExit = new Button();
        title = new Label();
        ((System.ComponentModel.ISupportInitialize)numRate).BeginInit();
        SuspendLayout();
        // 
        // cboVehicle
        // 
        cboVehicle.DropDownStyle = ComboBoxStyle.DropDownList;
        cboVehicle.Font = new Font("Segoe UI", 11F);
        cboVehicle.Location = new Point(52, 157);
        cboVehicle.Name = "cboVehicle";
        cboVehicle.Size = new Size(455, 33);
        cboVehicle.TabIndex = 2;
        // 
        // numRate
        // 
        numRate.Font = new Font("Segoe UI", 11F);
        numRate.Location = new Point(52, 232);
        numRate.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        numRate.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numRate.Name = "numRate";
        numRate.Size = new Size(455, 32);
        numRate.TabIndex = 3;
        numRate.Value = new decimal(new int[] { 20, 0, 0, 0 });
        // 
        // lblFee
        // 
        lblFee.AutoSize = true;
        lblFee.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblFee.ForeColor = Color.FromArgb(21, 128, 61);
        lblFee.Location = new Point(52, 340);
        lblFee.Name = "lblFee";
        lblFee.Size = new Size(130, 32);
        lblFee.TabIndex = 6;
        lblFee.Text = "Fee: ₱0.00";
        // 
        // btnCalc
        // 
        btnCalc.BackColor = Color.FromArgb(15, 23, 42);
        btnCalc.FlatAppearance.BorderSize = 0;
        btnCalc.FlatStyle = FlatStyle.Flat;
        btnCalc.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        btnCalc.ForeColor = Color.White;
        btnCalc.Location = new Point(52, 288);
        btnCalc.Name = "btnCalc";
        btnCalc.Size = new Size(220, 42);
        btnCalc.TabIndex = 4;
        btnCalc.Text = "Calculate Fee";
        btnCalc.UseVisualStyleBackColor = false;
        // 
        // btnExit
        // 
        btnExit.BackColor = Color.FromArgb(37, 99, 235);
        btnExit.FlatAppearance.BorderSize = 0;
        btnExit.FlatStyle = FlatStyle.Flat;
        btnExit.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        btnExit.ForeColor = Color.White;
        btnExit.Location = new Point(287, 288);
        btnExit.Name = "btnExit";
        btnExit.Size = new Size(220, 42);
        btnExit.TabIndex = 5;
        btnExit.Text = "Save Exit";
        btnExit.UseVisualStyleBackColor = false;
        // 
        // title
        // 
        title.AutoSize = true;
        title.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        title.ForeColor = Color.FromArgb(15, 23, 42);
        title.Location = new Point(46, 35);
        title.Name = "title";
        title.Size = new Size(284, 50);
        title.TabIndex = 0;
        title.Text = "⚡ Vehicle Exit";
        // 
        // ExitForm
        // 
        BackColor = Color.Silver;
        ClientSize = new Size(555, 411);
        Controls.Add(title);
        Controls.Add(cboVehicle);
        Controls.Add(numRate);
        Controls.Add(btnCalc);
        Controls.Add(btnExit);
        Controls.Add(lblFee);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "ExitForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Vehicle Exit";
        ((System.ComponentModel.ISupportInitialize)numRate).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
    private void AddLabel(string text,int x,int y){ var l = new Label(); l.Text=text; l.Location=new Point(x,y); l.AutoSize=true; l.Font=new Font("Segoe UI",9.5F,FontStyle.Bold); l.ForeColor=Color.FromArgb(51,65,85); Controls.Add(l); }
    private Label title;
}
