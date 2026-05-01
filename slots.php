<?php
require_once 'config.php'; requireLogin();
$message='';
if ($_SERVER['REQUEST_METHOD']==='POST') {
  if (isset($_POST['add_slot'])) {
    $slot=trim($_POST['slot_number']??'');
    if ($slot) { try { $stmt=$pdo->prepare('INSERT INTO parking_slots(slot_number) VALUES(?)'); $stmt->execute([$slot]); $message='Slot added successfully.'; } catch(Exception $e){ $message='Slot already exists.'; } }
  }
  if (isset($_POST['delete_slot'])) {
    $id=(int)$_POST['slot_id'];
    $stmt=$pdo->prepare("DELETE FROM parking_slots WHERE id=? AND status='vacant'"); $stmt->execute([$id]);
    $message=$stmt->rowCount()?'Slot deleted.':'Only vacant slots can be deleted.';
  }
}
$slots=$pdo->query('SELECT * FROM parking_slots ORDER BY slot_number')->fetchAll(PDO::FETCH_ASSOC);
include 'header.php';
?>
<h2 class="mb-4">Parking Slots</h2><?php if($message): ?><div class="alert alert-info"><?= e($message) ?></div><?php endif; ?>
<div class="card shadow-sm mb-4"><div class="card-body"><form method="post" class="row g-2"><div class="col-md-10"><input name="slot_number" class="form-control" placeholder="Example: C-01" required></div><div class="col-md-2"><button name="add_slot" class="btn btn-primary w-100">Add Slot</button></div></form></div></div>
<div class="row g-3"><?php foreach($slots as $s): ?><div class="col-md-3"><div class="card slot-card <?= $s['status'] ?>"><div class="card-body d-flex justify-content-between align-items-center"><div><h4><?= e($s['slot_number']) ?></h4><span class="badge bg-<?= $s['status']=='vacant'?'success':'danger' ?>"><?= e($s['status']) ?></span></div><?php if($s['status']=='vacant'): ?><form method="post" onsubmit="return confirm('Delete this slot?')"><input type="hidden" name="slot_id" value="<?= $s['id'] ?>"><button name="delete_slot" class="btn btn-outline-danger btn-sm">Delete</button></form><?php endif; ?></div></div></div><?php endforeach; ?></div>
<?php include 'footer.php'; ?>
