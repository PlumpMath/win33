using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Win33.User32;

namespace Win33.Model
{
   public class Window : IEquatable<Window>
   {
      private static readonly IntPtr HwndBroadcast = new IntPtr(0xffff);

      private readonly IntPtr _handle;
      private string _text;
      private string _className;

      public IntPtr Handle { get { return _handle; } }

      public string Title
      {
         get { return _text ?? (_text = GetText(_handle)); }
         set
         {
            User32Lib.SetWindowText(_handle, value);

            _text = null;
         }
      }

      public string ClassName
      {
         get { return _className ?? (_className = GetClassName(_handle)); }
      }

      public IEnumerable<Window> Children
      {
         get { return GetChildren(_handle); }
      }

      public Window(IntPtr handle)
      {
         _handle = handle;
      }

      public Window(Window wnd)
      {
         _handle = wnd.Handle;
      }

      public IntPtr SendMessage(uint msg, IntPtr wParam, IntPtr lParam)
      {
         return User32Lib.SendMessage(_handle, msg, wParam, lParam);
      }

      public IntPtr SendMessage(WindowMessage msg, IntPtr wParam, IntPtr lParam)
      {
         return SendMessage((uint)msg, wParam, lParam);
      }

      public static void PostMessage(IntPtr handle, uint msg, IntPtr wParam, IntPtr lParam)
      {
         bool ok = User32Lib.PostMessage(handle, msg, wParam, lParam);
         if(!ok) throw new Win32Exception();
      }

      public static void PostMessage(IntPtr handle, WindowMessage msg, IntPtr wParam, IntPtr lParam)
      {
         PostMessage(handle, (uint)msg, wParam, lParam);
      }

      public void PostMessage(uint msg, IntPtr wParam, IntPtr lParam)
      {
         PostMessage(_handle, msg, wParam, lParam);
      }

      public void PostMessage(WindowMessage msg, IntPtr wParam, IntPtr lParam)
      {
         PostMessage(_handle, msg, wParam, lParam);
      }

      public static void SendBroadcastMessage(WindowMessage msg, IntPtr wParam, IntPtr lParam)
      {
         PostMessage(HwndBroadcast, msg, wParam, lParam);
      }

      public IntPtr SendCopyData(Window sourceWindow, byte[] data)
      {
         if (sourceWindow == null) throw new ArgumentNullException("sourceWindow");
         if (data == null) throw new ArgumentNullException("data");

         IntPtr dataBuffer = Marshal.AllocHGlobal(data.Length);
         IntPtr structBuffer = IntPtr.Zero;
         try
         {
            Marshal.Copy(data, 0, dataBuffer, data.Length);

            COPYDATASTRUCT cd = new COPYDATASTRUCT();
            cd.dwData = 0;
            cd.lpData = dataBuffer;
            cd.cbData = data.Length;
            int cdsz = Marshal.SizeOf(cd);
            structBuffer = Marshal.AllocHGlobal(cdsz);
            Marshal.StructureToPtr(cd, structBuffer, false);

            return User32Lib.SendMessage(_handle, (int)WindowMessage.COPYDATA, sourceWindow.Handle, structBuffer);
         }
         finally
         {
            if(structBuffer != IntPtr.Zero) Marshal.FreeHGlobal(structBuffer);
            Marshal.FreeHGlobal(dataBuffer);
         }
      }

      public override bool Equals(object right)
      {
         if (ReferenceEquals(right, null)) return false;

         if (ReferenceEquals(this, right)) return true;

         if (GetType() != right.GetType()) return false;

         return Equals((Window) right);
      }

      public override int GetHashCode()
      {
         return _handle.GetHashCode();
      }

      public bool Equals(Window other)
      {
         if (ReferenceEquals(other, null)) return false;

         return _handle == other.Handle;
      }

      private static string GetText(IntPtr handle)
      {
         int length = User32Lib.GetWindowTextLength(handle);
         StringBuilder sb = new StringBuilder(length + 1);
         User32Lib.GetWindowText(handle, sb, sb.Capacity);
         return sb.ToString();
      }

      private static string GetClassName(IntPtr handle)
      {
         StringBuilder strClassName = new StringBuilder(512);
         User32Lib.GetClassName(handle, strClassName, strClassName.Capacity);
         return strClassName.ToString();
      }

      private static IEnumerable<Window> GetChildren(IntPtr handle)
      {
         List<IntPtr> childHandles = new List<IntPtr>();

         User32Lib.EnumChildWindows(handle, (hwnd, lParam) =>
                                                {
                                                   childHandles.Add(hwnd);
                                                   return true;
                                                }, IntPtr.Zero);

         return childHandles.Select(h => new Window(h));
      }

      public override string ToString()
      {
         return Title;
      }
   }
}
