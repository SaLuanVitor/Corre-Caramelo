using UnityEngine;

public class GeradorDeEspecial : MonoBehaviour
{
    public GameObject petiscoEspecialPrefab;
    
    // Limites da sua tela (ajuste os mesmos números que você usa no gerador normal)
    public float xMin = -8f, xMax = 8f, yMin = -4f, yMax = 4f;

    void Start()
    {
        // Descobre em qual fase o jogador está
        int nivelAtual = PlayerPrefs.GetInt("NivelAtual", 1);

        // Se for fase 3 ou maior, sorteia a posição e cria 1 único petisco especial
        if (nivelAtual >= 3)
        {
            float xAleatorio = Random.Range(xMin, xMax);
            float yAleatorio = Random.Range(yMin, yMax);
            Vector3 posicao = new Vector3(xAleatorio, yAleatorio, 0f);

            Instantiate(petiscoEspecialPrefab, posicao, Quaternion.identity);
        }
    }
}