namespace ParkingLotDesktop;
partial class EntryForm
{
    private System.ComponentModel.IContainer components = null; private TextBox txtPlate; private ComboBox cboType; private ComboBox cboSlot; private Button btnSave;
    protected override void Dispose(bool disposing){ if(disposing && components != null) components.Dispose(); base.Dispose(disposing); }
    private void InitializeComponent()
    {
        txtPlate = new TextBox();
        cboType = new ComboBox();
        cboSlot = new ComboBox();
        btnSave = new Button();
        title = new Label();
        SuspendLayout();
        // 
        // txtPlate
        // 
        txtPlate.Font = new Font("Segoe UI", 11F);
        txtPlate.Location = new Point(52, 162);
        txtPlate.Name = "txtPlate";
        txtPlate.Size = new Size(395, 32);
        txtPlate.TabIndex = 2;
        // 
        // cboType
        // 
        cboType.DropDownStyle = ComboBoxStyle.DropDownList;
        cboType.Font = new Font("Segoe UI", 11F);
        cboType.Location = new Point(52, 235);
        cboType.Name = "cboType";
        cboType.Size = new Size(395, 33);
        cboType.TabIndex = 3;
        // 
        // cboSlot
        // 
        cboSlot.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSlot.Font = new Font("Segoe UI", 11F);
        cboSlot.Location = new Point(52, 308);
        cboSlot.Name = "cboSlot";
        cboSlot.Size = new Size(395, 33);
        cboSlot.TabIndex = 4;
        // 
        // btnSave
        // 
        btnSave.BackColor = Color.FromArgb(37, 99, 235);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(52, 365);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(395, 44);
        btnSave.TabIndex = 5;
        btnSave.Text = "Save Entry";
        btnSave.UseVisualStyleBackColor = false;
        // 
        // title
        // 
        title.AutoSize = true;
        title.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        title.ForeColor = Color.FromArgb(15, 23, 42);
        title.Location = new Point(46, 35);
        title.Name = "title";
        title.Size = new Size(400, 50);
        title.TabIndex = 0;
        title.Text = "⚡ New Vehicle Entry";
        // 
        // EntryForm
        // 
        BackColor = Color.Silver;
        ClientSize = new Size(500, 430);
        Controls.Add(title);
        Controls.Add(txtPlate);
        Controls.Add(cboType);
        Controls.Add(cboSlot);
        Controls.Add(btnSave);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "EntryForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Vehicle Entry";
        ResumeLayout(false);
        PerformLayout();
    }
    private void AddLabel(string text,int x,int y){ var l = new Label(); l.Text=text; l.Location=new Point(x,y); l.AutoSize=true; l.Font=new Font("Segoe UI",9.5F,FontStyle.Bold); l.ForeColor=Color.FromArgb(51,65,85); Controls.Add(l); }
    private Label title;
}
