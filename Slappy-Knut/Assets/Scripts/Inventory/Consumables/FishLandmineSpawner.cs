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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(Count == 0) _audioSource.Play();
            else
            {
                Transform pTransform = FindObjectOfType<PlayerController>().transform;
                Vector3 p = pTransform.transform.position;
                Vector3 spawnOffset = new Vector3(p.x, p.y + 0.2f, p.z);
                Instantiate(prefab, spawnOffset, pTransform.rotation);
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
