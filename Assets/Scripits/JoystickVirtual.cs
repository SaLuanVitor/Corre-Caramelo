using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickVirtual : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform fundoJoystick;
    
    [Header("Configuração Visual")]
    public RectTransform alavanca; 
    
    public static Vector2 VetorInput { get; private set; }

    private float raio;

    void Start()
    {
        fundoJoystick = GetComponent<RectTransform>();
        raio = fundoJoystick.rect.width / 2f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 posicao;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(fundoJoystick, eventData.position, eventData.pressEventCamera, out posicao))
        {
            posicao.x = (posicao.x / fundoJoystick.rect.width);
            posicao.y = (posicao.y / fundoJoystick.rect.height);

            VetorInput = new Vector2(posicao.x * 2, posicao.y * 2);
            VetorInput = (VetorInput.magnitude > 1.0f) ? VetorInput.normalized : VetorInput;

            if (alavanca != null)
            {
                alavanca.anchoredPosition = new Vector2(VetorInput.x * raio, VetorInput.y * raio);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        VetorInput = Vector2.zero;
        if (alavanca != null)
        {
            alavanca.anchoredPosition = Vector2.zero;
        }
    }
}