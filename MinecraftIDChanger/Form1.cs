using System.Text;

namespace MinecraftIDChanger;

public partial class Form1 : Form
{
    // Static memory address for the Minecraft PS Vita username string
    private const uint BaseAddress = 0x8234628D;

    // PSN ID rules: 3–16 chars
    private const int PsnMaxLength = 16;
    private const int PsnMinLength = 3;

    public Form1()
    {
        InitializeComponent();

        // Remove initial focus from text boxes so combo placeholder is visible
        Shown += (s, e) =>
        {
            cmbRegion.SelectionLength = 0;
            ActiveControl = null;
        };
    }

    // ─── Core code generation ──────────────────────────────────────────────

    private static string BuildVitaCheatCode(string username, string codeName)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"_V0 {codeName}");

        byte[] bytes = Encoding.UTF8.GetBytes(username);

        for (int i = 0; i < bytes.Length; i += 4)
        {
            byte[] chunk = new byte[4];
            int count = Math.Min(4, bytes.Length - i);
            Array.Copy(bytes, i, chunk, 0, count);
            Array.Reverse(chunk);
            string hex = Convert.ToHexString(chunk);
            uint address = BaseAddress + (uint)i;
            sb.AppendLine($"$0200 {address:X8} {hex}");
        }

        int alignedLen = (bytes.Length + 3) / 4 * 4;
        uint nullAddress = BaseAddress + (uint)alignedLen;
        sb.AppendLine($"$0200 {nullAddress:X8} 00000000");

        return sb.ToString().TrimEnd();
    }

    // ─── Event handlers ────────────────────────────────────────────────────

    private void btnGenerate_Click(object sender, EventArgs e)
    {
        string username = txtNewID.Text;
        string codeName = string.IsNullOrWhiteSpace(txtCodeName.Text) ? "NewID" : txtCodeName.Text.Trim();

        if (string.IsNullOrEmpty(username))
        {
            ShowWarning("Please enter a username.", "Input Error");
            return;
        }

        if (username.Length < PsnMinLength)
        {
            ShowWarning($"PSN IDs must be at least {PsnMinLength} characters.", "Input Error");
            return;
        }

        if (username.Length > PsnMaxLength)
        {
            var result = MessageBox.Show(
                $"PSN IDs are normally capped at {PsnMaxLength} characters.\n" +
                "Exceeding this may cause in-game issues.\n\nContinue anyway?",
                "Length Warning",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.No)
                return;
        }

        rtbOutput.Text = BuildVitaCheatCode(username, codeName);
    }

    private void btnCopy_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(rtbOutput.Text))
        {
            ShowWarning("Please generate a code first.", "Nothing to Copy");
            return;
        }
        Clipboard.SetText(rtbOutput.Text);
        MessageBox.Show("Code copied to clipboard!", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(rtbOutput.Text))
        {
            ShowWarning("Please generate a code first.", "Nothing to Save");
            return;
        }

        using var sfd = new SaveFileDialog
        {
            FileName = cmbRegion.Text,
            Filter   = "PSV Files (*.psv)|*.psv|All Files (*.*)|*.*",
            Title    = "Save VitaCheat File"
        };

        if (sfd.ShowDialog() != DialogResult.OK) return;

        try
        {
            File.WriteAllText(sfd.FileName, rtbOutput.Text, Encoding.UTF8);
            MessageBox.Show($"File saved:\n{sfd.FileName}", "Saved Successfully",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (IOException ex)
        {
            ShowError($"Could not save file:\n{ex.Message}", "Save Error");
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        txtNewID.Clear();
        txtCodeName.Clear();
        rtbOutput.Clear();
        cmbRegion.SelectedIndex = 0;
        ActiveControl = null;
    }

    // ─── Helpers ───────────────────────────────────────────────────────────

    private static void ShowWarning(string message, string title) =>
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);

    private static void ShowError(string message, string title) =>
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
}
