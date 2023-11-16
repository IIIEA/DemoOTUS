using UnityEngine;

namespace ShootEmUp
{
  public static class ObjectExtensions
  {
    public static void SetActiveState<T>(this T instance, bool state) where T : Object
    {
      switch (instance)
      {
        case GameObject gameObject:
          gameObject.SetActive(state);
          break;
        case Component component:
          component.gameObject.SetActive(state);
          break;
      }
    }
    
    public static void SetParent<T>(this T instance, Transform parent) where T : Object
    {
      switch (instance)
      {
        case GameObject gameObject:
          gameObject.transform.SetParent(parent);
          break;
        case Component component:
          component.transform.SetParent(parent);
          break;
      }
    }
    
    public static bool GetActiveState<T>(this T instance)
    {
      return instance switch
      {
        GameObject gameObject => gameObject.activeSelf,
        Component component => component.gameObject.activeSelf,
        _ => false
      };
    }
  }
}