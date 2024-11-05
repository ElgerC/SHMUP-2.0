using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    [SerializeField] bool CanGetHit = true;
    [SerializeField] float hitImunityDur;
    [SerializeField] private bool ShieldActive = false;

    //Upgrade varaibles
    [SerializeField] private int UpgradeIndex = 1;

    //Versions variables
    [SerializeField] private PlayerScriptableObject playerSO;
    SpriteRenderer spriteRenderer;
    M_SceneManager sceneManager;

    //Abilities variables
    [SerializeField] private List<GameObject> abilityProjectiles = new List<GameObject>();
    [SerializeField] private GameObject spawnPoint;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponentInChildren<Rigidbody2D>();
        PlayerPrefs.SetInt("Score", 0);
        sceneManager = M_SceneManager.instance;
        sceneManager.sceneObjects.Add(gameObject);

        lineRen = GetComponent<LineRenderer>();
        lineRen.enabled = false;

        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        ReasignSprite();
    }
    private void Update()
    {
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
    public void Fire(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            if (Time.timeScale >= 1)
                if (canFire)
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
                            if (!autoFiring)
                            {
                                animator.SetTrigger("LaserFadeIn");
                                autoFiring = true;
                            }
                            else
                            {
                                animator.SetTrigger("LaserFadeOut");
                                autoFiring = false;
                            }

                            break;
                    }


    }

    private IEnumerator SingleShot()
    {
        canFire = false;
        Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(1 - (UpgradeIndex / 4));
        canFire = true;

    }
    private IEnumerator AutoShoot()
    {
        autoFiring = true;
        while (autoFiring)
        {
            Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, Random.Range(10 * UpgradeIndex, -10 * UpgradeIndex)));
            yield return new WaitForSeconds(0.55f - (0.15f * UpgradeIndex));
        }
    }
    public void LaserFadeIn()
    {
        lineRen.enabled = true;
    }
    public void LaserFadeOut()
    {
        lineRen.enabled = false;
    }
    public void LaserDmg()
    {
        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(transform.position, transform.up * laserLength, laserLength, enemyLayer);
        if (hit.Length > 0)
        {
            Debug.Log(hit[0]);
            foreach (RaycastHit2D obj in hit)
            {
                if (obj.transform.GetComponent<GeneralEnemyScript>() != null)
                {
                    GeneralEnemyScript generalEnemyScript = obj.transform.GetComponent<GeneralEnemyScript>();
                    generalEnemyScript.TakeDmg((generalEnemyScript.health / 5));
                }


            }
        }
    }
    public void Ability(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
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
        }
    }
    private void SpawnMulti(float amount,GameObject obj)
    {
        for (int i = 0; i < amount; i++)
        {
            float spawnRotate = (-17.5f * (UpgradeIndex - 1)) + (35 * i);
            spawnPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, spawnRotate));
            Instantiate(obj, new Vector3(gameObject.transform.position.x + (spawnPoint.transform.up.x*4), gameObject.transform.position.y + spawnPoint.transform.up.y,gameObject.transform.position.z), Quaternion.Euler(new Vector3(0,0,spawnRotate)));
        }
    }
    private void SpawnRocket()
    {
        GameObject obj = Instantiate(abilityProjectiles[1],new Vector3(transform.position.x, transform.position.y+1), Quaternion.identity);
        obj.GetComponent<Rocket>().blastRange = 1.5f * UpgradeIndex;
    }
    private void SpawnTarget()
    {
        GameObject obj = Instantiate(abilityProjectiles[2], new Vector3(transform.position.x, transform.position.y + 1), Quaternion.identity);
        obj.GetComponent<Target>().upgradeIndex = UpgradeIndex;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Drop")
        {
            if (CanGetHit && !ShieldActive)
            {
                Debug.Log("Player hit");
                health -= 1;
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
    public void Upgrade()
    {
        if (UpgradeIndex < 3)
        {
            UpgradeIndex++;
        }
        SpeedBoost(2);
    }
    private IEnumerator SpeedBoost(float delay)
    {
        float pastSpeed = moveSpeed;
        moveSpeed = moveSpeed * 1.5f;
        yield return new WaitForSeconds(delay);
        moveSpeed = pastSpeed;
    }
    public void ReasignSprite()
    {
        autoFiring = false;
        animator.SetFloat("Speed", 1 * UpgradeIndex);
        spriteRenderer.sprite = playerSO.playerVersions[sceneManager.sceneObjSpriteIndex].versionSprites[UpgradeIndex - 1];
    }
}
