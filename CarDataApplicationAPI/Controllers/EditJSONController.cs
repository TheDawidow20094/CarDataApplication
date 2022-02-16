using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using System.Text.Json;

namespace CarDataApplicationAPI.Controllers
{
    [Route("api/editjson")]
    [ApiController]

    public class EditJSONController : Controller
    {
        [HttpPost]
        public IActionResult EditJson(string dbpassword, int id, [FromBody] JsonElement data)
        {
            if (dbpassword != "dUmv9Fq/8D6y9Rwh")
            {
                return BadRequest("Wrong Password!");
            }

            return ExecuteDatabaseOperation(dbpassword, id, data);
        }

        private IActionResult ExecuteDatabaseOperation(string dbpassword, int id, JsonElement data)
        {

            string Connection = @"Data Source=localhost; Database=cardataappdb; User ID=AppUser; Password=" + dbpassword;
            MySqlConnection cn = new MySqlConnection(Connection);
            cn.Open();

            string sql = "UPDATE users SET JSON =" + "'" + data + "'" + "WHERE Id =" + id; 
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            cmd.CommandType = CommandType.Text;

            MySqlDataReader reader = cmd.ExecuteReader();
            return Ok("Done");
            cn.Close();

        }
    }
}
