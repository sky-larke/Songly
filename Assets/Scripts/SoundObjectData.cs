using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SoundObjectData", menuName = "SoundObjectData")]
public class SoundObjectData : ScriptableObject
{
    public new string name;     //name of the note/theme/phrase
    public AudioClip sound;     //sound asset for this note/theme/phrase
    public Sprite sprite;       //image of this soundObject

    public enum Type { Note, Theme, Phrase, WholeSong };   
    public Type type;                           //is it a note, theme, or phrase
    public List<SoundObjectData> elements;    //if it's a theme/phrase, what notes etc is it made from?  



    //just prints out the name and type
    public void Print() {
        Debug.Log(name + ": " + type.ToString());
    }
}
