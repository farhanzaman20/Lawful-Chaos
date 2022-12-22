#nullable disable
using Terminal.Gui;

namespace Shin {
    class CharacterCreator {
        public static void Initialize() {
            Application.Init();
            Toplevel top = Application.Top;

            Window win = new Window(new Rect(0, 0, top.Frame.Width, top.Frame.Height), "Character Creator");
            top.Add(win);

            Button quitBtn = new Button() {
                Text = "Quit",
                X = top.Frame.Width - 10,
                Y = 0
            };

            int maxStatAllocation = 20;


            quitBtn.Clicked += () => {
                Application.RequestStop();
            };

            // STR
            StatLabel strLab = new StatLabel("STR", 2, 3, ref maxStatAllocation);
            strLab.Add(win);

            win.Add(quitBtn);
            Application.Run();
        }
    }

    class StatLabel {
        public StatLabel(string name, int x, int y, ref int maxStat) {
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
}