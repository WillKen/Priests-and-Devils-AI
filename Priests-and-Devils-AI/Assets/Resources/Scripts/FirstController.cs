using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mydirector;
using Mycontroller;

public class FirstController : MonoBehaviour, SceneController, UserAction{

	private Vector3 river = new Vector3(6,-0.5F,0);
	UserInterface userGUI;
	public CoastController fromCoast;
	public CoastController toCoast;
	public BoatController boat;
	private Mycontroller.CharacterController[] characters;
	public Move actionManager;
	Judge judge;
	public AIcontroller agent;

	void Awake() {
		Director director = Director.getInstace ();
		director.current = this;
		userGUI = gameObject.AddComponent <UserInterface>() as UserInterface;
		characters = new Mycontroller.CharacterController[6];
		loadResources ();
		actionManager = gameObject.AddComponent<Move> ()as Move;
		agent = gameObject.AddComponent<AIcontroller>() as AIcontroller;
	}

	public void loadResources() {
		GameObject Water = Instantiate (Resources.Load ("Prefab/water", typeof(GameObject)),river,Quaternion.identity,null) as GameObject;

		Water.name = "Water";
		fromCoast = new CoastController ("from");
		toCoast = new CoastController ("to");
		boat = new BoatController ();
		judge = new Judge (fromCoast, toCoast, boat);

		for (int i = 0; i < 3; i++) {
			Mycontroller.CharacterController cha = new Mycontroller.CharacterController ("devil",i);
			cha.setName("devil" + i);
			cha.setPos (fromCoast.getEmptyPos());
			cha.getOnCoast (fromCoast);
			fromCoast.getOnCoast (cha);
			characters [i] = cha;
		}

		for (int i = 3; i < 6; i++) {
			Mycontroller.CharacterController cha = new Mycontroller.CharacterController ("priest",i);
			cha.setName("priest" + i);
			cha.setPos (fromCoast.getEmptyPos ());
			cha.getOnCoast (fromCoast);
			fromCoast.getOnCoast (cha);
			characters [i] = cha;
		}
	}

	public void moveBoat() {
		if (boat.is_empty ())
			return;
		actionManager.moveBoat (boat.getGameobj (), boat.Move_to (), boat.speed);
	}

	public void moveCharacter(Mycontroller.CharacterController characterCtrl) {
		if (characterCtrl.Is_On_Boat ()) {
			CoastController whichCoast;
			if (boat.get_is_from () == -1) {
				whichCoast = toCoast;
			} else {
				whichCoast = fromCoast;
			}

			boat.GetOffBoat (characterCtrl.getName());

			Vector3 end_pos = whichCoast.getEmptyPos();
			Vector3 mid_pos = new Vector3 (characterCtrl.getGameobj ().transform.position.x,end_pos.y, end_pos.z);
			actionManager.moveCharacter(characterCtrl.getGameobj(),mid_pos,end_pos,characterCtrl.move_speed);
			characterCtrl.getOnCoast (whichCoast);
			whichCoast.getOnCoast (characterCtrl);

		} else {								
			CoastController whichCoast = characterCtrl.getCoastController ();

			if (boat.getEmptyIndex () == -1) {		
				return;
			}

			if (whichCoast.get_is_from () != boat.get_is_from ())	
				return;

			whichCoast.getOffCoast(characterCtrl.getName());

			Vector3 end_pos = boat.getEmptyPosition();
			Vector3 mid_pos = new Vector3 (end_pos.x,characterCtrl.getGameobj().transform.position.y,end_pos.z);
			actionManager.moveCharacter(characterCtrl.getGameobj(),mid_pos,end_pos,characterCtrl.move_speed);

			characterCtrl.getOnBoat (boat);
			boat.GetOnBoat (characterCtrl);
		}
	}

public void AIaction()
    {

        int flag = judge.check();
        if (flag == 1 || flag ==2)
            return;
		Debug.Log("------------------------------------------------------");

        int numDevilLeft = judge.from_devil, numPriestLeft = judge.from_priest;
        int boatPos = boat.get_is_from();

        Debug.Log("State:"+numDevilLeft+" "+numPriestLeft+" "+boat.get_is_from());
        int[] P_D = agent.getAIaction(numDevilLeft, numPriestLeft, boat.get_is_from());
        Debug.Log("P_D[0] " + P_D[0]);
        Debug.Log("P_D[1] " + P_D[1]);
        
        int numPriestToBoat = 0, numDevilToBoat = 0;
        int[] personOnBoat = boat.getPersonOnBoat();
        Debug.Log("Boat0 "+personOnBoat[0]+" boat1"+personOnBoat[1]);
        if (personOnBoat[0] >= 3 && personOnBoat[0] <= 6)
        {
            if (P_D[0] > 0)
                numPriestToBoat++;
            else if (P_D[0] == 0)
            {
                Debug.Log("getoff P"+personOnBoat[0]);
               moveCharacter(characters[personOnBoat[0]]);
            }
        }
        else if (personOnBoat[0] >= 0 && personOnBoat[0] <= 2)
        {
            if (P_D[1] > 0)
                numDevilToBoat++;
            else if (P_D[1] == 0)
            {
                Debug.Log("getoff D"+personOnBoat[0]);
                moveCharacter(characters[personOnBoat[0]]);
            }
        }

        if (personOnBoat[1] >= 3 && personOnBoat[1] <= 6)
        {
            if (P_D[0] == 2 || (P_D[0] == 1 && numPriestToBoat == 0))
                numPriestToBoat++;
            else if (P_D[0] == 0 || (P_D[0] == 1 && numPriestToBoat == 1))
            {
                Debug.Log("getoff P"+personOnBoat[1]);
                moveCharacter(characters[personOnBoat[1]]);
            }
        }
        else if (personOnBoat[1] >= 0 && personOnBoat[1] <= 2)
        {
            if (P_D[1] == 2 || (P_D[1] == 1 && numDevilToBoat == 0))
                numDevilToBoat++;
            else if (P_D[1] == 0 || (P_D[1] == 1 && numDevilToBoat == 1))
            {
                Debug.Log("getoff D"+personOnBoat[1]);
                moveCharacter(characters[personOnBoat[1]]);
            }
        }

        for (int i = 0; i < P_D[0] - numPriestToBoat; i++)
        {
            int index = -1;
            if (boatPos == 1)
                index = fromCoast.getAPriestIndex();
            else if (boatPos == -1)
                index = toCoast.getAPriestIndex();
            Debug.Log("geton P"+index);
            moveCharacter(characters[index]);
        }
        for (int i = 0; i < P_D[1] - numDevilToBoat; i++)
        {

            int index = -1;
            if (boatPos == 1)
                index = fromCoast.getADevilIndex();
            else if (boatPos == -1)
                index = toCoast.getADevilIndex();
            Debug.Log("geton D"+index);
            moveCharacter(characters[index]);
        }

        Invoke("moveBoat", 0.5f);
        Debug.Log("------------------------------------------------------");
    }

	public void restart() {
		boat.reset ();
		fromCoast.reset ();
		toCoast.reset ();
		for (int i = 0; i < characters.Length; i++) {
			characters [i].reset ();
		}
	}
	void Update () {
		userGUI.status = judge.check ();
	}
}