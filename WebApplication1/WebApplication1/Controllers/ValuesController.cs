using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            //var url = "https://api.github.com/users/rafal00082/repos";
            //var url = "https://api.github.com/repos/rafal00082/enova/commits";
            //var url = "https://api.github.com/repositories?since=364";
            var url = "https://api.github.com/repos/rafal00082/enova/branches";

            //var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
            //var user = await client.User.Get("shiftkey");
            //var all = await client.Gist.GetAllForUser("rafal00082");
            //Console.WriteLine("{0} has {1} public repositories - go check out their profile at {2}",
            //    user.Name,
            //    user.PublicRepos,
            //    user.Url);


            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("anything","1"));
            //var resp = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri(url)));
            httpClient.BaseAddress = new Uri("https://api.github.com");
            var response = await httpClient.GetStringAsync("repos/rafal00082/enova/branches");
            var bList = JsonConvert.DeserializeObject<List<Branch>>(response);
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.UserAgent = "Anything";
                webRequest.Accept = "application/vnd.github.nebula-preview+json";
                webRequest.ServicePoint.Expect100Continue = false;

                try
                {
                    using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                    {
                        string reader = responseReader.ReadToEnd();
                        var branchesList = JsonConvert.DeserializeObject<List<Branch>>(reader);
                        foreach (var branch in branchesList)
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Ok(ex);
                }
            }
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var url = "https://api.github.com";
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.UserAgent = "Anything";
                webRequest.ServicePoint.Expect100Continue = false;

                try
                {
                    using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                    {
                        string reader = responseReader.ReadToEnd();
                        var jsonobj = JsonConvert.DeserializeObject(reader);
                    }
                }
                catch(Exception ex)
                {
                    return Ok(ex);
                }
            }

            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
