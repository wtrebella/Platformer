using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WTMain : MonoBehaviour {
	WTScene currentScene;
	WTHandler handler;

	void Start() {
		Go.defaultEaseType = EaseType.SineInOut;

		FutileParams fp = new FutileParams(true, true, false, false);
		fp.AddResolutionLevel(480f, 1.0f, 1.0f, "-res1");
		fp.AddResolutionLevel(1136f, 2.0f, 2.0f, "-res2");
		fp.AddResolutionLevel(2048f, 4.0f, 4.0f, "-res4");
		
		fp.backgroundColor = Color.white;
		fp.origin = Vector2.zero;

		Futile.instance.Init(fp);

		Futile.atlasManager.LoadAtlas("Atlases/MainSheet");
		Futile.atlasManager.LoadFont("franchise", "franchise", "Atlases/franchise", -7, -16);
		// futile done initing

		WTUtils.Init();

		handler = new WTHandler();
		handler.SignalUpdate += HandleUpdate;
		handler.SignalLateUpdate += HandleLateUpdate;
		handler.SignalFixedUpdate += HandleFixedUpdate;
		handler.SignalMultiTouch += HandleMultiTouch;

		Futile.stage.AddChild(handler);

		FPWorld.Create(64.0f);
	
		currentScene = new WTMainScene();
		Futile.stage.AddChild(currentScene);
	}

	void Update() {

	}

	public void HandleUpdate() {
		currentScene.HandleUpdate();
	}

	public void HandleLateUpdate() {
		currentScene.HandleLateUpdate();
	}

	public void HandleFixedUpdate() {
		currentScene.HandleFixedUpdate();
	}

	public void HandleMultiTouch(FTouch[] touches) {
		currentScene.HandleMultiTouch(touches);
	}
}