using MySql.Data.MySqlClient;
using System.Data;

namespace ParkingLotDesktop;

public static class Db
{
    public static string ServerConnectionString =
        "server=localhost;port=3306;uid=root;pwd=;SslMode=none;AllowPublicKeyRetrieval=true;";

    public static string ConnectionString =
        "server=localhost;port=3306;database=parking_lot_db;uid=root;pwd=;SslMode=none;AllowPublicKeyRetrieval=true;";

    public static MySqlConnection GetConnection() => new(ConnectionString);

    public static void EnsureDatabase()
    {
        using (var con = new MySqlConnection(ServerConnectionString))
        {
            con.Open();
            using var cmd = new MySqlCommand("CREATE DATABASE IF NOT EXISTS parking_lot_db", con);
            cmd.ExecuteNonQuery();
        }

        Execute(@"CREATE TABLE IF NOT EXISTS users (
            id INT AUTO_INCREMENT PRIMARY KEY,
            username VARCHAR(50) NOT NULL UNIQUE,
            password VARCHAR(255) NOT NULL,
            full_name VARCHAR(100) NOT NULL DEFAULT 'Janny Puray'
        )");

        Execute(@"CREATE TABLE IF NOT EXISTS parking_slots (
            id INT AUTO_INCREMENT PRIMARY KEY,
            slot_number VARCHAR(20) NOT NULL UNIQUE,
            status ENUM('vacant','occupied') NOT NULL DEFAULT 'vacant'
        )");

        Execute(@"CREATE TABLE IF NOT EXISTS vehicles (
            id INT AUTO_INCREMENT PRIMARY KEY,
            plate_number VARCHAR(50) NOT NULL,
            vehicle_type VARCHAR(50) NOT NULL,
            slot_id INT NULL,
            time_in DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
            time_out DATETIME NULL,
            fee DECIMAL(10,2) NOT NULL DEFAULT 0,
            status ENUM('parked','exited') NOT NULL DEFAULT 'parked'
        )");

        Execute("INSERT IGNORE INTO users(username,password,full_name) VALUES('admin','admin123','Janny Puray')");

        for (int i = 1; i <= 10; i++)
        {
            Execute("INSERT IGNORE INTO parking_slots(slot_number,status) VALUES(@slot,'vacant')",
                new MySqlParameter("@slot", $"A-{i:00}"));
        }
    }

    public static DataTable Query(string sql, params MySqlParameter[] parameters)
    {
        using var con = GetConnection();
        using var cmd = new MySqlCommand(sql, con);
        if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
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
        if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
        return cmd.ExecuteNonQuery();
    }

    public static object? Scalar(string sql, params MySqlParameter[] parameters)
    {
        using var con = GetConnection();
        con.Open();
        using var cmd = new MySqlCommand(sql, con);
        if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
        return cmd.ExecuteScalar();
    }
}
