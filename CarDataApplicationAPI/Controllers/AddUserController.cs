using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using System.Text.Json;

namespace CarDataApplicationAPI.Controllers
{
    [Route("api/adduser")]
    [ApiController]

    public class AddUserController : Controller
    {
        [HttpPost]
        public IActionResult AddUser([FromBody] JsonElement data)
        {

            var login = data.GetProperty("Login").GetString();
            var password = data.GetProperty("Password").GetString();
            var json = data.GetProperty("JSON").GetString();

            string Connection = @"Data Source=localhost; Database=cardataappdb; User ID=AppUser; Password=dUmv9Fq/8D6y9Rwh";
            MySqlConnection cn = new MySqlConnection(Connection);
            cn.Open();


            string sql = "INSERT INTO users(Login, Password, JSON) VALUES('" + login + "','" + password + "','" + json + "')";
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            cmd.CommandType = CommandType.Text;

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return BadRequest("False");
            }
            return Ok("Done");
            cn.Close();
        }
    }
}
