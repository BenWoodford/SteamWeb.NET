using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using RestSharp;
using SteamWeb.SteamWeb.Types;
using RestSharp.Deserializers;

namespace SteamWeb
{
    public class SteamWebClient
    {
        string device_name; string device_token; string url; int timeout;

        RestClient client;

        IRestResponse lastResponse;
        RestRequest lastRequest;

        public SteamWebClient(string device_id, string device_token, string url = "https://localhost:8443", int timeout = 2000)
        {
            this.device_name = device_id;
            this.device_token = device_token;
            this.url = url;
            this.timeout = timeout;
            client = new RestClient(url);
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }

        public bool AuthoriseClient()
        {
            PressButton(Buttons.GUIDE);
            return lastResponse.StatusCode == HttpStatusCode.Accepted;
        }
        public bool PressButton(Buttons button)
        {
            RestRequest request = GeneratePost("/steam/button/{button}/");
            request.AddParameter("button", button.ToString().ToLower(), ParameterType.UrlSegment);
            return (ProcessRequest(request).StatusCode == HttpStatusCode.Accepted);
        }

        public bool MoveMouse(int x, int y)
        {
            RestRequest request = GeneratePost("/steam/mouse/move");
            request.AddParameter("delta_x", x, ParameterType.GetOrPost);
            request.AddParameter("delta_y", y, ParameterType.GetOrPost);
            return (ProcessRequest(request).StatusCode == HttpStatusCode.Accepted);
        }

        public bool MouseButtons(MouseButtons button)
        {
            RestRequest request = GeneratePost("/steam/mouse/click");
            request.AddParameter("button", button.ToString().ToLower(), ParameterType.GetOrPost);
            return (ProcessRequest(request).StatusCode == HttpStatusCode.Accepted);            
        }

        public bool PressKey(Keys key)
        {
            return PressKey(key.ToString());
        }

        public bool PressKey(string key)
        {
            RestRequest request = GeneratePost("/steam/keyboard/key/");
            request.AddParameter("name", key.ToLower(), ParameterType.GetOrPost);
            return (ProcessRequest(request).StatusCode == HttpStatusCode.Accepted);
        }

        public Dictionary<string, Game> GetGames()
        {
            RestRequest request = GenerateGet("/steam/games/");
            IRestResponse<GameResult> response = client.Execute<GameResult>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data.data;
            }
            return new Dictionary<string, Game>();
        }

        public bool RunGame(int appID)
        {
            RestRequest request = GeneratePost("/steam/games/{appid}/run");
            request.AddParameter("appid", appID, ParameterType.UrlSegment);
            return (ProcessRequest(request).StatusCode == HttpStatusCode.Accepted);
        }

        public Space GetSpace()
        {
            RestRequest request = GenerateGet("/steam/space/");
            IRestResponse<SpaceResult> response = client.Execute<SpaceResult>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data.data;
            }
            return null;
        }

        public bool ChangeSpace(Spaces space)
        {
            RestRequest request = GeneratePost("/steam/space/");
            request.AddParameter("name", space.ToString().ToLower(), ParameterType.GetOrPost);
            return (ProcessRequest(request).StatusCode == HttpStatusCode.Accepted);
        }

        private RestRequest GeneratePost(string endpoint) {
            RestRequest request = new RestRequest(endpoint, Method.POST);
            request.AddParameter("device_name", device_name, ParameterType.Cookie);
            request.AddParameter("device_token", device_token, ParameterType.Cookie);
            request.Timeout = timeout;
            return request;
        }

        private RestRequest GenerateGet(string endpoint)
        {
            RestRequest request = new RestRequest(endpoint, Method.GET);
            request.AddParameter("device_name", device_name, ParameterType.Cookie);
            request.AddParameter("device_token", device_token, ParameterType.Cookie);
            request.Timeout = timeout;
            return request;
        }

        private IRestResponse ProcessRequest(RestRequest request)
        {
            lastRequest = request;
            return lastResponse = client.Execute(request);
        }

        public IRestResponse GetLastResponse()
        {
            return lastResponse;
        }

        public RestRequest GetLastRequest()
        {
            return lastRequest;
        }
    }
}
