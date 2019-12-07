using UnityEngine;

namespace Mycontroller {
    public class AIcontroller : MonoBehaviour {

        public int[] getAIaction(int devilNUM, int priestNUM, int boatPosition)
        {
            //P_D[0]  NUMBER OF PRIEST TO MOVE
            //P_D[1]  NUMBER OF DEVIL TO MOVE
            int[] P_D = new int[2];

            if (priestNUM == 3 && devilNUM == 3 && boatPosition == 1)
            {
                P_D[0] = 1;
                P_D[1] = 1;
            }
            else if(priestNUM == 3 && devilNUM == 2 && boatPosition == -1)
            {
                P_D[0] = 0;
                P_D[1] = 1;
            }
            else if(priestNUM == 2 && devilNUM == 2 && boatPosition == -1)
            {
                P_D[0] = 1;
                P_D[1] = 0;
            }
            else if(priestNUM == 3 && devilNUM == 1 && boatPosition == -1)
            {
                P_D[0] = 0;
                P_D[1] = 1;
            }
            else if(priestNUM == 3 && devilNUM == 2 && boatPosition == 1)
            {
                P_D[0] = 0;
                P_D[1] = 2;
            }
            else if(priestNUM == 3 && devilNUM == 0 && boatPosition == -1)
            {
                P_D[0] = 0;
                P_D[1] = 1;
            }
            else if(priestNUM == 3 && devilNUM == 1 && boatPosition == 1)
            {
                P_D[0] = 2;
                P_D[1] = 0;
            }
            else if(priestNUM == 1 && devilNUM == 1 && boatPosition == -1)
            {
                P_D[0] = 1;
                P_D[1] = 1;
            }
            else if(priestNUM == 2 && devilNUM == 2 && boatPosition == 1)
            {
                P_D[0] = 2;
                P_D[1] = 0;
            }
            else if(priestNUM == 0 && devilNUM == 2 && boatPosition == -1)
            {
                P_D[0] = 0;
                P_D[1] = 1;
            }
            else if(priestNUM == 0 && devilNUM == 3 && boatPosition == 1)
            {
                P_D[0] = 0;
                P_D[1] = 2;
            }
            else if(priestNUM == 0 && devilNUM == 1 && boatPosition == -1)
            {
                P_D[0] = 0;
                P_D[1] = 1;
            }
            else if(priestNUM == 2 && devilNUM == 1 && boatPosition == 1)
            {
                P_D[0] = 2;
                P_D[1] = 0;
            }
            else if(priestNUM == 1 && devilNUM == 1 && boatPosition == 1)
            {
                P_D[0] = 1;
                P_D[1] = 1;
            }
            else if(priestNUM == 0 && devilNUM == 2 && boatPosition == 1)
            {
                P_D[0] = 0;
                P_D[1] = 2;
            }
            return P_D;
        }
    }
 }