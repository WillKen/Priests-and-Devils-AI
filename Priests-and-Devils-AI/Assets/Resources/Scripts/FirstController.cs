using UnityEngine;
using myGame;


public class FirstController : MonoBehaviour, ISceneController, IUserAction {

    public CharactersController[] characters;
    public BoatController boat;
    public BankController leftBank, rightBank;
    public AIcontroller agent;
    public UserInterface Uinterface;

    void Awake()
    {
        Director director = Director.GetInstance();
        director.currentSceneController = this;
        director.currentSceneController.loadResources();
        Uinterface = gameObject.AddComponent<UserInterface>() as UserInterface;
        Uinterface.gameState = 0;
        agent = gameObject.AddComponent<AIcontroller>() as AIcontroller;
    }

    public void loadResources()
    {
        GameObject water = Instantiate(Resources.Load("Prefabs/water"), new Vector3(6,-0.5F,0), Quaternion.identity) as GameObject;
    
        boat = new BoatController();
        boat.boat = Instantiate(Resources.Load("Prefabs/boat"), new Vector3(4,0,0), Quaternion.identity) as GameObject;
        boat.boat.AddComponent(typeof(UserClick));
        boat.setName("boat");
        boat.move = boat.boat.AddComponent(typeof(Move)) as Move;

        leftBank = new BankController("left");
        leftBank.bank = Instantiate(Resources.Load("Prefabs/stone"), new Vector3(0,0,0), Quaternion.identity) as GameObject;
        
        rightBank = new BankController("right");
        rightBank.bank = Instantiate(Resources.Load("Prefabs/stone"), new Vector3(12,0,0), Quaternion.identity) as GameObject;

        characters = new CharactersController[6];
        for (int i = 0; i < 3; i++)
        {
            characters[i] = new CharactersController("devil", i);
            characters[i].character = Instantiate(Resources.Load("Prefabs/devil"), Vector3.zero, Quaternion.identity) as GameObject;
            characters[i].setName("devil" + i);
            characters[i].character.transform.position = new Vector3((float)(2.5 - i), (float)1.25, 0);
            leftBank.putACharacter(characters[i]);

            characters[i].clickGUI = characters[i].character.AddComponent(typeof(UserClick)) as UserClick;
            characters[i].clickGUI.setController(characters[i]);
            characters[i].move = characters[i].character.AddComponent(typeof(Move)) as Move;
        }
        for(int i = 3; i < 6; i++)
        {
            characters[i] = new CharactersController("priest", i);
            characters[i].character = Instantiate(Resources.Load("Prefabs/priest"), Vector3.zero, Quaternion.identity) as GameObject;
            characters[i].setName("priest" + i);
            characters[i].character.transform.position = new Vector3((float)(2.5 - i), (float)1.25, 0);
            leftBank.putACharacter(characters[i]);

            characters[i].clickGUI = characters[i].character.AddComponent(typeof(UserClick)) as UserClick;
            characters[i].clickGUI.setController(characters[i]);
            characters[i].move = characters[i].character.AddComponent(typeof(Move)) as Move;
        }
    }

    public bool checkLose()
    {
        int countDevilLeft = leftBank.getNumDevil(), countPriestLeft = leftBank.getNumPriest();
        int countDevilRight = rightBank.getNumDevil(), countPriestRight = rightBank.getNumPriest();
        int[] personOnBoat = boat.getPersonOnBoat();
        int d = 0, p = 0;
        for(int i = 0; i < 2; i++)
        {
            if (personOnBoat[i] < 3 && personOnBoat[i] >= 0) d++;
            else if (personOnBoat[i] >= 3) p++;
        }
        if (boat.getState() == 1)
        {
            countDevilLeft += d;
            countPriestLeft += p;
        }
        else if(boat.getState() == 2)
        {
            countDevilRight += d;
            countPriestRight += p;
        }
        if ((countDevilLeft > countPriestLeft && countPriestLeft != 0) || (countDevilRight > countPriestRight && countPriestRight != 0))
        {
            return true;
        }
        return false;
    }

    public bool checkWin()
    {
        if (boat.getNumEmptyPos() == 2 && (leftBank.getNumDevil() + leftBank.getNumPriest() == 0) && (rightBank.getNumDevil() + rightBank.getNumPriest() == 6))
            return true;
        return false;
    }

