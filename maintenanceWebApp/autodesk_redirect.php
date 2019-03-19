<!DOCTYPE html>
<!--Get autodesk authorization -->

<html>
   <head>
      <title>Authorize Redirect</title>
      <link rel="stylesheet" href="index.css">
<script type="text/javascript">

function sendRequest(){
   // get the id, uri, secret and code for the ajax request
   var client_id = "<?php echo $id ?>";
   var callback = "<?php echo $uri ?>";
   var client_secret = "<?php echo $secret ?>";
   var auth_code = "<?php echo $code ?>";

   // set up the XMLHTTP object
   var xmlhttp = new XMLHttpRequest();
   xmlhttp.onreadystatechange = function() {
      // check if the response is ready
      if (this.readyState == 4 && this.status == 200) {
         var myObj = JSON.parse(this.responseText);
         var refresh = myObj.refresh_token;
         var access = myObj.access_token;

         var xhttp = new XMLHttpRequest();
         xhttp.open("POST", "create_text.php", true);
         xhttp.send("refresh="+refresh+"&access="+access+"");   
      }
   };
   // send the request
   xmlhttp.open("POST", "https://developer.api.autodesk.com/authentication/v1/gettoken", true);
   xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
   xmlhttp.send("client_id="+client_id+"&client_secret="+client_secret+"&grant_type=authorization_code&code="+auth_code+"&redirect_uri="+callback+"");
}
</script>
</head>
   <body onload="sendRequest()">

<?php
include "header.php";
$msg = "";

// get values for app access
include 'connectvars.php'; 

$id = constant("ID");
$uri = constant("URI");
$secret = constant("SECRET");

$code = $_GET['code'];

?>
</body>
</html>
