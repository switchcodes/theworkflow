using UnityEngine;
using UnityEngine.SceneManagement;

public class myScript : MonoBehaviour
{

    public Animator animator;
    public AudioSource punchSound;
    public AudioSource voice;
    private static readonly int B = Animator.StringToHash("Bool");

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (animator == null) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool(B, true);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool(B, true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool(B, false);
        }
    }

    public void PlayPunchSound() {
	    punchSound.Play();
    }

    public void PlayZuccVoice() {
	    voice.Play();
    }
    
    public void ReloadScene() {
	    SceneManager.LoadScene("SampleScene");
    }
}
