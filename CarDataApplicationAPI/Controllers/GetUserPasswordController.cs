using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;

namespace CarDataApplicationAPI.Controllers
{
    [Route("api/getuserpassword")]
    [ApiController]

    public class GetUserPasswordController : Controller
    {
        [HttpGet]
        public IActionResult GetPassword(string dbpassword, int id)
        {
            if (dbpassword != "dUmv9Fq/8D6y9Rwh")
            {
                return BadRequest("Wrong Password!");
            }

            return ExecuteDatabaseOperation(dbpassword, id);
        }

        private IActionResult ExecuteDatabaseOperation(string dbpassword, int id)
        {
            string Connection = @"Data Source=localhost; Database=cardataappdb; User ID=AppUser; Password=" + dbpassword;
            MySqlConnection cn = new MySqlConnection(Connection);
            cn.Open();

            string sql = "SELECT `Password` FROM `users` WHERE `Id` =" + id;
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            cmd.CommandType = CommandType.Text;


            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return Ok(reader[0]);
                cn.Close();
            }
            return Ok("No data!");
            cn.Close();
        }
    }
}
