using UnityEngine;
using UnityEngine.UI;

public class HealthEnemy : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public GameObject healthBarPrefab;
    private Slider healthSlider;
    private Transform barTransform;
    private GameObject bar;
    public float CD;

    void Start()
    {
        currentHealth = maxHealth;
        
         bar = Instantiate(healthBarPrefab, transform.position + Vector3.up * CD, Quaternion.identity);
        barTransform = bar.transform;

        barTransform.SetParent(transform); // để đi theo enemy
        bar.gameObject.SetActive(false);
        healthSlider = bar.GetComponentInChildren<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int amount)
    {
        bar.SetActive(true );   
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject); // hoặc animation chết
        }
    }

    void Update()
    {
        // Luôn quay về camera nếu muốn
        barTransform.rotation = Quaternion.identity;
    }
}
