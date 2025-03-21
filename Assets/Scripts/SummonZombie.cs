using System.Collections; //Importa a biblioteca para coleções
using System.Collections.Generic; //Importa outra biblioteca para coleções genéricas
using UnityEngine; //Importa a biblioteca principal do Unity para manipulação de objetos no jogo

public class SummonZombie : MonoBehaviour //Define a classe SummonZombie, que herda de MonoBehaviour para ser usada como um componente no Unity
{
    public GameObject zombie; //Referência para o GameObject do zombie que será ativado
    public Collider trigger; //Referência para o Collider que servirá como um gatilho para ativar o zombie
    
    void OnTriggerEnter(Collider other) //Método chamado quando um objeto entra na área de colisão (gatilho)
    {
        if(other.CompareTag("Player")) //Verifica se o objeto que entrou tem a tag "Player"
        {
            zombie.SetActive(true); //Ativa o GameObject do zombie, tornando-o visível e funcional no jogo
            trigger.enabled = false; //Desativa o Collider do gatilho para evitar ativações repetidas
        }
    }
}
