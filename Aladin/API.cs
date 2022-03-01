using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CyberDojo.Aladin;

public class API
{
    [Test]
    public async Task Test()
    {
        var users = await GetAllUsers();
        var user = users;
    }

    public static async Task<List<User>> GetAllUsers()
    {
        var url = "https://jsonmock.hackerrank.com/api/article_users?page=1";
        var httpClient = new HttpClient();
        var result = await httpClient.GetStringAsync(url);
        var response = JsonConvert.DeserializeObject<Result>(result);
        var list = response.Data;
        var total = response.Total;
        if (response.PerPage > 0)
        {
            for (var i = 2; i <= total / response.PerPage; i++)
            {
                var users = await GetUsers(i);
                list.AddRange(users);
            }
        }
        return list;
    }

    private static async Task<List<User>> GetUsers(int page)
    {
        var url = $"https://jsonmock.hackerrank.com/api/article_users?page={page}";
        var httpClient = new HttpClient();
        var result = await httpClient.GetStringAsync(url);
        var response = JsonConvert.DeserializeObject<Result>(result);
        return response.Data;
    }
    
    public static async Task<List<string>> getUsernames(int threshold)
    {
        var users = await GetAllUsers();
        return users.Where(x => x.Submitted >= threshold).OrderBy(x => x.Submitted).Select(x => x.Username).ToList();


    }
}

public class Result
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public List<User> Data { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string About { get; set; }
    public int Submitted { get; set; }
}