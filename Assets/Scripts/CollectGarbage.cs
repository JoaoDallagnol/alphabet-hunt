using TMPro;
using UnityEngine;

public class CollectGarbage : MonoBehaviour {

    private TextMeshPro collectableTextValue;

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Collectible") {
            Debug.Log(collision.gameObject.GetComponent<TextMeshPro>().text);
            Destroy(collision.gameObject);
        }
    }
}
