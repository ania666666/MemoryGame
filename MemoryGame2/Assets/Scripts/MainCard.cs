using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour {

    [SerializeField]
    private test2 controller;
    [SerializeField]
    private GameObject Card_Back;

    public void OnMouseDown()//odkrywannie karty
    {
        if(Card_Back.activeSelf && controller.canReveal)//karta niedwrócona i można odwrócić
        {
            Card_Back.SetActive(false);
            controller.CardRevealed(this);
        }
    }
    private int _id;
    public int id
    {
        get { return _id; }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
    public void Unreveal()//chownie karty z powrotem
    {
        Card_Back.SetActive(true);
    }
}
