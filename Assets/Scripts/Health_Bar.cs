using UnityEngine.UI;
using UnityEngine;

public class Health_Bar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private UnityEngine.UI.Image atFullHP;
    [SerializeField] private UnityEngine.UI.Image atCurrentHP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        atFullHP.fillAmount = health.currHealth / 10;
    }

    // Update is called once per frame
    void Update()
    {
        atCurrentHP.fillAmount = health.currHealth / 10;
    }
}
