#nullable disable

namespace Shin {
    class Program {
        static void Main () {
            CharactorCreation();
        }

        static void CharactorCreation() {
            #region Charactor Creation
            /*
            Stat Allocation Planning
            All characters will start with 5 points in every stat, and then will be able to allocate
            20 points among the 5 stats. MAG will be locked at 5 for the Main Character because he 
            cannot use magic at all, and he will instead get INT.
            */
            
            CharacterCreator.StatInit();
            #endregion
        }
    }
}
