using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public AudioSource neckSnap;
    public GameObject deathMenu;
    

    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        greenhealth.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        animator = GetComponent<Animator>();
    //    DontDestroyOnLoad(gameObject);
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
        float fillF = frontHealthBar.fillAmount; // Otetaan muuttujaan t�ytetyn etupalkin t�ytt�aste
        float fillB = backHealthBar.fillAmount; // Otetaan muuttujaan t�ytetyn taustapalkin t�ytt�aste
        float hFraction = health / maxHealth; // Lasketaan pelaajan healthin suhde maksimi healthiin.
        if (fillB > hFraction) // Jos taustapalkin t�ytt�aste on suurempi kuin pelaajan healthin suhde makshealthiin.
        {
            frontHealthBar.fillAmount = hFraction; // P�ivitet��n vastaamaan pelaajan healthin suhdetta makshealthiin
            backHealthBar.color = Color.red; // Asetetaan taustan v�ri punaiseksi
            lerpTimer += Time.deltaTime; // Ajanotto k�ynnistyy
            float percentComplete = lerpTimer / chipSpeed; // Lasketaan kulunut aika suhteessa chipSpeed-muuttujaan
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete); // Lerpataan taustapalkin t�ytt�astetta hFraction-muuttujan suuntaan prosenttim��r�n mukaan
        }
        if (fillF < hFraction) // Jos etupalkin t�ytt�aste on pienempi kuin pelaajan health suhde makshealthiin
        {
            backHealthBar.fillAmount = hFraction; // Asetetaan taustapalkin t�ytt�aste pelaajan healthin suhteeksi makshealthiin
            backHealthBar.color = Color.green; // Asetetaan taustan v�ri vihre�ksi
            lerpTimer += Time.deltaTime; // Ajanotto k�ynnistyy
            float percentComplete = lerpTimer / chipSpeed; // Lasketaan kulunut aika suhteessa chipSpeed-muuttujaan
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete); // Lerpataan etupalkin t�ytt�astetta taustapalkin t�ytt�asteen suuntaan prosenttim��r�n mukaan
        }
    }
    public void TakeDamage(float damage) //metodi v�hent�� pelaajan terveytt� vahingon verran
    {
        health -= damage; //v�hennet��n pelaajan terveytt�
        lerpTimer = 0f; //asetetaan lerpTimer nollaan
        Debug.Log("Damage otettu, " + health + " jäljellä"); //tulostetaan "Damage otettu"
        durationTimer = 0; //asetetaan durationTimer nollaan
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1); //asetetaan overlayn v�ri t�ydeksi punaiseksi
    }

    public void RestoreHealth(float healAmount) //metodi palauttaa pelaajan terveytt� annetun m��r�n
    {
        health += healAmount; //lis�t��n pelaajan terveytt�
        lerpTimer = 0f; //asetetaan lerpTimer nollaan
        Debug.Log("Healthia saatu"); //tulostetaan "Healthia saatu"�
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
        if (health <= 0) //jos pelaajan health on pienempi tai yht�suuri kuin nolla
        {
            // Debug.Log("You died.");
            playerDead = true; //merkit��n pelaaja kuolleeksi
            StartCoroutine(DeathMenuActivation());
        }
    }

    IEnumerator DeathMenuActivation()
    {
        yield return new WaitForSeconds(2f);
        {         
            deathMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void HealthScreens()
    {
        // Tarkistaa onko greenhealth-v�rin l�pin�kyvyysarvo suurempi kuin 0
        if (greenhealth.color.a > 0)
        {
            Debug.Log("healtgained");

            // Lis�� keston ajastinta Time.deltaTimen avulla
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                // V�henn� vihre�n terveyspalkin l�pin�kyvyytt� ajan kuluessa
                float tempAlpha = greenhealth.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                greenhealth.color = new Color(greenhealth.color.r, greenhealth.color.g, greenhealth.color.b, tempAlpha);
            }
        }

        // Tarkista, onko overlay-v�rin l�pin�kyvyysarvo suurempi kuin 0
        if (overlay.color.a > 0)
        {
            // Tarkista, onko health alle 30
            if (health < 30)
                return;

            // Lis�� keston ajastinta Time.deltaTimen avulla
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                // V�henn� p��llekk�isyyden (overlay) l�pin�kyvyytt� ajan kuluessa
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("FootprintEnemy"))
        {
            neckSnap.Play();
            TakeDamage(100f);
        }
    }

}