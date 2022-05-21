using System.Data;
using System.Data.SqlTypes;
using System.Configuration;
using System.Data.SqlClient;
using MusicApp.Controllers;

namespace MusicApp.Models
{
    public class SongRepo : ISongRepo
    {
        public List<Song> _songList = new List<Song>();   
        public IEnumerable<Song> GetAllSongs()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(LocalDB)\\TestDB";
            builder.InitialCatalog = "Music";
            builder.IntegratedSecurity = true;
            SqlConnection con = new SqlConnection(builder.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT SongID,SongTitle,ReleaseDate FROM Song", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _songList.Add(new Song
                    {
                        SongID = rdr.GetInt32("SongID"),
                        SongTitle = rdr.GetString("SongTitle"),
                        ReleaseDate = rdr.GetDateTime("ReleaseDate")
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return _songList;
        }

        public Song Add(Song song)
        {
           _songList = (List<Song>)GetAllSongs();
            int id = _songList.Max(s => s.SongID) + 1;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(LocalDB)\\TestDB";
            builder.InitialCatalog = "Music";
            builder.IntegratedSecurity = true;
            SqlConnection con = new SqlConnection(builder.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("insert into Song (SongID,SongTitle,ReleaseDate) " +
                    "VALUES (@SongID,@SongTitle,@ReleaseDate)", con);
                cmd.Parameters.AddWithValue("@SongID", id);
                cmd.Parameters.AddWithValue("@SongTitle", song.SongTitle);
                cmd.Parameters.AddWithValue("@ReleaseDate", song.ReleaseDate);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                song.SongID = id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return song;

        }

        public bool Delete(int id)
        {
            bool del = false;
            /* =_songList.FirstOrDefault(s => s.SongID == id)*/
            

            if (GetSongs(id)!=null)
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "(LocalDB)\\TestDB";
                builder.InitialCatalog = "Music";
                builder.IntegratedSecurity = true;
                SqlConnection con = new SqlConnection(builder.ConnectionString);
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from Song where SongID=" + id, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    if (GetSongs(id) == null)
                    {
                        del = true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            return del;
        }
        public bool Update(int id, string st, string release)
        {
            bool updt = false;
            Song song=null;
            /* =_songList.FirstOrDefault(s => s.SongID == id)*/
            _songList = (List<Song>)GetAllSongs();


            if (_songList.Exists(s => s.SongID == id))
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "(LocalDB)\\TestDB";
                builder.InitialCatalog = "Music";
                builder.IntegratedSecurity = true;
                SqlConnection con = new SqlConnection(builder.ConnectionString);
                try
                {
                    SqlCommand cmd = new SqlCommand("update Song SET SongTitle='"+st+ "' ,ReleaseDate='"+release+ "' where SongID="+id, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    song = GetSongs(id);
                    updt = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            return updt;
        }

        public Song GetSongs(int Id)
        {
            _songList = (List<Song>)GetAllSongs();
            return _songList.FirstOrDefault(s => s.SongID == Id);
        }
    }
}
