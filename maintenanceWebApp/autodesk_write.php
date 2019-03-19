<!DOCTYPE html>
<!--Get autodesk authorization -->

<html>
   <head>
      <title>Authorize Write Access</title>
      <link rel="stylesheet" href="index.css">
   </head>
<body>

<?php
include "header.php";
$msg = "";

// get values for app access
include 'connectvars.php'; 

$id = "";
$uri = "";

echo "<br><h3>Instructions</h3>";

echo "<p>\"Click the link below to login to your Autodesk BIM 360 account for write access. This will then redirect you.<br> 
   Wait until you are presented with a page having a url ending in /write_access.txt and then save the page to your system.<br> 
   Then return to the Infrastructure Maintenance app where you will be presented with a file browser. <br> Find the file you just saved
   and load it. You will then be ready to use the Autodesk integration.\"</p>";

echo "<a href=\"https://developer.api.autodesk.com/authentication/v1/authorize?response_type=code&client_id=$id&redirect_uri=$uri&scope=data:write\">Get Write Access</a>";
?>

</body>
</html>
