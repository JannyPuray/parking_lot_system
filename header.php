<?php require_once 'config.php'; ?>
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Parking Lot System</title>
  <link rel="preconnect" href="https://fonts.googleapis.com">
  <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
  <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;600;700;800;900&display=swap" rel="stylesheet">
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
  <link rel="stylesheet" href="assets/css/style.css">
</head>
<body>
<?php if (isLoggedIn()): ?>
<nav class="navbar navbar-expand-lg navbar-dark app-navbar sticky-top">
  <div class="container-fluid px-4">
    <a class="navbar-brand fw-bold d-flex align-items-center" href="dashboard.php"><span class="brand-badge">⚡</span> Parking Lot System</a>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav"><span class="navbar-toggler-icon"></span></button>
    <div class="collapse navbar-collapse" id="navbarNav">
      <ul class="navbar-nav me-auto gap-1">
        <li class="nav-item"><a class="nav-link" href="dashboard.php">Dashboard</a></li>
        <li class="nav-item"><a class="nav-link" href="slots.php">Parking Slots</a></li>
        <li class="nav-item"><a class="nav-link" href="vehicle_entry.php">Vehicle Entry</a></li>
        <li class="nav-item"><a class="nav-link" href="vehicle_exit.php">Vehicle Exit</a></li>
        <li class="nav-item"><a class="nav-link" href="reports.php">Reports</a></li>
      </ul>
      <span class="owner-chip me-3">Owner: Janny Puray</span>
      <a href="logout.php" class="btn btn-outline-light btn-sm rounded-pill px-3 fw-bold">Logout</a>
    </div>
  </div>
</nav>
<?php endif; ?>
<main class="container app-shell py-4">
