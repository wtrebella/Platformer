using UnityEngine;
using System.Collections;

public static class WTUtils {
	public static Vector2 screenCenter {get; private set;}
	public static PhysicMaterial defaultPhysicMaterial {get; private set;}
	public static float defaultRigidBodyAngularDrag {get; private set;}
	public static float defaultRigidBodyMass {get; private set;}
	public static float defaultRigidBodyMaxAngularVelocity = 10000;
	public static void Init() {
		screenCenter = new Vector2(Futile.screen.halfWidth, Futile.screen.halfHeight);

		defaultPhysicMaterial = new PhysicMaterial();
		defaultPhysicMaterial.bounciness = 0.9f;
		defaultPhysicMaterial.dynamicFriction = 0.1f;
		defaultPhysicMaterial.staticFriction = 0.1f;
		defaultPhysicMaterial.frictionCombine = PhysicMaterialCombine.Maximum;

		defaultRigidBodyAngularDrag = 1.0f;
		defaultRigidBodyMass = 10.0f;
	}

	public static WTPhysicsNode physicsNodeAttachedToGameObject(GameObject gameObject) {
		return gameObject.GetComponent<WTPhysicsComponent>().container;
	}

	public static Vector2 GetLocalTopLeftRectPoint(Rect rect) {
		return new Vector2(rect.xMin, rect.yMax);
	}

	public static Vector2 GetLocalTopRightRectPoint(Rect rect) {
		return new Vector2(rect.xMax, rect.yMax);
	}

	public static Vector2 GetLocalBottomLeftRectPoint(Rect rect) {
		return new Vector2(rect.xMin, rect.yMin);
	}

	public static Vector2 GetLocalBottomRightRectPoint(Rect rect) {
		return new Vector2(rect.xMax, rect.yMax);
	}

	public static Vector2 GetGlobalTopLeftRectPoint(Rect rect, FNode node) {
		if (node.isOnStage) return node.LocalToGlobal(new Vector2(rect.xMin, rect.yMax));
		else return new Vector2(-1, -1);
	}

	public static Vector2 GetGlobalTopRightRectPoint(Rect rect, FNode node) {
		if (node.isOnStage) return node.LocalToGlobal(new Vector2(rect.xMax, rect.yMax));
		else return new Vector2(-1, -1);
	}

	public static Vector2 GetGlobalBottomLeftRectPoint(Rect rect, FNode node) {
		if (node.isOnStage) return node.LocalToGlobal(new Vector2(rect.xMin, rect.yMin));
		else return new Vector2(-1, -1);
	}

	public static Vector2 GetGlobalBottomRightRectPoint(Rect rect, FNode node) {
		if (node.isOnStage) return node.LocalToGlobal(new Vector2(rect.xMax, rect.yMin));
		else return new Vector2(-1, -1);
	}
}
