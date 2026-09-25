using UnityEngine;

public class MushroomScript : MonoBehaviour
{
    public enum MushroomType { Bad, Good }
    public MushroomType type;

    public void Consume()
    {
        // Знищення поточного і створення нового гриба
        MushroomSpawner.Instance.SpawnMushroom(type == MushroomType.Bad);
        Destroy(gameObject);
    }
}