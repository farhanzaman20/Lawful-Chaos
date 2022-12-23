#nullable disable
using Terminal.Gui;

namespace Shin {
    static class CharacterCreator {
        
        public static void Initialize() {
            Application.Init();
            Toplevel top = Application.Top;

            Window win = new Window(new Rect(0, 0, top.Frame.Width, top.Frame.Height), "Character Creator");
            top.Add(win);
            GUISettings.Keybinds(win);

            Button quitBtn = new Button() {
                Text = "Quit",
                X = 0,
                Y = 0
            };

            quitBtn.Clicked += () => {
                Application.RequestStop();
            };

            // STR
            StatLabel strLabel = new StatLabel("STR", 5, 3);
            strLabel.Add(win);

            // MAG
            StatLabel magLabel = new StatLabel("MAG", 20, 3);
            magLabel.Add(win);

            // VIT
            StatLabel vitLabel = new StatLabel("VIT", 35, 3);
            vitLabel.Add(win);

            // AGL
            StatLabel aglLabel = new StatLabel("AGL", 50, 3);
            aglLabel.Add(win);

            // LUC
            StatLabel lucLabel = new StatLabel("LUC", 65, 3);
            lucLabel.Add(win);

            win.Add(quitBtn);
            Application.Run();
        }
    }

    class StatLabel {
        public StatLabel(string name, int x, int y) {
            Name = name;
            X = x;
            Y = y;

            MainLabel = new Label() {
                Text = Name,
                X = X,
                Y = Y
            };

            AllocatedPoints = new Label() {
                X = Pos.Right(MainLabel),
                Y = Y
            };
            RefreshPointLabel();

            MinusButton = new Button() {
                Text = "-",
                X = Pos.Left(AllocatedPoints) - 5,
                Y = Y + 1
            };

            MinusButton.Clicked += () => {
                if (PointAmmount > 5) {
                    PointAmmount--;
                    RefreshPointLabel();
                }
            };

            PlusButton = new Button() {
                Text = "+",
                X = Pos.Left(AllocatedPoints) + 1,
                Y = Y + 1
            };

            PlusButton.Clicked += () => {
                if (PointAmmount < 12) {
                    PointAmmount++;
                    RefreshPointLabel();
                }
            };
        }

        public Label MainLabel;
        public Button PlusButton;
        public Button MinusButton;
        public Label AllocatedPoints;
        private string Name;
        private int X;
        private int Y;
        private int PointAmmount = 5;

        public void Add(Window window) {
            window.Add(MainLabel, AllocatedPoints, MinusButton, PlusButton);
        }

        private void RefreshPointLabel() {
            AllocatedPoints.Text = $" - {PointAmmount}";
        }
    }

    static class GUISettings {
        public static void Keybinds(Window win) {
            // Add Vim keys for navigation
            win.AddKeyBinding(Key.H, Command.Left);
            win.AddKeyBinding(Key.L, Command.Right);

            
        }
    }
}