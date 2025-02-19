using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public float Speed;
    public Transform Background;
    private float leftLimit;
    private float rightLimit;

    void Start() {
        SpriteRenderer bgRenderer = Background.GetComponent<SpriteRenderer>();

        float halfWidth = bgRenderer.bounds.size.x / 2;
        leftLimit = Background.position.x - halfWidth + (transform.localScale.x / 4);
        rightLimit = Background.position.x + halfWidth - (transform.localScale.x / 4);
    }
    void Update() {
        Move();
    }
    
    void Move() {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            transform.position.y,
            transform.position.z
        );
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Collectible") {
            Destroy(collision.gameObject);

            string collectible = collision.gameObject.GetComponent<TextMeshPro>().text;

            if (char.IsDigit(collectible[0])) {
                GameController.instance.GameOver();
            } else {
                List<GameObject> alphabetList = Spawn.instance.alphabet.ToList();

                string firstLetterText = alphabetList[0].GetComponent<TextMeshPro>().text;

                if (collectible == firstLetterText) {
                    alphabetList.RemoveAt(0);
                    GameController.instance.UpdateAlphabetUI(collectible);
                    GameController.instance.AddScore();
                    GameController.instance.UpdateScore();

                    if (alphabetList.Count == 0) {
                        GameController.instance.GameEnd();
                    }
                    Spawn.instance.alphabet = alphabetList.ToArray();
                } else {
                    GameController.instance.GameOver();
                }
            }
        }
    }
}
