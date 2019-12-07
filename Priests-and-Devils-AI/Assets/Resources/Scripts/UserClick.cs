using UnityEngine;


namespace myGame
{
    public class UserClick : MonoBehaviour
    {
        IUserAction action;
        CharactersController charactersController;
        ISceneController handel;

        void Start()
        {
            action = Director.GetInstance().currentSceneController as IUserAction;
            handel = Director.GetInstance().currentSceneController as ISceneController;
        }
        
        public void setController(CharactersController c)
        {
            charactersController = c;
        }

        void OnMouseDown()
        {
            Debug.Log(handel.getState());
            if ( handel.getState() == 0)
            {
                Debug.Log(gameObject.name);
                if (gameObject.name == "boat")
                {
                    action.moveBoat();
                }
                else
                {
                    action.clickACharacter(charactersController);
                }
            }
        }


    }
}

