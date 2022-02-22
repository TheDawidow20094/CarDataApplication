using CarDataApplicationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Newtonsoft.Json;
using System.Data;

namespace CarDataApplicationAPI.Controllers
{
    [Route("api/getuser")]
    [ApiController]

    public class GetUserController : Controller
    {
        [HttpGet()]
        public IActionResult GetUserById(string dbpassword, string login, string password)
        {
            if (dbpassword != "dUmv9Fq/8D6y9Rwh")
            {
                return BadRequest("Wrong Password!");
            }

            return ExecuteDatabaseOperation(dbpassword, login, password);

        }

        private IActionResult ExecuteDatabaseOperation(string dbpassword, string login, string password)
        {
            string Connection = @"Data Source=localhost; Database=cardataappdb; User ID=AppUser; Password=" + dbpassword;
            MySqlConnection cn = new MySqlConnection(Connection);
            cn.Open();

            string sql = "SELECT * FROM `users` WHERE `Login` = \"" + login  + " \" AND `Password` = \"" + password + " \" ";
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            cmd.CommandType = CommandType.Text;

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                UserModel newUser = new();
                string Json = reader["JSON"].ToString();
                newUser = JsonConvert.DeserializeObject<UserModel>(Json);
                newUser.Id = int.Parse(reader["Id"].ToString());
                newUser.Login = reader["Login"].ToString();

                string Data = JsonConvert.SerializeObject(newUser);

                return Ok(Data);
                cn.Close();
            }
            return Ok("No user found");

            cn.Close();
        }
    }
}
