using UnityEngine;


namespace myGame
{
    public class BoatController
    {
        public GameObject boat;
        private int boatState;//1-LEFT 2-RIGHT
        private int[] indexCharactersOnBoat;  

        public Move move;

        public BoatController()
        {
            boatState = 1;
            indexCharactersOnBoat = new int[2];
            for (int i = 0; i < 2; i++)
            {
                indexCharactersOnBoat[i] = -1;
            }
            
        }

        public void setName(string n)
        {
            this.boat.name = n;
        }

        public int getState()
        {
            return this.boatState;
        }

        public int getNumEmptyPos()
        {
            int counter = 0;
            for(int i = 0; i < 2; i++)
            {
                if (indexCharactersOnBoat[i] == -1)
                    counter++;
            }
            return counter;
        }

        public Vector3 getEmptyPos()
        {
            for (int i = 0; i < 2; i++)
            {
                if (indexCharactersOnBoat[i] == -1)
                {
                    if (boatState == 1)
                        return new Vector3(4.5f-i,0.5f,0);
                    else if (boatState == 2)
                        return new Vector3(8.5f-i, 0.5f, 0);
                    break;
                }
            }
            return Vector3.zero;
        }

        public void putACharacter(CharactersController characterscontroller)
        {
            for(int i = 0; i < 2; i++)
            {
                if(indexCharactersOnBoat[i] == -1)  //character坐标的变化不由boat实现，boat不用变
                {
                    indexCharactersOnBoat[i] = characterscontroller.getCharacterIndex();
                    break;
                }
            }
        }

        public void removeACharacter(CharactersController characterscontroller)
        {
            for(int i = 0; i < 2; i++)
            {
                if (indexCharactersOnBoat[i] == characterscontroller.getCharacterIndex())
                {
                    indexCharactersOnBoat[i] = -1;
                    break;
                }
            }
        }

        public void moveToAnotherBank()
        {
            if (boatState == 1)
            {
                Vector3 target = boat.transform.position + new Vector3(4, 0, 0);
                move.setDestination(target);
                this.boatState = 2;
            }
            else if (boatState == 2)
            {
                Vector3 target = boat.transform.position - new Vector3(4, 0, 0);
                move.setDestination(target);
                this.boatState = 1;
            }
        }

        public int[] getPersonOnBoat()
        {
            int[] result = new int[2];    
            for(int i = 0; i < 2; i++)
            {
                result[i] = indexCharactersOnBoat[i];
            }
            return result;
        }

        public int getNumDevil()
        {
            int num = 0;
            for(int i = 0; i < 2; i++)
            {
                if (indexCharactersOnBoat[i] >= 0 && indexCharactersOnBoat[i] <= 2)
                    num++;
            }
            return num;
        }

        public int getNumPriest()
        {
            int num = 0;
            for (int i = 0; i < 2; i++)
            {
                if (indexCharactersOnBoat[i] >= 3 && indexCharactersOnBoat[i] <= 6)
                    num++;
            }
            return num;
        }


        public void reset()
        {
            boatState = 1;
            for (int i = 0; i < 2; i++)
            {
                indexCharactersOnBoat[i] = -1;
            }
            boat.transform.position = new Vector3(4, 0, 0);
        }

    }
}
