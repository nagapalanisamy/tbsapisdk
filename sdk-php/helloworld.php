<?php

	// config details
	$baseUrl     = '<YOUR BASE URL>';
	$method_name = "Business/HelloWorld";
	
	$public_key  = '<YOUR PUBLIC KEY>';
	$private_key = '<YOUR PRIVATE KEY>';;
	$user_token  = '<YOUR USER TOKEN>';
	
	
	// Step 1: Add Current Date Time in UTC format eg: 'Tuesday, Nov 28, 2017 11:51:37 AM'
	$timestamp      = time()+date("Z");
    $utc_time_stamp = gmdate("l, M d, Y h:i:s A",$timestamp);
	

    // Step 2: Add Your System Ip Address
    $ip_address =  '<YOUR IP ADDRESS>';

	
    // Step 4: Generate Authentication Key
    $message = "GET" . "\n". $utc_time_stamp . "\n" . $ip_address . "\n" . $user_token. "\n" . strtolower('/'.$method_name);
	
	
    // Step 5: Generate Authentication with public key and private key
	$sha_auth_msg = hash_hmac("sha256", $message, $private_key, true);
    $hmc_auth_key = base64_encode($sha_auth_msg);


	// Create a stream
	$opts = array(
	
			'http'=>array(
			
			'method'=>"GET",
			
			'header'=>array(
			
					'Timestamp:' . $utc_time_stamp,
					
					'IpAddress:' . $ip_address,
					
					'UserToken:' . $user_token,
					
					'Authentication:' . $public_key . ':' . $hmc_auth_key
					
					)
			)
		);
	
	$context = stream_context_create($opts);
	
	// Open the file using the HTTP headers set above
	$file = file_get_contents($baseUrl . $method_name, false, $context);
	
	echo $file;

 ?>