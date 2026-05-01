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
        $_SESSION['full_name'] = $user['full_name'];
        redirect('dashboard.php');
    }
    $error = 'Invalid username or password.';
}
?>
<!DOCTYPE html><html lang="en"><head><meta charset="UTF-8"><meta name="viewport" content="width=device-width, initial-scale=1.0"><title>Login - Parking Lot System</title><link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet"><link rel="stylesheet" href="assets/css/style.css"></head><body class="login-page"><div class="login-card card shadow"><div class="card-body p-4"><h3 class="text-center mb-3">Parking Lot System</h3><p class="text-center text-muted">Admin Login</p><?php if ($error): ?><div class="alert alert-danger"><?= e($error) ?></div><?php endif; ?><form method="post"><div class="mb-3"><label class="form-label">Username</label><input type="text" name="username" class="form-control" required autofocus></div><div class="mb-3"><label class="form-label">Password</label><input type="password" name="password" class="form-control" required></div><button class="btn btn-primary w-100">Login</button></form><div class="mt-3 small text-muted text-center">Default: admin / admin123</div></div></div></body></html>

    