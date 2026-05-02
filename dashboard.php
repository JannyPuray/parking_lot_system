<?php
require_once 'config.php';
requireLogin();
$totalSlots = $pdo->query('SELECT COUNT(*) FROM parking_slots')->fetchColumn();
$vacantSlots = $pdo->query("SELECT COUNT(*) FROM parking_slots WHERE status='vacant'")->fetchColumn();
$occupiedSlots = $pdo->query("SELECT COUNT(*) FROM parking_slots WHERE status='occupied'")->fetchColumn();
$parkedVehicles = $pdo->query("SELECT COUNT(*) FROM vehicles WHERE status='parked'")->fetchColumn();
$todayIncome = $pdo->query("SELECT COALESCE(SUM(fee),0) FROM vehicles WHERE status='exited' AND DATE(time_out)=CURDATE()")->fetchColumn();
$recent = $pdo->query('SELECT v.*, s.slot_number FROM vehicles v JOIN parking_slots s ON v.slot_id=s.id ORDER BY v.id DESC LIMIT 8')->fetchAll(PDO::FETCH_ASSOC);
include 'header.php';
?>
<div class="d-flex justify-content-between align-items-center mb-4"><h2>Dashboard</h2><a href="vehicle_entry.php" class="btn btn-primary">New Vehicle Entry</a></div>
<div class="row g-3 mb-4">
  <div class="col-md-3"><div class="card stat-card"><div class="card-body"><h6>Total Slots</h6><h2><?= $totalSlots ?></h2></div></div></div>
  <div class="col-md-3"><div class="card stat-card success"><div class="card-body"><h6>Vacant Slots</h6><h2><?= $vacantSlots ?></h2></div></div></div>
  <div class="col-md-3"><div class="card stat-card danger"><div class="card-body"><h6>Occupied Slots</h6><h2><?= $occupiedSlots ?></h2></div></div></div>
  <div class="col-md-3"><div class="card stat-card warning"><div class="card-body"><h6>Today's Income</h6><h2>₱<?= number_format($todayIncome,2) ?></h2></div></div></div>
</div>
<div class="card shadow-sm"><div class="card-header bg-white fw-bold">Recent Transactions</div><div class="table-responsive"><table class="table table-striped mb-0"><thead><tr><th>Plate</th><th>Type</th><th>Slot</th><th>Time In</th><th>Time Out</th><th>Fee</th><th>Status</th></tr></thead><tbody><?php foreach($recent as $r): ?><tr><td><?= e($r['plate_number']) ?></td><td><?= e($r['vehicle_type']) ?></td><td><?= e($r['slot_number']) ?></td><td><?= e($r['time_in']) ?></td><td><?= e($r['time_out'] ?: '-') ?></td><td>₱<?= number_format($r['fee'],2) ?></td><td><span class="badge bg-<?= $r['status']=='parked'?'warning':'success' ?>"><?= e($r['status']) ?></span></td></tr><?php endforeach; ?></tbody></table></div></div>
<?php include 'footer.php'; ?>

