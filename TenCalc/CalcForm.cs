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
	public enum Mode { None, Control, Shift, Alt };
	public enum Status { On, Off }

	public partial class CalcForm : Form
	{
		readonly Calculator calc = new();
		readonly KeyboardHook hook = new();
		readonly KeyPad keyPad = new();
		Image FormImage;

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
				FormUpdate(Keys.None);
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
		Keys MouseDownKey;
		private void CalcForm_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left) {
				var btn = SkinData.GetButton(e.Location);
				if (btn == null) {
					downPoint = new Size(e.Location);
					formDrag = true;
				} else {
					(var key, var can) = keyPad.KeyDown(btn.Key);
					if (key != Keys.None) {
						MouseDownKey = key;
						calc.KeyDown(MouseDownKey);
						FormUpdate(MouseDownKey);
					}
				}
			} else if (e.Button == MouseButtons.Right) {
				contextMenuStrip1.Show(this, e.Location);
			}
		}

		private void CalcForm_MouseUp(object sender, MouseEventArgs e)
		{
			if (MouseDownKey != Keys.None) {
				keyPad.KeyUp(MouseDownKey);
				FormUpdate(MouseDownKey);
			}
			formDrag = false;
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
			bool cancel;
			(key, cancel) = keyPad.KeyDown(key);
			if (Visible) {
				if (key != Keys.None) {
					switch (key) {
					case Keys.NumLock:
						if (!calc.IsEnter) {
							calc.Clear();
							e.Cancel = true;
						} else {
							Hide();
						}
						break;
					case Keys.MButton:  // Enter
						if (!calc.IsEnter) {
							calc.Enter();
							e.Cancel = true;
						} else {
							// todo: 結果送信
							// todo: NumLock ON
							e.Cancel = true;
							Hide();
						}
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
						calc.KeyDown(key);
						e.Cancel = true;
						break;
					case Keys.Add:
						calc.Add();
						e.Cancel = true;
						break;
					case Keys.Subtract:
						calc.Sub();
						e.Cancel = true;
						break;
					case Keys.Multiply:
						calc.Mul();
						e.Cancel = true;
						break;
					case Keys.Divide:
						calc.Div();
						e.Cancel = true;
						break;
					}
					FormUpdate(key);
				} else {
					e.Cancel = cancel;
				}
			} else if (SkinData.IsLoad && key == Keys.NumLock && IsKeyLocked(Keys.NumLock) != Properties.Settings.Default.NumLock) {
				FormUpdate(Keys.None);
				Show();
			}
		}

		private void Hook_KeyUpEvent(object sender, KeyHookEventArgs e)
		{
			Keys key = ConvertKey(e.VkCode, e.IsLLKHF_EXTENDED);
			key = keyPad.KeyUp(key);
			if (Visible && key != Keys.None) {
				FormUpdate(key);
			}
		}

		private void FormUpdate(Keys key)
		{
			if (!SkinData.IsLoad) {
				return;
			}
			Bitmap bmp = SkinData.OffImages[keyPad.Mode];
			FormImage = (Image)bmp.Clone();
			using (Graphics g = Graphics.FromImage(FormImage)) {
				// todo: 表示
				// todo: ボタン
				var btn = SkinData.GetButton(key);
				if (btn != null) {
					if (keyPad.DownKey != Keys.None) {
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
			} else {
				switch (keyCode) {
				case Keys.Enter:
					keyCode = Keys.MButton; // Enter
					break;
				}
			}
			return keyCode;
		}
		#endregion

		#region メニュー項目
		private void toolStripMenuItemSetting_Click(object sender, EventArgs e)
		{
			var dlg = new SettingForm();
			if (dlg.ShowDialog() == DialogResult.OK && SkinData.Name != Properties.Settings.Default.SkinName) {
				SkinData.LoadSkinFile();
				FormUpdate(Keys.None);
			}
		}

		private void toolStripMenuItemInfo_Click(object sender, EventArgs e)
		{
			var dlg = new AboutBox();
			dlg.ShowDialog();
		}

		private void toolStripMenuItemExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		#endregion

	}
}
