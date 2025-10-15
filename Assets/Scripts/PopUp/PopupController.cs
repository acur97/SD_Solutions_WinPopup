using UnityEngine;

public class PopupController : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject root;
    [SerializeField] private float openTime = 1;
    [SerializeField] private float openOvershoot = 0;

    [Space]
    [SerializeField] private float closeTime = 1;
    [SerializeField] private float closedOvershoot = 0;

    [Header("Particles")]
    [SerializeField] private ParticleSystem lateralBubbles;
    [SerializeField] private ParticleSystem centralBubbles;

    private void Awake()
    {
        OffState();
    }

    private void OffState()
    {
        root.SetActive(false);
        root.transform.localScale = Vector3.zero;
    }

    public void OpenPopup()
    {
        root.SetActive(true);
        root.transform.localScale = Vector3.zero;

        centralBubbles.Play();
        lateralBubbles.Play();

        LeanTween.scale(root, Vector3.one, openTime).setEaseOutElastic().setOvershoot(openOvershoot);
    }

    public void ClosePopup()
    {
        lateralBubbles.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        LeanTween.scale(root, Vector3.zero, closeTime).setEaseInBack().setOvershoot(closedOvershoot).setOnComplete(OffState);
    }
}