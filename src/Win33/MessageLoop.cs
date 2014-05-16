using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Win33.User32;

namespace Win33
{
   public class MessageLoop
   {
      public void RunIndefinite()
      {
         MSG msg;

         sbyte ret;
         while((ret = User32Lib.GetMessage(out msg, IntPtr.Zero, 0, 0)) != -1)
         {
            if (ret == -1)
            {
               //-1 indicates an error
            }
            else
            {
               User32Lib.TranslateMessage(ref msg);
               User32Lib.DispatchMessage(ref msg);
            }
         }
      }
   }
}
