using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JenkinsClient
{
    public class JenkinsDataLoader
    {
        JenkinsServerInfo _serverInfo = null;
        public JenkinsDataLoader(JenkinsServerInfo serverInfo)
        {
            _serverInfo = serverInfo;
        }

        string JenkinsServerUrl = "https://ci.jenkins-ci.org/job/jenkins_pom/api/json";

        public async Task<BuildProject> GetProjectData(string url)
        {

            var requestUri = new Uri(JenkinsServerUrl);
            using (HttpClient client = new System.Net.Http.HttpClient())
            {
                var resp = await client.GetAsync(requestUri);

                string response = await resp.Content.ReadAsStringAsync();
                var projectResponse = JsonConvert.DeserializeObject<BuildProject>(response);

                return projectResponse;
                //var name = jObject["displayName"];
            }

        }

        public async Task<BuildInformation> GetBuildInformation(Build build)
        {
            return await GetBuildInformation(build.url);
        }

        public async Task<BuildInformation> GetBuildInformation(string Url)
        {
            var requestUri = new Uri(Url);

            //HttpClientHandler handler = new HttpClientHandler()
            //{
            //    PreAuthenticate = true
            //};

         

            //handler.Credentials = new NetworkCredential("joel.hess", "48dea3108351cefe89f30bb027033731", "realm");

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {

                var resp = await client.GetAsync(requestUri);
                if (resp.IsSuccessStatusCode)
                {
                    string response = await resp.Content.ReadAsStringAsync();
                    var jsonResponse = JsonConvert.DeserializeObject(response);
                    var buildInfo = JsonConvert.DeserializeObject<BuildInformation>(response);
                    return buildInfo;
                }
                
            }

            return null;
        }

        void AddUserInformationToRequest(HttpClient client)
        {
            if (client != null)
            {
                var byteArray = Encoding.ASCII.GetBytes(_serverInfo.UserName + ":" + _serverInfo.ApiToken);//"joel.hess:1e76a66f0565b3dbf3f741922b0f9435");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            }

        }

    }
}
