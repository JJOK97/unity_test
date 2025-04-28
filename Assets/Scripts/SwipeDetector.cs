using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    public LayerMask slimeLayerMask;
    public float swipeRadius = 1.0f;
    private Vector2 lastTouchPosition;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTouchPosition = Input.mousePosition;

            if (lastTouchPosition == Vector2.zero)
            {
                lastTouchPosition = currentTouchPosition;
            }

            ProcessSwipe(currentTouchPosition);
            lastTouchPosition = currentTouchPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTouchPosition = Vector2.zero;
        }
    }

    void ProcessSwipe(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // y=0 평면

        float distance;
        if (groundPlane.Raycast(ray, out distance))
        {
            Vector3 worldPos = ray.origin + ray.direction * distance;

            Collider[] hits = Physics.OverlapSphere(worldPos, swipeRadius, slimeLayerMask);

            foreach (Collider hit in hits)
            {
                if (hit != null)
                {
                    Debug.Log($"[HIT] {hit.GetComponent<Collider>().gameObject.name}");
                    Destroy(hit.GetComponent<Collider>().gameObject);
                    GameManager.Instance.AddTrashCleaned(1);
                }
            }
        }
        else
        {
            Debug.Log("[MISS] 레이가 지면과 교차하지 않았습니다.");
        }
    }

}
