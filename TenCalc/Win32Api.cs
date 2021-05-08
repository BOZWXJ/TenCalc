using PInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TenCalc
{
	class Win32Api
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct KBDLLHOOKSTRUCT
		{
			public uint wVk;
			public uint wScan;
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
	}
}
