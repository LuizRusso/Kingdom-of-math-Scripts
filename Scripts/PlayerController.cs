using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text coinsText;
    public int moedas = 0;
    private Coroutine decreasingRedBarCoroutine; // Variável para armazenar a referência à coroutine.

    private Rigidbody2D _playerRigidibody2D;
    private Animator    _playerAnimator;
    public float        _playerSpeed;
    public float       _playerInitialSpeed;
    public float        _playerRunSpeed;
    private Vector2     _playerDirection;
    public Image        lifebar;
    public Image        redBar;

    private bool _isAttack = false;

    public int maxHealth = 100;
    int currentHealth;

    public float originalPlayerSpeed;
    public float originalPlayerRunSpeed;

    public Text moedaText; // Referência ao texto das moedas.
    public RawImage moedaBackground; // Referência à imagem de fundo das moedas.
    public Texture2D moedaBackgroundTexture;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidibody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();

        _playerInitialSpeed = _playerSpeed;

        currentHealth = maxHealth;

        StoreOriginalSpeeds();
        if (moedaBackground != null && moedaBackgroundTexture != null)
        {
            // Defina a imagem de fundo das moedas.
            moedaBackground.texture = moedaBackgroundTexture;
        }

        // Atualize o texto e a quantidade de moedas.
        UpdateMoedaText();
    }

    // Update is called once per frame
    void Update()
    {
        _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (_playerDirection.sqrMagnitude > 0)
        {
            _playerAnimator.SetInteger("Movimento", 1);
        }
        else 
        {
            _playerAnimator.SetInteger("Movimento", 0);
        }

        Flip();

        PlayerRun();

        // Verificar o clique do mouse para atacar
        if (Input.GetMouseButtonDown(0)) // 0 representa o botão esquerdo do mouse
        {
            _isAttack = true;
            _playerSpeed = 0;
        }

        // Verificar o lançamento do botão do mouse para encerrar o ataque
        if (Input.GetMouseButtonUp(0))
        {
            _isAttack = false;
            _playerSpeed = _playerInitialSpeed;
        }

        if(_isAttack)
        {
            _playerAnimator.SetInteger("Movimento", 2);
        }
    }

    public void StoreOriginalSpeeds()
    {
        originalPlayerSpeed = _playerSpeed;
        originalPlayerRunSpeed = _playerRunSpeed;
    }

    public void RestoreOriginalSpeeds()
    {
        _playerSpeed = originalPlayerSpeed;
        _playerRunSpeed = originalPlayerRunSpeed;
    }

    void FixedUpdate()
    {
        _playerRigidibody2D.MovePosition(_playerRigidibody2D.position + _playerDirection * _playerSpeed * Time.fixedDeltaTime);
    }

    void Flip()
    {
        if (_playerDirection.x > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        }
        else if (_playerDirection.x < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }
    }

    void PlayerRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _playerSpeed = _playerRunSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _playerSpeed = _playerInitialSpeed;
        }
    }

    public void SetHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        Vector3 lifebarScale = lifebar.rectTransform.localScale;
        lifebarScale.x = (float)currentHealth / maxHealth;
        lifebar.rectTransform.localScale = lifebarScale;

        // Verifique se já existe uma coroutine em andamento e a interrompa.
        if (decreasingRedBarCoroutine != null)
        {
            StopCoroutine(decreasingRedBarCoroutine);
        }

        // Inicie a nova coroutine.
        decreasingRedBarCoroutine = StartCoroutine(decreasingRedBar(lifebarScale));
    }

    IEnumerator decreasingRedBar(Vector3 newScale)
    {
        float duration = 0.5f; // Duração da animação
        float startTime = Time.time;
        Vector3 startScale = redBar.transform.localScale;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            redBar.transform.localScale = Vector3.Lerp(startScale, newScale, t);
            yield return null;
        }

        redBar.transform.localScale = newScale; // Garanta que a escala final seja exatamente o que foi desejado.
    }

    public void AddMoedas(int quantidade)
    {
        moedas += quantidade;
        UpdateMoedaText();
    }

    // Função para atualizar o texto das moedas.
    private void UpdateMoedaText()
    {
        if (moedaText != null)
        {
            moedaText.text = "Moedas: " + moedas.ToString();
        }
    }
}