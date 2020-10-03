using Yube;

public class Rock : MovingEnemy
{
	protected override void OnShieldContact()
	{
		ResourceManager.Instance.ReleaseInstance(this);
	}
}