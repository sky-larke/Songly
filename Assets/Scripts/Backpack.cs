using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{


    public Canvas elementCanvas; // set in inspector
    public List<GameObject> elements; //holds notes, phrases, themes as queue
    public GameObject imagePrefab; // must be a blank Image object

    public static Backpack Instance;
    void Awake() {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        elements = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddElement(GameObject element) {
        // add to queue data structure
        elements.Insert(0, element);
        Debug.Log(elements);
        // instantiate image gameobject to canvas as child of elementCanvas
        GameObject newImage = Instantiate(imagePrefab); //
        newImage.transform.parent = elementCanvas.transform;
        // get sprite from gameObject element and set newImage to that sprite
        Sprite s = element.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite;
        newImage.GetComponent<Image>().sprite = s;
    }

    public void Clear() {
        elements.Clear();
    }
}
