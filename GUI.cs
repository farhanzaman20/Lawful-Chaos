#nullable disable
using Terminal.Gui;

namespace Shin {
    class CharacterCreator {
        public static void Initialize() {
            Application.Init();
            Toplevel top = Application.Top;

            Window win = new Window(new Rect(0, 0, top.Frame.Width, top.Frame.Height), "");
            top.Add(win);



            Application.Run();
        }
    }
}