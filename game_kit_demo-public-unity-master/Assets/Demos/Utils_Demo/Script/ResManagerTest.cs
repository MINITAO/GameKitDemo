using UnityEngine;
using System.Collections;

public class ResManagerTest : MonoBehaviour {

	public bool isEncryption = false;

	private SpriteRenderer sr = null;
	private string m_Folder="";
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		if(isEncryption){
			m_Folder = "Encryption/";
		}

		//加载图集
		ResManager.Instance.LoadAsset(
			new ResManager.Asset(m_Folder+"sprites.xml",ResManager.AssetType.Sprites)
			,delegate(ResManager.Asset asset) {
				print(asset.url+" Loaded");
				//获取图集中的小图
				sr.sprite  = asset.sprites["a_body.png"];
				StartCoroutine(ChangeImg());
			}
		);


	}
	IEnumerator ChangeImg(){
		yield return new WaitForSeconds(2f);
		//换一张图集中的小图
		sr.sprite = ResManager.Instance.GetAsset(m_Folder+"sprites.xml").sprites["a_head.png"];

		yield return new WaitForSeconds(2f);
		//销毁图集
		ResManager.Instance.DisposeAsset(m_Folder+"sprites.xml");

		//加载一张单一的图片
		ResManager.Instance.LoadAsset(new ResManager.Asset(m_Folder+"storyboard.png",ResManager.AssetType.Sprite),
			delegate(ResManager.Asset obj) {
				print(obj.url+" Loaded");
				sr.sprite = obj.sprite;
			});

		yield return new WaitForSeconds(2f);

		//销毁所有
		ResManager.Instance.DisposeAll();
	}

}
