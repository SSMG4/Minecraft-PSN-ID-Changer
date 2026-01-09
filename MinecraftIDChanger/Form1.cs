using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace MinecraftIDChanger
{
    public partial class Form1 : Form
    {
        // Static memory address for Minecraft PS Vita Username
        private const uint BASE_ADDRESS = 0x8234628D;

        public Form1()
        {
            InitializeComponent();

            this.Shown += (s, e) => {
                cmbRegion.SelectionLength = 0; // Deselect text
                this.ActiveControl = null;     // Remove focus from the box
            };
        }

        // Main logic to generate the Vitacheat code
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string username = txtNewID.Text;
            string codeName = string.IsNullOrWhiteSpace(txtCodeName.Text) ? "NewID" : txtCodeName.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"_V0 {codeName}");

            byte[] bytes = Encoding.UTF8.GetBytes(username);

            // Process bytes in 4-byte chunks for VitaCheat $0200 (32-bit)
            for (int i = 0; i < bytes.Length; i += 4)
            {
                byte[] chunk = new byte[4] { 0, 0, 0, 0 };
                int length = Math.Min(4, bytes.Length - i);
                Array.Copy(bytes, i, chunk, 0, length);

                // Little-endian conversion for the PS Vita's memory
                Array.Reverse(chunk);
                string hexValue = BitConverter.ToString(chunk).Replace("-", "");

                uint currentAddress = BASE_ADDRESS + (uint)i;
                sb.AppendLine($"$0200 {currentAddress:X8} {hexValue}");
            }
            rtbOutput.Text = sb.ToString();
        }

        // Copies the generated text to the Windows Clipboard
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtbOutput.Text))
            {
                Clipboard.SetText(rtbOutput.Text);
                MessageBox.Show("Code copied to clipboard!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Opens the File Explorer to save the .psv file
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rtbOutput.Text))
            {
                MessageBox.Show("Please generate a code first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.FileName = cmbRegion.Text;
                sfd.Filter = "PSV Files|*.psv|All Files|*.*";
                sfd.Title = "Save Vitacheat File";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(sfd.FileName, rtbOutput.Text);
                        MessageBox.Show("File saved successfully!", "Success");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving file: " + ex.Message);
                    }
                }
            }
        }
    }
}