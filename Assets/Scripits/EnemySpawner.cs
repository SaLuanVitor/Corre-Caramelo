using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject inimigoPrefab; 
    public Transform[] pontosDeSpawn; 

    void Start()
    {
        // 1. Trava de segurança para avisar se esquecemos de colocar algo no Inspector
        if (inimigoPrefab == null)
        {
            Debug.LogError("ERRO: O Prefab do Inimigo está vazio no Gerador!");
            return;
        }
        if (pontosDeSpawn.Length == 0)
        {
            Debug.LogError("ERRO: Você não colocou nenhum Ponto de Spawn no Gerador!");
            return;
        }

        int nivel = PlayerPrefs.GetInt("NivelAtual", 1);
        int quantidadeInimigos = (nivel + 1) / 2;

        // 2. Avisa no Console exatamente quantos inimigos ele vai criar
        Debug.Log("Iniciando Fase " + nivel + ". O Gerador vai criar " + quantidadeInimigos + " inimigos.");

        for (int i = 0; i < quantidadeInimigos; i++)
        {
            int indice = Random.Range(0, pontosDeSpawn.Length);
            
            // 3. O Segredo: Garante que o Z seja 0 para ele não nascer no "fundo" da tela invisível
            Vector3 posicaoSegura = pontosDeSpawn[indice].position;
            posicaoSegura.z = 0f;

            Instantiate(inimigoPrefab, posicaoSegura, Quaternion.identity);
        }
    }
}