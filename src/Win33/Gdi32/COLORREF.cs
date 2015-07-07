using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;

namespace Win33.Gdi32
{
   [StructLayout(LayoutKind.Sequential)]
   public struct COLORREF
   {
      public uint ColorDWORD;

      public COLORREF(Color color)
      {
         ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
      }

      public COLORREF(IntPtr color)
      {
         ColorDWORD = (uint) color.ToInt32();
      }

      public Color GetColor()
      {
         return System.Drawing.Color.FromArgb((int)(0x000000FFU & ColorDWORD),
        (int)(0x0000FF00U & ColorDWORD) >> 8, (int)(0x00FF0000U & ColorDWORD) >> 16);
      }

      public void SetColor(Color color)
      {
         ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
      }
   }
}
