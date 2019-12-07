using UnityEngine;

namespace myGame {
    public class AIcontroller : MonoBehaviour {

        public int[] getAIaction(int devilNUM, int priestNUM, int boatPosition)
        {
            //move[0]  NUMBER OF PRIEST TO MOVE
            //move[1]  NUMBER OF DEVIL TO MOVE
            int[] move = new int[2];

            if (priestNUM == 3 && devilNUM == 3 && boatPosition == 1)
            {
                move[0] = 1;
                move[1] = 1;
            }
            else if(priestNUM == 3 && devilNUM == 2 && boatPosition == 2)
            {
                move[0] = 0;
                move[1] = 1;
            }
            else if(priestNUM == 2 && devilNUM == 2 && boatPosition == 2)
            {
                move[0] = 1;
                move[1] = 0;
            }
            else if(priestNUM == 3 && devilNUM == 1 && boatPosition == 2)
            {
                move[0] = 0;
                move[1] = 1;
            }
            else if(priestNUM == 3 && devilNUM == 2 && boatPosition == 1)
            {
                move[0] = 0;
                move[1] = 2;
            }
            else if(priestNUM == 3 && devilNUM == 0 && boatPosition == 2)
            {
                move[0] = 0;
                move[1] = 1;
            }
            else if(priestNUM == 3 && devilNUM == 1 && boatPosition == 1)
            {
                move[0] = 2;
                move[1] = 0;
            }
            else if(priestNUM == 1 && devilNUM == 1 && boatPosition == 2)
            {
                move[0] = 1;
                move[1] = 1;
            }
            else if(priestNUM == 2 && devilNUM == 2 && boatPosition == 1)
            {
                move[0] = 2;
                move[1] = 0;
            }
            else if(priestNUM == 0 && devilNUM == 2 && boatPosition == 2)
            {
                move[0] = 0;
                move[1] = 1;
            }
            else if(priestNUM == 0 && devilNUM == 3 && boatPosition == 1)
            {
                move[0] = 0;
                move[1] = 2;
            }
            else if(priestNUM == 0 && devilNUM == 1 && boatPosition == 2)
            {
                move[0] = 0;
                move[1] = 1;
            }
            else if(priestNUM == 2 && devilNUM == 1 && boatPosition == 1)
            {
                move[0] = 2;
                move[1] = 0;
            }
            else if(priestNUM == 1 && devilNUM == 1 && boatPosition == 1)
            {
                move[0] = 1;
                move[1] = 1;
            }
            else if(priestNUM == 0 && devilNUM == 2 && boatPosition == 1)
            {
                move[0] = 0;
                move[1] = 2;
            }
            return move;
        }
    }
 }