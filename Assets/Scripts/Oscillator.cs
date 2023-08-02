using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
  // Start is called before the first frame update
  Vector3 startingPosition;
  [SerializeField] private Vector3 movementVector;
  [SerializeField] private Vector3 rotationVector;
  private float movementFactor;
  [SerializeField] private float period = 2f;
  void Start()
  {
    startingPosition = transform.position;
  }

  // Update is called once per frame
  void Update()
  {

    const float tau = Mathf.PI * 2;
    float cycles = period <= Mathf.Epsilon ? 0 : Time.time / period;
    float rawSinWave = Mathf.Sin(cycles * tau);
    movementFactor = (rawSinWave + 1f) / 2f;

    // translate
    Vector3 offset = movementVector * movementFactor;
    transform.position = startingPosition + offset;

    // rotate
    Vector3 offsetAngle = rotationVector * movementFactor / 2;
    transform.Rotate(offsetAngle);
  }
}
