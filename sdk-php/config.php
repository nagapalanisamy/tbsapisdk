<?php

// config details

	$baseUrl     = '<YOUR BASE URL>';

	$url_w2create = "FormW2/Create";
	$url_w2validate = "FormW2/Validate";
	$url_w2get = "FormW2/Get";
	$url_w2transmit = "FormW2/Transmit";
	
	$public_key  = '<YOUR PUBLIC KEY>';
	$private_key = '<YOUR PRIVATE KEY>';
	$user_token  = '<YOUR USER TOKEN>';
	
	
	// Step 1: Add Current Date Time in UTC format eg: 'Tuesday, Nov 28, 2017 11:51:37 AM'
	$timestamp      = time()+date("Z");
    $utc_time_stamp = gmdate("l, M d, Y h:i:s A",$timestamp);
	

    // Step 2: Add Your System Ip Address
    $ip_address =  '<YOUR IP ADDRESS>';

	
    // Step 4: Generate Authentication Key
    $get_message = "GET" . "\n". $utc_time_stamp . "\n" . $ip_address . "\n" . $user_token. "\n" . strtolower('/'.$url_w2get);
    
	
	
    // Step 5: Generate Authentication with public key and private key
	$get_sha_auth_msg = hash_hmac("sha256", $get_message, $private_key, true);
	
	

    $get_hmc_auth_key = base64_encode($get_sha_auth_msg);
   


	$GET_headers = array(
								'http'=>array(
								'method'=>"GET",
								'header'=>array(
										'Timestamp:' . $utc_time_stamp,
										'IpAddress:' . $ip_address,
										'UserToken:' . $user_token,
										'Authentication:' . $public_key . ':' . $get_hmc_auth_key
										)
								)
							);

?>