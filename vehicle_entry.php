<?php
require_once 'config.php'; requireLogin();
$message='';
if ($_SERVER['REQUEST_METHOD']==='POST') {
  $plate=strtoupper(trim($_POST['plate_number']??''));
  $type=trim($_POST['vehicle_type']??'');
  $driver=trim($_POST['driver_name']??'');
  $contact=trim($_POST['contact_number']??'');
  $slot_id=(int)($_POST['slot_id']??0);
  if ($plate && $type && $slot_id) {
    $check=$pdo->prepare("SELECT COUNT(*) FROM vehicles WHERE plate_number=? AND status='parked'"); $check->execute([$plate]);
    if ($check->fetchColumn()>0) { $message='This vehicle is already parked.'; }
    else {
      $pdo->beginTransaction();
      $stmt=$pdo->prepare("INSERT INTO vehicles(plate_number,vehicle_type,driver_name,contact_number,slot_id,time_in,status) VALUES(?,?,?,?,?,NOW(),'parked')");
      $stmt->execute([$plate,$type,$driver,$contact,$slot_id]);
      $up=$pdo->prepare("UPDATE parking_slots SET status='occupied' WHERE id=?"); $up->execute([$slot_id]);
      $pdo->commit();
      $message='Vehicle entry recorded successfully.';
    }
  } else { $message='Please complete all required fields.'; }
}
$slots=$pdo->query("SELECT * FROM parking_slots WHERE status='vacant' ORDER BY slot_number")->fetchAll(PDO::FETCH_ASSOC);
include 'header.php';
?>
<h2 class="mb-4">Vehicle Entry</h2><?php if($message): ?><div class="alert alert-info"><?= e($message) ?></div><?php endif; ?>
<div class="card shadow-sm"><div class="card-body"><form method="post" class="row g-3">
<div class="col-md-6"><label class="form-label">Plate Number *</label><input name="plate_number" class="form-control" required></div>
<div class="col-md-6"><label class="form-label">Vehicle Type *</label><select name="vehicle_type" class="form-select" required><option value="">Select type</option><option>Motorcycle</option><option>Car</option><option>Van</option><option>Truck</option></select></div>
<div class="col-md-6"><label class="form-label">Driver Name</label><input name="driver_name" class="form-control"></div>
<div class="col-md-6"><label class="form-label">Contact Number</label><input name="contact_number" class="form-control"></div>
<div class="col-md-12"><label class="form-label">Available Slot *</label><select name="slot_id" class="form-select" required><option value="">Select slot</option><?php foreach($slots as $s): ?><option value="<?= $s['id'] ?>"><?= e($s['slot_number']) ?></option><?php endforeach; ?></select></div>
<div class="col-md-12"><button class="btn btn-primary">Save Entry</button></div>
</form></div></div>
<?php include 'footer.php'; ?>

// This file is part of the Parking Lot Management System (PLMS) project.
