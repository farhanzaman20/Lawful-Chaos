#nullable disable
using Terminal.Gui;

namespace Shin {
    static class CharacterCreator {
        public static void Character() {

        }

        public static void StatInit() {
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

            int statTotal = 20;
            Label totalLabel = new Label() {
                Text = $"Remaining Stat Points to Allocate: {statTotal}",
                X = 20,
                Y = 1
            };

            // STR
            StatLabel strLabel = new StatLabel("STR", 5, 3);
            strLabel.MinusButton.Clicked += () => {
                if (strLabel.PointAmmount > 5 && statTotal < 20) {
                    strLabel.PointAmmount--;
                    strLabel.RefreshPointLabel();
                    statTotal++;
                    totalLabel.Text = $"Remaining Stat Points to Allocate: {statTotal}";
                }
            };

            strLabel.PlusButton.Clicked += () => {
                if (strLabel.PointAmmount <= 12 && statTotal > 0) {
                    strLabel.PointAmmount++;
                    strLabel.RefreshPointLabel();
                    statTotal--;
                    totalLabel.Text = $"Remaining Stat Points to Allocate: {statTotal}";
                }
            };

            strLabel.Add(win);

            // MAG
            StatLabel magLabel = new StatLabel("MAG", 20, 3);
            magLabel.MinusButton.Clicked += () => {
                if (magLabel.PointAmmount > 5 && statTotal < 20) {
                    magLabel.PointAmmount--;
                    magLabel.RefreshPointLabel();
                    statTotal++;
                    totalLabel.Text = $"Remaining Stat Points to Allocate: {statTotal}";
                }
            };

            magLabel.PlusButton.Clicked += () => {
                if (magLabel.PointAmmount <= 12 && statTotal > 0) {
                    magLabel.PointAmmount++;
                    magLabel.RefreshPointLabel();
                    statTotal--;
                    totalLabel.Text = $"Remaining Stat Points to Allocate: {statTotal}";
                }
            };
            magLabel.Add(win);

            // VIT
            StatLabel vitLabel = new StatLabel("VIT", 35, 3);
            vitLabel.MinusButton.Clicked += () => {
                if (vitLabel.PointAmmount > 5 && statTotal < 20) {
                    vitLabel.PointAmmount--;
                    vitLabel.RefreshPointLabel();
                    statTotal++;
                    totalLabel.Text = $"Remaining Stat Points to Allocate: {statTotal}";
                }
            };

            vitLabel.PlusButton.Clicked += () => {
                if (vitLabel.PointAmmount <= 12 && statTotal > 0) {
                    vitLabel.PointAmmount++;
                    vitLabel.RefreshPointLabel();
                    statTotal--;
                    totalLabel.Text = $"Remaining Stat Points to Allocate: {statTotal}";
                }
            };
            vitLabel.Add(win);

            // AGL
            StatLabel aglLabel = new StatLabel("AGL", 50, 3);
            aglLabel.MinusButton.Clicked += () => {
                if (aglLabel.PointAmmount > 5 && statTotal < 20) {
                    aglLabel.PointAmmount--;
                    aglLabel.RefreshPointLabel();
                    statTotal++;
                    totalLabel.Text = $"Remaining Stat Points to Allocate: {statTotal}";
                }
            };

            aglLabel.PlusButton.Clicked += () => {
                if (aglLabel.PointAmmount <= 12 && statTotal > 0) {
                    aglLabel.PointAmmount++;
                    aglLabel.RefreshPointLabel();
                    statTotal--;
                    totalLabel.Text = $"Remaining Stat Points to Allocate: {statTotal}";
                }
            };
            aglLabel.Add(win);

            // LUC
            StatLabel lucLabel = new StatLabel("LUC", 65, 3);
            lucLabel.MinusButton.Clicked += () => {
                if (lucLabel.PointAmmount > 5 && statTotal < 20) {
                    lucLabel.PointAmmount--;
                    lucLabel.RefreshPointLabel();
                    statTotal++;
                    totalLabel.Text = $"Remaining Stat Points to Allocate: {statTotal}";
                }
            };

            lucLabel.PlusButton.Clicked += () => {
                if (lucLabel.PointAmmount <= 12 && statTotal > 0) {
                    lucLabel.PointAmmount++;
                    lucLabel.RefreshPointLabel();
                    statTotal--;
                    totalLabel.Text = $"Remaining Stat Points to Allocate: {statTotal}";
                }
            };
            lucLabel.Add(win);

            Label statInfo = new Label {
                X = 3,
                Y = 8,
                Text = @"
                STR: Physical attack and hit rate
                MAG: Magic attack and defence
                VIT: Health and physcial defence
                AGL: Speed and evasion
                LUC: Various effects"
            };

            Button confirmBtn = new Button {
                X = 35,
                Y = 6,
                Text = "Confirm"
            };

            confirmBtn.Clicked += () => {
                int nestedConfirm = MessageBox.Query(
                    50, 7, "Confirm", "Are you okay with this stat distribution?", "Yes", "No"
                );

                if (nestedConfirm == 0 )
            };

            win.Add(statInfo, confirmBtn, totalLabel, quitBtn);
            Application.Run();
        }
    }

    internal class StatLabel {
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

            PlusButton = new Button() {
                Text = "+",
                X = Pos.Left(AllocatedPoints) + 1,
                Y = Y + 1
            };
        }

        public Label MainLabel;
        public Button PlusButton;
        public Button MinusButton;
        public Label AllocatedPoints;
        private string Name;
        private int X;
        private int Y;
        public int PointAmmount = 5;

        public void Add(Window window) {
            window.Add(MainLabel, AllocatedPoints, MinusButton, PlusButton);
        }

        public void RefreshPointLabel() {
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