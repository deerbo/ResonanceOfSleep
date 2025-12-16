using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public TerrainLayer grassLayer;

    // =====terrain gen stuff=====
    public int width = 256;
    public int height = 256;
    public int depth = 50;

    public float scale = 3f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public int octaves = 4;
    public float persistence = 0.3f;   // How much each octave contributes
    public float lacunarity = 3.0f;    // Frequency multiplier between octaves

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    public TerrainData GenerateTerrain (TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, height);
        
        float[,] heights = GenerateHeights();
        terrainData.SetHeights(0, 0, heights);

        return terrainData;
    }

    public float[,] GenerateHeights ()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    public float CalculateHeight (int x, int y)
    {
        float amplitude = 1f;
        float frequency = 1f;
        float noiseHeight = 0f;

        for (int i = 0; i < octaves; i++)
        {
            float xCoord = (x / (float)width  * scale * frequency) + offsetX;
            float yCoord = (y / (float)height * scale * frequency) + offsetY;

            float perlinValue = Mathf.PerlinNoise(xCoord, yCoord);

            noiseHeight += perlinValue * amplitude;

            amplitude *= persistence;  // amplitude decreases
            frequency *= lacunarity;   // frequency increases
        }

        return noiseHeight;
    }

}