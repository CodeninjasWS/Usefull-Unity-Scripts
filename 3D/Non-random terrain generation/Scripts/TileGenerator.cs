using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class TileData
{
    public int tileNumber;
}

public class TileGenerator : MonoBehaviour
{
    public Transform gridParent;
    public TextAsset jsonFile;
    public GameObject[] tilePrefabs;

    private void Start()
    {
        GenerateGridFromJSON();
    }

    private void GenerateGridFromJSON()
    {
        string jsonData = jsonFile.text;
        GridData gridData = JsonUtility.FromJson<GridData>(jsonData);

        int rows = gridData.rows;
        int columns = gridData.columns;
        List<TileData> tiles = gridData.tiles;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int tileIndex = row * columns + col;
                if (tileIndex < tiles.Count)
                {
                    TileData tileData = tiles[tileIndex];

                    if (tileData.tileNumber >= 0 && tileData.tileNumber < tilePrefabs.Length)
                    {
                        GameObject tilePrefab = tilePrefabs[tileData.tileNumber];
                        Vector3 position = new Vector3(col, 0f, row); // Set the Y position to 0 to make it flat
                        GameObject tileGO = Instantiate(tilePrefab, position, Quaternion.identity, gridParent);
                        // You may also adjust the scale, rotation, or other properties of the tile here.
                    }
                    else
                    {
                        Debug.LogWarning("Invalid tile number in JSON data at row: " + row + ", col: " + col);
                    }
                }
            }
        }
    }

}

[System.Serializable]
public class GridData
{
    public int rows;
    public int columns;
    public List<TileData> tiles;
}
