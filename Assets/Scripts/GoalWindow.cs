using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalWindow : MonoBehaviour
{

    public Canvas elementCanvas; // set in inspector
    public GameObject imagePrefab; // must be a blank Image object

    // Singleton stuff
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


    public void DisplayGoal(SoundObjectData goalData) {
        Clear();
        foreach (SoundObjectData el in goalData.elements) {
            // instantiate image as child of elementCanvas (so its sprite shows up in the goal window)
            GameObject newImage = Instantiate(imagePrefab);
            newImage.transform.SetParent(elementCanvas.transform);
            newImage.GetComponent<Image>().sprite = el.sprite;
        }
    }


    public void Clear() {
        // delete the sprite images from the scene
        foreach (Transform child_img in elementCanvas.transform) {
            GameObject.Destroy(child_img.gameObject);
        }
    }
}
