//using System;
//using System.Windows.Forms;

//namespace CloudShot.Utils
//{
//  [Flags]
//  public enum KeyModifiers
//  {
//    None = 0,
//    Alt = 1,
//    Control = 2,
//    Shift = 4,
//    Windows = 8
//  }

//  public class Hotkey
//  {
//    private static int hotkeyIndex = 100;

//    public int Id { get; private set; }
//    public KeyModifiers Modifier { get; private set; }
//    public Keys Key { get; private set; }

//    #region Events

//    public event EventHandler<EventArgs> HotkeyPressed;

//    public void RaiseHotkey()
//    {
//      // Copy to a temporary variable to be thread-safe.
//      EventHandler<EventArgs> temp = HotkeyPressed;
//      if (temp != null)
//        temp(this, EventArgs.Empty);
//    }

//    #endregion

//    public Hotkey(KeyModifiers modifier, Keys key)
//    {
//      hotkeyIndex++;//просто счетчик
//      this.Id = hotkeyIndex;
//      this.Modifier = modifier;
//      this.Key = key;
//    }

//    public override string ToString()
//    {
//      string result = string.Empty;

//      if ((Modifier & KeyModifiers.Control) != 0)
//        result += "Ctrl + ";
//      if ((Modifier & KeyModifiers.Shift) != 0)
//        result += "Shift + ";
//      if ((Modifier & KeyModifiers.Alt) != 0)
//        result += "Alt + ";
//      if ((Modifier & KeyModifiers.Windows) != 0)
//        result += "Win + ";

//      result += Key.ToString();

//      return result;
//    }

//    public static bool TryParse(string value, out Hotkey hotkey)
//    {
//      hotkey = null;

//      if (string.IsNullOrEmpty(value))
//        return false;

//      KeyModifiers modifiers = KeyModifiers.None;
//      Keys key;

//      if (value.Contains("Control") || value.Contains("Ctrl"))
//        modifiers = modifiers | KeyModifiers.Control;

//      if (value.Contains("Shift"))
//        modifiers = modifiers | KeyModifiers.Shift;

//      if (value.Contains("Alt"))
//        modifiers = modifiers | KeyModifiers.Alt;

//      if (value.Contains("Win"))
//        modifiers = modifiers | KeyModifiers.Windows;

//      string keyValue = value;

//      int pos = value.LastIndexOf('+');
//      if ((pos != -1) && (pos + 1 < value.Length))
//      {
//        keyValue = value.Substring(pos + 1).Trim();
//      }

//      try
//      {
//        key = (Keys)Enum.Parse(typeof(Keys), keyValue);
//      }
//      catch (Exception)
//      {
//        return false;
//      }

//      hotkey = new Hotkey(modifiers, key);

//      return true;
//    }
//  }
//}
