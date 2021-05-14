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
		readonly CalcCtrl ctrl = new CalcCtrl();
		readonly KeyboardHook hook = new KeyboardHook();
		readonly SkinData skin = new SkinData();

		public CalcForm()
		{
			InitializeComponent();
		}

		private void CalcForm_Load(object sender, EventArgs e)
		{
			notifyIcon1.Icon = Properties.Resources.TenCalc;

			hook.Hook();
			hook.KeyDownEvent += Hook_KeyDownEvent;
			hook.KeyUpEvent += Hook_KeyUpEvent;

			if (!skin.LoadSkinFile()) {
				MessageBox.Show("Skin 読込異常", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			} else {
				//TransparencyKey = BackColor;
				Size = skin.BackgroundImages[Button.Status.Off].Size;
				DesktopLocation = Properties.Settings.Default.WindowPosition;
				pictureBox1.Image = skin.BackgroundImages[Button.Status.Off];
				pictureBox1.Refresh();
			}
		}

		private void CalcForm_Shown(object sender, EventArgs e)
		{
			if (skin.IsLoad && IsKeyLocked(Keys.NumLock) == Properties.Settings.Default.NumLock) {
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
		}

		private void CalcForm_FormClosed(object sender, FormClosedEventArgs e)
		{
		}

		#region 入力処理
		bool formDrag = false;
		Size downPoint;
		Keys downKey = Keys.None;
		Button.Status modeOn = Button.Status.On;
		Button.Status modeOff = Button.Status.Off;

		private void CalcForm_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left) {
				var btn = skin.GetButton(e.Location);
				if (btn == null) {
					downPoint = new Size(e.Location);
					formDrag = true;
				} else if (downKey == Keys.None) {
					ctrl.KeyDown(btn.Key);
					ButtonUpdate(btn.Key, modeOn);
					downKey = btn.Key;
				}
			} else if (e.Button == MouseButtons.Right) {
				contextMenuStrip1.Show(pictureBox1, e.Location);
			}
		}

		private void CalcForm_MouseUp(object sender, MouseEventArgs e)
		{
			if (downKey != Keys.None) {
				ButtonUpdate(downKey, modeOff);
			}
			formDrag = false;
			downKey = Keys.None;
		}

		private void CalcForm_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && formDrag) {
				DesktopLocation = Point.Subtract(Cursor.Position, downPoint);
			}
		}

		private void Hook_KeyDownEvent(object sender, KeyHookEventArgs e)
		{
			Keys key = ConvertKey(e.VkCode, e.IsLLKHF_EXTENDED);
			if (Visible) {
				if (downKey != Keys.None) {
					e.Cancel = true;
					return;
				}
				switch (key) {
				case Keys.NumLock:
					if (!ctrl.IsClear) {
						ctrl.Clear();
						e.Cancel = true;
					} else {
						downKey = key;
						Hide();
					}
					break;
				case Keys.ControlKey:
				case Keys.RControlKey:
				case Keys.LControlKey:
					BackgroundUpdate(ModifierKeys | Keys.Control);
					break;
				case Keys.ShiftKey:
				case Keys.RShiftKey:
				case Keys.LShiftKey:
					BackgroundUpdate(ModifierKeys | Keys.Shift);
					break;
				case Keys.Menu:
				case Keys.RMenu:
				case Keys.LMenu:
					BackgroundUpdate(ModifierKeys | Keys.Alt);
					break;
				case Keys.NumPad0:
				case Keys.NumPad1:
				case Keys.NumPad2:
				case Keys.NumPad3:
				case Keys.NumPad4:
				case Keys.NumPad5:
				case Keys.NumPad6:
				case Keys.NumPad7:
				case Keys.NumPad8:
				case Keys.NumPad9:
				case Keys.Decimal:
					ctrl.KeyDown(key);
					e.Cancel = true;
					break;
				case Keys.Add:
					ctrl.Add();
					e.Cancel = true;
					break;
				case Keys.Subtract:
					ctrl.Sub();
					e.Cancel = true;
					break;
				case Keys.Multiply:
					ctrl.Mul();
					e.Cancel = true;
					break;
				case Keys.Divide:
					ctrl.Div();
					e.Cancel = true;
					break;
				case Keys.Enter:
					if (e.IsLLKHF_EXTENDED) {
						ctrl.Enter();
						e.Cancel = true;
					}
					break;
				}
				if (e.Cancel) {
					downKey = key;
					ButtonUpdate(key, modeOn);
				}
			} else if (skin.IsLoad) {
				if (key == Keys.NumLock) {
					if (downKey == key) {
						e.Cancel = true;
						return;
					}
					downKey = key;
					Show();
					ButtonUpdate(key, modeOn);
				}
			}
		}

		private void Hook_KeyUpEvent(object sender, KeyHookEventArgs e)
		{
			if (Visible) {
				Keys key = ConvertKey(e.VkCode, e.IsLLKHF_EXTENDED);
				switch (key) {
				case Keys.ControlKey:
				case Keys.RControlKey:
				case Keys.LControlKey:
					BackgroundUpdate(ModifierKeys & ~Keys.Control);
					break;
				case Keys.ShiftKey:
				case Keys.RShiftKey:
				case Keys.LShiftKey:
					BackgroundUpdate(ModifierKeys & ~Keys.Shift);
					break;
				case Keys.Menu:
				case Keys.RMenu:
				case Keys.LMenu:
					BackgroundUpdate(ModifierKeys & ~Keys.Alt);
					break;
				}
				ButtonUpdate(key, modeOff);
			}
			downKey = Keys.None;
		}

		private void BackgroundUpdate(Keys key)
		{
			bool change = false;
			if (!skin.IsControl) {
				key &= ~Keys.Control;
			}
			if (!skin.IsShift) {
				key &= ~Keys.Shift;
			}
			if (!skin.IsAlt) {
				key &= ~Keys.Alt;
			}
			if ((key & Keys.Modifiers) == 0) {
				change = modeOn != Button.Status.On;
				modeOn = Button.Status.On;
				modeOff = Button.Status.Off;
			} else if (key == Keys.Control) {
				change = modeOn != Button.Status.OnControl;
				modeOn = Button.Status.OnControl;
				modeOff = Button.Status.OffControl;
			} else if (key == Keys.Shift) {
				change = modeOn != Button.Status.OnShift;
				modeOn = Button.Status.OnShift;
				modeOff = Button.Status.OffShift;
			} else if (key == Keys.Alt) {
				change = modeOn != Button.Status.OnAlt;
				modeOn = Button.Status.OnAlt;
				modeOff = Button.Status.OffAlt;
			}
			if (change) {
				Size = skin.BackgroundImages[modeOff].Size;
				pictureBox1.Image = skin.BackgroundImages[modeOff];
				pictureBox1.Refresh();
			}
		}

		private void ButtonUpdate(Keys key, Button.Status status)
		{
			var btn = skin.GetButton(key);
			if (btn != null) {
				using (Graphics g = Graphics.FromImage(pictureBox1.Image)) {
					g.DrawImage(btn.Bmp[status], btn.Rectangle);
				}
				pictureBox1.Refresh();
			}
		}

		private Keys ConvertKey(Keys keyCode, bool extended)
		{
			if (!extended) {
				switch (keyCode) {
				case Keys.Delete:
					keyCode = Keys.Decimal;
					break;
				case Keys.Insert:
					keyCode = Keys.NumPad0;
					break;
				case Keys.End:
					keyCode = Keys.NumPad1;
					break;
				case Keys.Down:
					keyCode = Keys.NumPad2;
					break;
				case Keys.PageDown:
					keyCode = Keys.NumPad3;
					break;
				case Keys.Left:
					keyCode = Keys.NumPad4;
					break;
				case Keys.Clear:
					keyCode = Keys.NumPad5;
					break;
				case Keys.Right:
					keyCode = Keys.NumPad6;
					break;
				case Keys.Home:
					keyCode = Keys.NumPad7;
					break;
				case Keys.Up:
					keyCode = Keys.NumPad8;
					break;
				case Keys.PageUp:
					keyCode = Keys.NumPad9;
					break;
				}
			}
			return keyCode;
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
