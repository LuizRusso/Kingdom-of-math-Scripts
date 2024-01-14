 using UnityEngine;
using UnityEngine.UI;

public class MercadoScript : MonoBehaviour
{
    public Canvas mercadoCanvas; // Referência ao Canvas do mercado
    public Text moedasText; // Texto para exibir a quantidade de moedas do jogador

    public int precoItem1 = 20; // Preço do primeiro item
    public int precoItem2 = 50; // Preço do segundo item
    public int precoItem3 = 30;
    public int precoItem4 = 70;
    public int precoItem5 = 100;

    private PlayerController playerController; // Referência ao script PlayerController

    void Start()
    {
        mercadoCanvas.enabled = false; // Desativa o Canvas no início

        // Encontrar o objeto com o script PlayerController em tempo de execução
        playerController = FindObjectOfType<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("PlayerController não encontrado!");
        }
    }

    void Update()
    {
        // Verifica se o jogador está próximo ao mercante e pressionou a tecla Control
        if (Input.GetKeyDown(KeyCode.LeftControl) && EstaNasProximidadesDoMercante())
        {
            if (!mercadoCanvas.enabled)
            {
                AbrirMercado();
            }
            else
            {
                FecharMercado();
            }
        }
    }
void AbrirMercado()
{
    if (EstaNasProximidadesDoMercante())
    {
        mercadoCanvas.enabled = true;
        AtualizarMoedas();
    }
    else
    {
        Debug.Log("Você não está nas proximidades do mercante.");
    }
}

    void FecharMercado()
    {
        mercadoCanvas.enabled = false;
    }

   public void ComprarItem1()
{
    if (EstaNasProximidadesDoMercante())
    {
        if (playerController != null && playerController.moedas >= precoItem1)
        {
            playerController.moedas -= precoItem1;
            Debug.Log("Item 1 comprado!");
            AtualizarMoedas();
        }
        else
        {
            Debug.Log("Moedas insuficientes para comprar Item 1.");
        }
    }
    else
    {
        Debug.Log("Você não está nas proximidades do mercante.");
    }
}

public void ComprarItem2()
{
    if (EstaNasProximidadesDoMercante())
    {
        if (playerController != null && playerController.moedas >= precoItem2)
        {
            playerController.moedas -= precoItem2;
            Debug.Log("Item 2 comprado!");
            AtualizarMoedas();
        }
        else
        {
            Debug.Log("Moedas insuficientes para comprar Item 2.");
        }
    }
    else
    {
        Debug.Log("Você não está nas proximidades do mercante.");
    }
}

public void ComprarItem3()
{
    if (EstaNasProximidadesDoMercante())
    {
        if (playerController != null && playerController.moedas >= precoItem3)
        {
            playerController.moedas -= precoItem3;
            Debug.Log("Item 3 comprado!");
            AtualizarMoedas();
        }
        else
        {
            Debug.Log("Moedas insuficientes para comprar Item 3.");
        }
    }
    else
    {
        Debug.Log("Você não está nas proximidades do mercante.");
    }
}

public void ComprarItem4()
{
    if (EstaNasProximidadesDoMercante())
    {
        if (playerController != null && playerController.moedas >= precoItem4)
        {
            playerController.moedas -= precoItem4;
            Debug.Log("Item 4 comprado!");
            AtualizarMoedas();
        }
        else
        {
            Debug.Log("Moedas insuficientes para comprar Item 4.");
        }
    }
    else
    {
        Debug.Log("Você não está nas proximidades do mercante.");
    }
}

public void ComprarItem5()
{
    if (EstaNasProximidadesDoMercante())
    {
        if (playerController != null && playerController.moedas >= precoItem5)
        {
            playerController.moedas -= precoItem5;
            Debug.Log("Item 5 comprado!");
            AtualizarMoedas();
        }
        else
        {
            Debug.Log("Moedas insuficientes para comprar Item 5.");
        }
    }
    else
    {
        Debug.Log("Você não está nas proximidades do mercante.");
    }
}


    void AtualizarMoedas()
    {
        moedasText.text = "Moedas: " + (playerController != null ? playerController.moedas.ToString() : "0");
    }

    bool EstaNasProximidadesDoMercante()
    {

    if (playerController != null)
    {
        // Defina a distância máxima para considerar que o jogador está nas proximidades do mercante.
        float distanciaMaxima = 5f;

        // Verifique se a distância entre o jogador e o mercante é menor que a distância máxima.
        return Vector3.Distance(transform.position, playerController.transform.position) < distanciaMaxima;
    }

    return false;

    }
}