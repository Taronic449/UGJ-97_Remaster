using System.Collections.Generic;
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

    // Method to clone the camera and assign it to players
    public void CloneAndAssignCameras()
    {
   

        if (PlayerManager.Instance.players == null || PlayerManager.Instance.players.Count == 0)
        {
            Debug.LogError("No players assigned. Please assign players in the Inspector.");
            return;
        }

        int playerCount = PlayerManager.Instance.players.Count;

        for (int i = 0; i < playerCount; i++)
        {
            PlayerController player = PlayerManager.Instance.players[i];
            if (player != null)
            {
                // Clone the main camera
                Camera clonedCamera = Instantiate(gameObject, player.transform.position + cameraOffset, Quaternion.identity).GetComponent<Camera>();

                player.realCam = clonedCamera;

                // Make the cloned camera follow the player
                CameraFollower follower = clonedCamera.gameObject.AddComponent<CameraFollower>();
                follower.target = player.transform;

                // Configure the viewport rect for split-screen
                Rect viewport = GetViewportRectForPlayer(i, playerCount);
                clonedCamera.rect = viewport;

                Debug.Log($"Cloned camera attached to player: {player.name}");
            }
            else
            {
                Debug.LogWarning("One of the player slots is empty. Skipping.");
            }
        }
    }

    // Calculate viewport rect for each player
    private Rect GetViewportRectForPlayer(int playerIndex, int totalPlayers)
    {
        // Assuming only 1-4 players for now
        switch (totalPlayers)
        {
            case 1:
                return new Rect(0, 0, 1, 1); // Full screen
            case 2:
                return playerIndex == 0
                    ? new Rect(0, 0.5f, 1, 0.5f) // Top half
                    : new Rect(0, 0, 1, 0.5f);   // Bottom half
            case 3:
                if (playerIndex == 0) return new Rect(0, 0.5f, 0.5f, 0.5f); // Top-left
                if (playerIndex == 1) return new Rect(0.5f, 0.5f, 0.5f, 0.5f); // Top-right
                return new Rect(0, 0, 0.5f, 0.5f); // Bottom-left
            case 4:
                if (playerIndex == 0) return new Rect(0, 0.5f, 0.5f, 0.5f); // Top-left
                if (playerIndex == 1) return new Rect(0.5f, 0.5f, 0.5f, 0.5f); // Top-right
                if (playerIndex == 2) return new Rect(0, 0, 0.5f, 0.5f); // Bottom-left
                return new Rect(0.5f, 0, 0.5f, 0.5f); // Bottom-right
            default:
                Debug.LogError("Unsupported number of players for split-screen.");
                return new Rect(0, 0, 1, 1); // Fallback to full screen
        }
    }
}
