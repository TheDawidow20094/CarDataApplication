using CarDataApplicationAPI.Models.ModelsFromDB;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using System.Text.Json;

namespace CarDataApplicationAPI.Controllers
{
    [Route("api/changeuserpassword")]
    [ApiController]

    public class ChangeUserPassword : Controller
    {
        private ChangePasswordModel newModel;
        private MySqlCommand cmd;

        [HttpPost]
        public IActionResult ChangePassword(string dbpassword, [FromBody] JsonElement data)
        {
            if (dbpassword != "dUmv9Fq/8D6y9Rwh")
            {
                return BadRequest("Wrong Password!");
            }

            AssignModelValue(data);

            return ExecuteDatabaseOperation(dbpassword);
        }

        private IActionResult ExecuteDatabaseOperation(string dbpassword)
        {
            MySqlConnection cn = new MySqlConnection(@"Data Source=localhost; Database=cardataappdb; User ID=AppUser; Password=" + dbpassword);
            cn.Open();

            if (CheckOldPassword(dbpassword))
            {
                string sql = "UPDATE users SET Password =" + "'" + newModel.NewPassword + "'" + "WHERE Id =" + newModel.UserId;
                cmd = new(sql, cn);
                cmd.CommandType = CommandType.Text;

                MySqlDataReader reader = cmd.ExecuteReader();
                return Ok("Done");
                cn.Close();
            }

            return BadRequest("Wrong Old Password!");
            cn.Close();
        }

        private bool CheckOldPassword(string dbpassword)
        {
            bool iscorrect = false;
            MySqlConnection cn = new MySqlConnection(@"Data Source=localhost; Database=cardataappdb; User ID=AppUser; Password=" + dbpassword);
            cn.Open();

            string sql = "SELECT Password FROM `users` WHERE `Password` =" + "'" + newModel.OldPassword + "'";
            cmd = new(sql, cn);
            cmd.CommandType = CommandType.Text;

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                iscorrect = true;
            }

            cn.Close();
            return iscorrect;
        }

        private void AssignModelValue(JsonElement data)
        {
            int userid = int.Parse(data.GetProperty("UserId").GetString());
            string oldpassword = data.GetProperty("OldPassword").GetString();
            string newpassword = data.GetProperty("NewPassword").GetString();
            newModel = new(userid, newpassword, oldpassword);
        }
    }
}
