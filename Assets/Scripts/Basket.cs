using TMPro;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public float Speed;

    void Update() {
        Move();
    }
    
    void Move() {
        
        Vector3 movement = new(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Collectible") {
            Destroy(collision.gameObject);

            string collectible = collision.gameObject.GetComponent<TextMeshPro>().text;

            if (char.IsDigit(collectible[0])) {
                Debug.Log("É NUMERO ----");
            } else {
                Debug.Log("É Letra +++++");
            }
        }
    }
}
