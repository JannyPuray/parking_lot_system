# Parking Lot System PHP + MySQL

A simple web-based parking lot management system using PHP, MySQL, Bootstrap, and PDO.

## Features
- Admin login
- Dashboard statistics
- Add and delete parking slots
- Vehicle entry
- Vehicle exit with automatic fee calculation
- Transaction reports with date filter
- Print reports

## Default Login
- Username: `admin`
- Password: `admin123`

## Installation
1. Install XAMPP, WAMP, Laragon, or any PHP/MySQL server.
2. Copy the `parking_lot_system` folder into your web server directory:
   - XAMPP: `htdocs`
   - WAMP: `www`
3. Open phpMyAdmin.
4. Import `database.sql`.
5. Edit `config.php` if your MySQL username or password is different.
6. Open in browser:
   - `http://localhost/parking_lot_system`

## Fee Setting
The parking fee rate is currently set in `vehicle_exit.php`:

```php
$rate = 20;
```

Change `20` to your preferred hourly rate.

## Notes
- Bootstrap is loaded through CDN, so internet access is useful for styling.
- Database name: `parking_lot_db`
