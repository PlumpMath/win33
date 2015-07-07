using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using Win33.Model;
using Win33.User32;

namespace Win33
{
   public static class Windows
   {
      private static Window _desktop;

      public static IEnumerable<Window> All
      {
         get
         {
            var hwnds = new List<IntPtr>();

            User32Lib.EnumWindows(
               (hwnd, lparam) =>
                  {
                     hwnds.Add(hwnd);
                     return true;
                  }, 0);

            return hwnds.Select(w => new Window(w));
         }
      }

      public static Window Desktop
      {
         get { return _desktop ?? (_desktop = new Window(User32Lib.GetDesktopWindow())); }
      }

      /// <summary>
      /// Registers window class
      /// </summary>
      /// <param name="style"></param>
      /// <param name="className"></param>
      /// <param name="windowProc"></param>
      /// <returns>Class atom if call succeeds, otherwise throws <see cref="Win32Exception"/></returns>
      public static ushort RegisterClass(ClassStyle style, string className, WindowProc windowProc)
      {
         if (className == null) throw new ArgumentNullException("className");
         if (windowProc == null) throw new ArgumentNullException("windowProc");

         var wndClass = new WNDCLASS
                           {
                              style = style,
                              lpfnWndProc = windowProc.NativeProcEntry,
                              cbClsExtra = 0,
                              cbWndExtra = 0,
                              hInstance = Process.GetCurrentProcess().Handle,
                              hIcon = IntPtr.Zero,
                              hCursor = IntPtr.Zero,
                              hbrBackground = IntPtr.Zero,
                              lpszMenuName = null,
                              lpszClassName = className
                           };

         ushort classAtom = User32Lib.RegisterClass(ref wndClass);

         if(0 == classAtom) throw new Win32Exception();

         windowProc.WndClass = wndClass;

         return classAtom;
      }

      public static uint RegisterMessage(string messageName)
      {
         uint messageId = User32Lib.RegisterWindowMessage(messageName);

         if(0 == messageId) throw new Win32Exception();

         return messageId;
      }

      public static Window CreateNew(WindowStyleEx exStyle, string className, string windowName, WindowStyle style,
         Point? location, Size? size, Window parent)
      {
         if (className == null) throw new ArgumentNullException("className");
         if (windowName == null) throw new ArgumentNullException("windowName");

         IntPtr hWnd = User32Lib.CreateWindowEx(exStyle, className, windowName, style,
                                                location == null ? 0 : location.Value.X,
                                                location == null ? 0 : location.Value.Y,
                                                size == null ? 128 : size.Value.Width,
                                                size == null ? 128 : size.Value.Height,
                                                parent == null ? IntPtr.Zero : parent.Handle,
                                                IntPtr.Zero,
                                                Process.GetCurrentProcess().Handle,
                                                IntPtr.Zero);

         if(hWnd == IntPtr.Zero)
         {
            var ex = new Win32Exception();

            if(0 == ex.NativeErrorCode)
            {
               throw new Win32Exception("CreateWindowEx finished with success, however HWND returned is null, did you return FALSE from WindowProc on WM_NCCREATE?", ex);
            }
            else
            {
               throw ex;
            }
         }

         return new Window(hWnd);
      }
   }
}
