using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public int maxHealth = 100;
    private int maxExperience = 500;
    public int experience = 0;
    public int gold = 0;
    public int currentHealth { get; private set; }

    public HealthBar healthBar;
    public ExperienceBar experienceBar;

    public Stat damage;
    public Stat armor;

    void Awake()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
        if (experienceBar != null)
        {
            experienceBar.SetMaxExperience(maxExperience);
            experienceBar.SetExperience(experience);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (experienceBar != null)
            {
                experienceBar.SetExperience(200);
            }
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

        healthBar.SetHealth(currentHealth, maxHealth);

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
