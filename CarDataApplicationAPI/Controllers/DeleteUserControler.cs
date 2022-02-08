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
        public IActionResult DeleteUser(int id)
        {
            string Connection = @"Data Source=localhost; Database=cardataappdb; User ID=root; Password=''";
            MySqlConnection cn = new MySqlConnection(Connection);
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
