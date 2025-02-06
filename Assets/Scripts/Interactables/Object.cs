using UnityEngine;

public class Object : Interactable
{
    [SerializeField]
    private GameObject Door;
    private bool doorOpen;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Interact()
    {
        doorOpen = !doorOpen;
        Door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }
}