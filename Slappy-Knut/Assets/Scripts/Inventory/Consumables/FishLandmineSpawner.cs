using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishLandmineSpawner : MonoBehaviour
{
    public Image null_Image;
    public GameObject prefab;
    public static int Count;
    public TextMeshProUGUI countText;
    
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        countText.text = Count.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !PauseGame.IsPaused)
        {
            if(Count == 0) _audioSource.Play();
            else
            {
                Transform pTransform = FindObjectOfType<PlayerController>().transform;
                Instantiate(prefab, pTransform.position, pTransform.rotation);
                Count--;
                countText.text = Count.ToString();
            }
        }
        if (Count == 0) null_Image.enabled = true;
        else null_Image.enabled = false;
    }

    public void Add()
    {
        Count++;
        countText.text = Count.ToString();
    }
}
