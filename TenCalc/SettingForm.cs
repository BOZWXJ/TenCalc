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
				ComboBoxSkin.Items.Add(Path.GetFileNameWithoutExtension(file));
			}
			ComboBoxSkin.SelectedItem = Properties.Settings.Default.SkinName;
			CheckBoxNumLock.Checked = Properties.Settings.Default.NumLock;
			CheckBoxSendResult.Checked = Properties.Settings.Default.SendResult;
			CheckBoxSendClose.Checked = Properties.Settings.Default.SendClose;
			CheckBoxSendEnter.Checked = Properties.Settings.Default.SendEnter;
			CheckBoxSendSeparator.Checked = Properties.Settings.Default.SendSeparator;
			CheckBoxSendIMEOff.Checked = Properties.Settings.Default.SendIMEOff;
		}

		private void ButtonOk_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.SkinName = (string)ComboBoxSkin.SelectedItem;
			Properties.Settings.Default.NumLock = CheckBoxNumLock.Checked;
			Properties.Settings.Default.SendResult = CheckBoxSendResult.Checked;
			Properties.Settings.Default.SendClose = CheckBoxSendClose.Checked;
			Properties.Settings.Default.SendEnter = CheckBoxSendEnter.Checked;
			Properties.Settings.Default.SendSeparator = CheckBoxSendSeparator.Checked;
			Properties.Settings.Default.SendIMEOff = CheckBoxSendIMEOff.Checked;
			Properties.Settings.Default.Save();

			DialogResult = DialogResult.OK;
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}
