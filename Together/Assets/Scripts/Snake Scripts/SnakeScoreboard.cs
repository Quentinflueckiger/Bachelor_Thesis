using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScoreboard : MonoBehaviour
{
    [SerializeField]
    GameObject playerScoreboardItemPrefab;

    [SerializeField]
    Transform playerScoreboardList;

    SnakeGameManager sgm;

    private void Awake()
    {
        sgm = SnakeGameManager.Instance;

        if (sgm == null)
            Debug.Log("Failed to retrieve Game manager.");
    }

    private void OnEnable()
    {
        // TODO : Detail
        //        Sort the list by score
        GameObject[] players = sgm.GetPlayers();

        foreach (GameObject player in players)
        {
            GameObject itemGameObject = Instantiate(playerScoreboardItemPrefab, playerScoreboardList);
            PlayerScoreboardItem item = itemGameObject.GetComponent<PlayerScoreboardItem>();
            if (item != null)
                item.Setup(player.GetComponent<SnakeSetUpPlayer>().GetPlayerName(), player.GetComponent<SnakeTailController>().GetTail().Count);
        }
        
    }

    private void OnDisable()
    {
        foreach (Transform child in playerScoreboardList)
        {
            Destroy(child.gameObject);
        }
    }
}
