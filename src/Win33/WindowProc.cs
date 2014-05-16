using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Win33.User32;

namespace Win33
{
   internal delegate IntPtr WindowProcProcessor(IntPtr windowHandle, uint msg, IntPtr wParam, IntPtr lParam);

   public class MessageEventArgs : EventArgs
   {
      public IntPtr WindowHandle { get; private set; }
      public WindowMessage Message { get; private set; }
      public IntPtr WParam { get; private set; }
      public IntPtr LParam { get; private set; }

      /// <summary>
      /// Set this code to override window proc result
      /// </summary>
      public IntPtr? Result { get; set; }

      public MessageEventArgs(IntPtr windowHandle, WindowMessage message, IntPtr wParam, IntPtr lParam)
      {
         this.WindowHandle = windowHandle;
         this.Message = message;
         this.WParam = wParam;
         this.LParam = lParam;
      }
   }

   public class CopyDataEventArgs : EventArgs
   {
      public byte[] Data { get; private set; }

      public IntPtr Result { get; set; }

      public CopyDataEventArgs(byte[] data)
      {
         this.Data = data;
      }

      public string AsString(Encoding encoding)
      {
         if (encoding == null) throw new ArgumentNullException("encoding");
         if (Data == null) return null;

         return encoding.GetString(Data);
      }
   }

   public class WindowProc
   {
      public event EventHandler<MessageEventArgs> MessageReceived;

      public event EventHandler<CopyDataEventArgs> DataCopied;

      internal WNDCLASS WndClass { get; set; }

      internal IntPtr NativeProcEntry(IntPtr hWnd, uint msgIdx, IntPtr wParam, IntPtr lParam)
      {
         WindowMessage msg = (WindowMessage) msgIdx;
         //Console.WriteLine("33WM: " + msg);

         switch(msg)
         {
            case WindowMessage.NCCREATE:
               return new IntPtr(1);
            case WindowMessage.COPYDATA:
               if (DataCopied != null)
               {
                  return ThrowCopyData(Message.Create(hWnd, (int)msgIdx, wParam, lParam));
               }
               break;
            default:
               if (MessageReceived != null)
               {
                  var args = new MessageEventArgs(hWnd, msg, wParam, lParam);
                  MessageReceived(this, args);

                  if (args.Result != null)
                  {
                     //Console.WriteLine("overriden result: " + args.Result);
                     return args.Result.Value;
                  }
               }

               IntPtr result = User32Lib.DefWindowProc(hWnd, msgIdx, wParam, lParam);
               return result;
         }

         return IntPtr.Zero;
      }

      private IntPtr ThrowCopyData(Message msg)
      {
         COPYDATASTRUCT cd = (COPYDATASTRUCT)msg.GetLParam(typeof (COPYDATASTRUCT));
         byte[] buffer = new byte[cd.cbData];
         Marshal.Copy(cd.lpData, buffer, 0, cd.cbData);

         var args = new CopyDataEventArgs(buffer);
         DataCopied(this, args);

         return args.Result;
      }

      static void PostQuitMessage(int exitCode)
      {
         User32Lib.PostQuitMessage(exitCode);
      }
   }
}
