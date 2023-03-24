using UnityEngine;

public class PanelObject : MonoBehaviour
{
    [SerializeField] bool checkmouse = true;
    [SerializeField] Vector2 size;

    public float GetX()
    {
        return size.x;
    }
    public float GetY()
    {
        return size.y;
    }

    public void SetMouse(bool b) { checkmouse = b; }

    private void OnMouseDown()
    {
        if (checkmouse) RoofManager.instance.SetObject(gameObject.transform.parent.gameObject, size.x, size.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Zone") Destroy(gameObject);
        PriceCalculator.instance.ListViewer();
    }
}
