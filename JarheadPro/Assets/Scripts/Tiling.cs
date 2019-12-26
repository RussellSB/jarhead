using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling : MonoBehaviour
{
    //float cameraTipX;
    //float tileTipX;
    float offsetX = 2;
    public bool hasARightTile = false;
    public bool hasALeftTile = false;
    public bool reverseScale = false; // used if object isnt tileable

    private float spriteWidth = 0f;
    private Camera cam;

    public float pos;

    private void Awake()
    {
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        //cameraTipX = Camera.main.transform.position.x + Camera.main.pixelWidth / 2;
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // Does it still need buddies? If not do nothing
        if (!hasALeftTile || !hasARightTile)
        {
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height; // half width of camera view

            float edgeVisiblePositionRight = (transform.position.x + spriteWidth / 4) - (camHorizontalExtend * 4); // x pos where cam can see sprite edge
            float edgeVisiblePositionLeft = (transform.position.x - spriteWidth / 4) + (camHorizontalExtend * 4) ; // x pos where cam can see sprite edge

            // Checking if we can see the edge of the element and calling MakeNewTile if we can
            if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && !hasARightTile)
            {
                createTile(1);
                hasARightTile = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePositionRight + offsetX && !hasALeftTile)
            {
                createTile(-1);
                hasALeftTile = true;
            }
        }

        pos = cam.transform.position.x - transform.position.x;
        if (pos > 80) gameObject.SetActive(false);
    }

    void createTile(int rightOrLeft)
    {
        // Calculating the new position for our new Tile
        Vector3 newPosition = new Vector3(transform.position.x + (spriteWidth/4 - 6) * rightOrLeft, transform.position.y, transform.position.z);
        Transform newTile = Instantiate(transform, newPosition, transform.rotation) as Transform; //like (Transform)

        // If not tileable let's reverse the x size of our object to get rid of ugly scenes c:
        if (reverseScale)
        {
            newTile.localScale = new Vector3(newTile.localScale.x * -1, newTile.localScale.y, newTile.localScale.z);}

        newTile.parent = transform.parent;
        if (rightOrLeft > 0)
        {
            newTile.GetComponent<Tiling>().hasALeftTile = true;
        }
        else
        {
            newTile.GetComponent<Tiling>().hasARightTile = true;
        }
    }
}
