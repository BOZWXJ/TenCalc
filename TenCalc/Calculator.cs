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
		readonly Display display;
		public Calculator(Display disp)
		{
			display = disp;
		}

		internal void Clear()
		{

		}

		public void Enter()
		{
			display.GetValue(out long significand, out int exponent);
			System.Diagnostics.Debug.WriteLine($"Enter {significand}*10^{exponent}");

			display.SetValue(significand, exponent);
		}

		public void Add()
		{
			display.GetValue(out long significand, out int exponent);
			System.Diagnostics.Debug.WriteLine($"Add {significand}*10^{exponent}");

			display.SetValue(significand, exponent);
		}

		public void Sub()
		{
			display.GetValue(out long significand, out int exponent);
			System.Diagnostics.Debug.WriteLine($"Sub {significand}*10^{exponent}");

			display.SetValue(significand, exponent);
		}

		public void Mul()
		{
			display.GetValue(out long significand, out int exponent);
			System.Diagnostics.Debug.WriteLine($"Mul {significand}*10^{exponent}");

			display.SetValue(significand, exponent);
		}

		public void Div()
		{
			display.GetValue(out long significand, out int exponent);
			System.Diagnostics.Debug.WriteLine($"Div {significand}*10^{exponent}");

			display.SetValue(significand, exponent);
		}

	}
}
