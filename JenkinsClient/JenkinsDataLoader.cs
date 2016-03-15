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
        string JenkinsServerUrl = "https://ci.jenkins-ci.org/job/jenkins_pom/api/json";

        public async void GetProjectData(string url)
        {

            var requestUri = new Uri(JenkinsServerUrl);
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                var resp = await client.GetAsync(requestUri);

                string response = await resp.Content.ReadAsStringAsync();
                var jObject = JObject.Parse(response);
                var jsonResponse = JsonConvert.DeserializeObject<BuildProject>(response);

                //var name = jObject["displayName"];
            }

        }

        public async Task<Build> GetBuildStatus(string Url)
        {
            var requestUri = new Uri(Url);

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                var resp = await client.GetAsync(requestUri);

                string response = await resp.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject(response);
                var build = new Build();

                return build;
            }


        }
    }
}
