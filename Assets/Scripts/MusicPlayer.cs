using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    private static MusicPlayer instance;
    private void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
