using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;

namespace CarDataApplicationAPI.Controllers
{
    [Route("api/deleteuser")]
    [ApiController]


    public class DeleteUserControler : Controller
    {
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string dbpassword, int id)
        {
            MySqlConnection cn = new MySqlConnection(@"Data Source=localhost; Database=cardataappdb; User ID=AppUser; Password=" + dbpassword);

            try { cn.Open(); }
            catch (MySqlException) { return BadRequest("Wrong Password!"); }

            return ExecuteDatabaseOperation(dbpassword, id, cn);

        }

        private IActionResult ExecuteDatabaseOperation(string dbpassword, int id, MySqlConnection cn)
        {
            cn.Open();

            string sql = "DELETE FROM users WHERE Id=" + id.ToString();
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            cmd.CommandType = CommandType.Text;

            cmd.ExecuteReader();
            return Ok("Done");
            cn.Close();
        }
    }
}
