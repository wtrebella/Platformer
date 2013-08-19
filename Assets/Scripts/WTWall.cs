using UnityEngine;
using System.Collections;

// just a stupid simple rigid wall for things to bounce off of

public class WTWall : WTPhysicsNode {
	public FSprite sprite;

	public WTWall(float width, float height) : base("wall") {
		sprite = new FSprite("whiteSquare");
		sprite.width = width;
		sprite.height = height;
		sprite.color = new Color(0.1f, 0.1f, 0.1f);
		sprite.scale = 0.95f;
		AddChild(sprite);

		physicsComponent.AddBoxCollider(width, height);
		physicsComponent.SetupPhysicMaterial(1.0f, 0.1f, 0.1f);
		SetPosition(WTUtils.screenCenter);
	}
}
