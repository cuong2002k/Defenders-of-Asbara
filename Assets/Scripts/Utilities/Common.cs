using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
public class Common
{

  [Conditional("DEBUG")]
  public static void Log(object message)
  {
      Debug.Log(message);
  }    

  [Conditional("DEBUG")]
  public static void Log(string message, params object[] arg)
  {
    Debug.Log(string.Format(message, arg));
  }

  [Conditional("DEBUG")]
  public static void LogWarning(object message, Object context)
  {
    Debug.LogWarning(message, context);
  }

  [Conditional("DEBUG")]
  public static void LogWarning(Object context, string format, params object[] args)
  { 
    Debug.LogWarning(string.Format(format, args), context);
  }

  [Conditional("DEBUG")]
  public static void Warning(bool condition, object message)
  {
    if(!condition) Debug.LogWarning(message);
  }

  [Conditional("DEBUG")]
  public static void Warning(bool condition, object message, Object context)
  {
    if(!condition) Debug.LogWarning(message, context);
  }

  [Conditional("DEBUG")]
  public static void Warning(bool condition, Object context, string format, params object[] args )
  {
    if(!condition) Debug.LogWarning(string.Format(format, args), context);
  }

  // --------------------------------------
  // -------------ASSERT-------------------

  // throw unity exception if condition = false
  [Conditional("ASSERT")]
  public static void Assert(bool condition)
  {
    if(!condition) throw new UnityException();
  }

  // throw unity exception if condition = false and show console
  [Conditional("ASSERT")]
  public static void Assert(bool condition, string message)
  {
    if(!condition) throw new UnityException(message);
  }

  // throw unity exception if condition = false and show console
  [Conditional("ASSERT")]
  public static void Assert(bool condition, string format, params object[] args)
  {
    if(!condition) throw new UnityException(string.Format(format, args));
  }




  
}
