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
            MySqlConnection cn = new MySqlConnection(@"Data Source=localhost; Database=cardataappdb; User ID=AppUser; Password=" + dbpassword);

            try { cn.Open(); }
            catch (MySqlException) { return BadRequest("Wrong Password!"); }

            return ExecuteDatabaseOperation(dbpassword, id, data, cn);
        }

        private IActionResult ExecuteDatabaseOperation(string dbpassword, int id, JsonElement data, MySqlConnection cn)
        {
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
