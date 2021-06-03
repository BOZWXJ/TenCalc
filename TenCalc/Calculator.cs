using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TenCalc
{
	class Calculator
	{
		public bool IsEnter { get; private set; } = true;

		public void Clear()
		{
			//System.Diagnostics.Debug.WriteLine("Calculator Clear");
			IsEnter = true;
		}

		public void Enter()
		{
			//System.Diagnostics.Debug.WriteLine("Calculator Enter");
			IsEnter = true;
		}

		public void KeyDown(Keys key)
		{
			//System.Diagnostics.Debug.WriteLine($"Calculator {key}");
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
				IsEnter = false;
				break;
			}
		}

		public void Add()
		{
			//System.Diagnostics.Debug.WriteLine("Calculator Add");
			IsEnter = false;
		}

		public void Sub()
		{
			//System.Diagnostics.Debug.WriteLine("Calculator Sub");
			IsEnter = false;
		}

		public void Mul()
		{
			//System.Diagnostics.Debug.WriteLine("Calculator Mul");
			IsEnter = false;
		}

		public void Div()
		{
			//System.Diagnostics.Debug.WriteLine("Calculator Div");
			IsEnter = false;
		}

	}
}
