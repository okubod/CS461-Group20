<?php
// get values for connection
include 'connectvars.php'; 

// Escape user inputs for security
$refresh = mysqli_real_escape_string($conn, $_POST['refresh']);
$access = mysqli_real_escape_string($conn, $_POST['access']);

$myFile = fopen("access.txt", "w") or die("Unable to open file!");
$identifier = "oauth\n";
fwrite($myFile, $identifier);
$refreshNew = $refresh + "\n";
fwrite($myFile, $refreshNew);
$accessNew = $access + "\n";
fwrite($myFile, $accessNew);
fclose($myFile);

// redirect the user
header('Location: http://web.engr.oregonstate.edu/~coopchri/infrastructure_maintenance/access.txt');
?>
