using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject inimigoPrefab; // Arraste o Prefab Mlk1 aqui
    public Transform[] pontosDeSpawn; // Arraste objetos vazios da cena aqui

    void Start()
    {
        if (inimigoPrefab == null || pontosDeSpawn.Length == 0) return;

        int nivel = PlayerPrefs.GetInt("NivelAtual", 1);
        
        // NOVA TRAVA: Se for o Nível 1, o script para por aqui e não gera ninguém.
        if (nivel == 1) 
        {
            return; 
        }
        
        // A partir do Nível 2 em diante, a matemática original entra em ação:
        // Nível 2 = 1 inimigo extra | Nível 3 e 4 = 2 inimigos extras
        int quantidadeInimigos = (nivel + 1) / 2;

        for (int i = 0; i < quantidadeInimigos; i++)
        {
            int indice = Random.Range(0, pontosDeSpawn.Length);
            Instantiate(inimigoPrefab, pontosDeSpawn[indice].position, Quaternion.identity);
        }
    }
}