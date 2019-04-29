using UnityEngine;

[CreateAssetMenu(fileName = "NewDomino", menuName ="Domino")]
public class DominoCard : ScriptableObject
{
    public Sprite artwork;
    public int upperValue;
    public int lowerValue;

    private bool isUpperUsed = false;
    private bool isLowerUsed = false;

    public void Print()
    {
        Debug.Log("Upper value : " + upperValue + " lower value : " + lowerValue);
    }

    #region Getters/Setters
    public bool IsDouble()
    {
        return upperValue == lowerValue;
    }

    public int GetUpperValue()
    {
        return upperValue;
    }

    public int GetLowerValue()
    {
        return lowerValue;
    }

    public bool GetIsUpperUsed()
    {
        return isUpperUsed;
    }

    public bool GetIsLowerUsed()
    {
        return isLowerUsed;
    }

    public void SetIsUpperUsed()
    {
        isUpperUsed = true;
    }

    public void SetIsLowerUsed()
    {
        isLowerUsed = true;
    }
    #endregion
}
