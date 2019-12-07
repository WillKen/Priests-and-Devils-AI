using UnityEngine;

namespace myGame
{
    public class BankController
    {
        public GameObject bank;
        private int index;//LEFT-0 RIGHT-1
        public int[] indexCharactersOnBank; 

        public BankController(string i)
        {
            if (i == "left") index = 0;
            else if (i == "right") index = 1;

            indexCharactersOnBank = new int[6];
            for(int j = 0; j < 6; j++)
            {
                indexCharactersOnBank[j] = -1;
            }
        }

        public void removeACharacter(CharactersController characterscontroller)
        {
            indexCharactersOnBank[characterscontroller.getCharacterIndex()] = -1;
        }

        public void putACharacter(CharactersController characterscontroller)
        {
            indexCharactersOnBank[characterscontroller.getCharacterIndex()] = characterscontroller.getType();
        }

        public int getNumDevil()
        {
            int count = 0;
            for(int i = 0; i < 6; i++)
            {
                if (indexCharactersOnBank[i] == 0) count++;
            }
            return count;
        }

        public int getNumPriest()
        {
            int count = 0;
            for (int i = 0; i < 6; i++)
            {
                if (indexCharactersOnBank[i] == 1) count++;
            }
            return count;
        }

        public int getAPriestIndex()
        {
            for(int i = 0; i < 6; i++)
            {
                if (indexCharactersOnBank[i] == 1)
                    return i;
            }
            return -1;
        }

        public int getADevilIndex()
        {
            for(int i = 0; i < 6; i++)
            {
                if (indexCharactersOnBank[i] == 0)
                    return i;
            }
            return -1;
        }
        
        public void reset()
        {
            if(index == 0)
            {
                int j;
                for(j = 0; j < 3; j++)
                    indexCharactersOnBank[j] = 0;
                for (; j < 6; j++)
                    indexCharactersOnBank[j] = 1;
            }

            else if (index == 1)
            {
                for (int j = 0; j < 6; j++)
                {
                    indexCharactersOnBank[j] = -1;
                }
            }
        }
    }
}
