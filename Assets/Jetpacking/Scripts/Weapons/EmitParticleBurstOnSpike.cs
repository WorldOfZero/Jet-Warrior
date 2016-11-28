using UnityEngine;
using System.Collections;

public class EmitParticleBurstOnSpike : MonoBehaviour {

    public ParticleSystem particles;
    public float emissionPerSecond = 100;

    public void OnSpikeCollision(RaycastHit hit)
    {
        particles.transform.position = hit.point;
        particles.Emit(Mathf.CeilToInt(emissionPerSecond * Time.deltaTime));
    }
}
