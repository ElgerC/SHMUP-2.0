using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //General variables
    public Rigidbody2D rb;

    //Movement variables
    public float moveSpeed;
    private Vector2 _moveDirection;

    //Bullet variables
    [SerializeField] GameObject bullet;
    [SerializeField] bool canFire = false;

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
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponentInChildren<Rigidbody2D>();
        PlayerPrefs.SetInt("Score", 0);
        M_SceneManager.instance.sceneObjects.Add(gameObject);
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
                Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
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
        if(UpgradeIndex < 3)
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
        spriteRenderer.sprite = playerSO.playerVersions[M_SceneManager.instance.sceneObjSpriteIndex].versionSprites[UpgradeIndex-1];
    }
}
