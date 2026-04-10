namespace MinecraftIDChanger;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        cmbRegion        = new ComboBox();
        txtCodeName      = new TextBox();
        txtNewID         = new TextBox();
        rtbOutput        = new RichTextBox();
        btnGenerate      = new Button();
        btnCopy          = new Button();
        btnSave          = new Button();
        btnClear         = new Button();
        labNewID         = new Label();
        labGeneratedCode = new Label();
        labCodeName      = new Label();
        SuspendLayout();

        // ── cmbRegion ──────────────────────────────────────────────────────
        cmbRegion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        cmbRegion.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbRegion.FormattingEnabled = true;
        cmbRegion.Items.AddRange(new object[]
        {
            "PCSB00560.psv",   // EU
            "PCSE00491.psv",   // US
            "PCSG00302.psv"    // JP
        });
        cmbRegion.Location      = new Point(767, 535);
        cmbRegion.Name          = "cmbRegion";
        cmbRegion.Size          = new Size(121, 23);
        cmbRegion.TabIndex      = 5;
        cmbRegion.SelectedIndex = 0;

        // ── labNewID ───────────────────────────────────────────────────────
        labNewID.AutoSize = true;
        labNewID.Font     = new Font("Segoe UI", 10F, FontStyle.Bold);
        labNewID.Location = new Point(13, 9);
        labNewID.Name     = "labNewID";
        labNewID.Text     = "New ID";

        // ── txtNewID ───────────────────────────────────────────────────────
        txtNewID.Location  = new Point(13, 32);
        txtNewID.Name      = "txtNewID";
        txtNewID.Size      = new Size(425, 23);
        txtNewID.TabIndex  = 0;
        txtNewID.MaxLength = 64;

        // ── labCodeName ────────────────────────────────────────────────────
        labCodeName.AutoSize = true;
        labCodeName.Font     = new Font("Segoe UI", 10F, FontStyle.Bold);
        labCodeName.Location = new Point(443, 9);
        labCodeName.Name     = "labCodeName";
        labCodeName.Text     = "Code Name";

        // ── txtCodeName ────────────────────────────────────────────────────
        txtCodeName.Anchor   = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtCodeName.Location = new Point(443, 32);
        txtCodeName.Name     = "txtCodeName";
        txtCodeName.Size     = new Size(526, 23);
        txtCodeName.TabIndex = 1;

        // ── labGeneratedCode ───────────────────────────────────────────────
        labGeneratedCode.AutoSize = true;
        labGeneratedCode.Font     = new Font("Segoe UI", 10F, FontStyle.Bold);
        labGeneratedCode.Location = new Point(13, 66);
        labGeneratedCode.Name     = "labGeneratedCode";
        labGeneratedCode.Text     = "Generated Code";

        // ── rtbOutput ──────────────────────────────────────────────────────
        rtbOutput.Anchor   = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        rtbOutput.Font     = new Font("Consolas", 9.5F);
        rtbOutput.Location = new Point(13, 90);
        rtbOutput.Name     = "rtbOutput";
        rtbOutput.ReadOnly = true;
        rtbOutput.Size     = new Size(957, 434);
        rtbOutput.TabIndex = 6;

        // ── btnGenerate ────────────────────────────────────────────────────
        btnGenerate.Anchor   = AnchorStyles.Bottom | AnchorStyles.Left;
        btnGenerate.Location = new Point(13, 535);
        btnGenerate.Name     = "btnGenerate";
        btnGenerate.Size     = new Size(110, 25);
        btnGenerate.TabIndex = 2;
        btnGenerate.Text     = "Generate Code";
        btnGenerate.UseVisualStyleBackColor = true;
        btnGenerate.Click   += new EventHandler(btnGenerate_Click);

        // ── btnCopy ────────────────────────────────────────────────────────
        btnCopy.Anchor   = AnchorStyles.Bottom | AnchorStyles.Left;
        btnCopy.Location = new Point(130, 535);
        btnCopy.Name     = "btnCopy";
        btnCopy.Size     = new Size(75, 25);
        btnCopy.TabIndex = 3;
        btnCopy.Text     = "Copy";
        btnCopy.UseVisualStyleBackColor = true;
        btnCopy.Click   += new EventHandler(btnCopy_Click);

        // ── btnClear ───────────────────────────────────────────────────────
        btnClear.Anchor   = AnchorStyles.Bottom | AnchorStyles.Left;
        btnClear.Location = new Point(212, 535);
        btnClear.Name     = "btnClear";
        btnClear.Size     = new Size(75, 25);
        btnClear.TabIndex = 4;
        btnClear.Text     = "Clear";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click   += new EventHandler(btnClear_Click);

        // ── btnSave ────────────────────────────────────────────────────────
        btnSave.Anchor   = AnchorStyles.Bottom | AnchorStyles.Right;
        btnSave.Location = new Point(895, 535);
        btnSave.Name     = "btnSave";
        btnSave.Size     = new Size(75, 25);
        btnSave.TabIndex = 7;
        btnSave.Text     = "Save";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click   += new EventHandler(btnSave_Click);

        // ── Form1 ──────────────────────────────────────────────────────────
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode       = AutoScaleMode.Font;
        ClientSize          = new Size(984, 572);
        MinimumSize         = new Size(800, 500);
        Controls.AddRange(new Control[]
        {
            labNewID, txtNewID,
            labCodeName, txtCodeName,
            labGeneratedCode, rtbOutput,
            btnGenerate, btnCopy, btnClear, btnSave,
            cmbRegion
        });
        Name = "Form1";
        Text = "Minecraft PS Vita PSN ID Changer";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ComboBox    cmbRegion;
    private TextBox     txtCodeName;
    private TextBox     txtNewID;
    private RichTextBox rtbOutput;
    private Button      btnGenerate;
    private Button      btnCopy;
    private Button      btnSave;
    private Button      btnClear;
    private Label       labNewID;
    private Label       labGeneratedCode;
    private Label       labCodeName;
}
