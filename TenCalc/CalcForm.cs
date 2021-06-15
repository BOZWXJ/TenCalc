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
	public enum Mode { None, Shift, Control, Alt, BaseNShift, BaseNControl, };
	[Flags]
	public enum Button
	{
		None = 0,
		NumPad0 = 1 << 0,
		NumPad1 = 1 << 1,
		NumPad2 = 1 << 2,
		NumPad3 = 1 << 3,
		NumPad4 = 1 << 4,
		NumPad5 = 1 << 5,
		NumPad6 = 1 << 6,
		NumPad7 = 1 << 7,
		NumPad8 = 1 << 8,
		NumPad9 = 1 << 9,
		Decimal = 1 << 10,
		Add = 1 << 11,
		Sub = 1 << 12,
		Mul = 1 << 13,
		Div = 1 << 14,
		NumLock = 1 << 15,
		Enter = 1 << 16,
		ShiftKey = 1 << 17,
		ControlKey = 1 << 18,
		MenuKey = 1 << 19,
		LButton = 1 << 20,
		RButton = 1 << 21,
		Button0 = 1 << 22,
		Button1 = 1 << 23,
		Button2 = 1 << 24,
		Button3 = 1 << 25,
		Button4 = 1 << 26,
		Button5 = 1 << 27,
		Button6 = 1 << 28,
		Button7 = 1 << 29,
		Button8 = 1 << 30,
		Button9 = 1 << 31,
	}

	public partial class CalcForm : Form
	{
		readonly KeyboardHook hook = new();
		readonly Display display = new();
		readonly KeyPad keyPad;
		Image FormImage;

		public CalcForm()
		{
			InitializeComponent();
			keyPad = new KeyPad(display);
		}

		private void CalcForm_Load(object sender, EventArgs e)
		{
			notifyIcon1.Icon = Properties.Resources.TenCalc;

			hook.Hook();
			hook.KeyDownEvent += Hook_KeyDownEvent;
			hook.KeyUpEvent += Hook_KeyUpEvent;

			if (!SkinData.LoadSkinFile()) {
				MessageBox.Show("Skin 読込異常", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			} else {
				DesktopLocation = Properties.Settings.Default.WindowPosition;
			}
		}

		private void CalcForm_Shown(object sender, EventArgs e)
		{
			if (SkinData.IsLoad && IsKeyLocked(Keys.NumLock) == Properties.Settings.Default.NumLock) {
				FormUpdate(true);
				Show();
			} else {
				Hide();
			}
		}

		private void CalcForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			hook.UnHook();
			Properties.Settings.Default.WindowPosition = DesktopLocation;
			Properties.Settings.Default.Save();
			notifyIcon1.Visible = false;

			// todo: NumLock の状態を戻す

		}

		private void CalcForm_FormClosed(object sender, FormClosedEventArgs e)
		{
		}

		private void FormUpdate(bool down)
		{
			if (!SkinData.IsLoad) { return; }

			Bitmap bmp = SkinData.OffImages[keyPad.Mode];
			FormImage = (Image)bmp.Clone();
			using (Graphics g = Graphics.FromImage(FormImage)) {
				// todo: 表示
				if (down) {
					System.Diagnostics.Debug.WriteLine(display);
				}

				foreach (var btn in SkinData.Buttons.Values) {
					if (keyPad.DownButton == btn.Key) {
						g.DrawImage(btn.OnBmp[keyPad.Mode], btn.Rectangle);
					} else {
						g.DrawImage(btn.OffBmp[keyPad.Mode], btn.Rectangle);
					}
				}
			}
			BackgroundImage = FormImage;
			Size = BackgroundImage.Size;
			Refresh();
		}

		#region 入力処理
		bool formDrag = false;
		Size downPoint;
		private void CalcForm_MouseDown(object sender, MouseEventArgs e)
		{
			Button btn = keyPad.MouseDown(e);
			switch (btn) {
			case Button.LButton:
				downPoint = new Size(e.Location);
				formDrag = true;
				break;
			case Button.RButton:
				contextMenuStrip1.Show(this, e.Location);
				break;
			case Button.NumLock:
				Hide();
				CalcForm_MouseUp(sender, e);
				break;
			case Button.None:
				break;
			default:
				FormUpdate(true);
				break;
			}
		}

		private void CalcForm_MouseUp(object sender, MouseEventArgs e)
		{
			Button btn = keyPad.MouseUp(e);
			if (Visible && btn != Button.None) {
				FormUpdate(false);
			}
			formDrag = false;
		}

		private void CalcForm_MouseMove(object sender, MouseEventArgs e)
		{
			if (formDrag) {
				DesktopLocation = Point.Subtract(Cursor.Position, downPoint);
			}
		}

		private void Hook_KeyDownEvent(object sender, KeyHookEventArgs e)
		{
			if (Visible) {
				// 表示中
				Button btn = keyPad.KeyDown(e);
				switch (btn) {
				case Button.NumLock:
					Hide();
					break;
				case Button.None:
					break;
				default:
					FormUpdate(true);
					break;
				}
			} else if (SkinData.IsLoad && e.VkCode == Keys.NumLock && IsKeyLocked(Keys.NumLock) != Properties.Settings.Default.NumLock) {
				// 非表示中
				FormUpdate(true);
				Show();
			}
		}

		private void Hook_KeyUpEvent(object sender, KeyHookEventArgs e)
		{
			Button btn = keyPad.KeyUp(e);
			if (Visible && btn != Button.None) {
				FormUpdate(false);
			}
		}
		#endregion

		#region メニュー項目
		private void toolStripMenuItemSetting_Click(object sender, EventArgs e)
		{
			SettingForm dlg = new();
			if (dlg.ShowDialog() == DialogResult.OK && SkinData.Name != Properties.Settings.Default.SkinName) {
				SkinData.LoadSkinFile();
				FormUpdate(true);
			}
		}

		private void toolStripMenuItemInfo_Click(object sender, EventArgs e)
		{
			AboutBox dlg = new();
			dlg.ShowDialog();
		}

		private void toolStripMenuItemExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		#endregion

	}
}
