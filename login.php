<?php
require_once 'config.php';
if (isLoggedIn()) redirect('dashboard.php');
$error = '';
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $username = trim($_POST['username'] ?? '');
    $password = $_POST['password'] ?? '';
    $stmt = $pdo->prepare('SELECT * FROM users WHERE username = ? LIMIT 1');
    $stmt->execute([$username]);
    $user = $stmt->fetch(PDO::FETCH_ASSOC);
    if ($user && password_verify($password, $user['password'])) {
        $_SESSION['user_id'] = $user['id'];
        $_SESSION['username'] = $user['username'];
        $_SESSION['full_name'] = 'Janny Puray';
        redirect('dashboard.php');
    }
    $error = 'Invalid username or password.';
}
?>
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8"><meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Login - Parking Lot System</title>
  <link rel="preconnect" href="https://fonts.googleapis.com"><link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
  <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;600;700;800;900&display=swap" rel="stylesheet">
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet"><link rel="stylesheet" href="assets/css/style.css">
</head>
<body class="login-page">
  <div class="login-card card">
    <div class="card-body p-5">
      <div class="text-center mb-4"><div class="brand-badge mx-auto mb-3">⚡</div><h2 class="fw-black mb-1">Parking Lot System</h2><p class="text-muted mb-0">Admin Login • Owner: Janny Puray</p></div>
      <?php if ($error): ?><div class="alert alert-danger rounded-4"><?= e($error) ?></div><?php endif; ?>
      <form method="post">
        <div class="mb-3"><label class="form-label fw-bold">Username</label><input type="text" name="username" class="form-control" value="admin" required autofocus></div>
        <div class="mb-4"><label class="form-label fw-bold">Password</label><input type="password" name="password" class="form-control" value="admin123" required></div>
        <button class="btn btn-modern w-100">Login</button>
      </form>
      <div class="mt-3 small text-muted text-center">Default: admin / admin123</div>
    </div>
  </div>
</body>
</html>

// change the default credentials and use strong passwords in production.