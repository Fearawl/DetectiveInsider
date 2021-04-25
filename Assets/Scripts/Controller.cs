using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class Controller : MonoBehaviour
{
    RigidbodyFirstPersonController mainController;
    new ObjectSearcher camera;
    new Rigidbody rigidbody;
    new Collider collider;

    public Room currentRoom;
    public PickableItem picked;
    public Transform pickedPlace;
    public Animator eyesAnimator;


    public AudioClip startFix, hitSound;

    InputButton useButton, menuButton;

    void Start() {
        mainController = GetComponent<RigidbodyFirstPersonController>();
        camera = GetComponentInChildren<ObjectSearcher>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        useButton = new InputButton("Use", 
        () => {
            if (picked) {
                ItemInteractionTarget target = camera.selectedObject != null ? camera.selectedObject.GetComponent<ItemInteractionTarget>() : null;
                if (target != null && target.enabled){
                    target.TryInsertItem(picked);
                } else  {
                    picked.transform.parent = currentRoom.transform;
                    picked.OnDropped();
                    picked = null;
                }     
            } else if (camera.selectedObject != null) {
                if (camera.selectedObject.tag == "Pickable")
                {
                    picked = camera.selectedObject.GetComponent<PickableItem>();
                    picked.transform.parent = pickedPlace;
                    picked.transform.localRotation = Quaternion.identity;
                    picked.transform.localPosition = Vector3.zero;
                    picked.OnTaken();
                }
                else if (camera.selectedObject.tag == "Door")
                {
                    camera.selectedObject.GetComponent<Door>().Toggle();
                }
            }
        });
        menuButton = new InputButton("Exit", () => FindObjectOfType<Menu>().Toggle());

        eyesAnimator.Play("EyesOpen");
    }

    public void EndGameWin() {
        StartCoroutine(EndGameWinImpl());
    }

    IEnumerator EndGameWinImpl() {
        yield return new WaitForSeconds(5f);
        eyesAnimator.Play("WinGame");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MenuScene");
    }

    void Update()
    {
        useButton.Update();
        menuButton.Update();
    }

    void UpdateCameraAtPosition() {
        mainController.mouseLook.LookRotation(transform, camera.transform);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.relativeVelocity.magnitude > 5f) {
            eyesAnimator.Play("Damage");
        }
    }

    List<Room> triggeredRooms = new List<Room>();

    void OnTriggerEnter(Collider collider){
        Room room = collider.GetComponent<Room>();
        if (room != null){
            triggeredRooms.Add(room);
            currentRoom = room;
        }
    }

    void OnTriggerExit(Collider collider){
        Room room = collider.GetComponent<Room>();
        if (room != null){
            triggeredRooms.Remove(room);
            if (currentRoom == room)
                currentRoom = triggeredRooms[0];
        }
    }
}

class InputButton {
    string buttonID;
    bool locked = false;
    Action onPressed = null;


    public InputButton(string buttonID, Action onPressed)
    {
        this.buttonID = buttonID;
        this.onPressed = onPressed;
    }

    public void Update(){
        if (Input.GetButton(buttonID)){
            if (!locked){
                onPressed?.Invoke();
                locked = true;
            }
        } else {
            if (locked){
                locked = false;
            }
        }
    }
}
