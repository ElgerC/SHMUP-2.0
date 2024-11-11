using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //General variables
    public Rigidbody2D rb;
    private Animator animator;

    //Movement variables
    public float moveSpeed;
    private Vector2 _moveDirection;

    //Shoot variables
    [SerializeField] GameObject bullet;
    private bool canFire = true;
    private bool autoFiring = false;
    [SerializeField] private LayerMask enemyLayer;
    private LineRenderer lineRen;
    [SerializeField] float laserLength;

    //Health variables
    [SerializeField] float health;
    bool CanGetHit = true;
    [SerializeField] float hitImunityDur;
    private bool ShieldActive = false;
    HealthUIManager healthUIManager;

    //Upgrade varaibles
    [SerializeField] private int UpgradeIndex = 1;

    //Versions variables
    [SerializeField] private PlayerScriptableObject playerSO;
    SpriteRenderer spriteRenderer;
    M_SceneManager sceneManager;

    //Abilities variables
    [SerializeField] private List<GameObject> abilityProjectiles = new List<GameObject>();
    [SerializeField] private GameObject spawnPoint;
    private float charge;
    private float maxCharge = 100;
    [SerializeField] private Slider chargeSlider;

    private void Awake()
    {
        //Asignment
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponentInChildren<Rigidbody2D>();
        sceneManager = M_SceneManager.instance;
        lineRen = GetComponent<LineRenderer>();
        healthUIManager = FindObjectOfType<HealthUIManager>();
        animator = GetComponent<Animator>();

        //setting
        PlayerPrefs.SetInt("Score", 0);
        sceneManager.sceneObjects.Add(gameObject);
        lineRen.enabled = false;
        chargeSlider.maxValue = maxCharge;
    }

    private void Start()
    {
        ReasignSprite();
        healthUIManager.ChangeUIHealth(health);
    }

    private void Update()
    {
        //Checking for the right version and setting lineRender positions if true
        if (sceneManager.sceneObjSpriteIndex == 2)
        {
            Vector3 lRSP = transform.position;
            Vector3 lREP = new Vector3(transform.position.x, transform.position.y + laserLength, transform.position.z);

            lineRen.SetPositions(new Vector3[] { lRSP, lREP });
        }
    }

    //Movement
    public void Movement(InputAction.CallbackContext ctx)
    {
        _moveDirection = ctx.action.ReadValue<Vector2>();
        rb.velocity = new Vector2(_moveDirection.x * moveSpeed, 0);
    }

    //Shooting
    #region
    public void Fire(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && Time.timeScale >= 1 && canFire)
            switch (sceneManager.sceneObjSpriteIndex)
            {
                case 0:
                    StartCoroutine(SingleShot());
                    break;
                case 1:
                    if (!autoFiring)
                        StartCoroutine(AutoShoot());
                    else
                        autoFiring = false;
                    break;
                case 2:
                    //checking if the player is firing and either fading in or out the laser based on that
                    if (!autoFiring)
                    {
                        animator.SetTrigger("LaserFadeIn");
                        autoFiring = true;
                    }
                    else
                    {
                        animator.SetBool("LaserFadeOut", true);
                        autoFiring = false;
                    }
                    break;
            }
    }

    //Firing a single time
    private IEnumerator SingleShot()
    {
        canFire = false;

        Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(1 - (UpgradeIndex / 4));

        canFire = true;
    }
    //Firing continously until the player presses again
    private IEnumerator AutoShoot()
    {
        autoFiring = true;
        while (autoFiring)
        {
            //Choosing rotation randomly and spawning based on that
            Quaternion rot = Quaternion.Euler(0, 0, Random.Range(10 * UpgradeIndex, -10 * UpgradeIndex));
            Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), rot);

            yield return new WaitForSeconds(0.55f - (0.15f * UpgradeIndex));
        }
    }

    public void LaserFadeIn()
    {
        lineRen.enabled = true;
        animator.SetBool("LaserFadeOut", false);
    }

    public void LaserFadeOut()
    {
        lineRen.enabled = false;
    }
    //laser dmg tick (called in animator)
    public void LaserDmg()
    {
        //local variables
        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(transform.position, transform.up * laserLength, laserLength, enemyLayer);

        //Checking if it hit anything
        if (hit.Length > 0)
        {
            Debug.Log(hit[0]);
            foreach (RaycastHit2D obj in hit)
            {
                //checking for script
                if (obj.transform.GetComponent<GeneralEnemyScript>() != null)
                {
                    GeneralEnemyScript generalEnemyScript = obj.transform.GetComponent<GeneralEnemyScript>();
                    generalEnemyScript.TakeDmg(generalEnemyScript.health / 5);
                }


            }
        }
    }
    #endregion
    #region

    //Do ability based on version linked E 
    public void Ability(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && charge >= 50)
        {
            switch (sceneManager.sceneObjSpriteIndex)
            {
                case 0:
                    SpawnMulti(UpgradeIndex, abilityProjectiles[0]);
                    break;
                case 1:
                    SpawnRocket();
                    break;
                case 2:
                    SpawnTarget();
                    break;
            }
            charge -= 50;
            chargeSlider.value = charge;

        }
    }

    // spawn multiple barriers with the rottation being based of upgrade index
    private void SpawnMulti(float amount, GameObject obj)
    {
        for (int i = 0; i < amount; i++)
        {
            //local variables
            float spawnRotate = (-17.5f * (UpgradeIndex - 1)) + (35 * i);
            spawnPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, spawnRotate));

            Instantiate(obj, new Vector3(gameObject.transform.position.x + (spawnPoint.transform.up.x * 4), gameObject.transform.position.y + spawnPoint.transform.up.y, gameObject.transform.position.z), Quaternion.Euler(new Vector3(0, 0, spawnRotate)));
        }
    }

    //spawning a rocket and setting blast range
    private void SpawnRocket()
    {
        GameObject obj = Instantiate(abilityProjectiles[1], new Vector3(transform.position.x, transform.position.y + 1), Quaternion.identity);
        obj.GetComponent<Rocket>().blastRange = 1.5f * UpgradeIndex;
    }

    //spawning target and setting its int 
    private void SpawnTarget()
    {
        GameObject obj = Instantiate(abilityProjectiles[2], new Vector3(transform.position.x, transform.position.y + 1), Quaternion.identity);
        obj.GetComponent<Target>().upgradeIndex = UpgradeIndex;
    }

    #endregion
    //increasing upgrade level en increasing speed for a temp time
    public void Upgrade()
    {
        if (UpgradeIndex < 3)
        {
            UpgradeIndex++;
        }
        StartCoroutine(SpeedBoost(2));
    }
    public void ReasignSprite()
    {
        switch (sceneManager.sceneObjSpriteIndex)
        {
            case 0:
                lineRen.enabled = false;
                animator.SetBool("LaserFadeOut", true);
                autoFiring = false;
                transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                break;
            case 1:
                lineRen.enabled = false;
                animator.SetBool("LaserFadeOut", true);
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case 2:
                animator.SetBool("LaserFadeOut", false);
                animator.SetFloat("Speed", 1 * UpgradeIndex);
                autoFiring = false;
                transform.localScale = new Vector3(1, 1, 1);
                break;
        }
        spriteRenderer.sprite = playerSO.playerVersions[sceneManager.sceneObjSpriteIndex].versionSprites[UpgradeIndex - 1];
    }
    #region
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Drop")
        {
            if (CanGetHit && !ShieldActive)
            {
                Debug.Log("Player hit");
                health -= 1;
                healthUIManager.ChangeUIHealth(health);
                if (health <= 0)
                    SceneManager.LoadScene("EndScene");
                StartCoroutine(Imunity(hitImunityDur));
            }
            if (ShieldActive)
            {
                ShieldActive = false;
                return;
            }
        }

    }
    private IEnumerator Imunity(float duration)
    {
        CanGetHit = false;
        yield return new WaitForSeconds(duration);
        CanGetHit = true;
    }
    public IEnumerator ShieldDur(float duration)
    {
        ShieldActive = true;
        yield return new WaitForSeconds(duration);
        ShieldActive = false;
    }
    public void Shield(float time)
    {
        StartCoroutine(ShieldDur(time));
    }
    private IEnumerator SpeedBoost(float delay)
    {
        float pastSpeed = moveSpeed;
        moveSpeed = moveSpeed * 1.5f;
        yield return new WaitForSeconds(delay);
        moveSpeed = pastSpeed;
    }
    #endregion 
    public void AddCharge(float amount, string identifier)
    {
        if (charge <= (maxCharge * 1.25))
        {
            if ((identifier == "Enemy1" | identifier == "Enemy2" | identifier == "Enemy3") && sceneManager.sceneObjSpriteIndex == 1)
            {
                charge += amount * 2;
            }
            else if (identifier == "Boss" && sceneManager.sceneObjSpriteIndex == 2)
            {
                charge += amount * 2;
            }
            else if (identifier == "Drop" && sceneManager.sceneObjSpriteIndex == 0)
            {
                charge += amount * 2;
            }
            else
                charge += amount;
            chargeSlider.value = charge;
        }


    }
}
