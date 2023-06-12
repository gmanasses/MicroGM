using System;
using UnityEngine;

public class SpeechTrigger : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField] private MessageContent[] _messages;
    [SerializeField] private Character[] _characters;


    // --- Core Fuctions ---
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            StartSpeech();
            other.GetComponent<PlayerController>().DisablePlayerInput();
        }
    }


    // --- Fuctions ---
    public void StartSpeech() {
        GameObject.FindObjectOfType<SpeechManager>().OpenSpeech(_messages, _characters);
    }

}


[Serializable]
public class MessageContent {
    public int _characterID;
    public string message;
}


[Serializable]
public class Character {
    public string name;
    public Sprite faceImage;
}
