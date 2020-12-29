using SMYROOMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMYROOMS.DB
{
   public class MealDataEntry
    {
        public List<BoardCode> getSMRBoardCode()
        {
            List<BoardCode> BoardData = new List<BoardCode>();
            using (SqlConnection conn = new SqlConnection("Server=CBDEV3;Database=SMyRooms;User Id=bery; Password=CityBookers;"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetMealCodes", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;



                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        BoardCode  board = new BoardCode();
                        board.Code =  rdr["BoardCode"].ToString();
                        board.Name = rdr["BoardName"].ToString();
                        
                        BoardData.Add(board);
                    }
                }
            }
            return BoardData;
        }
    }
}
