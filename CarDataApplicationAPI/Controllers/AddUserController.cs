using CarDataApplicationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json;

namespace CarDataApplicationAPI.Controllers
{
    [Route("api/adduser")]
    [ApiController]

    public class AddUserController : Controller
    {
        [HttpPost]
        public IActionResult AddUser(string dbpassword, [FromBody] JsonElement data)
        {
            MySqlConnection cn = new MySqlConnection(@"Data Source=localhost; Database=cardataappdb; User ID=AppUser; Password=" + dbpassword);

            try { cn.Open(); }
            catch (MySqlException) { return BadRequest("Wrong Password!"); }

            return ExecuteDatabaseOperation(dbpassword, data, cn);

        }

        private IActionResult ExecuteDatabaseOperation(string dbpassword, JsonElement data, MySqlConnection cn)
        {
            UserModel NewUser = JsonConvert.DeserializeObject<UserModel>(data.ToString());
            string Json = System.Text.Json.JsonSerializer.Serialize<UserModel>(NewUser);
            cn.Open();

            string sql = "INSERT INTO users(Login, Password, JSON) VALUES('" + NewUser.Login + "','" + NewUser.Password + "','" + Json + "')";
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            cmd.CommandType = CommandType.Text;

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return Ok("False");
                cn.Close();
            }
            return Ok("Done");
            cn.Close();
        }
    }
}
