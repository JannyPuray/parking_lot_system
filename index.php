<?php require_once 'config.php'; redirect(isLoggedIn() ? 'dashboard.php' : 'login.php'); ?>

// This file is the entry point of the application. It checks if the user is logged in and redirects to the appropriate page (dashboard or login).