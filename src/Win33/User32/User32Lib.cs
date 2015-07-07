using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Win33.User32
{
   static class User32Lib
   {
      internal delegate bool CallBackPtr(IntPtr hwnd, int lParam);
      internal delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

      [DllImport("user32.dll")]
      internal static extern int EnumWindows(CallBackPtr callPtr, int lPar);

      [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

      [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
      internal static extern int GetWindowTextLength(IntPtr hWnd);

      [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
      internal static extern bool SetWindowText(IntPtr hwnd, String lpString);

      [DllImport("user32.dll")]
      internal static extern int GetClassName(IntPtr hWnd, StringBuilder buf, int nMaxCount);

      [DllImport("user32.dll")]
      internal static extern IntPtr GetDesktopWindow();

      [DllImport("user32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

      [return: MarshalAs(UnmanagedType.Bool)]
      [DllImport("user32.dll", SetLastError = true)]
      internal static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

      [DllImport("user32.dll")]
      internal static extern ushort RegisterClass([In] ref WNDCLASS lpWndClass);

      [DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
      internal static extern uint RegisterWindowMessage(string lpString);

      [DllImport("user32.dll", SetLastError = true)]
      internal static extern IntPtr CreateWindowEx(
         WindowStyleEx dwExStyle,
         string lpClassName,
         string lpWindowName,
         WindowStyle dwStyle,
         int x,
         int y,
         int nWidth,
         int nHeight,
         IntPtr hWndParent,
         IntPtr hMenu,
         IntPtr hInstance,
         IntPtr lpParam);

      [DllImport("user32.dll")]
      internal static extern IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

      [DllImport("user32.dll")]
      internal static extern void PostQuitMessage(int nExitCode);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="lpMsg"></param>
      /// <param name="hWnd"></param>
      /// <param name="wMsgFilterMin"></param>
      /// <param name="wMsgFilterMax"></param>
      /// <returns>Function can return TRUE, FALSE or -1 so return type is marshalled as sbyte (size is 1b just like BYTE)</returns>
      [DllImport("user32.dll")]
      internal static extern sbyte GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

      [DllImport("user32.dll")]
      internal static extern bool TranslateMessage([In] ref MSG lpMsg);

      [DllImport("user32.dll")]
      internal static extern IntPtr DispatchMessage([In] ref MSG lpmsg);
   }
}
