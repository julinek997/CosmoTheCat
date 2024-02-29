using UnityEngine;

public class TiledBackground : MonoBehaviour
{
    public GameObject tilePrefab;
    public int width = 10;
    public int height = 10;
    public float spacing = 1f;

    void Start()
    {
        CreateBackground();
    }

    void CreateBackground()
    {
        Vector3 tileSize = tilePrefab.GetComponent<Renderer>().bounds.size;

        Vector3 adjustedInitialPosition = transform.position - new Vector3((width - 1) * spacing * tileSize.x / 2, (height - 1) * spacing * tileSize.y / 2, 0);

        adjustedInitialPosition -= new Vector3(tileSize.x / 2, tileSize.y / 2, 0);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = adjustedInitialPosition + new Vector3(x * (tileSize.x + spacing), y * (tileSize.y + spacing), 0);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
                tile.transform.parent = transform;
            }
        }
    }
}
