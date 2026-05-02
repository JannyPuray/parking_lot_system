<?php
require_once 'config.php'; requireLogin();
$from=$_GET['from'] ?? date('Y-m-d');
$to=$_GET['to'] ?? date('Y-m-d');
$stmt=$pdo->prepare("SELECT v.*, s.slot_number FROM vehicles v JOIN parking_slots s ON v.slot_id=s.id WHERE DATE(v.time_in) BETWEEN ? AND ? ORDER BY v.id DESC");
$stmt->execute([$from,$to]);
$rows=$stmt->fetchAll(PDO::FETCH_ASSOC);
$total=0; foreach($rows as $r){ $total += (float)$r['fee']; }
include 'header.php';
?>
<div class="d-flex justify-content-between align-items-center mb-4"><h2>Reports</h2><button onclick="window.print()" class="btn btn-outline-primary">Print</button></div>
<div class="card shadow-sm mb-4 no-print"><div class="card-body"><form class="row g-2"><div class="col-md-5"><label class="form-label">From</label><input type="date" name="from" value="<?= e($from) ?>" class="form-control"></div><div class="col-md-5"><label class="form-label">To</label><input type="date" name="to" value="<?= e($to) ?>" class="form-control"></div><div class="col-md-2 d-flex align-items-end"><button class="btn btn-primary w-100">Filter</button></div></form></div></div>
<div class="card shadow-sm"><div class="card-header bg-white d-flex justify-content-between"><strong>Transaction Report</strong><strong>Total Income: ₱<?= number_format($total,2) ?></strong></div><div class="table-responsive"><table class="table table-bordered table-striped mb-0"><thead><tr><th>Plate</th><th>Type</th><th>Driver</th><th>Contact</th><th>Slot</th><th>Time In</th><th>Time Out</th><th>Fee</th><th>Status</th></tr></thead><tbody><?php foreach($rows as $r): ?><tr><td><?= e($r['plate_number']) ?></td><td><?= e($r['vehicle_type']) ?></td><td><?= e($r['driver_name']) ?></td><td><?= e($r['contact_number']) ?></td><td><?= e($r['slot_number']) ?></td><td><?= e($r['time_in']) ?></td><td><?= e($r['time_out'] ?: '-') ?></td><td>₱<?= number_format($r['fee'],2) ?></td><td><?= e($r['status']) ?></td></tr><?php endforeach; if(!$rows): ?><tr><td colspan="9" class="text-center text-muted py-4">No records found.</td></tr><?php endif; ?></tbody></table></div></div>
<?php include 'footer.php'; ?>

// generates a report of all vehicle transactions within a specified date range. It allows admins to filter records by date and displays details such as plate number, vehicle type, driver information, parking slot, time in/out, fee, and status. The total income for the selected period is also calculated and displayed at the top of the report.  