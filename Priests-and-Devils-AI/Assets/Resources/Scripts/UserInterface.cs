using UnityEngine;

namespace myGame
{
    public class UserInterface : MonoBehaviour
    {
        private IUserAction action;
        public int gameState = 0;
        GUIStyle style;
        GUIStyle button;
        // Use this for initialization
        void Start()
        {
            action = Director.GetInstance().currentSceneController as IUserAction;
            style = new GUIStyle();
            style.fontSize = 40;
            style.alignment = TextAnchor.MiddleCenter;


            button = new GUIStyle("button");
            button.fontSize = 30;
        }

        // Update is called once per frame
        void OnGUI()
        {
            if (gameState == 1)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 50), "You Win!",style);
            }
            else if(gameState == -1)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 , 100, 50), "You Lose!",style);
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height/2+100, 140, 50), "Restart"))
            {
                gameState = 0;
                action.restart();
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2 - 100, 140, 50), "AI Move"))
            {
                action.AIaction();
            }
        }

    }
}

