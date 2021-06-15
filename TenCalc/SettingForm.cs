using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TenCalc
{
	public partial class SettingForm : Form
	{
		public SettingForm()
		{
			InitializeComponent();
			Icon = Properties.Resources.TenCalc;
		}

		private void SettingForm_Shown(object sender, EventArgs e)
		{
			string path = Path.Combine(Path.GetDirectoryName(Application.StartupPath), "Skin");
			foreach (var file in Directory.GetFiles(path, "*.zip")) {
				comboBoxSkin.Items.Add(Path.GetFileNameWithoutExtension(file));
			}
			comboBoxSkin.SelectedItem = Properties.Settings.Default.SkinName;
			checkBoxNumLock.Checked = Properties.Settings.Default.NumLock;
			checkBoxSendResult.Checked = Properties.Settings.Default.SendResult;
			checkBoxSendClose.Checked = Properties.Settings.Default.SendClose;
			checkBoxSendEnter.Checked = Properties.Settings.Default.SendEnter;
			checkBoxSendSeparator.Checked = Properties.Settings.Default.SendSeparator;
			checkBoxSendIMEOff.Checked = Properties.Settings.Default.SendIMEOff;
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.SkinName = (string)comboBoxSkin.SelectedItem;
			Properties.Settings.Default.NumLock = checkBoxNumLock.Checked;
			Properties.Settings.Default.SendResult = checkBoxSendResult.Checked;
			Properties.Settings.Default.SendClose = checkBoxSendClose.Checked;
			Properties.Settings.Default.SendEnter = checkBoxSendEnter.Checked;
			Properties.Settings.Default.SendSeparator = checkBoxSendSeparator.Checked;
			Properties.Settings.Default.SendIMEOff = checkBoxSendIMEOff.Checked;
			Properties.Settings.Default.Save();

			DialogResult = DialogResult.OK;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}
