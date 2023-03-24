using UnityEngine;

public class Respawn : MonoBehaviour
{
  // original position and rotation
  private Vector3 originalPosition;
  private Quaternion originalRotation;
  [SerializeField] private float respawnDelay = 1f;
  [SerializeField] private float respawnY = 0.3f;
  [SerializeField] private GameObject objPrefab;
  private ParticleSystem particles;
  bool isRespawning = false;
  [SerializeField] private GameObject resetObj;
  private Transform resetTransform;

  // Start is called before the first frame update
  void Start()
  {
    originalPosition = transform.position;
    originalRotation = transform.rotation;
    particles = gameObject.GetComponentInChildren<ParticleSystem>();
    particles.Stop();

    resetTransform = resetObj.GetComponent<Transform>();
  }

  // Update is called once per frame
  void Update()
  {
    if (transform.position.y < respawnY && !isRespawning)
    {
      isRespawning = true;
      particles.Play();
      Invoke("RespawnObject", respawnDelay);
    }
  }

  void RespawnObject()
  {
    transform.position = originalPosition;
    transform.rotation = originalRotation;
    GetComponent<Rigidbody>().velocity = Vector3.zero;
    GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    particles.Stop();

    if (resetObj != null)
    {
      resetObj.transform.position = resetTransform.position;
    }
    isRespawning = false;
  }
}
