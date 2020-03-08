using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRenderer : MonoBehaviour {
	#region Properties
	[SerializeField] private SpriteRenderer _targetRenderer = null;
	[SerializeField] private Camera _sourceRenderCamera = null;
	#endregion

	#region Constants
	private static readonly int mainTexID = Shader.PropertyToID("_MainTex");
	#endregion

	#region Callbacks
	private void Awake() {
		Texture2D spriteTex = new Texture2D(_sourceRenderCamera.targetTexture.width, _sourceRenderCamera.targetTexture.height,
			TextureFormat.ARGB32, false);
		_targetRenderer.sprite = Sprite.Create(spriteTex,
			new Rect(0f, 0f, _sourceRenderCamera.targetTexture.width, _sourceRenderCamera.targetTexture.height), 
			new Vector2(0.5f, 0.5f), 100);
	}

	private void Update() {
		Render();		
	}
	#endregion

	#region Rendering
	private void Render() {
		if (_sourceRenderCamera.activeTexture == null) return;
		MaterialPropertyBlock prop = new MaterialPropertyBlock();
		prop.SetTexture(mainTexID, _sourceRenderCamera.activeTexture);
		_targetRenderer.SetPropertyBlock(prop);
	}
	#endregion
}
