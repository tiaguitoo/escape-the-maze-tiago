using System.Collections; // Importa a biblioteca para coleções, embora não seja utilizada neste script
using System.Collections.Generic; // Importa outra biblioteca para coleções genéricas, também não utilizada aqui
using UnityEngine; // Importa a biblioteca principal do Unity para manipulação de objetos no jogo
using UnityEngine.AI; // Importa a biblioteca para navegação com NavMesh

public class ZombieChase : MonoBehaviour // Define a classe ZombieChase, que herda de MonoBehaviour para ser usada como um componente no Unity
{
    private Transform player; //Referência para a posição do jogador
    private NavMeshAgent agent; //Referência para o agente de navegação (NavMeshAgent) do zombie
    public LayerMask obstacleLayers; //Máscara de camadas para detectar obstáculos no caminho do zombie
    private Animator animator; //Referência para o Animator do zombie
    private bool playerCaught = false; //Variável que verifica se o jogador foi apanhado
    public Transform[] destinations; //Lista de pontos para onde o zombie pode caminhar aleatoriamente
    public float chaseDistance = 10f; //Distância máxima para iniciar a perseguição ao jogador
    public float walkingSpeed, runningSpeed; //Velocidades do zombie para caminhar e correr
    private int currentDestinationIndex = 0; //Índice do destino atual na lista de destinos

    public SceneTransitionManager stm; //Referência para o gerenciador de transição de cenas

    public AudioSource AudioToPause; //Áudio a ser pausado quando o zombie começa a correr
    public AudioSource AudioRun; //Áudio de perseguição do zombie
    public CameraFootsteps playerCameraFootsteps; // Referência para os efeitos sonoros dos passos do jogador
    public GameObject zombieCam; //Câmera do zombie, ativada quando o jogador é apanhado

    void Start()
    {
        animator = GetComponent<Animator>(); //Obtém o componente Animator anexado ao zombie
        player = GameObject.FindWithTag("Player").transform; //Encontra o jogador pelo tag "Player"
        agent = GetComponent<NavMeshAgent>(); //Obtém o componente NavMeshAgent do zombie
        WalkToRandomDestination(); //Faz o zombie caminhar para um destino aleatório ao iniciar o jogo
    }

    void Update()
    {
        if (!playerCaught) //Se o jogador ainda não foi apanhado
        {
            float distanceToPlayer = Vector3.Distance(agent.transform.position, player.position); //Calcula a distância entre o zombie e o jogador
            
            if(distanceToPlayer < chaseDistance) //Se o jogador estiver dentro do alcance de perseguição
            {
                if(AudioToPause.isPlaying) //Se o áudio ambiente estiver a tocar, interrompe e inicia o áudio de perseguição
                {
                    AudioToPause.Stop();
                    AudioRun.Play();
                    playerCameraFootsteps.TriggerScared(); //Ativa o efeito sonoro de susto no jogador
                }

                //Inicia a perseguição ao jogador
                agent.destination = player.position;
                agent.speed = runningSpeed;

                animator.SetTrigger("isRunning"); //Ativa a animação de corrida
                animator.ResetTrigger("isWalking"); //Desativa a animação de caminhada
                animator.ResetTrigger("isIdle"); //Desativa a animação de ociosidade
            }
            else //Se o jogador estiver fora do alcance de perseguição
            {
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance) //Se o zombie chegou ao seu destino
                {
                    if(AudioRun.isPlaying) //Se o áudio de perseguição estiver a tocar, interrompe-o e volta ao áudio normal
                    {
                        AudioRun.Stop();
                        AudioToPause.Play();
                    }

                    WalkToRandomDestination(); //Faz o zombie voltar a caminhar aleatoriamente
                }
            }
        }
    }

    bool CanSeePlayer() //Função para verificar se o zombie pode ver o jogador
    {
        RaycastHit hit;
        Vector3 directionToPlayer = (player.position - agent.transform.position).normalized; //Calcula a direção do jogador
        if (Physics.Raycast(agent.transform.position, directionToPlayer, out hit, chaseDistance, obstacleLayers)) //Verifica se há obstáculos entre o zombie e o jogador
            return hit.transform == player; //Retorna verdadeiro se o jogador for atingido pelo raio
        return false; //Retorna falso se houver um obstáculo a bloquear a visão
    }

    void ChasePlayer() //Método para fazer o zombie perseguir o jogador
    {
        agent.SetDestination(player.position); //Define o destino do zombie para a posição do jogador
        agent.speed = runningSpeed; //Define a velocidade de corrida

        animator.SetTrigger("isRunning"); //Ativa a animação de corrida
        animator.ResetTrigger("isWalking"); //Desativa a animação de caminhada
        animator.ResetTrigger("isIdle"); //Desativa a animação de ociosidade
    }

    IEnumerator GoIdle() //Método para colocar o zombie em estado de ociosidade temporária
    {
        animator.SetTrigger("isIdle"); //Ativa a animação de ociosidade
        animator.ResetTrigger("isRunning"); //Desativa a animação de corrida
        animator.ResetTrigger("isWalking"); //Desativa a animação de caminhada

        agent.ResetPath(); //Interrompe qualquer movimento do NavMeshAgent
        agent.speed = 0f; //Define a velocidade do zombie para 0

        yield return new WaitForSeconds(3f); //Espera 3 segundos antes de continuar
        
        WalkToRandomDestination(); //Faz o zombie voltar a caminhar aleatoriamente
    }

    void WalkToRandomDestination() //Método para fazer o zombie andar para um destino aleatório
    {
        agent.SetDestination(destinations[currentDestinationIndex].position); //Define o destino atual como a posição do próximo destino
        currentDestinationIndex = (currentDestinationIndex + 1) % destinations.Length; //Atualiza o índice do destino para o próximo na lista
        agent.speed = walkingSpeed; //Define a velocidade de caminhada

        animator.SetTrigger("isWalking"); //Ativa a animação de caminhada
        animator.ResetTrigger("isRunning"); //Desativa a animação de corrida
        animator.ResetTrigger("isIdle"); //Desativa a animação de ociosidade
    }
    
    void OnTriggerEnter(Collider other) //Método chamado quando o zombie colide com outro objeto
    {
        if (other.CompareTag("Player")) //Se o objeto colidido for o jogador
        {
            playerCaught = true; //Define que o jogador foi apanhado
            zombieCam.SetActive(true); //Ativa a câmera do zombie (provavelmente para uma cena de captura)
            stm.GoToScene(3); //Muda para a cena 3 (provavelmente uma tela de game over)
        }
    }
}
