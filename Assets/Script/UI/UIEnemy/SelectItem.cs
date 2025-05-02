using UnityEngine;

public class SelectItem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Information;
    GameObject infor;
    void Start()
    {
        infor = Instantiate(Information, transform.position + Vector3.right * 1.002f, Quaternion.identity);
        infor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouspos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouspos, Vector2.zero);
            if (hit.collider)
            {

                infor.SetActive(true);
            }
            else
            {
                infor.SetActive(false);
            }
        }
    }
}
