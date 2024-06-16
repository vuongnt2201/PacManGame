using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Teleporter : Singleton<Teleporter>
{
    public ParticleSystem teleporterEnterSmokePuff;
    public ParticleSystem teleporterExitSmokePuff;
    public Light teleporterEnterEffectLight;
    public Light teleporterExitEffectLight;
    public float lightIntensity;
    public float lightSwitchTweenSpeed;
    public float lightFlickerEffectDuration;
    public Transform teleporterExit;
    public float timeItTakesToTeleport;
    private bool lightIsOn;
    void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Pacman"))
            HandleEnterTeleporter(collision);
    }

    private void HandleEnterTeleporter(Collider collision)
    {
        collision.attachedRigidbody.velocity = Vector3.zero;
        PlayTeleportEffects(teleporterEnterSmokePuff, teleporterEnterEffectLight);
        collision.gameObject.SetActive(false);
        StartCoroutine(TeleportObject(collision));
    }

    private void PlayTeleportEffects(ParticleSystem teleporterSmokePuff, Light teleporterLightEffect)
    {
        teleporterSmokePuff.Play();
        teleporterLightEffect.DOIntensity(lightIntensity, lightSwitchTweenSpeed).SetEase(Ease.InBounce).OnComplete(() => {
                lightIsOn = true;
                StartCoroutine(FlickerLight(teleporterLightEffect));
                StartCoroutine(TurnOffLightAfterFlicker(teleporterLightEffect));
        });
    }

    IEnumerator TeleportObject(Collider collision)
    {
        if(teleporterExit == null) 
            yield return null;
        yield return new WaitForSeconds(timeItTakesToTeleport);
        if(collision.CompareTag("Pacman"))
        {
            PacMan _character = collision.gameObject.GetComponent<PacMan>();
            _character.transform.position = teleporterExit.position;
            _character.ResetMoving();
            collision.gameObject.SetActive(true);
            PlayTeleportEffects(teleporterExitSmokePuff, teleporterExitEffectLight);
        }   
    }

    IEnumerator FlickerLight(Light teleporterLightEffect)
    {
        Vector3 shakeVector3 = new Vector3();
        DOTween.Shake(() => shakeVector3,
            x => shakeVector3 = x,
            lightFlickerEffectDuration, 7, 3, 5);
        while(lightIsOn)
        {
            teleporterLightEffect.intensity = shakeVector3.x;
            yield return null;
        }
    }

    IEnumerator TurnOffLightAfterFlicker(Light teleporterLightEffect)
    {
        yield return new WaitForSeconds(lightFlickerEffectDuration);
        lightIsOn = false;
        teleporterLightEffect.DOIntensity(0, lightSwitchTweenSpeed).SetEase(Ease.OutBounce);
    }
}
