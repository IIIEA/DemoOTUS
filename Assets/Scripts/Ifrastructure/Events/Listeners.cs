namespace Ifrastructure.Events
{
  public interface IGameStartListener : IGameListener
  {
    void OnStart();
  }

  public interface IGamePauseListener : IGameListener
  {
    void OnPause();
  }

  public interface IGameResumeListener : IGameListener
  {
    void OnResume();
  }

  public interface IGameFinishListener : IGameListener
  {
    void OnFinish();
  }

  public interface IGameUpdateListener : IGameListener
  {
    void OnUpdate(float deltaTime);
  }

  public interface IGameFixedUpdateListener : IGameListener
  {
    void OnFixedUpdate(float fixedDeltaTime);
  }

  public interface IGameListener
  {
  }
}