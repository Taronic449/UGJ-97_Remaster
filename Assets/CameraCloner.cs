using UnityEngine;

public class CameraCloner : MonoBehaviour
{
    public Vector3 cameraOffset = new Vector3(0, 0, -10); // Default offset for 2D cameras

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();

        if (mainCamera == null)
        {
            Debug.LogError("CameraCloner must be attached to a GameObject with a Camera component.");
        }
    }

    public void CloneAndAssignCameras()
    {
        if (PlayerManager.Instance.players == null || PlayerManager.Instance.players.Count == 0)
        {
            Debug.LogError("No players assigned. Please assign players in the Inspector.");
            return;
        }

        int playerCount = PlayerManager.Instance.players.Count;

        for (int i = 0; i < 2; i++)
        {
            PlayerController player = PlayerManager.Instance.players[i];
            
            if (player != null)
            {
                Camera clonedCamera = Instantiate(gameObject, player.transform.position + cameraOffset, Quaternion.identity).GetComponent<Camera>();

                player.realCam = clonedCamera;

                CameraFollower follower = clonedCamera.GetComponent<CameraFollower>();
                follower.target = player.transform;

                Rect viewport = GetViewportRectForPlayer(player.playerType == PlayerController.PlayerType.yori ? 0 : 1, playerCount);
                clonedCamera.rect = viewport;

                Debug.Log($"Cloned camera attached to player: {player.name}");
            }
            else
            {
                Debug.LogWarning("One of the player slots is empty. Skipping.");
            }
        }

        Destroy(gameObject);
    }

    private Rect GetViewportRectForPlayer(int playerIndex, int totalPlayers)
    {
        switch (totalPlayers)
        {
            case 1:
                return new Rect(0, 0, 1, 1);
            case 2:
                return playerIndex == 0
                    ? new Rect(0, 0.5f, 1, 0.5f)
                    : new Rect(0, 0, 1, 0.5f);
            default:
                Debug.LogError("Unsupported number of players for split-screen.");
                return new Rect(0, 0, 1, 1); 
        }
    }
}
