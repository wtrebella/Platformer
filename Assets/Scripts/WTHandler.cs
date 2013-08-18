using UnityEngine;
using System.Collections;
using System;

public class WTHandler : FNode, FMultiTouchableInterface {
	public event Action SignalUpdate;
	public event Action<FTouch[]> SignalMultiTouch;

	public bool isPaused = false;

	public WTHandler() {
		EnableMultiTouch();
		ListenForUpdate(HandleUpdate);
	}

	public void HandleMultiTouch(FTouch[] touches) {
		if (isPaused) return;

		if (SignalMultiTouch != null) SignalMultiTouch(touches);
	}

	public void HandleUpdate() {
		if (isPaused) return;

		if (SignalUpdate != null) SignalUpdate();
	}
}
