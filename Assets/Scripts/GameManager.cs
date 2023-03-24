using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  [SerializeField] GameObject cubePrefab;
  [SerializeField] GameObject spherePrefab;
  [SerializeField] GameObject capsulePrefab;
  [SerializeField] GameObject cylinderPrefab;
  [SerializeField] Button spawnCubeButton;
  [SerializeField] Button spawnSphereButton;
  [SerializeField] Button spawnCapsuleButton;
  [SerializeField] Button spawnCylinderButton;

  void SpawnObject(string objectName)
  {
    float randomX = Random.Range(-3f, 3f);
    float randomY = Random.Range(1f, 3f);
    float randomZ = Random.Range(0f, 3f);

    switch (objectName)
    {
      case "Cube":
        Instantiate(cubePrefab, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
        break;
      case "Sphere":
        Instantiate(spherePrefab, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
        break;
      case "Capsule":
        Instantiate(capsulePrefab, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
        break;
      case "Cylinder":
        Instantiate(cylinderPrefab, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
        break;
    }
  }

  // Start is called before the first frame update
  void Start()
  {
    spawnCubeButton.onClick.AddListener(() => SpawnObject("Cube"));
    spawnSphereButton.onClick.AddListener(() => SpawnObject("Sphere"));
    spawnCapsuleButton.onClick.AddListener(() => SpawnObject("Capsule"));
    spawnCylinderButton.onClick.AddListener(() => SpawnObject("Cylinder"));
  }

  // Update is called once per frame
  void Update()
  {

  }
}
