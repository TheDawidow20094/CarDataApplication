using CarDataApplicationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Newtonsoft.Json;
using System.Data;

namespace CarDataApplicationAPI.Controllers
{
    [Route("api/getuserbyid")]
    [ApiController]

    public class GetUserByIdController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            string Connection = @"Data Source=localhost; Database=cardataappdb; User ID=AppUser; Password=dUmv9Fq/8D6y9Rwh";
            MySqlConnection cn = new MySqlConnection(Connection);
            cn.Open();

            string sql = "SELECT * FROM `users` WHERE `Id` =" + id;
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
            }
            return Ok("False");

            cn.Close();
        }
    }
}
