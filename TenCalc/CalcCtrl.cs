using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TenCalc
{
	class CalcCtrl
	{
		public bool IsClear { get; private set; } = true;

		public void KeyDown(Keys key)
		{
			System.Diagnostics.Debug.WriteLine(key);
			switch (key) {
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
				IsClear = false;
				break;
			}
		}

		public void Clear()
		{
			System.Diagnostics.Debug.WriteLine("Clear");
			IsClear = true;
		}

		public void Add()
		{
			System.Diagnostics.Debug.WriteLine("Add");
		}

		public void Sub()
		{
			System.Diagnostics.Debug.WriteLine("Sub");
		}

		public void Mul()
		{
			System.Diagnostics.Debug.WriteLine("Mul");
		}

		public void Div()
		{
			System.Diagnostics.Debug.WriteLine("Div");
		}

		public void Enter()
		{
			System.Diagnostics.Debug.WriteLine("Enter");
		}
	}
}
