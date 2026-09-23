using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RiverAnimator : MonoBehaviour
{
    public Vector2 flowSpeed = new Vector2(0.1f, 0.05f);
    public float minHeight = 0.01f;
    public float maxHeight = 0.06f;
    public float heightPulsationSpeed = 1.5f;

    private Renderer rend;
    private Material riverMat;
    private Vector2 currentOffset;

    void Start()
    {
        rend = GetComponent<Renderer>();
        riverMat = rend.material;
    }

    void Update()
    {
        if (riverMat == null) return;

        // Зміщення текстури на основі часу кадру
        currentOffset += flowSpeed * Time.deltaTime;
        currentOffset.x = Mathf.Repeat(currentOffset.x, 1f);
        currentOffset.y = Mathf.Repeat(currentOffset.y, 1f);
        // Застосування зміщення до базової текстури і карти нормалей
        riverMat.SetTextureOffset("_MainTex", currentOffset);
        riverMat.SetTextureOffset("_BumpMap", currentOffset);

        // Зміщення карти висот
        if (riverMat.HasProperty("_ParallaxMap"))
        {
            riverMat.SetTextureOffset("_ParallaxMap", currentOffset);
        }

        // Плавна пульсація висоти
        float sineWave = (Mathf.Sin(Time.time * heightPulsationSpeed) + 1f) / 2f;
        float currentHeight = Mathf.Lerp(minHeight, maxHeight, sineWave);
        if (riverMat.HasProperty("_Parallax"))
        {
            riverMat.SetFloat("_Parallax", currentHeight);
        }
    }
}