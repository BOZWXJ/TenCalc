using PInvoke;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace TenCalc
{
	class KeyboardHook
	{
		private User32.WindowsHookDelegate proc;
		private User32.SafeHookHandle hookId = User32.SafeHookHandle.Null;

		public void Hook()
		{
			if (hookId == User32.SafeHookHandle.Null) {
				proc = HookProcedure;
				using (var curProcess = Process.GetCurrentProcess()) {
					using (ProcessModule curModule = curProcess.MainModule) {
						hookId = User32.SetWindowsHookEx(User32.WindowsHookType.WH_KEYBOARD_LL, proc, Kernel32.GetModuleHandle(curModule.ModuleName), 0);
					}
				}
			}
		}

		public void UnHook()
		{
			hookId.Close();
			hookId = User32.SafeHookHandle.Null;
		}

		public int HookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
		{
			bool cancel = false;
			if (nCode >= 0 && (wParam == (IntPtr)User32.WindowMessage.WM_KEYDOWN || wParam == (IntPtr)User32.WindowMessage.WM_SYSKEYDOWN)) {
				Win32Api.KBDLLHOOKSTRUCT kb = (Win32Api.KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32Api.KBDLLHOOKSTRUCT));
				if ((kb.dwFlags & Win32Api.KBDLLHOOKSTRUCTF.LLKHF_INJECTED) == 0) {
					cancel = OnKeyDownEvent(kb);
				}
			} else if (nCode >= 0 && (wParam == (IntPtr)User32.WindowMessage.WM_KEYUP || wParam == (IntPtr)User32.WindowMessage.WM_SYSKEYUP)) {
				Win32Api.KBDLLHOOKSTRUCT kb = (Win32Api.KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32Api.KBDLLHOOKSTRUCT));
				if ((kb.dwFlags & Win32Api.KBDLLHOOKSTRUCTF.LLKHF_INJECTED) == 0) {
					cancel = OnKeyUpEvent(kb);
				}
			}
			if (cancel) {
				return (int)new IntPtr(1);
			} else {
				return User32.CallNextHookEx(hookId.DangerousGetHandle(), nCode, wParam, lParam);
			}
		}

		public delegate void KeyEventHandler(object sender, KeyHookEventArgs e);
		public event KeyEventHandler KeyDownEvent;
		public event KeyEventHandler KeyUpEvent;

		protected bool OnKeyDownEvent(Win32Api.KBDLLHOOKSTRUCT kb)
		{
			KeyHookEventArgs e = new(kb);
			KeyDownEvent?.Invoke(this, e);
			return e.Cancel;
		}

		protected bool OnKeyUpEvent(Win32Api.KBDLLHOOKSTRUCT kb)
		{
			KeyHookEventArgs e = new(kb);
			KeyUpEvent?.Invoke(this, e);
			return e.Cancel;
		}
	}
	public class KeyHookEventArgs : CancelEventArgs
	{
		public Keys VkCode { get; }
		public uint ScanCode { get; }
		public Win32Api.KBDLLHOOKSTRUCTF Flags { get; }
		public bool IsLLKHF_EXTENDED { get; }
		public uint Time { get; }

		public KeyHookEventArgs(Win32Api.KBDLLHOOKSTRUCT kb)
		{
			VkCode = (Keys)kb.wVk;
			ScanCode = kb.wScan;
			Flags = kb.dwFlags;
			IsLLKHF_EXTENDED = (Flags & Win32Api.KBDLLHOOKSTRUCTF.LLKHF_EXTENDED) != 0;
			Time = kb.time;
		}
	}
}
