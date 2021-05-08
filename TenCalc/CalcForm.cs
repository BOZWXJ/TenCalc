using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TenCalc
{
	public partial class CalcForm : Form
	{
		CalcCtrl ctrl = new CalcCtrl();

		public CalcForm()
		{
			InitializeComponent();
			notifyIcon1.Icon = Properties.Resources.TenCalc;

			if (LicenseManager.UsageMode == LicenseUsageMode.Runtime) {
				ctrl.Initialize();
				Size = ctrl.BackgroundImage.Size;
				DesktopLocation = Properties.Settings.Default.WindowPosition;
				pictureBox1.Image = ctrl.BackgroundImage;
			}
		}

		private void CalcForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Properties.Settings.Default.WindowPosition = DesktopLocation;
			Properties.Settings.Default.Save();
			notifyIcon1.Visible = false;
		}

		private void CalcForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			ctrl.Dispose();
		}

		#region マウス処理
		bool formDrag = false;
		Keys key = Keys.None;
		Size mousePoint;
		private void CalcForm_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left) {
				var btn = ctrl.CheckButton(e.Location);
				if (btn == null) {
					mousePoint = new Size(e.Location);
					formDrag = true;
				} else {
					key = btn.Keys;
					using (Graphics g = Graphics.FromImage(pictureBox1.Image)) {
						g.DrawImage(btn.Bmp[Button.Status.On], btn.Rectangle);
					}
					pictureBox1.Refresh();
				}
			} else if (e.Button == MouseButtons.Right) {
				contextMenuStrip1.Show(pictureBox1, e.Location);
			}
		}
		private void CalcForm_MouseUp(object sender, MouseEventArgs e)
		{
			if (key != Keys.None) {
				using (Graphics g = Graphics.FromImage(pictureBox1.Image)) {
					var btn = ctrl.Buttons[key];
					g.DrawImage(btn.Bmp[Button.Status.Off], btn.Rectangle);
				}
				pictureBox1.Refresh();
			}
			formDrag = false;
			key = Keys.None;
		}
		private void CalcForm_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && formDrag) {
				DesktopLocation = Point.Subtract(Cursor.Position, mousePoint);
			}
		}
		#endregion

		#region メニュー項目
		private void toolStripMenuItemSetting_Click(object sender, EventArgs e)
		{
			new SettingForm().ShowDialog();
		}
		private void toolStripMenuItemInfo_Click(object sender, EventArgs e)
		{
			new AboutBox().ShowDialog();
		}
		private void toolStripMenuItemExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		#endregion
	}
}
