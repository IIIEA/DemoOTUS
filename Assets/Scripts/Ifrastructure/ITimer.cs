using System;

namespace Ifrastructure
{
  public interface ITimer
  {
    public event Action<int> OnCountdownStart;
    public event Action<int> OnCountdown;
    public event Action OnCountdownEnd;
  }
}