using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalWindow : MonoBehaviour
{

    public Canvas elementCanvas; // set in inspector
    public GameObject imagePrefab; // must be a blank Image object

    public static GoalWindow Instance;
    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DisplayGoal(List<string> notes) {
        foreach (string n in notes) {
            Sprite s = GameManager.instance.images[n];
            // instantiate image gameobject to canvas as child of elementCanvas
            GameObject newImage = Instantiate(imagePrefab); //
            newImage.transform.parent = elementCanvas.transform;
            newImage.GetComponent<Image>().sprite = s;
        }
    }
}
