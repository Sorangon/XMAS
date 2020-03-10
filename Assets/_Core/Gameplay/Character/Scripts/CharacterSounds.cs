using UnityEngine;

[RequireComponent(typeof(CharacterInputs))]
[RequireComponent(typeof(CharacterCollisions))]
public class CharacterSounds : MonoBehaviour
{
    private CharacterInputs characterInputs;
    private CharacterCollisions characterCollisions;

    [SerializeField] private AudioClip[] dashSoundList = null;
    [SerializeField] private AudioClip[] chargeDashSoundList = null;
    [SerializeField] private AudioClip[] hitRockSoundList = null;
    [SerializeField] private AudioClip[] hitTreeSoundList = null;
    [SerializeField] private AudioClip[] hitBorderSoundList = null;

    private void Awake()
    {
        characterInputs = GetComponent<CharacterInputs>();
        characterCollisions = GetComponent<CharacterCollisions>();
    }

    private void Start()
    {
        characterInputs.OnThrow += PlayDashSound;
        characterCollisions.OnHitRock += PlayHitRockSound;
        characterCollisions.OnHitTree += PlayHitTreeSound;
        characterCollisions.OnHitBorder += PlayHitBorderSound;
    }

    private void PlayPickedSoundInList(AudioClip[] _soundList) {
        if(_soundList != null && _soundList.Length > 0) {
            AudioClip pickedDashSound = _soundList[Random.Range(0, _soundList.Length)];
            AudioManager.Instance.PlaySound(pickedDashSound);
        }
    }

    private void PlayDashSound()
    {
        PlayPickedSoundInList(dashSoundList);
        Debug.Log("Play dash SFX");
    }

    private void PlayChargeDashSound()
    {
        PlayPickedSoundInList(chargeDashSoundList);
    }

    private void PlayHitRockSound(){
        PlayPickedSoundInList(hitRockSoundList);
    }

    private void PlayHitTreeSound(){
        PlayPickedSoundInList(hitTreeSoundList);
    }

    private void PlayHitBorderSound(){
        PlayPickedSoundInList(hitBorderSoundList);
    }
}
