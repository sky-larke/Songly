using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Note : MonoBehaviour 
{
    public TextMeshProUGUI textObject;
    public string name; //name of the note/theme/phrase
    public AudioClip sound; //sound asset for this note/theme/phrase
    public Sprite sprite; //image of this soundObject



    // Start is called before the first frame update
    void Start()
    {
        textObject.text = name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Hello");
        // play the note?
        SoundManager.Instance.PlaySound(sound);
        // add to backpack
        Backpack.Instance.AddElement(gameObject);
        Debug.Log(Backpack.Instance.elements.Count);
        // disappear
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
