using Yube;

public class Rock : MovingEnemy
{
	protected override void OnMapBorderReached()
	{
		ResourceManager.Instance.ReleaseInstance(this);
	}

	protected override void OnShieldContact()
	{
		ResourceManager.Instance.ReleaseInstance(this);
	}
}