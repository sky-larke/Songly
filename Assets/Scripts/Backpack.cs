using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{


    public Canvas elementCanvas; // set in inspector
    public List<SoundObjectData> elements; //holds notes, phrases, themes as list (queue?)
    public GameObject imagePrefab; // must be a blank Image object

    //singleton stuff --------------------//
    public static Backpack Instance;
    void Awake() {
        Instance = this;
    }
    //------------------------------------//

    // Start is called before the first frame update
    void Start()
    {
        elements = new List<SoundObjectData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddElement(SoundObjectData element) {
        // add new item to end of list
        elements.Add(element);

        // display this soundObject's sprite on screen; create a new image as child of elementCanvas so the HorizontalLayoutGroup component can affect it
        GameObject newImage = Instantiate(imagePrefab);
        newImage.transform.SetParent(elementCanvas.transform);

        // get sprite from gameObject element and set newImage to that sprite
        Sprite s = element.sprite;
        newImage.GetComponent<Image>().sprite = s;
    }

    //empty the backpack
    public void Clear() {
        // empty the list
        elements.Clear();
        // delete the sprite images from the scene
        foreach (Transform child_img in elementCanvas.transform) {
            GameObject.Destroy(child_img.gameObject);
        }
    }


    // if the backpack elements are incorrect, play the elements' sounds in sequence.
    public void PlayElementsInSequence() {
        //TODO: see this answer: http://answers.unity.com/answers/904995/view.html
    }
}
