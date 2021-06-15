using PInvoke;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TenCalc
{
	public static class Win32Api
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct KBDLLHOOKSTRUCT
		{
			/// <summary>
			/// 仮想キーコード 1-255
			/// </summary>
			public uint wVk;
			/// <summary>
			/// ハードウェアスキャンコード
			/// </summary>
			public uint wScan;
			/// <summary>
			/// 拡張キーフラグ、イベント挿入フラグ、コンテキストコード、および遷移状態フラグ。
			/// LLKHF_EXTENDED (KF_EXTENDED >> 8) 拡張キーフラグ
			/// LLKHF_LOWER_IL_INJECTED 0x00000002 イベント挿入フラグ（より低い整合性レベルのプロセス）
			/// LLKHF_INJECTED 0x00000010 イベント挿入フラグ（任意のプロセス）
			/// LLKHF_ALTDOWN (KF_ALTDOWN >> 8) Test the context code.
			/// LLKHF_UP (KF_UP >> 8) Test the transition-state flag.
			/// </summary>
			public KBDLLHOOKSTRUCTF dwFlags;
			public uint time;
			public IntPtr dwExtraInfo;
		}

		[Flags]
		public enum KBDLLHOOKSTRUCTF : uint
		{
			LLKHF_EXTENDED = 0x00000001,
			LLKHF_INJECTED = 0x00000010,
			LLKHF_ALTDOWN = 0x00000020,
			LLKHF_UP = 0x00000080,
			LLKHF_LOWER_IL_INJECTED = 0x00000002
		}

		public static void SendKeys(string str)
		{
			foreach (var c in str) {
				switch (c) {
				case '0':
					SendKey(Keys.NumPad0);
					break;
				case '1':
					SendKey(Keys.NumPad1);
					break;
				case '2':
					SendKey(Keys.NumPad2);
					break;
				case '3':
					SendKey(Keys.NumPad3);
					break;
				case '4':
					SendKey(Keys.NumPad4);
					break;
				case '5':
					SendKey(Keys.NumPad5);
					break;
				case '6':
					SendKey(Keys.NumPad6);
					break;
				case '7':
					SendKey(Keys.NumPad7);
					break;
				case '8':
					SendKey(Keys.NumPad8);
					break;
				case '9':
					SendKey(Keys.NumPad9);
					break;
				case '.':
					SendKey(Keys.Decimal);
					break;
				case '+':
					SendKey(Keys.Add);
					break;
				case '-':
					SendKey(Keys.Subtract);
					break;
				case '*':
					SendKey(Keys.Multiply);
					break;
				case '/':
					SendKey(Keys.Divide);
					break;
				default:
					System.Windows.Forms.SendKeys.Send(c.ToString());
					break;
				}
			}
		}

		public static void SendKey(Keys key)
		{
			int vk = (int)key;
			int scan = User32.MapVirtualKey(vk, User32.MapVirtualKeyTranslation.MAPVK_VK_TO_VSC);

			User32.INPUT[] inputs = new User32.INPUT[2];
			inputs[0].type = User32.InputType.INPUT_KEYBOARD;
			inputs[0].Inputs.ki.wVk = (User32.VirtualKey)vk;
			inputs[0].Inputs.ki.wScan = (User32.ScanCode)scan;
			inputs[0].Inputs.ki.dwFlags = 0;
			inputs[0].Inputs.ki.dwExtraInfo_IntPtr = IntPtr.Zero;
			inputs[0].Inputs.ki.time = 0;
			inputs[1].type = User32.InputType.INPUT_KEYBOARD;
			inputs[1].Inputs.ki.wVk = (User32.VirtualKey)vk;
			inputs[1].Inputs.ki.wScan = (User32.ScanCode)scan;
			inputs[1].Inputs.ki.dwFlags = User32.KEYEVENTF.KEYEVENTF_KEYUP;
			inputs[1].Inputs.ki.dwExtraInfo_IntPtr = IntPtr.Zero;
			inputs[1].Inputs.ki.time = 0;
			User32.SendInput(inputs.Length, inputs, Marshal.SizeOf(inputs[0]));
		}

	}
}
