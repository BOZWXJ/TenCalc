using PInvoke;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
			if (nCode >= 0 && (wParam == (IntPtr)User32.WindowMessage.WM_KEYDOWN || wParam == (IntPtr)User32.WindowMessage.WM_SYSKEYDOWN)) {
				var kb = (Win32Api.KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32Api.KBDLLHOOKSTRUCT));
				var vkCode = (int)kb.wVk;
				OnKeyDownEvent(vkCode);

				System.Diagnostics.Debug.WriteLine($"{kb.wVk} {kb.wScan} {kb.dwFlags:x} {kb.dwExtraInfo:x}");

			} else if (nCode >= 0 && (wParam == (IntPtr)User32.WindowMessage.WM_KEYUP || wParam == (IntPtr)User32.WindowMessage.WM_SYSKEYUP)) {
				var kb = (Win32Api.KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32Api.KBDLLHOOKSTRUCT));
				var vkCode = (int)kb.wVk;
				OnKeyUpEvent(vkCode);
			}
			return User32.CallNextHookEx(hookId.DangerousGetHandle(), nCode, wParam, lParam);
		}

		public delegate void KeyEventHandler(object sender, KeyEventArg e);
		public event KeyEventHandler KeyDownEvent;
		public event KeyEventHandler KeyUpEvent;

		protected void OnKeyDownEvent(int keyCode)
		{
			KeyDownEvent?.Invoke(this, new KeyEventArg(keyCode));
		}
		protected void OnKeyUpEvent(int keyCode)
		{
			KeyUpEvent?.Invoke(this, new KeyEventArg(keyCode));
		}
	}

	public class KeyEventArg : CancelEventArgs
	{
		public int KeyCode { get; }

		public KeyEventArg(int keyCode)
		{
			KeyCode = keyCode;
		}
	}

}
