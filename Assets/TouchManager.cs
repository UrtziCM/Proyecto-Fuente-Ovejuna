using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float originalColliderSizeX;
    [SerializeField] float originalColliderSizeY;
    private Camera myCamera;
    private RaycastHit2D hit;
    private Touch touch;
    
    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
       if(Input.touchCount > 0)
       {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null) {
                touch = Input.GetTouch(0);
                if (hit.collider.gameObject.tag.Equals("puzzlePiece"))
                {
                    moveEspecificPiece(hit.collider.gameObject);
                }
            }
       }
    }

    private void moveEspecificPiece(GameObject piece)
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
        touchPosition.z = 0f;
        piece.transform.position = touchPosition;
        if(touch.phase == TouchPhase.Began)
        {
            ((BoxCollider2D)hit.collider).size = ((BoxCollider2D)hit.collider).size * 2;
        }
        if(touch.phase == TouchPhase.Ended)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
            touchPosition.z = 0f;
            piece.transform.position = touchPosition;
            ((BoxCollider2D)hit.collider).size = new Vector2(originalColliderSizeX, originalColliderSizeY);
        }
    }
}
