CREATE DATABASE IF NOT EXISTS parking_lot_db;
USE parking_lot_db;

CREATE TABLE IF NOT EXISTS users (
  id INT AUTO_INCREMENT PRIMARY KEY,
  username VARCHAR(50) NOT NULL UNIQUE,
  password VARCHAR(255) NOT NULL,
  full_name VARCHAR(100) NOT NULL,
  role VARCHAR(20) DEFAULT 'admin',
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS parking_slots (
  id INT AUTO_INCREMENT PRIMARY KEY,
  slot_number VARCHAR(20) NOT NULL UNIQUE,
  status ENUM('vacant','occupied') DEFAULT 'vacant',
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS vehicles (
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
);

INSERT INTO users (username, password, full_name, role) VALUES
('admin', '$2y$12$JQU2elG1U.PSVqgifuQlMOU9RJ7dfSi40E7.VEtXgouN.b983zfKq', 'System Administrator', 'admin')
ON DUPLICATE KEY UPDATE username=username;

INSERT INTO parking_slots (slot_number, status) VALUES
('A-01','vacant'),('A-02','vacant'),('A-03','vacant'),('A-04','vacant'),('A-05','vacant'),
('B-01','vacant'),('B-02','vacant'),('B-03','vacant'),('B-04','vacant'),('B-05','vacant')
ON DUPLICATE KEY UPDATE slot_number=slot_number;

