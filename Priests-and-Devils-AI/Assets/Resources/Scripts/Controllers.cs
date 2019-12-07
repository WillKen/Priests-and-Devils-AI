namespace myGame
{
    public class Director : System.Object
    {
        private static Director _instance;

        public ISceneController currentSceneController { get; set; }
        public bool running { get; set; }

        public static Director GetInstance()
        {
            if(_instance == null)
            {
                _instance = new Director();
            }

            return _instance;
        }
    }

    public interface ISceneController
    {
        void loadResources();
        int getState();
    }

    public interface IUserAction
    {
        void moveBoat();
        void clickACharacter(CharactersController character);
        void restart();
        void AIaction();
    }

}