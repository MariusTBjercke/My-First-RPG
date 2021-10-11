using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelDestruction : MonoBehaviour
{

    public GameObject ps;
    public float timeBeforeDestroyParticles = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {

        //Debug.Log(collision.gameObject.tag);

        //if (collision.gameObject.tag == "Weapon" && collision.GetComponent<Animator>().GetBool("attack") == true)
        //{
        //    destroyBarrel();
        //}

    }

    public void destroyBarrel()
    {
        Vector3 findCenter = GetComponent<Renderer>().bounds.center;

        GameObject particle = Instantiate(ps, findCenter, ps.transform.rotation);
        var expl = particle.GetComponent<ParticleSystem>();

        var emission = expl.emission;
        emission.rateOverTime = 0;

        emission.SetBursts(
            new ParticleSystem.Burst[]
            {
                new ParticleSystem.Burst(0.5f, 15),
            });

        Destroy(gameObject, 0.5f);
        Destroy(particle, timeBeforeDestroyParticles);
    }

}
