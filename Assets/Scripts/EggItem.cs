using UnityEngine;

public class EggItem : MonoBehaviour
{
    public int value = 5; // The value of eggs the player gets per collection

    void Start()
    {
        GameManager.Instance.RegisterEgg(gameObject); // Register the egg
    }

    private void OnTriggerEnter(Collider other)
    {
        RobotSharkController player = other.GetComponent<RobotSharkController>();

        if (player != null)
        {
            bool collected = player.CollectEggs(value);

            if (collected)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("You can't carry more eggs! Go to the deposit point.");
            }
        }
    }
}
