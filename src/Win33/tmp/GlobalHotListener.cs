//using System;
//using System.Collections.Generic;
//using System.Runtime.InteropServices;
//using System.Windows.Forms;

//namespace CloudShot.Utils
//{
//    // NativeWindow class to listen to operating system messages.
//    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
//    internal class GlobalHotListener : NativeWindow
//    {
//        private const int WM_HOTKEY = 0x0312;

//        private Form _parent;

//        private Dictionary<int, Hotkey> _hotkeys = new Dictionary<int, Hotkey>();

//        #region WinApi
//        [DllImport("user32.dll", SetLastError = true)]
//        private static extern bool RegisterHotKey(IntPtr hWnd,               // handle to window    
//                                                  int id,                   // hot key identifier    
//                                                  KeyModifiers fsModifiers, // key-modifier options    
//                                                  Keys vk                   // virtual-key code    
//        );

//        [DllImport("user32.dll", SetLastError = true)]
//        private static extern bool UnregisterHotKey(IntPtr hWnd,             // handle to window    
//                                                    int id                  // hot key identifier    
//        );
//        #endregion

//        public GlobalHotListener(Form parent)
//        {
//            parent.HandleCreated += new EventHandler(this.OnHandleCreated);
//            parent.HandleDestroyed += new EventHandler(this.OnHandleDestroyed);
//            _parent = parent;
//        }

//        public void Add(Hotkey hotkey)
//        {
//            if (!RegisterHotKey(_parent.Handle, hotkey.Id, hotkey.Modifier, hotkey.Key))
//            {
//                throw new ArgumentException("Can't register hotkey.");
//            }

//            _hotkeys[hotkey.Id] = hotkey;
//        }

//        public void Remove(Hotkey hotkey)
//        {
//            UnregisterHotKey(_parent.Handle, hotkey.Id);
//            _hotkeys.Remove(hotkey.Id);
//        }

//        // Listen for the control's window creation and then hook into it.
//        internal void OnHandleCreated(object sender, EventArgs e)
//        {
//            // Window is now created, assign handle to NativeWindow.
//            AssignHandle(((Form)sender).Handle);
//        }

//        internal void OnHandleDestroyed(object sender, EventArgs e)
//        {
//            //Удаляем ненужные теперь хоткеи
//            foreach (Hotkey h in _hotkeys.Values)
//            {
//                UnregisterHotKey(this.Handle, h.Id);
//            }

//            _hotkeys.Clear();

//            // Window was destroyed, release hook.
//            ReleaseHandle();
//        }

//        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
//        protected override void WndProc(ref Message m)
//        {
//            // Listen for operating system messages
//            switch (m.Msg)
//            {
//                case WM_HOTKEY:
//                    int id = m.WParam.ToInt32();
//                    if (_hotkeys.ContainsKey(id))
//                    {
//                        _hotkeys[id].RaiseHotkey();
//                    }
//                    break;
//            }
//            base.WndProc(ref m);
//        }
//    }
//}
