using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
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
            string Connection = @"Data Source=localhost; Database=cardataappdb; User ID=root; Password=''";
            MySqlConnection cn = new MySqlConnection(Connection);
            cn.Open();

            string sql = "SELECT * FROM `users` WHERE `Id` =" + id;
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            cmd.CommandType = CommandType.Text;

            string temp = string.Empty;
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                temp += reader["Id"].ToString();
                temp += reader["Login"].ToString();
                temp += reader["Password"].ToString();
                temp += reader["JSON"].ToString();

                return Ok(temp);
            }
            return Ok("False");

            cn.Close();
        }
    }
}
