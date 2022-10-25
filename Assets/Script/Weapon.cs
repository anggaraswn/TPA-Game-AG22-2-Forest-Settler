using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Weapon : MonoBehaviour
{
    public string name;
    public int damage;
    public int attSpeed;
    public bool pickedUp = false;
    public Player holder = null;
    public Player contact = null;
    public ItemUI ui;


    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(90f, 1f, transform.rotation.eulerAngles.z);
        ui = GameObject.Find("ItemUI").GetComponent<ItemUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (holder == null)
        {
            if (pickedUp)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (contact != null)
                    {
                        holder = contact;
                        Weapon weapon = GameObject.Find("mixamorig:RightHandIndex1").transform.GetComponentInChildren<Weapon>();
                        if (weapon != null)
                        {
                            weapon.Drop();
                        }

                        ui.HideUI();
                        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        holder = contact;
                        GameObject rightHand = GameObject.Find("mixamorig:RightHandIndex1");
                        gameObject.transform.parent = rightHand.transform;
                        gameObject.transform.position = new Vector3(
                                                        rightHand.transform.position.x,
                                                        rightHand.transform.position.y,
                                                        rightHand.transform.position.z);
                        gameObject.transform.rotation = Quaternion.Euler(rightHand.transform.rotation.x, rightHand.transform.rotation.y, rightHand.transform.rotation.z);
                        pickedUp = true;
                        //gameObject.transform.rotation = Quaternion.Euler(-((float)30.363), -20, (float)60.925);
                        //(0, -30, 56.135)
                    }
                }

            }
        }

        if (pickedUp == false)
        {
            Spin();
        }

    }

    public void HandHeld()
    {
        GameObject rightHand = GameObject.Find("mixamorig:RightHand");
        gameObject.transform.parent = rightHand.transform;
    }

    public void Drop()
    {
        pickedUp = false;
        Vector3 currPost = new Vector3(gameObject.transform.parent.transform.position.x + 2, 0.4f, gameObject.transform.parent.transform.position.z);
        gameObject.transform.parent = null;
        gameObject.transform.position = currPost;

        gameObject.transform.rotation = Quaternion.Euler(90f, gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z);
        holder = null;
        contact = null;
    }

    public void Spin()
    {
        transform.Rotate(0f, 0f, 1f, Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {
        if (holder == null && other.gameObject.GetComponent<Player>() != null)
        {
            pickedUp = true;
            contact = other.gameObject.GetComponent<Player>();
            //GameObject.Find("ItemUI").gameObject.GetComponent<CanvasGroup>().alpha = 1;
            ShowUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (holder == null && other.gameObject.GetComponent<Player>() != null)
        {
            pickedUp = false;
            contact = null;
            HideUI();
        }
    }

    void ShowUI()
    {
        ui.SetName(name);
        ui.ShowUI();
    }

    void HideUI()
    {
        ui.HideUI();
    }
}