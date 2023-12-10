using System.Collections.Generic;
using Ifrastructure.Events;

namespace Ifrastructure
{
  public interface IGameListenersProvider
  {
    IEnumerable<IGameListener> ProvideListeners();
  }
}