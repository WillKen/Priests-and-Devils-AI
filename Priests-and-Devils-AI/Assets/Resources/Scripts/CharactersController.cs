using UnityEngine;


namespace myGame
{
    public class CharactersController
    {
        public GameObject character;
        private int type;
        private int index;
        private int state;

        public Move move;

        public UserClick clickGUI;

        public CharactersController(string t, int i)
        {
            if (t == "devil") type = 0;
            else if (t == "priest") type = 1;

            index = i;
            state = 1;
        }

        public int getState()
        {
            return state;
        }

        public int getCharacterIndex()
        {
            return this.index;
        }

        public int getType()
        {
            return this.type;
        }

        public void setName(string n)
        {
            this.character.name = n;
        }

       
        public void setPositionOnLeftBank()
        {
            this.state = 1;
            move.setDestination(new Vector3((float)(2.5 - index), (float)1.25, 0));
        }

        public void setPositionOnRightBank()
        {
            this.state = 2;
            move.setDestination(new Vector3((float)(9.5 + index), (float)1.25, 0));
        }

        public void setPositionOnBoat(Vector3 pos)
        {
            this.state = 0;
            move.setDestination(pos);
        }

        public void moveOnBoat(int boatState, int posOnBoat)
        {
            if (boatState == 1)
            {
                Vector3 target = character.transform.position + new Vector3(4,0,0);
                move.setDestination(target);
            }
            else if (boatState == 2)
            {
                Vector3 target = character.transform.position - new Vector3(4,0,0);
                move.setDestination(target);
            }
        }

        public void reset()
        {
            setPositionOnLeftBank();
        }
    }
}