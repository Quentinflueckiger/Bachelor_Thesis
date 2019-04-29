using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DominoDisplay : MonoBehaviour
{
    private DominoCard dominoCard;

    public Image artwork;

    void Start()
    {
        artwork.sprite = dominoCard.artwork;
    }

    public void OnClick()
    {
        Debug.Log("Domino values : " + dominoCard.GetUpperValue() + " | " + dominoCard.GetLowerValue());
    }

    public void SetDominoCard(DominoCard domino)
    {
        dominoCard = domino;
    }
}
