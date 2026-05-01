using MySql.Data.MySqlClient;
using System.Data;

namespace ParkingLotDesktop;

public static class Db
{
    // IMPORTANT: Use the same database used by your PHP web system.
    // Change server, user, and password if needed.
    public static string ConnectionString =
        "server=localhost;port=3306;database=parking_lot_db;uid=root;pwd=;SslMode=none;AllowPublicKeyRetrieval=true;";

    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }

    public static DataTable Query(string sql, params MySqlParameter[] parameters)
    {
        using var con = GetConnection();
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.AddRange(parameters);
        using var adapter = new MySqlDataAdapter(cmd);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public static int Execute(string sql, params MySqlParameter[] parameters)
    {
        using var con = GetConnection();
        con.Open();
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.AddRange(parameters);
        return cmd.ExecuteNonQuery();
    }

    public static object? Scalar(string sql, params MySqlParameter[] parameters)
    {
        using var con = GetConnection();
        con.Open();
        using var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.AddRange(parameters);
        return cmd.ExecuteScalar();
    }
}
