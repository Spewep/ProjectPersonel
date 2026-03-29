using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public Transform respawner;
    public FadeManager fadeManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController cc = other.GetComponent<CharacterController>();
            PlayerMove pm = other.GetComponent<PlayerMove>();

            System.Action respawnAction = () =>
            {
                if (cc != null)
                {
                    cc.enabled = false;
                    other.transform.position = respawner.position;
                    cc.enabled = true;
                }
                else
                {
                    other.transform.position = respawner.position;
                }

                if (pm != null)
                    pm.ResetPlayer();
            };

            if (fadeManager != null)
            {
                fadeManager.FadeOutInCinematic(respawnAction);
            }
            else
            {
                respawnAction.Invoke();
            }
        }
    }
}