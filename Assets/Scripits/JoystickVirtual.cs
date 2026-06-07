using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickVirtual : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform fundoJoystick;
    private RectTransform alavanca;
    
    // Essa variável guarda para onde o dedão está empurrando (X e Y)
    public static Vector2 VetorInput { get; private set; }

    private float raio;

    void Start()
    {
        fundoJoystick = GetComponent<RectTransform>();
        // Pega o primeiro filho (que é a alavanca)
        alavanca = transform.GetChild(0).GetComponent<RectTransform>();
        raio = fundoJoystick.sizeDelta.x / 2f;
    }

    // Roda enquanto o jogador arrasta o dedo na tela
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 posicao;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(fundoJoystick, eventData.position, eventData.pressEventCamera, out posicao))
        {
            posicao.x = (posicao.x / fundoJoystick.sizeDelta.x);
            posicao.y = (posicao.y / fundoJoystick.sizeDelta.y);

            VetorInput = new Vector2(posicao.x * 2, posicao.y * 2);
            VetorInput = (VetorInput.magnitude > 1.0f) ? VetorInput.normalized : VetorInput;

            // Move a bolinha visual da alavanca na tela
            alavanca.anchoredPosition = new Vector2(VetorInput.x * raio, VetorInput.y * raio);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    // Quando o jogador tira o dedo da tela, tudo volta pro centro e o cachorro para
    public void OnPointerUp(PointerEventData eventData)
    {
        VetorInput = Vector2.zero;
        alavanca.anchoredPosition = Vector2.zero;
    }
}