using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Win33.Gdi32;
using Win33.Model;
using Win33.Model.CommonControls;
using Win33.User32;

namespace Win33.Test
{
   [TestFixture]
   public class WindowsTest
   {
      [Test]
      public void AllWindowsTest()
      {
         var wins = Windows.All;

         Assert.IsTrue(wins.Count() > 0);

         string firstTitle = wins.First().Title;

         Assert.IsNotNull(firstTitle);

         List<string> allTitles = wins.Select(w => w.Title).ToList();
      }

      [Test]
      public void SetTitleTest()
      {
         var nonempty = Windows.All.First(w => w.Title.Length > 0);

         string oldTitle = nonempty.Title;

         try
         {
            string title = Guid.NewGuid().ToString();
            nonempty.Title = title;

            Assert.AreEqual(nonempty.Title, title);
         }
         finally
         {
            nonempty.Title = oldTitle;
         }
      }

      [Test]
      public void GetWindowPropsTest()
      {
         var first = Windows.All.First();

         Assert.Greater(first.ClassName.Length, 0);
      }

      [Test]
      public void GetDesktopWindowTest()
      {
         Window w = Windows.Desktop;

         Assert.AreNotEqual(IntPtr.Zero, w.Handle);
      }

      [Test, Ignore]
      public void ChangeSolutionExplorerColorTest()
      {
         Window vswin = Windows.All.First(w => w.Title.IndexOf("visual studio", StringComparison.InvariantCultureIgnoreCase) != -1);

         foreach (Window w in vswin.Children.Where(
               w => w.Title.IndexOf("solution explorer", StringComparison.InvariantCultureIgnoreCase) != -1))
         {

            COLORREF color = new COLORREF(Color.Red);
            w.SendMessage(0x1100 + 29, IntPtr.Zero, new IntPtr(color.ColorDWORD));
         }
      }

      [Test, Ignore]
      public void ChangeAllVsExplorsColorsTest()
      {
         foreach (Window w in Windows.All.Where(w => w.Title.IndexOf("visual studio", StringComparison.InvariantCultureIgnoreCase) != -1)
            .SelectMany(w => w.Children.Where(
               child => child.Title.IndexOf("solution explorer", StringComparison.InvariantCultureIgnoreCase) != -1)))
         {
            TreeView tv = new TreeView(w);

            //tv.TextColor = Color.Red;
            tv.TextColor = Color.Black;
         }
         
      }

      private IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
      {
         if (msg == (uint)WindowMessage.NCCREATE) return new IntPtr(1);

         return IntPtr.Zero;
      }

      [Test]
      public void RegisterClassTest()
      {
         ushort atom =
            Windows.RegisterClass(ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw | ClassStyle.DoubleClicks,
                                  "test class", new WindowProc());
      }

      [Test]
      public void RegisterWindowMessageTest()
      {
         uint msgId = Windows.RegisterMessage("MyCustomMessage");
      }

      [Test]
      public void CreateWindowTest()
      {
         string className = "test" + Guid.NewGuid().ToString();

         ushort atom =
            Windows.RegisterClass(ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw | ClassStyle.DoubleClicks,
                                  className, new WindowProc());

         Window wnd = Windows.CreateNew(WindowStyleEx.WS_EX_APPWINDOW | WindowStyleEx.WS_EX_WINDOWEDGE,
                                        className, "test window",
                                        WindowStyle.WS_BORDER | WindowStyle.WS_SYSMENU | WindowStyle.WS_MINIMIZEBOX,
                                        null, null, null);
      }

      [Test]
      public void SendSkypeMessageTest()
      {
         Windows.RegisterClass(ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw | ClassStyle.DoubleClicks,
                               "test class", new WindowProc());


      }

   }
}
