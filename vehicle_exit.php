<?php
require_once 'config.php'; requireLogin();
$message=''; $rate=20;
if ($_SERVER['REQUEST_METHOD']==='POST' && isset($_POST['exit_vehicle'])) {
  $vehicle_id=(int)$_POST['vehicle_id'];
  $stmt=$pdo->prepare("SELECT * FROM vehicles WHERE id=? AND status='parked'"); $stmt->execute([$vehicle_id]); $v=$stmt->fetch(PDO::FETCH_ASSOC);
  if ($v) {
    $timeIn=strtotime($v['time_in']); $timeOut=time();
    $hours=max(1,ceil(($timeOut-$timeIn)/3600));
    $fee=$hours*$rate;
    $pdo->beginTransaction();
    $up=$pdo->prepare("UPDATE vehicles SET time_out=NOW(), fee=?, status='exited' WHERE id=?"); $up->execute([$fee,$vehicle_id]);
    $slot=$pdo->prepare("UPDATE parking_slots SET status='vacant' WHERE id=?"); $slot->execute([$v['slot_id']]);
    $pdo->commit();
    $message='Vehicle exited. Total fee: ₱'.number_format($fee,2).' for '.$hours.' hour(s).';
  }
}
$parked=$pdo->query("SELECT v.*, s.slot_number FROM vehicles v JOIN parking_slots s ON v.slot_id=s.id WHERE v.status='parked' ORDER BY v.time_in ASC")->fetchAll(PDO::FETCH_ASSOC);
include 'header.php';
?>
<h2 class="mb-4">Vehicle Exit</h2><?php if($message): ?><div class="alert alert-success"><?= e($message) ?></div><?php endif; ?>
<div class="alert alert-secondary">Rate: ₱<?= number_format($rate,2) ?> per hour. Minimum charge is 1 hour.</div>
<div class="card shadow-sm"><div class="table-responsive"><table class="table table-striped mb-0"><thead><tr><th>Plate</th><th>Type</th><th>Driver</th><th>Slot</th><th>Time In</th><th>Current Hours</th><th>Estimated Fee</th><th>Action</th></tr></thead><tbody>
<?php foreach($parked as $p): $hours=max(1,ceil((time()-strtotime($p['time_in']))/3600)); ?>
<tr><td><?= e($p['plate_number']) ?></td><td><?= e($p['vehicle_type']) ?></td><td><?= e($p['driver_name']) ?></td><td><?= e($p['slot_number']) ?></td><td><?= e($p['time_in']) ?></td><td><?= $hours ?></td><td>₱<?= number_format($hours*$rate,2) ?></td><td><form method="post" onsubmit="return confirm('Record vehicle exit?')"><input type="hidden" name="vehicle_id" value="<?= $p['id'] ?>"><button name="exit_vehicle" class="btn btn-success btn-sm">Exit</button></form></td></tr>
<?php endforeach; if(!$parked): ?><tr><td colspan="8" class="text-center text-muted py-4">No parked vehicles.</td></tr><?php endif; ?>
</tbody></table></div></div>
<?php include 'footer.php'; ?>

