using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CollectGarbage : MonoBehaviour {

    private TextMeshPro collectableTextValue;

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Collectible") {
            Destroy(collision.gameObject);

            List<GameObject> alphabetList = Spawn.instance.alphabet.ToList();

            string firstLetterText = alphabetList[0].GetComponent<TextMeshPro>().text;

            if (collision.gameObject.GetComponent<TextMeshPro>().text == firstLetterText) {
               GameController.instance.DecreaseScore();
               GameController.instance.UpdateScore();
            }
        }
    }
}
