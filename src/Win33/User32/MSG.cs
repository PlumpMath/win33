using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Win33.User32
{
   [StructLayout(LayoutKind.Sequential)]
   struct MSG
   {
      public IntPtr hwnd;
      public uint message;
      public IntPtr wParam;
      public IntPtr lParam;
      public uint time;
      public POINT pt;
   }
}
