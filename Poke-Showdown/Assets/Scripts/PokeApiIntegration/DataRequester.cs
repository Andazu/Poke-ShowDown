using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace PokeApiIntegration
{
    public static class DataRequester
    {
        public static async Task<string> GetRequest(string uri)
        {
            using var webRequest = UnityWebRequest.Get(uri);
            await webRequest.SendWebRequest();
            var result = "";
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError: //connection error or dataprocessing error, log an error in console
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(string.Format("Something went wrong: {0}", webRequest.error));
                    break;
                case UnityWebRequest.Result.Success:
                    result = webRequest.downloadHandler.text;
                    break;
            }
            return result;
        }
    }
}