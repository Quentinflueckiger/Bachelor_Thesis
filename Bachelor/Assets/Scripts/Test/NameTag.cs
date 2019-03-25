using UnityEngine;
using UnityEngine.UI;

public class NameTag : MonoBehaviour
{
    public Text nameTag;
    public string playerName;
    public Vector3 tagOffSet;

    private void Awake()
    {
        // TODO : Transfert user name from lobby
        Transform canvas = GameObject.Find("Canvas").transform;
        nameTag = Instantiate(nameTag, canvas);

        nameTag.text = playerName;
    }

    private void Update()
    {
        Vector3 newPosition = Camera.main.WorldToScreenPoint(transform.position + tagOffSet);
        nameTag.transform.position = newPosition;
    }
}
