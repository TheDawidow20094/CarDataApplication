using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;

namespace CarDataApplicationAPI.Controllers
{
    [Route("api/checksernameexist")]
    [ApiController]

    public class CheckUserNameExistController : Controller
    {
        [HttpGet]
        public IActionResult ChceckUserExist(string dbpassword, string login)
        {
            MySqlConnection cn = new MySqlConnection(@"Data Source=localhost; Database=cardataappdb; User ID=AppUser; Password=" + dbpassword);

            try { cn.Open(); }
            catch (MySqlException) { return BadRequest("Wrong Password!"); }

            return ExecuteDatabaseOperation(dbpassword, login, cn);
        }

        private IActionResult ExecuteDatabaseOperation(string dbpassword, string login, MySqlConnection cn)
        {
            cn.Open();

            string sql = "SELECT Login FROM `users` WHERE `Login` =" + "'" + login + "'";
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            cmd.CommandType = CommandType.Text;

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return Ok("Username already taken!");
                cn.Close();
            }
                return Ok("Username is available");
                cn.Close();
        }
    }

}
