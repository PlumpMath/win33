using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Win33.Gdi32;

namespace Win33.Model.CommonControls
{
   public class TreeView : Window
   {
      public TreeView(IntPtr handle) : base(handle)
      {
      }

      public TreeView(Window w) : base(w)
      {
         
      }

      public Color BackgroundColor
      {
         get
         {
            IntPtr color = SendMessage(WindowMessage.TVM_GETBKCOLOR, IntPtr.Zero, IntPtr.Zero);

            return new COLORREF(color).GetColor();
         }
         set { SendMessage(WindowMessage.TVM_SETBKCOLOR, IntPtr.Zero, new IntPtr(new COLORREF(value).ColorDWORD)); }
      }

      public Color TextColor
      {
         get
         {
            IntPtr color = SendMessage(WindowMessage.TVM_GETTEXTCOLOR, IntPtr.Zero, IntPtr.Zero);

            return new COLORREF(color).GetColor();
         }
         set { SendMessage(WindowMessage.TVM_SETTEXTCOLOR, IntPtr.Zero, new IntPtr(new COLORREF(value).ColorDWORD)); }
      }
   }
}
