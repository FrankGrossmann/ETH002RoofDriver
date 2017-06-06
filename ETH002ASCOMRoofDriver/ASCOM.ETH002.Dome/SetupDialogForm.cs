using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ASCOM.Utilities;
using ASCOM.ETH002;
using ASCOM.ETH002.Properties;

namespace ASCOM.ETH002
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        public SetupDialogForm()
        {
            InitializeComponent();
            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here
            // Update the state variables with results from the dialogue
            Dome.comPort = string.Empty;
            Dome.tl.Enabled = chkTrace.Checked;
        }

        private void cmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("http://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void InitUI()
        {
            chkTrace.Checked = Dome.tl.Enabled;

            Settings settings = Settings.Default;
        }

        private void comboBoxRelaisToUse_SelectedValueChanged(object sender, EventArgs e)
        {
            Settings.Default.Relais = (Relais)comboBoxRelaisToUse.SelectedItem;
        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {
            foreach(var value in Enum.GetValues(typeof(Relais)))
            {
                comboBoxRelaisToUse.Items.Add(value);
            }
            comboBoxRelaisToUse.SelectedItem = Settings.Default.Relais;
        }
    }
}