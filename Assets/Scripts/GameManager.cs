using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton stuff -----------------//
    public static GameManager instance;
    private void Awake() {
        instance = this;
    }
    //----------------------------------//

    [Header("SET IN INSPECTOR")]
    public GameObject spawnArea;
    public Text winText; //text that shows up on screen when you win
    public GameObject soundObjectPrefab;

    public int maxItemsSpawned = 10;

    [Tooltip("ordered list of all goals to complete in the game")]
    public List<SoundObjectData> allGoals;      //ordered list of all goals in the game (complete a goal by collecting all of the listed soundObject elements)
    public SoundObjectData currentGoal;        //current soundObject to construct this round - contains data about which notes/etc to collect
    public int currentGoalIdx;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        // begin game at first goal
        currentGoalIdx = 0;
        currentGoal = allGoals[currentGoalIdx];
        GoalWindow.Instance.DisplayGoal(currentGoal);
        GenerateLevel();
    }



    private void Update() {
        //check if backpack equals goal?
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (IsGoalMet()) {
                Debug.Log("WIN");

                //winText.gameObject.SetActive(true);

                // play sound asset
                audioSource.clip = currentGoal.sound;
                GetComponent<AudioSource>().Play();
                
                //advance to next goal
                currentGoalIdx += 1;
                currentGoal = allGoals[currentGoalIdx]; //TODO - this index will go out of bounds on final level

                //empty backpack
                Backpack.Instance.Clear();
                //display new goal
                GoalWindow.Instance.DisplayGoal(currentGoal);

                //spawn new items on screen
                Invoke("GenerateLevel", 2f);            //spawn items in new level after 2 sec delay

            } else {
                Debug.Log("Not WIN");
                Backpack.Instance.Clear();
                Invoke("GenerateLevel", 2f); //restart current level after 2 sec dely
            }
        }
    }

 



    // Returns true if the items in backpack eqaul the required items of the goal
    // **goal is just a SoundObjectData, which has a list of other SoundObjectData to collect. Compare by their string names.
    public bool IsGoalMet() {
        // check that goal and backpack contain same number of elements
        if (currentGoal.elements.Count != Backpack.Instance.elements.Count) {
            Debug.Log("Not same length: " + currentGoal.elements.Count + " " + Backpack.Instance.elements.Count);
            return false;
        }
        // check that names of all elements are equal
        for (int i = 0; i < currentGoal.elements.Count; i++) {
            string targetName = currentGoal.elements[i].name;
            string backpackName = Backpack.Instance.elements[i].name;
            if (targetName != backpackName) {
                Debug.Log("Target " + targetName + " != " + "Backpack" + backpackName);
                return false;
            }
        }
        return true;
    }



    // spawn SoundObjects around the screen, making sure the right items are spawned to complete the goal
    public void GenerateLevel() {
        ClearLevel();
        SpawnArea area = spawnArea.GetComponent<SpawnArea>();
        int spawnCount = 0;
        // first spawn the necessary elements to complete the goal
        foreach (SoundObjectData data in currentGoal.elements) {
            Vector3 spawnPoint = area.GetRandomSpawnPoint();
            GameObject go = Instantiate(soundObjectPrefab, spawnPoint, Quaternion.identity);
            go.GetComponent<SoundObject>().data = data;
            go.transform.SetParent(transform); //set as children of GameManager
            spawnCount += 1;
        }
        // TODO then spawn a bunch of random things
        for (int i = 0; i < maxItemsSpawned - spawnCount; i++) {
            Vector3 spawnPoint = area.GetRandomSpawnPoint();
            //Instantiate(soundObjectPrefab, spawnPoint, Quaternion.identity);
        }
    }

    public void ClearLevel() {
        // delete all children of GameManager (this is where we spawn the SoundObjects)
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
