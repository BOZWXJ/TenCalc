using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TenCalc
{
	// キーリピート、同時押しをキャンセルする
	class KeyPad
	{
		public Mode Mode { get; private set; } = Mode.None;
		public Keys DownKey { get; private set; } = Keys.None;

		public (Keys key, bool cancel) KeyDown(Keys keyCode)
		{
			Keys key = Keys.None;
			bool cancel = false;

			switch (keyCode) {
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
			case Keys.MButton:  // Enter
			case Keys.Add:
			case Keys.Subtract:
			case Keys.Multiply:
			case Keys.Divide:
			case Keys.NumLock:
				if (DownKey == Keys.None) {
					DownKey = keyCode;
					key = keyCode;
				}
				cancel = true;
				break;
			case Keys.ControlKey:
			case Keys.RControlKey:
			case Keys.LControlKey:
				if (DownKey == Keys.None && SkinData.IsControl) {
					Mode = Mode.Control;
					key = keyCode;
				}
				break;
			case Keys.ShiftKey:
			case Keys.RShiftKey:
			case Keys.LShiftKey:
				if (DownKey == Keys.None && SkinData.IsShift) {
					Mode = Mode.Shift;
					key = keyCode;
				}
				break;
			case Keys.Menu:
			case Keys.RMenu:
			case Keys.LMenu:
				if (DownKey == Keys.None && SkinData.IsAlt) {
					Mode = Mode.Alt;
					key = keyCode;
				}
				break;
			}
			return (key, cancel);
		}

		public Keys KeyUp(Keys keyCode)
		{
			Keys result = Keys.None;

			if (DownKey == keyCode) {
				DownKey = Keys.None;
				result = keyCode;
				if (Control.ModifierKeys == Keys.Control && SkinData.IsControl) {
					Mode = Mode.Control;
				} else if (Control.ModifierKeys == Keys.Shift && SkinData.IsShift) {
					Mode = Mode.Shift;
				} else if (Control.ModifierKeys == Keys.Alt && SkinData.IsAlt) {
					Mode = Mode.Alt;
				} else {
					Mode = Mode.None;
				}
			} else if (DownKey == Keys.None) {
				switch (keyCode) {
				case Keys.ControlKey:
				case Keys.RControlKey:
				case Keys.LControlKey:
					if (Mode == Mode.Control) {
						Mode = Mode.None;
						result = keyCode;
					}
					break;
				case Keys.ShiftKey:
				case Keys.RShiftKey:
				case Keys.LShiftKey:
					if (Mode == Mode.Shift) {
						Mode = Mode.None;
						result = keyCode;
					}
					break;
				case Keys.Menu:
				case Keys.RMenu:
				case Keys.LMenu:
					if (Mode == Mode.Alt) {
						Mode = Mode.None;
						result = keyCode;
					}
					break;
				}
			}
			return result;
		}
	}
}
