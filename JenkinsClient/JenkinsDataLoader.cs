using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                var resp = await client.GetAsync(requestUri);

                string response = await resp.Content.ReadAsStringAsync();
                var jObject = JObject.Parse(response);
                var projectResponse = JsonConvert.DeserializeObject<BuildProject>(response);

                return projectResponse;
                //var name = jObject["displayName"];
            }

        }

        public async Task<BuildInformation> GetBuildInformation(Build Build)
        {
            return await GetBuildInformation(Build.url);
        }

        public async Task<BuildInformation> GetBuildInformation(string Url)
        {
            var requestUri = new Uri(Url);

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                var resp = await client.GetAsync(requestUri);

                string response = await resp.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject(response);
                var buildInfo = JsonConvert.DeserializeObject<BuildInformation>(response);

                return buildInfo;
            }
        }
    }
}
