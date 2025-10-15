using System;
using UnityEngine;

public class RandomNoiseMovement : MonoBehaviour
{
    [Flags]
    public enum NoiseType
    {
        None = 0,
        Position = 1 << 0,
        Rotation = 1 << 1,
        Scale = 1 << 2
    }

    [SerializeField] private NoiseType noiseType;
    [SerializeField] private Vector2 speed = Vector2.one;
    [SerializeField] private Vector2 amplitude = Vector2.one;

    private Vector2 random = Vector2.zero;
    private Vector2 position;
    private Vector2 positionSeed;

    private float rotation;
    private float rotationSeed;

    private Vector2 scale;
    private Vector2 scaleSeed;

    private void Awake()
    {
        position = transform.localPosition;
        positionSeed = new Vector2(GetRandom(), GetRandom());

        rotation = transform.localEulerAngles.z;
        rotationSeed = GetRandom();

        scale = transform.localScale;
        scaleSeed = new Vector2(GetRandom(), GetRandom());
    }

    private float GetRandom()
    {
        return UnityEngine.Random.Range(-10000, 10000);
    }

    private void Update()
    {
        if (noiseType == NoiseType.None)
            return;

        if ((noiseType & NoiseType.Position) == NoiseType.Position)
        {
            random.x = GetNoise(true, positionSeed.x);
            random.y = GetNoise(false, positionSeed.y);

            transform.localPosition = new Vector3(position.x + random.x, position.y + random.y, transform.localPosition.z);
        }
        if ((noiseType & NoiseType.Rotation) == NoiseType.Rotation)
        {
            random.x = GetNoise(true, rotationSeed);

            transform.localEulerAngles = new Vector3(0, 0, rotation + random.x);
        }
        if ((noiseType & NoiseType.Scale) == NoiseType.Scale)
        {
            random.x = GetNoise(true, scaleSeed.x);
            random.y = GetNoise(false, scaleSeed.y);

            transform.localScale = new Vector3(scale.x + random.x, scale.y + random.y, transform.localScale.z);
        }
    }

    private float GetNoise(bool up, float seed)
    {
        if (up)
        {
            return ((Mathf.PerlinNoise(Time.time * speed.x, seed) * 2) - 1) * amplitude.x;
        }
        else
        {
            return ((Mathf.PerlinNoise(-Time.time * speed.y, seed) * 2) - 1) * amplitude.y;
        }
    }
}