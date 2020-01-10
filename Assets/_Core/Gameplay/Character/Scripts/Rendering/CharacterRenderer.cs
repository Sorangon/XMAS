using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRenderer : MonoBehaviour {
	#region Properties
	[SerializeField] private SpriteRenderer _targetRenderer = null;
	[SerializeField] private Camera _sourceRenderCamera = null;
	#endregion

	#region Currents
	private Texture2D _textureCache = null;
	#endregion

	#region Callbacks
	private void Awake() {
		Texture2D spriteTex = new Texture2D(_sourceRenderCamera.targetTexture.width, _sourceRenderCamera.targetTexture.height,
			TextureFormat.ARGB32, false);
		_targetRenderer.sprite = Sprite.Create(spriteTex,
			new Rect(0f, 0f, _sourceRenderCamera.targetTexture.width, _sourceRenderCamera.targetTexture.height), 
			new Vector2(0.5f, 0.5f));
	}

	private void Update() {
		Render();		
	}
	#endregion

	#region Rendering
	private void Render() {
		_targetRenderer.material.SetTexture("_MainTex", _sourceRenderCamera.activeTexture);
		//Vector2Int rtDim = new Vector2Int(_sourceRenderCamera.targetTexture.width, _sourceRenderCamera.targetTexture.height);

		//_textureCache.ReadPixels(new Rect(0f, 0f, rtDim.x, rtDim.y), 0, 0);
		//RenderTexture.active = null;

		//Sprite finalSprite = Sprite.Create(_textureCache, new Rect(0f, 0f, rtDim.x, rtDim.y), new Vector2(0.5f, 0.5f));
		//_targetRenderer.sprite = finalSprite;
	}
	#endregion
}
