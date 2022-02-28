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
            MySqlConnection cn = new MySqlConnection(@"Data Source=localhost; Database=cardataappdb; User ID=AppUser; Password=" + dbpassword);

            try { cn.Open(); }
            catch (MySqlException) { return BadRequest("Wrong Password!"); }

            return ExecuteDatabaseOperation(dbpassword, id, cn);
        }

        private IActionResult ExecuteDatabaseOperation(string dbpassword, int id, MySqlConnection cn)
        {
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
