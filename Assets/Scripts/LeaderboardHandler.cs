using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardHandler : MonoBehaviour
{
  private const string URL = "http://localhost:8080/leaderboard";

  public void GenerateRequest()
  {
    StartCoroutine(ProcessRequest(URL));
  }

  private IEnumerator ProcessRequest(string uri)
  {
    using (UnityWebRequest request = UnityWebRequest.Get(uri))
    {
      request.SetRequestHeader("x-api-key", "025f5b0f040fb0c553d4755a991f98a250b8405863d34a9b2b6b5d2f31f507f7");
      yield return request.SendWebRequest();
      if (request.result == UnityWebRequest.Result.ConnectionError)
      {
        Debug.Log(request.error);
      }
      else
      {
        Debug.Log(request.downloadHandler.text);
      }
    }
  }
}