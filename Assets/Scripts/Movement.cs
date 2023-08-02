using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] private Rigidbody rb;
  [SerializeField] private float thrustValue = 15.0f;
  [SerializeField] private float rotationValue = 15.0f;
  [SerializeField] private AudioClip mainEngine;
  [SerializeField] private ParticleSystem boosterParticle;
  [SerializeField] private ParticleSystem boosterParticleRight;
  [SerializeField] private ParticleSystem boosterParticleLeft;
  private AudioSource audioSource;
  bool audioPlay = false;
  //   bool audioAlreadyPlaying = false;
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
    Debug.Log(audioSource);
  }

  // Update is called once per frame
  void Update()
  {
    ProcessThrust();
    ProcessRotation();
    PlayThrust();
  }

  void ProcessThrust()
  {
    if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
    {
      // Debug.Log("up");
      rb.AddRelativeForce(Vector3.up * thrustValue * Time.deltaTime, ForceMode.Impulse);
      audioPlay = true;
      if (!boosterParticle.IsAlive())
      {
        boosterParticle.Play();
      }

    }
    else
    {
      audioPlay = false;
      boosterParticle.Stop();
    }

  }

  void ProcessRotation()
  {
    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
    {
      // Debug.Log("left");
      ApplyRotation(rotationValue);
      if (!boosterParticleRight.IsAlive())
      {
        boosterParticleRight.Play();
      }


    }
    else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
    {
      // Debug.Log("rigth");
      ApplyRotation(-rotationValue);
      if (!boosterParticleLeft.IsAlive())
      {
        boosterParticleLeft.Play();
      }

    }
    else
    {
      boosterParticleLeft.Stop();
      boosterParticleRight.Stop();
    }
  }

  private void ApplyRotation(float rotationThisFrame)
  {
    rb.freezeRotation = true;
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
  }

  private void PlayThrust()
  {
    if (audioPlay == true && !audioSource.isPlaying)
    {
      audioSource.PlayOneShot(mainEngine);
    }
    else if (audioPlay == false)
    {
      audioSource.Stop();
    }
  }


}
