using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public HealthBar healthBar;

    public Stat damage;
    public Stat armor;

    void Awake()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
            Debug.Log("Took damage");
        }
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Die some way
        // Meant to be overwritten
    }

}
