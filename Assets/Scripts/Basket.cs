using System.Collections.Generic;
using System.Linq;
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
                GameController.instance.GameOver();
            } else {
                List<GameObject> alphabetList = Spawn.instance.alphabet.ToList();

                string firstLetterText = alphabetList[0].GetComponent<TextMeshPro>().text;

                if (collectible == firstLetterText) {
                    alphabetList.RemoveAt(0);
                    if (alphabetList.Count == 0) {
                        //TODO TELA DE WIN
                        GameController.instance.GameOver();
                    }
                    Spawn.instance.alphabet = alphabetList.ToArray();
                } else {
                    GameController.instance.GameOver();
                }
            }
        }
    }
}
