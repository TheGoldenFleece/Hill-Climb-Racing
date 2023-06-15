using System.Drawing;
using UnityEngine;
using UnityEngine.U2D;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] SpriteShapeController spriteShapeController;
    [SerializeField] float xMultiplier = 2f;
    [SerializeField] float yMultiplier = 2f;
    [SerializeField] int spawnLength = 50;
    [SerializeField] float noiseStep = .5f;
    [SerializeField] float depth = 10f;
    [SerializeField] GameObject FuelPrefab;
    [SerializeField] Transform Vehicle;

    Vector3 position;

    int offset;

    float centerX;
    float previousDistance;

    private void Start()
    {
        centerX = spawnLength * xMultiplier / 2 + transform.position.x;
        Debug.Log(centerX);

        GenerateGround();
        offset = (int)(spawnLength * xMultiplier * 0.1);
    }

    private void Update()
    {
        if (Vehicle.transform.position.x > centerX)
        {
            AddGround();
            Debug.Log(centerX);

            centerX += offset * xMultiplier;
        }
    }

    void GenerateRandomFuel()
    {
        int chance = Random.Range(0, (int)(spawnLength * xMultiplier / (spawnLength / 6)));

        if (chance != 0) return;

        float randomY = Random.Range(.1f, 1.5f);

        Vector3 spawnPosition = spriteShapeController.transform.TransformPoint(position);
        GameObject fuel = Instantiate(FuelPrefab, new Vector3(spawnPosition.x, spawnPosition.y + randomY), Quaternion.identity);
    }

    public void AddGround()
    {
        int startPoint = spawnLength + 1;

        //remove two last points
        for (int i = 0; i < 2; i++)
        {
            spriteShapeController.spline.RemovePointAt(spriteShapeController.spline.GetPointCount() - 1);
        }

        for (int point = startPoint; point < startPoint + offset; point++)
        {
            float y = Mathf.PerlinNoise(0, point * noiseStep);

            position = new Vector3(spriteShapeController.spline.GetPosition(point - 1).x, transform.position.y) +
                       new Vector3(xMultiplier, y * yMultiplier);
            spriteShapeController.spline.InsertPointAt(point, position);

            GenerateRandomFuel();
        }

        float bottomY = transform.position.y - depth;

        //right bottom point
        int rightBottomPoint = spriteShapeController.spline.GetPointCount();
        position = new Vector3(position.x, bottomY);
        spriteShapeController.spline.InsertPointAt(rightBottomPoint, position);

        //left bottom point
        int leftBottomPoint = spriteShapeController.spline.GetPointCount();
        position = new Vector3(spriteShapeController.spline.GetPosition(offset).x, bottomY);
        spriteShapeController.spline.InsertPointAt(leftBottomPoint, position);

        //delete first offset points
        int index = 0;
        while (index < offset)
        {
            spriteShapeController.spline.RemovePointAt(0);
            index++;
        }
    }

    void GenerateGround()
    {
        //remove previous
        spriteShapeController.spline.Clear();

        int point = 0;

        //first points should be smooth

        for (; point < 5; point++)
        {
            position = transform.position + new Vector3(point * xMultiplier, 0);
            spriteShapeController.spline.InsertPointAt(point, position);
        }
        

        for (; point <= spawnLength; point++)
        {
            float y = Mathf.PerlinNoise(0, point * noiseStep);
            position = transform.position +
                       new Vector3(point * xMultiplier, y * yMultiplier);
            spriteShapeController.spline.InsertPointAt(point, position);

            GenerateRandomFuel();
        }

        //right bottom point
        float bottomY = transform.position.y - depth;

        int rightBottomPoint = spriteShapeController.spline.GetPointCount();
        spriteShapeController.spline.InsertPointAt(rightBottomPoint, new Vector3(position.x, bottomY));

        //left bottom point
        int leftBottomPoint = spriteShapeController.spline.GetPointCount();
        spriteShapeController.spline.InsertPointAt(leftBottomPoint, new Vector3(transform.position.x, bottomY));
    }

}

