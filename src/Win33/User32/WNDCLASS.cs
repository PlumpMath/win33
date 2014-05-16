using System;
using System.Runtime.InteropServices;

namespace Win33.User32
{
   [StructLayout(LayoutKind.Sequential)]
   struct WNDCLASS
   {
      public ClassStyle style;
      [MarshalAs(UnmanagedType.FunctionPtr)]
      public WindowProcProcessor lpfnWndProc;
      public int cbClsExtra;
      public int cbWndExtra;
      public IntPtr hInstance;
      public IntPtr hIcon;
      public IntPtr hCursor;
      public IntPtr hbrBackground;
      public string lpszMenuName;
      public string lpszClassName;
   }
}
