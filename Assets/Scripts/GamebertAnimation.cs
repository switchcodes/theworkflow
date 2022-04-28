using UnityEngine;
using UnityEngine.UI;

public class GamebertAnimation : MonoBehaviour
{
    public Sprite[] animatedImages;

    public Image animatedImageObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animatedImageObj.sprite = animatedImages[(int)(Time.time*10)%animatedImages.Length];
    }
}
