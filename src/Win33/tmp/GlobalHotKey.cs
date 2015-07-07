//using System;
//using System.Runtime.InteropServices;
//using System.Windows.Forms;

//namespace CloudShot.Utils
//{
//    static class GlobalHotKey
//    {
//        [DllImport("user32.dll", SetLastError = true)]
//        public static extern bool RegisterHotKey(IntPtr hWnd,               // handle to window    
//                                                  int id,                   // hot key identifier    
//                                                  KeyModifiers fsModifiers, // key-modifier options    
//                                                  Keys vk                   // virtual-key code    
//        );

//        [DllImport("user32.dll", SetLastError = true)]
//        public static extern bool UnregisterHotKey(IntPtr hWnd,             // handle to window    
//                                                    int id                  // hot key identifier    
//        );

//        [Flags]
//        public enum KeyModifiers
//        {
//            None = 0,
//            Alt = 1,
//            Control = 2,
//            Shift = 4,
//            Windows = 8
//        }
//    }
//}

///* ------- using HOTKEYs in a C# application -------

//   -- code snippet by James J Thompson --

// in form load : Ctrl+Shift+J

//  bool success = RegisterHotKey(Handle, 100, KeyModifiers.Control | KeyModifiers.Shift, Keys.J);


// in form closing :

//  UnregisterHotKey(Handle, 100);
 

// handling a hot key just pressed :

//     protected override void WndProc( ref Message m )
//     {	
//         const int WM_HOTKEY = 0x0312; 	
	
//         switch(m.Msg)	
//       {	
//             case WM_HOTKEY:	
				
//                 MessageBox.Show("Hotkey pressed");		

//                 ProcessHotkey();

//                 break;	
//         } 	
//         base.WndProc(ref m );
//     }
// * 
// * 
// * // Get the last error and display it.
//        int error = Marshal.GetLastWin32Error();
// */