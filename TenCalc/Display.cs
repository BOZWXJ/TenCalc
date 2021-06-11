using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TenCalc
{
	class Display
	{
		public bool IsInput { get { return InputLen > 0; } }
		// 仮数部
		public Digit[] Value { get; private set; }
		public const int ValueSize = 10;
		// 指数部
		public char[] Exponent { get; private set; }
		public const int ExponentSize = 2;
		// 表示
		public bool Add { get; private set; }
		public bool Sub { get; private set; }
		public bool Mul { get; private set; }
		public bool Div { get; private set; }
		public bool K { get; private set; }
		public bool Deg { get; private set; }
		public bool Rad { get; private set; }
		public bool Gra { get; private set; }
		public bool BaseN { get; private set; }
		public bool Dec { get; private set; }
		public bool Hex { get; private set; }
		public bool Bin { get; private set; }
		public bool Oct { get; private set; }

		public Display()
		{
			Value = new Digit[ValueSize + 1];
			Exponent = new char[ExponentSize + 1];
			for (int i = 0; i < Value.Length; i++) {
				Value[i] = new Digit();
			}
			Clear();
		}

		#region 入力処理

		int InputLen = 0;
		bool InputExp = false;
		bool DecimalPoint = false;

		public void Clear()
		{
			InputLen = 0;
			InputExp = false;
			DecimalPoint = false;

			Value[0].Character = '0';
			Value[0].DecimalPoint = true;
			for (int i = 1; i < Value.Length; i++) {
				Value[i].Character = ' ';
				Value[i].DecimalPoint = false;
			}
			Exponent[0] = ' ';
			Exponent[1] = ' ';
			Exponent[2] = ' ';
		}

		public void KeyInput(Button key)
		{
			if (InputLen >= ValueSize) { return; }

			switch (key) {
			case Button.NumPad0:
			case Button.NumPad1:
			case Button.NumPad2:
			case Button.NumPad3:
			case Button.NumPad4:
			case Button.NumPad5:
			case Button.NumPad6:
			case Button.NumPad7:
			case Button.NumPad8:
			case Button.NumPad9:
				InsertNum(key.ToString()[6]);
				break;
			case Button.Decimal:
				if (!InputExp && !DecimalPoint) {
					DecimalPoint = true;
					if (InputLen == 0) {
						InputLen++;
					}
				}
				break;
			default:
				return;
			}
		}
		private void InsertNum(char c)
		{
			if (InputExp) {
				Exponent[1] = Exponent[0];
				Exponent[0] = c;
				return;
			}

			if (InputLen == 0) {
				Clear();
				Value[0].Character = c;
				InputLen = 1;
				return;
			}
			if (InputLen == 1 && !DecimalPoint && Value[0].Character == '0') {
				Value[0].Character = c;
				return;
			}
			if (InputLen == ValueSize) {
				return;
			}

			InputLen++;
			for (int i = Value.Length - 1; i > 0; i--) {
				Value[i].Character = Value[i - 1].Character;
				if (DecimalPoint) {
					Value[i].DecimalPoint = Value[i - 1].DecimalPoint;
				}
			}
			Value[0].Character = c;
			Value[0].DecimalPoint = !DecimalPoint;
		}

		public void BackSpace()
		{
			if (InputExp) {
				if (Exponent[0] == '0' && Exponent[1] == '0') {
					Exponent[0] = ' ';
					Exponent[1] = ' ';
					Exponent[2] = ' ';
					InputExp = false;
				} else {
					Exponent[0] = Exponent[1];
					Exponent[1] = '0';
				}
				return;
			}

			if (InputLen == 0) {
				return;
			}
			InputLen--;
			if (DecimalPoint && Value[0].DecimalPoint) {
				DecimalPoint = false;
			}
			for (int i = 0; i < Value.Length - 1; i++) {
				Value[i].Character = Value[i + 1].Character;
				Value[i].DecimalPoint = Value[i + 1].DecimalPoint;
			}
			Value[Value.Length - 1].Character = ' ';
			Value[Value.Length - 1].DecimalPoint = false;
			if (InputLen == 0) {
				Value[0].Character = '0';
			}
			if (!DecimalPoint) {
				Value[0].DecimalPoint = true;
			}
		}

		public void Sign()
		{
			if (InputExp) {
				if (Exponent[2] == ' ') {
					Exponent[2] = '-';
				} else {
					Exponent[2] = ' ';
				}
				return;
			}

			if (InputLen == 0 && Value[0].Character == '0' && Value[1].Character == ' ') { return; }

			for (int i = 1; i < Value.Length; i++) {
				if (Value[i].Character == ' ') {
					Value[i].Character = '-';
					break;
				} else if (Value[i].Character == '-') {
					Value[i].Character = ' ';
					break;
				}
			}
		}

		public void Exp()
		{
			if (!InputExp) {
				if (InputLen == 0) {
					Clear();
					InputLen = 1;
					Value[0].Character = '1';
				}
				InputExp = true;
				Exponent[0] = '0';
				Exponent[1] = '0';
			}
		}

		public void Pi()
		{
			Clear();
			Value[0].Character = '4';
			Value[0].DecimalPoint = false;
			Value[1].Character = '5';
			Value[2].Character = '6';
			Value[3].Character = '2';
			Value[4].Character = '9';
			Value[5].Character = '5';
			Value[6].Character = '1';
			Value[7].Character = '4';
			Value[8].Character = '1';
			Value[9].Character = '3';
			Value[9].DecimalPoint = true;
		}

		#endregion

		public void GetValue(out long significand, out int exponent)
		{
			exponent = 0;
			if (Exponent[0] != ' ') {
				exponent = (Exponent[0] - '0') + ((Exponent[1] - '0') * 10);
				if (Exponent[2] == '-') {
					exponent = -exponent;
				}
			}

			significand = 0;
			long x = 1;
			bool p = false;
			for (int i = 0; i < ValueSize; i++) {
				if ('0' <= Value[i].Character && Value[i].Character <= '9') {
					significand += int.Parse(Value[i].Character.ToString()) * x;
				} else if (Value[i].Character == '-') {
					significand = -significand;
				}
				if (!p && !Value[i].DecimalPoint) {
					exponent--;
				} else {
					p = true;
				}
				x *= 10;
			}
		}

		public void SetValue(long significand, int exponent)
		{
			Clear();



		}

		public void SetError()
		{
		}

		public override string ToString()
		{
			return $"|{string.Concat(Value.Reverse().Select(p => $"{p.Character}{(p.Comma ? "," : "")}{(p.DecimalPoint ? "." : "")}"))}|{new string(Exponent.Reverse().ToArray())}|{InputLen},{DecimalPoint}";
		}

	}

	public class Digit
	{
		public char Character = ' ';
		public bool DecimalPoint = false;
		public bool Comma = false;
	}
}
