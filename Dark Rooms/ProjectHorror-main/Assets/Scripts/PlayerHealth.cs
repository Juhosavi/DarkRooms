using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    private float lerpTimer;
    public float maxHealth = 100;
    public float chipSpeed = 2;
    public Image frontHealthBar;
    public Image backHealthBar;

    public Image overlay;
    public float duration;
    public float fadeSpeed;
    private float durationTimer;
    public bool playerDead;
    public GameObject healthImage;
    public Animator animator;
    public Image greenhealth;
    public bool healtgain;
    
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        greenhealth.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        animator = GetComponent<Animator>();
       DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       
        DeadMan();
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        HealthScreens();    
    }
    public void UpdateHealthUI()
    {
       // Debug.Log(health); // Tulostaa pelaajan nykyisen healthin konsoliin
        float fillF = frontHealthBar.fillAmount; // Otetaan muuttujaan täytetyn etupalkin täyttöaste
        float fillB = backHealthBar.fillAmount; // Otetaan muuttujaan täytetyn taustapalkin täyttöaste
        float hFraction = health / maxHealth; // Lasketaan pelaajan healthin suhde maksimi healthiin.
        if (fillB > hFraction) // Jos taustapalkin täyttöaste on suurempi kuin pelaajan healthin suhde makshealthiin.
        {
            frontHealthBar.fillAmount = hFraction; // Päivitetään vastaamaan pelaajan healthin suhdetta makshealthiin
            backHealthBar.color = Color.red; // Asetetaan taustan väri punaiseksi
            lerpTimer += Time.deltaTime; // Ajanotto käynnistyy
            float percentComplete = lerpTimer / chipSpeed; // Lasketaan kulunut aika suhteessa chipSpeed-muuttujaan
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete); // Lerpataan taustapalkin täyttöastetta hFraction-muuttujan suuntaan prosenttimäärän mukaan
        }
        if (fillF < hFraction) // Jos etupalkin täyttöaste on pienempi kuin pelaajan health suhde makshealthiin
        {
            backHealthBar.fillAmount = hFraction; // Asetetaan taustapalkin täyttöaste pelaajan healthin suhteeksi makshealthiin
            backHealthBar.color = Color.green; // Asetetaan taustan väri vihreäksi
            lerpTimer += Time.deltaTime; // Ajanotto käynnistyy
            float percentComplete = lerpTimer / chipSpeed; // Lasketaan kulunut aika suhteessa chipSpeed-muuttujaan
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete); // Lerpataan etupalkin täyttöastetta taustapalkin täyttöasteen suuntaan prosenttimäärän mukaan
        }
    }
    public void TakeDamage(float damage) //metodi vähentää pelaajan terveyttä vahingon verran
    {
        health -= damage; //vähennetään pelaajan terveyttä
        lerpTimer = 0f; //asetetaan lerpTimer nollaan
        Debug.Log("Damage otettu"); //tulostetaan "Damage otettu"
        durationTimer = 0; //asetetaan durationTimer nollaan
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1); //asetetaan overlayn väri täydeksi punaiseksi
    }

    public void RestoreHealth(float healAmount) //metodi palauttaa pelaajan terveyttä annetun määrän
    {
        health += healAmount; //lisätään pelaajan terveyttä
        lerpTimer = 0f; //asetetaan lerpTimer nollaan
        Debug.Log("Healthia saatu"); //tulostetaan "Healthia saatu"¨
        durationTimer = 0; //asetetaan durationTimer nollaan
        healtgain = true;
        greenhealth.color = new Color(greenhealth.color.r, greenhealth.color.g, greenhealth.color.b, 1);




    }
    public void TestInterac()
    {
        Debug.Log("Interact toimii");
    }

    public void DeadMan() //metodi tarkistaa, onko pelaaja kuollut
    {
        if (health <= 0) //jos pelaajan health on pienempi tai yhtäsuuri kuin nolla
        {
            playerDead = true; //merkitään pelaaja kuolleeksi
        }
    }
    public void HealthScreens()
    {
        // Tarkistaa onko greenhealth-värin läpinäkyvyysarvo suurempi kuin 0
        if (greenhealth.color.a > 0)
        {
            Debug.Log("healtgained");

            // Lisää keston ajastinta Time.deltaTimen avulla
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                // Vähennä vihreän terveyspalkin läpinäkyvyyttä ajan kuluessa
                float tempAlpha = greenhealth.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                greenhealth.color = new Color(greenhealth.color.r, greenhealth.color.g, greenhealth.color.b, tempAlpha);
            }
        }

        // Tarkista, onko overlay-värin läpinäkyvyysarvo suurempi kuin 0
        if (overlay.color.a > 0)
        {
            // Tarkista, onko health alle 30
            if (health < 30)
                return;

            // Lisää keston ajastinta Time.deltaTimen avulla
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                // Vähennä päällekkäisyyden (overlay) läpinäkyvyyttä ajan kuluessa
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

}