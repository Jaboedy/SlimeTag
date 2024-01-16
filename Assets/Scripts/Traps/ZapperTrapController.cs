using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ZapperTrapController : MonoBehaviour
{
    private Animator zappTrappAnim;
    private float randomNum;
    // Start is called before the first frame update
    void Start()
    {
        zappTrappAnim = GetComponent<Animator>();
        StartCoroutine(SetUpLoop());
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private IEnumerator SetUpLoop()
    {
        while (gameObject == true)
        {
            randomNum = Random.Range(7f, 15f);
            yield return new WaitForSeconds(randomNum);
            zappTrappAnim.SetTrigger("indicate");
            yield return new WaitForSeconds(2f);
            zappTrappAnim.SetTrigger("shock");
        }
    }
}
