using UnityEngine;

public interface ISoundEmitter
{
    public AudioClip AudioClip { get; set; }
    public void PlaySound(AudioClip clip, Transform position)
    {
        ServiceLocator.Instance.GetService<EPSoundsManager>().PlayClip(clip, position);
    }
}