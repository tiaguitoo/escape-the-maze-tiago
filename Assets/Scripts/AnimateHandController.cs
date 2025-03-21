using System.Collections; // Importa a biblioteca para coleções, embora não seja usada neste script
using System.Collections.Generic; // Importa outra biblioteca para coleções genéricas, também não utilizada aqui
using UnityEngine; // Importa a biblioteca principal do Unity para manipulação de objetos no jogo
using UnityEngine.InputSystem; // Importa a biblioteca do sistema de entrada do Unity para capturar ações do jogador

public class AnimateHandController : MonoBehaviour //Define a classe AnimateHandController, que herda de MonoBehaviour para ser usada como um componente no Unity
{
    public InputActionReference gripInputActionReference; //Referência para a ação de entrada que controla o aperto da mão (grip)
    public InputActionReference triggerInputActionReference; //Referência para a ação de entrada que controla o gatilho (trigger)

    private Animator handAnimator; //Variável para armazenar o componente Animator da mão
    private float gripValue; //Variável que armazena o valor da entrada do grip
    private float triggerValue; //Variável que armazena o valor da entrada do trigger
    
    void Start()
    {
        handAnimator = GetComponent<Animator>(); //Obtém e armazena o componente Animator anexado ao GameObject
    }

    void Update()
    {
        AnimateGrip(); //Chama a função para animar o aperto da mão
        AnimateTrigger(); //Chama a função para animar o gatilho da mão
    }

    private void AnimateGrip()
    {
        gripValue = gripInputActionReference.action.ReadValue<float>(); //Lê o valor do grip do controle do jogador
        handAnimator.SetFloat("Grip", gripValue); //Define o valor do parâmetro "Grip" no Animator para animar a mão
    }

    private void AnimateTrigger()
    {
        triggerValue = triggerInputActionReference.action.ReadValue<float>(); //Lê o valor do trigger do controle do jogador
        handAnimator.SetFloat("Trigger", triggerValue); //Define o valor do parâmetro "Trigger" no Animator para animar o dedo no gatilho
    }
}
