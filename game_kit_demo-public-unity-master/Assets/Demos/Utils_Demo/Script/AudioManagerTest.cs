using UnityEngine;
using System.Collections;

public class AudioManagerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	


	public void PlayButtonClose(){
		AudioManager.Instance.PlaySoundEffect("Audio/button_close");
	}

	public void PlayBgMusic(){
		AudioManager.Instance.PlayBgMusic("Audio/clockwork-horror");
	}
	public void StopBgMusic(){
		AudioManager.Instance.StopBgMusic();
	}

	public void PlayMagic(){
		AudioManager.Instance.PlayMusic("Audio/MagicWand",1f,false,false);
	}
	public void StopMagic(){
		AudioManager.Instance.StopMusicByPath("Audio/MagicWand");
	}

	public void StopAllMusic(){
		AudioManager.Instance.StopAll(true);
	}

	//会被ResManager缓存
	public void PlaySoundEffectFromStreamingAssets(){
		ResManager.Instance.LoadAsset(new ResManager.Asset("Audio/MagicWand.mp3",ResManager.AssetType.AudioClip),delegate(ResManager.Asset asset) {
			if(asset.audioClip) AudioManager.Instance.PlaySoundEffect(asset.audioClip);
		});
	}

	//不会缓存
	public void PlaySoundEffectFromStreamingAssets2(){
		AudioManager.Instance.loadPath = AudioManager.LoadPath.StreamingAssets;
		AudioManager.Instance.PlaySoundEffect("Audio/MagicWand.mp3");
		AudioManager.Instance.loadPath = AudioManager.LoadPath.Resources;
	}
}