    public void moveBoat()
    {
        //The boat is empty
        if (boat.getNumEmptyPos() == 2) return;

        boat.moveToAnotherBank();   
        int[] personOnBoat = boat.getPersonOnBoat();
        for(int i = 0; i < 2; i++)
        {
            if (personOnBoat[i] == -1) continue;
            characters[personOnBoat[i]].moveOnBoat((boat.getState() == 1 ? 2 : 1), i);
        }

        if (checkLose() == true)
        {
            Uinterface.gameState = -1;
            return;
        }

    }

    public void clickACharacter(CharactersController characterscontroller)
    {
        if (characterscontroller.getState() == 1 && boat.getState() == 1 && boat.getNumEmptyPos() > 0)
        {
            characterscontroller.setPositionOnBoat(boat.getEmptyPos());
            leftBank.removeACharacter(characterscontroller);
            boat.putACharacter(characterscontroller);
        }
        else if (characterscontroller.getState() == 2 && boat.getState() == 2 && boat.getNumEmptyPos() > 0)
        {
            characterscontroller.setPositionOnBoat(boat.getEmptyPos());
            rightBank.removeACharacter(characterscontroller);
            boat.putACharacter(characterscontroller);
        }
        else if (characterscontroller.getState() == 0 && boat.getState() == 1)
        {
            characterscontroller.setPositionOnLeftBank();
            boat.removeACharacter(characterscontroller);
            leftBank.putACharacter(characterscontroller);
        }
        else if (characterscontroller.getState() == 0 && boat.getState() == 2)
        {
            characterscontroller.setPositionOnRightBank();
            boat.removeACharacter(characterscontroller);
            rightBank.putACharacter(characterscontroller);
        }

        if (checkWin() == true)
        {
            Uinterface.gameState = 1;
            return;
        }
    }

    public void restart()
    {
        for(int i = 0; i < 6; i++)
        {
            characters[i].reset();
        }
        boat.reset();
        leftBank.reset();
        rightBank.reset();
        Uinterface.gameState = 0;
    }

    public void AIaction()
    {

        if (Uinterface.gameState == -1 || Uinterface.gameState == 1)
            return;

        int devilNUM = leftBank.getNumDevil(), priestNUM = leftBank.getNumPriest();  
        int devilOnBoat = boat.getNumDevil(), priestOnBoat = boat.getNumPriest();
        if (boat.getState() == 1)
        {
            devilNUM += devilOnBoat;
            priestNUM += priestOnBoat;
        }

        int boatPos = boat.getState();
        int[] move = agent.getAIaction(devilNUM, priestNUM, boat.getState());


        int priestToBoat = 0, devilToBoat = 0;
        int[] personOnBoat = boat.getPersonOnBoat();
        if (personOnBoat[0] >= 3 && personOnBoat[0] <= 6) 
        {
            if (move[0] > 0)           
                priestToBoat++;
            else if(move[0] == 0)      
            {
                clickACharacter(characters[personOnBoat[0]]);
            }
        }
        else if(personOnBoat[0] >= 0 && personOnBoat[0] <= 2) 
        {
            if (move[1] > 0)
                devilToBoat++;
            else if(move[1] == 0)
            {
                clickACharacter(characters[personOnBoat[0]]);
            }
        }
        
        if (personOnBoat[1] >= 3 && personOnBoat[1] <= 6)      
        {
            if (move[0] == 2 || (move[0] == 1 && priestToBoat == 0))
                priestToBoat++;
            else if (move[0] == 0 || (move[0] == 1 && priestToBoat == 1)) 
            {
                clickACharacter(characters[personOnBoat[1]]);
            }
        }
        else if (personOnBoat[1] >= 0 && personOnBoat[1] <= 2)   
        {
            if (move[1] == 2 || (move[1] == 1 && devilToBoat == 0))
                devilToBoat++;
            else if(move[1] == 0 || (move[1] == 1 && devilToBoat == 1))
            {
                clickACharacter(characters[personOnBoat[1]]);
            } 
        }


        for (int i = 0; i < move[0] - priestToBoat; i++)          
        {
            int index = -1;
            if (boatPos == 1)
                index = leftBank.getAPriestIndex();
            else if (boatPos == 2)
                index = rightBank.getAPriestIndex();

            clickACharacter(characters[index]);
        }
        for(int i = 0; i < move[1] - devilToBoat; i++)          
        {
            int index = -1;
            if (boatPos == 1)
                index = leftBank.getADevilIndex();
            else if (boatPos == 2)
                index = rightBank.getADevilIndex();
              
            clickACharacter(characters[index]);
        }

        Invoke("moveBoat", (float)0.5);
    }

    public int getState()
    {
        return Uinterface.gameState;
    }
}
