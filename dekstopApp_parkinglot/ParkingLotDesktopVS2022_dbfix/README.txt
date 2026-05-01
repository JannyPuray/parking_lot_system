PARKING LOT SYSTEM - DESKTOP COUNTERPART
Visual Studio 2022 / C# Windows Forms / MySQL

This desktop app connects to the same MySQL database used by the PHP web version.
That means the web system and desktop app share the same data.

FEATURES
- Admin login
- Dashboard summary
- View parked vehicles
- Add parking slots
- Vehicle entry
- Vehicle exit with fee calculation
- Transaction reports

REQUIREMENTS
- Visual Studio 2022
- .NET 6 Desktop Development workload
- MySQL/XAMPP running
- The same database from the PHP system imported in phpMyAdmin

SETUP
1. Open ParkingLotDesktopVS2022.sln in Visual Studio 2022.
2. Restore NuGet packages if Visual Studio asks.
3. Open ParkingLotDesktop/Db.cs.
4. Edit the connection string if needed:

   server=localhost;port=3306;database=parking_lot_system;uid=root;pwd=;

5. Make sure the PHP system database is already imported.
6. Press F5 to run.

DEFAULT LOGIN
admin / admin123

IMPORTANT
Both systems must use the same database name and tables:
- users
- parking_slots
- vehicles

If your PHP database name is different, change it in Db.cs.
