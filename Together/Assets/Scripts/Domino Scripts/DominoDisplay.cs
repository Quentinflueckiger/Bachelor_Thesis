using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DominoDisplay : MonoBehaviour
{
    public DominoCard dominoCard;

    public Image artwork;

    private bool isZoomed = false;
    private Vector3 oldPos;
    public Vector3 newPos;
    private Vector3 oldScale;

    void Start()
    {
        artwork.sprite = dominoCard.artwork;
        oldPos = this.gameObject.transform.position;
        oldScale = new Vector3(2f,2f,1f);
    }

    public void OnClick()
    {
        ChangeFocus();
    }

    public void SetDominoCard(DominoCard domino)
    {
        dominoCard = domino;
    }

    public void ChangeFocus()
    {
        Vector3 scalePlaceHolder = this.gameObject.transform.localScale;
        if (isZoomed)
        {
            this.gameObject.transform.localScale = oldScale;
            this.gameObject.transform.position = oldPos;
            isZoomed = false;
        }
        else
        {
            this.gameObject.transform.localScale = oldScale;
            this.gameObject.transform.position = newPos;
            isZoomed = true;
        }
        oldScale = scalePlaceHolder;
    }
}
