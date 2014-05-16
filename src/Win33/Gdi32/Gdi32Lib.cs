using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Win33.Gdi32
{
   static class Gdi32Lib
   {
      [DllImport("gdi32.dll")]
      internal static extern uint SetBkColor(IntPtr hdc, int crColor);
   }
}
