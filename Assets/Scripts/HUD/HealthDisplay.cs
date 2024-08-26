using TheGuild.Attributes;
using TheGuild.Combat;
using TMPro;
using UnityEngine;

namespace TheGuild.HUD
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] private bool displayPlayerHealth;
        private Health displayHealth;
        private Fighter playerFighter;
        private bool isSubbed = false;

        private void Awake()
        {
            GameObject playerGameOject = GameObject.FindGameObjectWithTag("Player");
            if (displayPlayerHealth)
            {
                displayHealth = playerGameOject.GetComponent<Health>();
                displayHealth.OnHealthChanged += OnHealthChanged;
            }
            else
            {
                playerFighter = playerGameOject.GetComponent<Fighter>();
                playerFighter.OnTargetChanged += PlayerFigher_OnTargetChanged;
            }
        }

        private void OnHealthChanged()
        {
            GetComponent<TextMeshProUGUI>().text = GetHealthPercentageText();
        }

        private void PlayerFigher_OnTargetChanged(Health target)
        {
            if (target == null)
            {
                if (isSubbed) displayHealth.OnHealthChanged -= OnHealthChanged;
                isSubbed = false;
                displayHealth = target;
                GetComponent<TextMeshProUGUI>().text = "";
            }
            else
            {
                displayHealth = target;
                displayHealth.OnHealthChanged += OnHealthChanged;
                isSubbed = true;
                GetComponent<TextMeshProUGUI>().text = GetHealthPercentageText();
            }
        }

        private string GetHealthPercentageText()
        {
            return displayHealth.GetHealthPercentage().ToString("P");
        }

        private void OnDestroy()
        {
            if (displayPlayerHealth)
            {
                displayHealth.OnHealthChanged -= OnHealthChanged;
            }
            else
            {
                playerFighter.OnTargetChanged -= PlayerFigher_OnTargetChanged;
                if (isSubbed) displayHealth.OnHealthChanged -= OnHealthChanged;
            }
        }
    }
}
