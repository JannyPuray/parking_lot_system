<?php
session_start();

$host = 'localhost';
$dbname = 'parking_lot_db';
$username = 'root';
$password = '';

try {
    // Connect to MySQL first, create the database if it is missing, then connect to the app database.
    $serverPdo = new PDO("mysql:host=$host;charset=utf8mb4", $username, $password);
    $serverPdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    $serverPdo->exec("CREATE DATABASE IF NOT EXISTS `$dbname` CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci");

    $pdo = new PDO("mysql:host=$host;dbname=$dbname;charset=utf8mb4", $username, $password);
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    initializeDatabase($pdo);
} catch (PDOException $e) {
    die('Database connection failed: ' . $e->getMessage());
}

function initializeDatabase(PDO $pdo) {
    $pdo->exec("CREATE TABLE IF NOT EXISTS users (
        id INT AUTO_INCREMENT PRIMARY KEY,
        username VARCHAR(50) NOT NULL UNIQUE,
        password VARCHAR(255) NOT NULL,
        full_name VARCHAR(100) NOT NULL,
        role VARCHAR(20) DEFAULT 'admin',
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");

    $pdo->exec("CREATE TABLE IF NOT EXISTS parking_slots (
        id INT AUTO_INCREMENT PRIMARY KEY,
        slot_number VARCHAR(20) NOT NULL UNIQUE,
        status ENUM('vacant','occupied') DEFAULT 'vacant',
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");

    $pdo->exec("CREATE TABLE IF NOT EXISTS vehicles (
        id INT AUTO_INCREMENT PRIMARY KEY,
        plate_number VARCHAR(30) NOT NULL,
        vehicle_type VARCHAR(30) NOT NULL,
        driver_name VARCHAR(100),
        contact_number VARCHAR(30),
        slot_id INT NOT NULL,
        time_in DATETIME NOT NULL,
        time_out DATETIME NULL,
        fee DECIMAL(10,2) DEFAULT 0.00,
        status ENUM('parked','exited') DEFAULT 'parked',
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        FOREIGN KEY (slot_id) REFERENCES parking_slots(id)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");

    $adminPassword = password_hash('admin123', PASSWORD_DEFAULT);
    $stmt = $pdo->prepare("INSERT IGNORE INTO users (username, password, full_name, role) VALUES ('admin', ?, 'Janny Puray', 'admin')");
    $stmt->execute([$adminPassword]);
    $pdo->exec("UPDATE users SET full_name='Janny Puray' WHERE username='admin'");

    $defaultSlots = ['A-01','A-02','A-03','A-04','A-05','B-01','B-02','B-03','B-04','B-05'];
    $stmt = $pdo->prepare("INSERT IGNORE INTO parking_slots (slot_number, status) VALUES (?, 'vacant')");
    foreach ($defaultSlots as $slot) {
        $stmt->execute([$slot]);
    }
}

function isLoggedIn() {
    return isset($_SESSION['user_id']);
}

function requireLogin() {
    if (!isLoggedIn()) {
        header('Location: login.php');
        exit;
    }
}

function redirect($url) {
    header("Location: $url");
    exit;
}

function e($value) {
    return htmlspecialchars((string)$value, ENT_QUOTES, 'UTF-8');
}
