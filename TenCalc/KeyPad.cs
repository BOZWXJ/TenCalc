using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TenCalc
{
	class KeyPad
	{
		readonly Calculator calc;
		readonly Display display;
		public KeyPad(Display disp)
		{
			calc = new Calculator(disp);
			display = disp;
		}

		public Mode Mode { get; private set; } = Mode.None;

		public Button DownButton { get; private set; } = Button.None;
		Button DownFlags = Button.None;

		public void AllClear()
		{
			calc.Clear();
			display.Clear();
		}

		Button MouseDownButton = Button.None;
		public Button MouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left) {
				ButtonData button = SkinData.GetButton(e.Location);
				if (button == null) {
					return Button.LButton;
				} else {
					Button btn = button.Key;
					if (!CheckButtonDown(ref btn)) {
						MouseDownButton = button.Key;
						SelectCalculatorMethod(button, out btn);
						return btn;
					}
				}
			} else if (e.Button == MouseButtons.Right) {
				return Button.RButton;
			}
			return Button.None;
		}

		public Button MouseUp(MouseEventArgs e)
		{
			if (MouseDownButton != Button.None) {
				Button btn = CheckButtonUp(MouseDownButton);
				return btn;
			}
			return Button.None;
		}

		public Button KeyDown(KeyHookEventArgs e)
		{
			Button btn = ConvertNumPadKey(e.VkCode, e.IsLLKHF_EXTENDED);
			if (!CheckButtonDown(ref btn)) {
				ButtonData button = SkinData.GetButton(btn);
				if (button != null) {
					e.Cancel = SelectCalculatorMethod(button, out btn);
				}
			} else {
				e.Cancel = true;
			}

			return btn;
		}

		public Button KeyUp(KeyHookEventArgs e)
		{
			Button btn = ConvertNumPadKey(e.VkCode, e.IsLLKHF_EXTENDED);
			btn = CheckButtonUp(btn);
			return btn;
		}

		private Button ConvertNumPadKey(Keys keyCode, bool extended)
		{
			// NumLock OFF 時の keyCode を変換
			// NumPad の Enter を区別
			Button result = Button.None;
			if (keyCode == Keys.NumPad0 || (keyCode == Keys.Insert && !extended)) {
				result = Button.NumPad0;
			} else if (keyCode == Keys.NumPad1 || (keyCode == Keys.End && !extended)) {
				result = Button.NumPad1;
			} else if (keyCode == Keys.NumPad2 || (keyCode == Keys.Down && !extended)) {
				result = Button.NumPad2;
			} else if (keyCode == Keys.NumPad3 || (keyCode == Keys.PageDown && !extended)) {
				result = Button.NumPad3;
			} else if (keyCode == Keys.NumPad4 || (keyCode == Keys.Left && !extended)) {
				result = Button.NumPad4;
			} else if (keyCode == Keys.NumPad5 || (keyCode == Keys.Clear && !extended)) {
				result = Button.NumPad5;
			} else if (keyCode == Keys.NumPad6 || (keyCode == Keys.Right && !extended)) {
				result = Button.NumPad6;
			} else if (keyCode == Keys.NumPad7 || (keyCode == Keys.Home && !extended)) {
				result = Button.NumPad7;
			} else if (keyCode == Keys.NumPad8 || (keyCode == Keys.Up && !extended)) {
				result = Button.NumPad8;
			} else if (keyCode == Keys.NumPad9 || (keyCode == Keys.PageUp && !extended)) {
				result = Button.NumPad9;
			} else if (keyCode == Keys.Decimal || (keyCode == Keys.Delete && !extended)) {
				result = Button.Decimal;
			} else if (keyCode == Keys.Add) {
				result = Button.Add;
			} else if (keyCode == Keys.Subtract) {
				result = Button.Sub;
			} else if (keyCode == Keys.Multiply) {
				result = Button.Mul;
			} else if (keyCode == Keys.Divide) {
				result = Button.Div;
			} else if (keyCode == Keys.NumLock) {
				result = Button.NumLock;
			} else if (keyCode == Keys.Enter && extended) {
				result = Button.Enter;
			} else if (keyCode == Keys.ShiftKey || keyCode == Keys.RShiftKey || keyCode == Keys.LShiftKey) {
				result = Button.ShiftKey;
			} else if (keyCode == Keys.ControlKey || keyCode == Keys.RControlKey || keyCode == Keys.LControlKey) {
				result = Button.ControlKey;
			} else if (keyCode == Keys.Menu || keyCode == Keys.RMenu || keyCode == Keys.LMenu) {
				result = Button.MenuKey;
			}
			return result;
		}

		private bool CheckButtonDown(ref Button btn)
		{
			// キーリピートのキャンセル
			bool cancel = false;
			switch (btn) {
			case Button.ControlKey:
				if (DownButton == Button.None && Mode == Mode.None && SkinData.IsControl) {
					Mode = Mode.Control;
				} else {
					btn = Button.None;
				}
				break;
			case Button.ShiftKey:
				if (DownButton == Button.None && Mode == Mode.None && SkinData.IsShift) {
					Mode = Mode.Shift;
				} else {
					btn = Button.None;
				}
				break;
			case Button.MenuKey:
				if (DownButton == Button.None && Mode == Mode.None && SkinData.IsAlt) {
					Mode = Mode.Alt;
					cancel = true;
				}
				break;
			default:
				if ((DownFlags & btn) == Button.None) {
					DownButton = btn;
					DownFlags |= btn;
				} else {
					// リピート時 キーフックメッセージをキャンセル
					cancel = true;
				}
				break;
			}
			return cancel;
		}

		private Button CheckButtonUp(Button btn)
		{
			DownFlags &= ~btn;
			Button result = Button.None;
			if (DownButton == btn) {
				DownButton = Button.None;
				result = btn;

				// 
				if (Control.ModifierKeys == Keys.Control && SkinData.IsControl) {
					Mode = Mode.Control;
				} else if (Control.ModifierKeys == Keys.Shift && SkinData.IsShift) {
					Mode = Mode.Shift;
				} else if (Control.ModifierKeys == Keys.Alt && SkinData.IsAlt) {
					Mode = Mode.Alt;
				} else if (Control.ModifierKeys == Keys.None) {
					Mode = Mode.None;
				}
			} else if (DownButton == Button.None) {
				switch (btn) {
				case Button.ControlKey:
					if (Mode == Mode.Control) {
						Mode = Mode.None;
						result = btn;
					}
					break;
				case Button.ShiftKey:
					if (Mode == Mode.Shift) {
						Mode = Mode.None;
						result = btn;
					}
					break;
				case Button.MenuKey:
					if (Mode == Mode.Alt) {
						Mode = Mode.None;
						result = btn;
					}
					break;
				}
			}
			return result;
		}

		private bool SelectCalculatorMethod(ButtonData button, out Button btn)
		{
			btn = button.Key;

			string cmd = string.Empty;
			if (button.Command.Length > (int)Mode) {
				cmd = button.Command[(int)Mode].ToLower();
			} else {
				return true;
			}

			bool cancel = true;
			switch (cmd) {
			case "numlock":
				if (display.IsInput) {
					display.Clear();
					btn = Button.Enter;
				} else {
					cancel = false;
				}
				break;
			case "enter":
				if (display.IsInput) {
					calc.Enter();
					btn = Button.Enter;
				} else {
					if (Properties.Settings.Default.SendResult) {
						// todo: 結果送信
					}
					// todo: NumLock ON
					if (Properties.Settings.Default.SendClose) {
						btn = Button.NumLock;
					}
				}
				break;
			case "input":
				display.KeyInput(button.Key);
				break;
			case "bs":
				display.BackSpace();
				break;
			case "+/-":
				display.Sign();
				break;
			case "exp":
				display.Exp();
				break;
			case "pi":
				display.Pi();
				break;

			case "add":
				calc.Add();
				break;
			case "sub":
				calc.Sub();
				break;
			case "mul":
				calc.Mul();
				break;
			case "div":
				calc.Div();
				break;

			case "mc":
			case "mr":
			case "m+":
			case "m-":
			case "%":
			case "mod":
			case "1/x":
			case "eng1":
			case "eng2":
			case "x^2":
			case "x^y":
			case "sqrt":
			case "cbrt":
			case "10^x":
			case "lg":
			case "e^x":
			case "ln":
			case "sin":
			case "cos":
			case "tan":
			case "asin":
			case "acos":
			case "atan":
			case "npr":
			case "ncr":

			case "comp":
			case "basen":
			case "deg":
			case "rad":
			case "gra":

			case "not":
			case "neg":
			case "a":
			case "b":
			case "c":
			case "d":
			case "e":
			case "f":
			case "and":
			case "or":
			case "xor":
			case "dec":
			case "hex":
			case "bin":
			case "oct":
				break;
			}
			return cancel;
		}
	}
}
