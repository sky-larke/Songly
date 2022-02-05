using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() {
        instance = this;
    }

    public Text winText;


    public AudioClip theme1;

    public List<string> goal;

    public List<Sprite> sprites;
    public Dictionary<string, Sprite> images;

    private void Start() {
        goal = new List<string>() { "A", "C", "A" };

        images = new Dictionary<string, Sprite>();
        images.Add("A", sprites[0]);
        images.Add("B", sprites[1]);
        images.Add("C", sprites[2]);
        images.Add("D", sprites[3]);
        images.Add("E", sprites[4]);
        images.Add("G", sprites[5]);


        GoalWindow.Instance.DisplayGoal(goal);
    }

    private void Update() {
        //check if backpack equals goal?
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (IsGoalMet()) {
                Debug.Log("WIN");
                winText.gameObject.SetActive(true);
                SoundManager.Instance.PlaySound(theme1);
            } else {
                Debug.Log("Not WIN");
                Backpack.Instance.Clear();
            }
        }
        

    }


    public bool IsGoalMet() {
        if (goal.Count != Backpack.Instance.elements.Count) {
            Debug.Log("Not same length: " + goal.Count + " " + Backpack.Instance.elements.Count);
            return false;
        }
        //goal = "string list"
        //backpack is a gameObject queue?
        for (int i = 0; i < goal.Count; i++) {
            if (goal[i] != Backpack.Instance.elements[i].GetComponent<Note>().name) {
                
                return false;
            }
        }
        return true;
    }

}
