using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
    }

    // Función para atacar
    private void Attack()
    {
        GameObject newAttack = Instantiate(attackObject, transform.position, transform.rotation);
        Destroy(newAttack, 2f);
    }
}
