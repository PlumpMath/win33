using System;
using System.Runtime.InteropServices;

namespace Win33.User32
{
   [StructLayout(LayoutKind.Sequential)]
   struct COPYDATASTRUCT
   {
      public UInt32 dwData;
      public int cbData;
      public IntPtr lpData;
   }
}
