using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
/// <summary>
/// Every asset that creates a sound must have this script as component
/// Ensure that all your sounds MUST BE IN Assets/Resources/Audio/*your Sound Folder/*sound
/// </summary>
public class AudioController : MonoBehaviour
{
    /// <summary>
    /// The folder within the Resources folder (if applicable)
    /// </summary>
    [SerializeField] private string _resourceLocation;

    private AudioSource _myAudioSource;
    // private List<VOCollection> _voCollection;
    private List<SFXCollection> _sfxCollection;

    public AudioSource audioSource { get { return _myAudioSource; } }

    private void Start() {
        _myAudioSource = GetComponent<AudioSource>();
    }

    #region RESOURCE_SEARCH
    private AudioClip SearchResource(SFXCollection sfx) {
        string location = "Audio/" + _resourceLocation + "/" + audioToString(sfx);
        return Resources.Load<AudioClip>(location);
    }

    string audioToString(SFXCollection s) {
        return s.ToString().ToLower();
    }
    #endregion

    #region STANDARD_SOUND_PLAYERS
    /// <summary>
    /// Play One Shot of the SFX
    /// </summary>
    /// <param name="soundName">Name of sound file</param>
    public void PlaySoundEffect(SFXCollection sfx) {
        _myAudioSource.PlayOneShot(SearchResource(sfx));
    }

    /// <summary>
    /// Stops previous audio, and plays this one
    /// </summary>
    /// <param name="soundName">Name of sound file</param>
    public void PlayAudioStopPrev(SFXCollection sfx) {
        AudioPlayerFullStop();
        _myAudioSource.PlayOneShot(SearchResource(sfx));
    }

    /// <summary>
    /// Takes 2 sounds, plays sound 1 first. When 1st sound is finished, plays the second.
    /// </summary>
    /// <param name="v1">Sound 1</param>
    /// <param name="v2">Sound 2</param>
    //public void PlayAudioStopPrev(VOCollection v1, VOCollection v2) { // Note: Replace VOCollection with another SoundCollection
    //    var sound1 = SearchResource(v1);
    //    var sound2 = SearchResource(v2);
    //    AudioPlayerFullStop();
    //    _myAudioSource.PlayOneShot(sound1);
    //    StartCoroutine(PlaySecondSound(sound2, sound1.length));

    //}

    IEnumerator PlaySecondSound(AudioClip sound2, float time1) {
        yield return new WaitForSeconds(time1);
        _myAudioSource.Stop();
        _myAudioSource.PlayOneShot(sound2);
    }
    #endregion

    #region PLAYLIST_PLAYERS
    /// <summary>
    /// Initializes the playlist to empty
    /// </summary>
    public void InitializePlaylist() {
        _sfxCollection = new List<SFXCollection>();
    }

    ///// <summary>
    ///// Adds an item to the Playlist
    ///// </summary>
    ///// <param name="vOs"></param>
    public void AddPlaylistQueue(SFXCollection sfx) {
        StopAllCoroutines();
        _sfxCollection.Add(sfx);
    }

    /// <summary>
    /// Plays all the current items in the Playlist, when it's finished the Playlist is reinitialized;
    /// </summary>
    public void PlaylistStart() {
        AudioPlayerFullStop();
        StartCoroutine(PlaylistPlayer());
    }

    IEnumerator PlaylistPlayer() {
        for (int i = 0; i < _sfxCollection.Count; i++) {
            var sound = SearchResource(_sfxCollection[i]);
            _myAudioSource.Stop();
            _myAudioSource.PlayOneShot(sound);
            yield return new WaitForSeconds(sound.length);
            continue;
        }

        InitializePlaylist();
    }
    #endregion

    void AudioPlayerFullStop() {
        _myAudioSource.Stop();
        StopAllCoroutines();
    }




}
