<?php require_once 'config.php'; ?>
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Parking Lot System</title>
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
  <link rel="stylesheet" href="assets/css/style.css">
</head>
<body>
<?php if (isLoggedIn()): ?>
<nav class="navbar navbar-expand-lg navbar-dark bg-dark">
  <div class="container-fluid">
    <a class="navbar-brand fw-bold" href="dashboard.php">Parking Lot System</a>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNav">
      <ul class="navbar-nav me-auto">
        <li class="nav-item"><a class="nav-link" href="dashboard.php">Dashboard</a></li>
        <li class="nav-item"><a class="nav-link" href="slots.php">Parking Slots</a></li>
        <li class="nav-item"><a class="nav-link" href="vehicle_entry.php">Vehicle Entry</a></li>
        <li class="nav-item"><a class="nav-link" href="vehicle_exit.php">Vehicle Exit</a></li>
        <li class="nav-item"><a class="nav-link" href="reports.php">Reports</a></li>
      </ul>
      <span class="navbar-text me-3">Hi, <?= e($_SESSION['full_name'] ?? 'Admin') ?></span>
      <a href="logout.php" class="btn btn-outline-light btn-sm">Logout</a>
    </div>
  </div>
</nav>
<?php endif; ?>
<main class="container py-4">

