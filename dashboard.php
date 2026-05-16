<?php
require_once 'config.php';
requireLogin();
$totalSlots = $pdo->query('SELECT COUNT(*) FROM parking_slots')->fetchColumn();
$vacantSlots = $pdo->query("SELECT COUNT(*) FROM parking_slots WHERE status='vacant'")->fetchColumn();
$occupiedSlots = $pdo->query("SELECT COUNT(*) FROM parking_slots WHERE status='occupied'")->fetchColumn();
$todayIncome = $pdo->query("SELECT COALESCE(SUM(fee),0) FROM vehicles WHERE status='exited' AND DATE(time_out)=CURDATE()")->fetchColumn();
$recent = $pdo->query('SELECT v.*, s.slot_number FROM vehicles v JOIN parking_slots s ON v.slot_id=s.id ORDER BY v.id DESC LIMIT 8')->fetchAll(PDO::FETCH_ASSOC);
include 'header.php';
?>
<section class="hero-panel">
  <div>
    <div class="eyebrow"><span class="lightning">⚡</span>Live parking command center</div>
    <h1 class="hero-title">Dashboard</h1>
    <p class="hero-subtitle">Welcome back, <strong>Janny Puray</strong>.</p>
  </div>
  <a href="vehicle_entry.php" class="btn btn-modern">+ New Vehicle Entry</a>
</section>
<div class="row g-3 mb-4">
  <div class="col-md-3"><div class="card stat-card blue"><div class="card-body"><h6>Total Slots</h6><h2><?= $totalSlots ?></h2><span class="stat-icon">🅿️</span></div></div></div>
  <div class="col-md-3"><div class="card stat-card success"><div class="card-body"><h6>Vacant Slots</h6><h2><?= $vacantSlots ?></h2><span class="stat-icon">✅</span></div></div></div>
  <div class="col-md-3"><div class="card stat-card danger"><div class="card-body"><h6>Occupied</h6><h2><?= $occupiedSlots ?></h2><span class="stat-icon">🚗</span></div></div></div>
  <div class="col-md-3"><div class="card stat-card warning"><div class="card-body"><h6>Today's Income</h6><h2>₱<?= number_format($todayIncome,2) ?></h2><span class="stat-icon">💰</span></div></div></div>
</div>
<div class="modern-card"><div class="card-header bg-white fw-bold p-3">Recent Transactions</div><div class="table-responsive"><table class="table table-hover"><thead><tr><th>Plate</th><th>Type</th><th>Slot</th><th>Time In</th><th>Time Out</th><th>Fee</th><th>Status</th></tr></thead><tbody><?php if(!$recent): ?><tr><td colspan="7" class="text-center text-muted py-4">No transactions yet.</td></tr><?php endif; ?><?php foreach($recent as $r): ?><tr><td class="fw-bold"><?= e($r['plate_number']) ?></td><td><?= e($r['vehicle_type']) ?></td><td><?= e($r['slot_number']) ?></td><td><?= e($r['time_in']) ?></td><td><?= e($r['time_out'] ?: '-') ?></td><td>₱<?= number_format($r['fee'],2) ?></td><td><span class="badge bg-<?= $r['status']=='parked'?'warning':'success' ?>"><?= e($r['status']) ?></span></td></tr><?php endforeach; ?></tbody></table></div></div>
<?php include 'footer.php'; ?>
