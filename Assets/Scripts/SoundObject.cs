using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    [Header("SET IN INSPECTOR:")]
    public SoundObjectData data;    //contains all the data about this sound object (name, sprite, sound, etc)



    // Start is called before the first frame update
    void Start()
    {
        // display the specific sprite (stored in SoundObjectData)
        GetComponent<SpriteRenderer>().sprite = data.sprite;
    }



    // Update is called once per frame
    void Update()
    {
        
    }



    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("Player Collided with me");
            // play the note?
            PlaySound();
            // add to backpack
            Backpack.Instance.AddElement(data);
            Debug.Log(Backpack.Instance.elements.Count);
            // disappear
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
        
    }

    // Play this object's sound asset
    void PlaySound() {
        AudioSource.PlayClipAtPoint(data.sound, Vector3.zero);   //PlayClipAtPoint makes a new external audiosource, plays the sound, then deletes it.
    }
}
